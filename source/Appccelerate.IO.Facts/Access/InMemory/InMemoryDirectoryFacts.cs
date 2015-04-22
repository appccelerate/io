//-------------------------------------------------------------------------------
// <copyright file="InMemoryDirectoryFacts.cs" company="Appccelerate">
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
//-------------------------------------------------------------------------------
namespace Appccelerate.IO.Access.InMemory
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using FluentAssertions;
    using Xunit;

    public class InMemoryDirectoryFacts
    {
        private const string Root = "C:\\";
        private const string FolderName = "folder";
        private const string Folder = Root + FolderName;
        private const string FileInFolder = Folder + "\\file" + FileExtension;
        private const string OtherFileInFolder = Folder + "\\otherFile.txt";
        private const string SubFolder = Folder + "\\SubFolder";
        private const string FileInSubFolder = SubFolder + "\\file" + FileExtension;
        private const string OtherFolder = Root + "otherFolder";
        private const string FileExtension = ".ext";

        private static readonly byte[] DummyFile = { byte.MaxValue, byte.MinValue };
        private readonly InMemoryFileSystem fileSystem;
        private readonly InMemoryDirectory testee;

        public InMemoryDirectoryFacts()
        {
            this.fileSystem = new InMemoryFileSystem(new TimeDoesNotMatterDateTimeProvider());

            this.testee = new InMemoryDirectory(this.fileSystem, Enumerable.Empty<IDirectoryExtension>());
        }

        [Fact]
        public void ThrowsIOExceptionOnDeletingADirectory_WhenDirectoryContainsAFile()
        {
            this.fileSystem.CreateDirectory(Folder);
            this.fileSystem.AddFile(FileInFolder, DummyFile);

            Action action = () => this.testee.Delete(Folder);

            action.ShouldThrow<IOException>();
        }

        [Fact]
        public void ThrowsIOExceptionOnDeletingADirectory_WhenDirectoryContainsASubDirectory()
        {
            this.fileSystem.CreateDirectory(Folder);
            this.fileSystem.CreateDirectory(SubFolder);

            Action action = () => this.testee.Delete(Folder);

            action.ShouldThrow<IOException>();
        }

        [Fact]
        public void DeletesADirectoryRecursively()
        {
            this.AddFileHierarchyToFileSystem();

            this.testee.Delete(Folder, true);

            this.fileSystem.DirectoryExists(Folder).Should().BeFalse();
        }

        [Fact]
        public void GetsFilesMatchingPatternRecursively()
        {
            this.AddFileHierarchyToFileSystem();

            IEnumerable<string> result = this.testee.GetFiles(Folder, "*" + FileExtension, SearchOption.AllDirectories);

            result.Should().HaveCount(2)
                .And.Contain(FileInFolder)
                .And.Contain(FileInSubFolder);
        }

        [Fact]
        public void GetsFilesMatchingPatternWithWildcardAtBeginningInTopDirectoryOnly()
        {
            this.AddFileHierarchyToFileSystem();

            IEnumerable<string> result = this.testee.GetFiles(Folder, "*" + FileExtension, SearchOption.TopDirectoryOnly);

            result.Should().HaveCount(1)
                .And.Contain(FileInFolder);
        }

        [Fact]
        public void GetsFilesMatchingPatternWithWildcardAtEndInTopDirectoryOnly()
        {
            this.AddFileHierarchyToFileSystem();

            IEnumerable<string> result = this.testee.GetFiles(Folder, "*.*", SearchOption.TopDirectoryOnly);

            result.Should().HaveCount(2)
                .And.Contain(FileInFolder)
                .And.Contain(OtherFileInFolder);
        }

        [Fact]
        public void GetsMatchingFilesInTopDirectoryOnly_WhenNoSearchOptionProvided()
        {
            this.AddFileHierarchyToFileSystem();

            IEnumerable<string> result = this.testee.GetFiles(Folder, "*");

            result.Should().HaveCount(2)
                .And.Contain(FileInFolder)
                .And.Contain(OtherFileInFolder);
        }

        [Fact]
        public void GetsDirectories()
        {
            this.AddFileHierarchyToFileSystem();

            IEnumerable<string> result = this.testee.GetDirectories(Root);

            result.Should().HaveCount(2)
                .And.Contain(Folder)
                .And.Contain(OtherFolder);
        }

        [Fact]
        public void GetsDirectoriesMatchingPatternWithWildcardAtBeginningInTopDirectoryOnly_WhenNoSearchOptionProvided()
        {
            this.AddFileHierarchyToFileSystem();

            IEnumerable<string> result = this.testee.GetDirectories(Root, "*folder");

            result.Should().HaveCount(2)
                .And.Contain(Folder)
                .And.Contain(OtherFolder);
        }

        [Fact]
        public void GetsDirectoriesMatchingPatternWithWildcardAtEndInTopDirectoryOnly_WhenNoSearchOptionProvided()
        {
            this.AddFileHierarchyToFileSystem();

            IEnumerable<string> result = this.testee.GetDirectories(Root, "other*");

            result.Should().HaveCount(1)
                .And.Contain(OtherFolder);
        }

        private void AddFileHierarchyToFileSystem()
        {
            this.fileSystem.CreateDirectory(Folder);
            this.fileSystem.AddFile(FileInFolder, DummyFile);
            this.fileSystem.AddFile(OtherFileInFolder, DummyFile);
            
            this.fileSystem.CreateDirectory(SubFolder);
            this.fileSystem.AddFile(FileInSubFolder, DummyFile);

            this.fileSystem.CreateDirectory(OtherFolder);
        }
    }
}