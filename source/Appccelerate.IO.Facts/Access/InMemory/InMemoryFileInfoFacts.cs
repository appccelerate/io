//-------------------------------------------------------------------------------
// <copyright file="InMemoryFileInfoFacts.cs" company="Appccelerate">
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
    using System.IO;
    using System.Text;
    using FluentAssertions;
    using Xunit;

    public class InMemoryFileInfoFacts
    {
        private const string Root = "C:\\";
        private const string FolderName = "folder";
        private const string Folder = Root + FolderName;
        private const string FileInFolder = Folder + "\\file.ext";
        private static readonly byte[] FileContent = { 1, 2, 3 };

        private readonly InMemoryFileSystem fileSystem = new InMemoryFileSystem();
        private readonly InMemoryFileInfo testee;

        public InMemoryFileInfoFacts()
        {
            this.fileSystem.CreateDirectory(FileInFolder);
            this.fileSystem.AddFile(FileInFolder, FileContent);
            this.testee = new InMemoryFileInfo(this.fileSystem, FileInFolder);
        }

        [Fact]
        public void ReturnsDirectory()
        {
            IDirectoryInfo result = this.testee.Directory;

            result.FullName.Should().Be(Folder);
        }

        [Fact]
        public void ReturnsDirectoryName()
        {
            string result = this.testee.DirectoryName;

            result.Should().Be(FolderName);
        }

        [Fact]
        public void CopiesFile()
        {
            var destinationPath = Path.Combine(Folder, "someSubFolder", "dest.ext");
            this.fileSystem.CreateDirectory(Path.Combine(Folder, "someSubFolder"));

            IFileInfo result = this.testee.CopyTo(destinationPath);

            result.Exists.Should().BeTrue();
            result.Name.Should().Be("dest.ext");
            result.FullName.Should().Be(destinationPath);
        }

        [Fact]
        public void DoesNotOverwriteAFileOnCopyToByDefault()
        {
            var originalContent = Encoding.UTF8.GetBytes("Hello World");
            var destinationPath = Path.Combine(Folder, "dest.ext");
            this.fileSystem.AddFile(destinationPath, originalContent);

            Action action = () => this.testee.CopyTo(destinationPath);

            action.Should().Throw<IOException>();
            this.fileSystem.GetFile(destinationPath).Should().BeEquivalentTo(originalContent);
        }

        [Fact]
        public void OverwritesAFileOnCopyTo_WhenOverwriteFlagSet()
        {
            var destinationPath = Path.Combine(Folder, "dest.ext");
            this.fileSystem.AddFile(destinationPath, Encoding.UTF8.GetBytes("Hello World"));

            this.testee.CopyTo(destinationPath, true);

            this.fileSystem.GetFile(destinationPath).Should().BeEquivalentTo(FileContent);
        }

        [Fact]
        public void DoesNotOverwriteAFileOnCopyTo_WhenOverwriteFlagNotSet()
        {
            var originalContent = Encoding.UTF8.GetBytes("Hello World");
            var destinationPath = Path.Combine(Folder, "dest.ext");
            this.fileSystem.AddFile(destinationPath, originalContent);

            Action action = () => this.testee.CopyTo(destinationPath, false);

            action.Should().Throw<IOException>();
            this.fileSystem.GetFile(destinationPath).Should().BeEquivalentTo(originalContent);
        }

        [Fact]
        public void ThrowsDirectoryNotFoundExceptionOnCopyTo_WhenDestinationDirectoryDoesNotExist()
        {
            var destinationPath = Path.Combine(Folder, "someSubFolder", "dest.ext");

            Action action = () => this.testee.CopyTo(destinationPath);

            action.Should().Throw<DirectoryNotFoundException>();
            this.fileSystem.FileExists(destinationPath)
                .Should().BeFalse();
        }
    }
}