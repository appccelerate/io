// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InMemoryFileSystem.cs" company="Appccelerate">
//   Copyright (c) 2008-2015
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Appccelerate.IO.Access.InMemory
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class InMemoryFileSystem : IInMemoryFileSystem
    {
        private static readonly object Lock = new object();

        private readonly Tree<DirectoryEntry> fileSystem;

        public InMemoryFileSystem()
        {
            this.fileSystem = new Tree<DirectoryEntry>(new DirectoryEntry("root"));
        }

        public static string FormatFileNotFoundExceptionMessage(string absoluteFilePath)
        {
            return string.Concat("Could not find file `", absoluteFilePath, "`.");
        }

        public static string FormatDirectoryNotFoundExceptionMessage(string absoluteFilePath)
        {
            return string.Concat("Could not find a part of the path `", absoluteFilePath, "`.");
        }

        public void AddFile(AbsoluteFilePath absoluteFilePath, IEnumerable<byte> fileContent)
        {
            var parentDirectory = this.GetDirectoryBy(Directory.GetParent(absoluteFilePath).FullName);
            EnsureDirectoryExists(parentDirectory, absoluteFilePath);

            if (!this.FileExists(absoluteFilePath))
            {
                AddFileEntry(absoluteFilePath, fileContent, parentDirectory);
            }
            else
            {
                FileEntry current = GetFileByNameIgnoringCase(parentDirectory, Path.GetFileName(absoluteFilePath));
                current.Content = fileContent;
            }
        }

        public IEnumerable<byte> GetFile(AbsoluteFilePath absoluteFilePath)
        {
            var parentDirectory = this.GetDirectoryBy(Directory.GetParent(absoluteFilePath).FullName);
            EnsureDirectoryExists(parentDirectory, absoluteFilePath);

            this.EnsureFileExists(absoluteFilePath);

            FileEntry fileEntry = GetFileByNameIgnoringCase(parentDirectory, Path.GetFileName(absoluteFilePath));

            return fileEntry.Content;
        }

        public void DeleteFile(AbsoluteFilePath absoluteFilePath)
        {
            var parentDirectory = this.GetDirectoryBy(Directory.GetParent(absoluteFilePath).FullName);
            EnsureDirectoryExists(parentDirectory, absoluteFilePath);

            if (!this.FileExists(absoluteFilePath))
            {
                return;
            }

            var fileEntry = GetFileByNameIgnoringCase(parentDirectory, Path.GetFileName(absoluteFilePath));

            RemoveFileEntry(parentDirectory, fileEntry);
        }

        public void DeleteDirectory(AbsoluteFolderPath absoluteFolderPath)
        {
            var directoryEntryToDelete = this.GetDirectoryBy(absoluteFolderPath);
            EnsureDirectoryExists(directoryEntryToDelete, absoluteFolderPath);

            var parentDirectory = this.GetDirectoryBy(Directory.GetParent(absoluteFolderPath).FullName);

            RemoveDirectoryEntry(parentDirectory, directoryEntryToDelete);
        }

        public IEnumerable<AbsoluteFolderPath> GetSubdirectoriesOf(AbsoluteFolderPath absoluteFolderPath)
        {
            var currentItem = this.GetDirectoryBy(absoluteFolderPath);
            EnsureDirectoryExists(currentItem, absoluteFolderPath);

            return currentItem.Children.Select(i => new AbsoluteFolderPath(Path.Combine(absoluteFolderPath, i.Item.Name))).ToList();
        }

        public IEnumerable<AbsoluteFilePath> GetFilesOf(AbsoluteFolderPath absoluteFolderPath)
        {
            var currentItem = this.GetDirectoryBy(absoluteFolderPath);
            EnsureDirectoryExists(currentItem, absoluteFolderPath);

            return currentItem.Item.Files.Select(i => new AbsoluteFilePath(Path.Combine(absoluteFolderPath, i.Name))).ToList();
        }

        public IEnumerable<AbsoluteFilePath> GetFilesOfRecursive(AbsoluteFolderPath absoluteFolderPath)
        {
            var currentItem = this.GetDirectoryBy(absoluteFolderPath);
            EnsureDirectoryExists(currentItem, absoluteFolderPath);

            return this.GetFilesOfRecursiveInternal(absoluteFolderPath, currentItem);
        }

        public bool FileExists(AbsoluteFilePath absoluteFilePath)
        {
            string parentDirectoryPath = Directory.GetParent(absoluteFilePath).FullName;
            if (!this.DirectoryExists(parentDirectoryPath))
            {
                return false;
            }

            var parentDirectory = this.GetDirectoryBy(parentDirectoryPath);

            return GetFileByNameIgnoringCase(parentDirectory, Path.GetFileName(absoluteFilePath)) != null;
        }

        public bool DirectoryExists(AbsoluteFolderPath absoluteFolderPath)
        {
            return this.GetDirectoryBy(absoluteFolderPath) != null;
        }

        public void CreateDirectory(AbsoluteFolderPath absoluteFolderPath)
        {
            this.CreateDirectoryWith(absoluteFolderPath);
        }

        public string DumpFileSystem()
        {
            return this.Dump(this.fileSystem, string.Empty);
        }

        public void Move(AbsoluteFilePath absoluteSourceFilePath, AbsoluteFilePath absoluteDestinationFilePath)
        {
            IEnumerable<byte> contents = this.GetFile(absoluteSourceFilePath);

            AbsoluteFolderPath absoluteDestinationFolderPath = Path.GetDirectoryName(absoluteDestinationFilePath);
            this.CreateDirectory(absoluteDestinationFolderPath);

            this.AddFile(absoluteDestinationFilePath, contents);

            this.DeleteFile(absoluteSourceFilePath);
        }

        private IEnumerable<AbsoluteFilePath> GetFilesOfRecursiveInternal(AbsoluteFolderPath absoluteFolderPath, Tree<DirectoryEntry> directoryEntry)
        {
            List<AbsoluteFilePath> files = directoryEntry.Item.Files.Select(i => new AbsoluteFilePath(Path.Combine(absoluteFolderPath, i.Name))).ToList();

            foreach (Tree<DirectoryEntry> child in directoryEntry.Children)
            {
                IEnumerable<AbsoluteFilePath> filesInChild = this.GetFilesOfRecursiveInternal(absoluteFolderPath + "\\" + child.Item.Name, child);

                files.AddRange(filesInChild);
            }

            return files;
        }

        // ReSharper disable once UnusedParameter.Local
        private static void EnsureDirectoryExists(Tree<DirectoryEntry> parentDirectory, AbsolutePath inputPath)
        {
            if (parentDirectory == null)
            {
                throw new DirectoryNotFoundException(FormatDirectoryNotFoundExceptionMessage(inputPath));
            }
        }

        private static IEnumerable<string> SplitIntoSubPaths(string absoluteFilePath)
        {
            return absoluteFilePath.Split(new[] { Path.DirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);
        }

        private static Tree<DirectoryEntry> GetOrCreateSubdirectory(Tree<DirectoryEntry> currentItem, string subPath)
        {
            var firstSubPathItem = GetSubdirectoryIgnoringCase(currentItem, subPath);
            if (firstSubPathItem == null)
            {
                var newPathItem = AddDirectoryEntry(currentItem, subPath);

                return newPathItem;
            }

            return firstSubPathItem;
        }

        private static Tree<DirectoryEntry> GetSubdirectoryIgnoringCase(Tree<DirectoryEntry> currentItem, string subPath)
        {
            return currentItem.Children.FirstOrDefault(i => i.Item.Name.EqualsIgnoringCaseCultureInvariant(subPath));
        }

        private static FileEntry GetFileByNameIgnoringCase(Tree<DirectoryEntry> directory, string fileName)
        {
            return directory.Item.Files.FirstOrDefault(i => i.Name.EqualsIgnoringCaseCultureInvariant(fileName));
        }

        private static string DumpDirectory(DirectoryEntry directoryEntry, string currentPath)
        {
            var dumpBuilder = new StringBuilder();

            dumpBuilder.AppendLine(currentPath + Path.DirectorySeparatorChar + directoryEntry.Name + Path.DirectorySeparatorChar);

            foreach (var fileEntry in directoryEntry.Files)
            {
                dumpBuilder.AppendLine(string.Concat(currentPath, Path.DirectorySeparatorChar, directoryEntry.Name, Path.DirectorySeparatorChar, fileEntry.Name));
            }

            return dumpBuilder.ToString();
        }

        private void EnsureFileExists(AbsoluteFilePath absoluteFilePath)
        {
            if (!this.FileExists(absoluteFilePath))
            {
                throw new FileNotFoundException(FormatFileNotFoundExceptionMessage(absoluteFilePath));
            }
        }

        private void CreateDirectoryWith(string absoluteFilePath)
        {
            var subPaths = SplitIntoSubPaths(absoluteFilePath);

            Tree<DirectoryEntry> currentItem = this.fileSystem;
            foreach (var subPath in subPaths)
            {
                currentItem = GetOrCreateSubdirectory(currentItem, subPath);
            }
        }

        private Tree<DirectoryEntry> GetDirectoryBy(string absoluteFilePath)
        {
            var subPaths = SplitIntoSubPaths(absoluteFilePath);

            var currentItem = this.fileSystem;
            foreach (var subPath in subPaths)
            {
                currentItem = GetSubdirectoryIgnoringCase(currentItem, subPath);

                if (currentItem == null)
                {
                    return null;
                }
            }

            return currentItem;
        }

        private string Dump(Tree<DirectoryEntry> currentNode, string currentPath)
        {
            var result = DumpDirectory(currentNode.Item, currentPath);

            foreach (var child in currentNode.Children)
            {
                result += this.Dump(child, currentPath + Path.DirectorySeparatorChar + currentNode.Item.Name);
            }

            return result;
        }

        private static void AddFileEntry(AbsoluteFilePath absoluteFilePath, IEnumerable<byte> fileContent, Tree<DirectoryEntry> parentDirectory)
        {
            lock (Lock)
            {
                parentDirectory.Item.Files.Add(new FileEntry(Path.GetFileName(absoluteFilePath), fileContent));
            }
        }

        private static void RemoveFileEntry(Tree<DirectoryEntry> parentDirectory, FileEntry fileEntry)
        {
            lock (Lock)
            {
                parentDirectory.Item.Files.Remove(fileEntry);
            }
        }

        private static Tree<DirectoryEntry> AddDirectoryEntry(Tree<DirectoryEntry> currentItem, string subPath)
        {
            lock (Lock)
            {
                var newPathItem = new Tree<DirectoryEntry>(new DirectoryEntry(subPath));
                currentItem.Children.Add(newPathItem);
                return newPathItem;
            }
        }

        private static void RemoveDirectoryEntry(Tree<DirectoryEntry> parentDirectory, Tree<DirectoryEntry> directoryEntryToDelete)
        {
            lock (Lock)
            {
                parentDirectory.Children.Remove(directoryEntryToDelete);
            }
        }

        [DebuggerDisplay("Directory {Name}")]
        private class DirectoryEntry
        {
            public DirectoryEntry(string name)
            {
                this.Name = name;
                this.Files = new List<FileEntry>();
            }

            public string Name { get; private set; }

            public IList<FileEntry> Files { get; private set; }
        }

        [DebuggerDisplay("File {Name}")]
        private class FileEntry
        {
            public FileEntry(string name, IEnumerable<byte> content)
            {
                this.Content = content;
                this.Name = name;
            }

            public string Name { get; private set; }

            public IEnumerable<byte> Content { get; set; }
        }

        private class Tree<T>
        {
            public Tree(T root)
            {
                this.Item = root;
                this.Children = new List<Tree<T>>();
            }

            public T Item { get; private set; }

            public IList<Tree<T>> Children { get; private set; }
        }
    }
}