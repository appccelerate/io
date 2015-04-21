// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InMemoryDirectory.cs" company="Appccelerate">
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
    using System.IO;
    using System.Linq;
    using System.Security.AccessControl;

    using Appccelerate.IO.Access.Internals;

    public class InMemoryDirectory : IDirectory, IExtensionProvider<IDirectoryExtension>
    {
        private readonly IInMemoryFileSystem fileSystem;

        private readonly IEnumerable<IDirectoryExtension> extensions;

        public InMemoryDirectory(IInMemoryFileSystem fileSystem, IEnumerable<IDirectoryExtension> extensions)
        {
            this.fileSystem = fileSystem;
            this.extensions = extensions;
        }

        public IEnumerable<IDirectoryExtension> Extensions
        {
            get
            {
                return this.extensions;
            }
        }

        public bool Exists(string path)
        {
            return this.EncapsulateWithExtension(
                () => this.fileSystem.DirectoryExists(path),
                e => e.BeginExists(path),
                (e, r) => e.EndExists(r, path),
                (IDirectoryExtension e, ref Exception exception) => e.FailExists(ref exception, path));
        }

        public IEnumerable<string> GetFiles(string path)
        {
            return this.EncapsulateWithExtension(
                () => this.fileSystem.GetFilesOf(path).Select(x => x.Value).ToArray(),
                e => e.BeginGetFiles(path),
                (e, r) => e.EndGetFiles(r, path),
                (IDirectoryExtension e, ref Exception exception) => e.FailGetFiles(ref exception, path));
        }

        public IEnumerable<string> GetFiles(string path, string searchPattern)
        {
            return this.EncapsulateWithExtension(
                () =>
                {
                    IEnumerable<string> allFileCandidates = this.GetFiles(path);

                    return allFileCandidates.Where(x => System.IO.Path.GetFileName(x).IsLike(searchPattern)).ToArray();
                },
                e => e.BeginGetFiles(path, searchPattern),
                (e, r) => e.EndGetFiles(r, path, searchPattern),
                (IDirectoryExtension e, ref Exception exception) => e.FailGetFiles(ref exception, path, searchPattern));
        }

        public IEnumerable<string> GetFiles(string path, string searchPattern, SearchOption searchOption)
        {
            return this.EncapsulateWithExtension(
                () =>
                    {
                        IEnumerable<string> allFileCandidates = searchOption == SearchOption.TopDirectoryOnly ? 
                            this.GetFiles(path) : 
                            this.fileSystem.GetFilesOfRecursive(path).Select(x => x.Value);

                        return allFileCandidates.Where(x => System.IO.Path.GetFileName(x).IsLike(searchPattern)).ToArray();
                    },
                e => e.BeginGetFiles(path, searchPattern),
                (e, r) => e.EndGetFiles(r, path, searchPattern),
                (IDirectoryExtension e, ref Exception exception) => e.FailGetFiles(ref exception, path, searchPattern));
        }

        public IEnumerable<string> GetDirectories(string path)
        {
            return this.EncapsulateWithExtension(
                () => 
                    this.fileSystem.GetSubdirectoriesOf(path)
                        .Select(x => x.Value),
                e => e.BeginGetDirectories(path),
                (e, r) => e.EndGetDirectories(r.ToArray(), path),
                (IDirectoryExtension e, ref Exception exception) => e.FailGetDirectories(ref exception, path));
        }

        public IEnumerable<string> GetDirectories(string path, string searchPattern)
        {
            return this.EncapsulateWithExtension(
                () => 
                    this.fileSystem.GetSubdirectoriesOf(path)
                        .Select(x => x.Value)
                        .Where(x => System.IO.Path.GetFileName(x).IsLike(searchPattern)),
                e => e.BeginGetDirectories(path),
                (e, r) => e.EndGetDirectories(r.ToArray(), path),
                (IDirectoryExtension e, ref Exception exception) => e.FailGetDirectories(ref exception, path));
        }

        public IDirectoryInfo CreateDirectory(string path)
        {
            return this.EncapsulateWithExtension(
                () =>
                    {
                        this.fileSystem.CreateDirectory(path);
                        return new InMemoryDirectoryInfo(this.fileSystem, path);
                    },
                e => e.BeginCreateDirectory(path),
                (e, r) => e.EndCreateDirectory(r, path),
                (IDirectoryExtension e, ref Exception exception) => e.FailCreateDirectory(ref exception, path));
        }

        public void Delete(string path)
        {
            this.EncapsulateWithExtension(
                () =>
                {
                    this.EnsureDirectoryIsEmpty(path);
                    
                    this.fileSystem.DeleteDirectory(path);
                },
                e => e.BeginDelete(path),
                e => e.EndDelete(path),
                (IDirectoryExtension e, ref Exception exception) => e.FailDelete(ref exception, path));
        }

        public void Delete(string path, bool recursive)
        {
            this.EncapsulateWithExtension(
                () =>
                    {
                       if (!recursive)
                        {
                            this.EnsureDirectoryIsEmpty(path);
                        }

                        this.fileSystem.DeleteDirectory(path);
                    },
                e => e.BeginDelete(path, recursive),
                e => e.EndDelete(path, recursive),
                (IDirectoryExtension e, ref Exception exception) => e.FailDelete(ref exception, path, recursive));
        }
        
        public IDirectoryInfo CreateDirectory(string path, DirectorySecurity directorySecurity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetDirectories(string path, string searchPattern, SearchOption searchOption)
        {
            throw new NotImplementedException();
        }

        public DirectorySecurity GetAccessControl(string path)
        {
            throw new NotImplementedException();
        }

        public DirectorySecurity GetAccessControl(string path, AccessControlSections includeSections)
        {
            throw new NotImplementedException();
        }

        public DateTime GetCreationTime(string path)
        {
            throw new NotImplementedException();
        }

        public DateTime GetCreationTimeUtc(string path)
        {
            throw new NotImplementedException();
        }

        public string GetCurrentDirectory()
        {
            throw new NotImplementedException();
        }

        public string GetDirectoryRoot(string path)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetFileSystemEntries(string path)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetFileSystemEntries(string path, string searchPattern)
        {
            throw new NotImplementedException();
        }

        public DateTime GetLastAccessTime(string path)
        {
            throw new NotImplementedException();
        }

        public DateTime GetLastAccessTimeUtc(string path)
        {
            throw new NotImplementedException();
        }

        public DateTime GetLastWriteTime(string path)
        {
            throw new NotImplementedException();
        }

        public DateTime GetLastWriteTimeUtc(string path)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetLogicalDrives()
        {
            throw new NotImplementedException();
        }

        public IDirectoryInfo GetParent(string path)
        {
            throw new NotImplementedException();
        }

        public void Move(string sourceDirName, string destDirName)
        {
            throw new NotImplementedException();
        }

        public void SetAccessControl(string path, DirectorySecurity directorySecurity)
        {
            throw new NotImplementedException();
        }

        public void SetCreationTime(string path, DateTime creationTime)
        {
            throw new NotImplementedException();
        }

        public void SetCreationTimeUtc(string path, DateTime creationTimeUtc)
        {
            throw new NotImplementedException();
        }

        public void SetCurrentDirectory(string path)
        {
            throw new NotImplementedException();
        }

        public void SetLastAccessTime(string path, DateTime lastAccessTime)
        {
            throw new NotImplementedException();
        }

        public void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc)
        {
            throw new NotImplementedException();
        }

        public void SetLastWriteTime(string path, DateTime lastWriteTime)
        {
            throw new NotImplementedException();
        }

        public void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc)
        {
            throw new NotImplementedException();
        }

        private void EnsureDirectoryIsEmpty(string path)
        {
            if (this.fileSystem.GetSubdirectoriesOf(path).Any() || this.fileSystem.GetFilesOf(path).Any())
            {
                throw new IOException("The directory `" + path + "` is not empty and cannot be deleted.");
            }
        }
    }
}