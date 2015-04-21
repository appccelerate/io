// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InMemoryFileSystemFacts.cs" company="Appccelerate">
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
    using FluentAssertions;
    using Xunit;
    using Xunit.Extensions;

    public class InMemoryFileSystemFacts
    {
        private const string Root = "c:\\";
        private const string Folder = Root + "folder";
        private const string FileInFolder = Folder + "\\file.ext";
        private const string OtherFolder = Root + "otherfolder";
        private const string OtherFileInOtherFolder = OtherFolder + "\\otherfile.ext";

        private static readonly byte[] DummyFile = { 1, 2, 3 };

        public class DirectoryFacts
        {
            private readonly InMemoryFileSystem testee;

            public DirectoryFacts()
            {
                this.testee = new InMemoryFileSystem(new TimeDoesNotMatterInMemoryDateTimeProvider());
            }

            [Fact]
            public void IsInitiallyEmpty()
            {
                this.testee.GetFilesOfRecursive("\\")
                    .Should().BeEmpty();
            }

            [Fact]
            public void CreatesADirectory()
            {
                this.testee.CreateDirectory(Folder);

                this.testee.DirectoryExists(Folder)
                    .Should().BeTrue();
            }

            [Fact]
            public void SkipsCreatingADirectory_WhenDirectoryAlreadyExists()
            {
                this.testee.CreateDirectory(Folder);
                this.testee.CreateDirectory(Folder);

                this.testee.DirectoryExists(Folder)
                    .Should().BeTrue();
            }

            [Theory]
            [InlineData(Folder, Folder, true)]
            [InlineData(Folder, "c:\\other", false)]
            public void ReturnsWhetherADirectoryExists(string create, string query, bool expected)
            {
                this.testee.CreateDirectory(create);

                bool result = this.testee.DirectoryExists(query);

                result.Should().Be(expected);
            }

            [Fact]
            public void DeletesADirectory()
            {
                this.testee.CreateDirectory(Folder);

                this.testee.DeleteDirectory(Folder);

                this.testee.DirectoryExists(Folder)
                    .Should().BeFalse();
            }

            [Fact]
            public void ThrowsDirectoryNotFoundExceptionOnDeletingADirectory_WhenDirectoryDoesNotExist()
            {
                Action action = () => this.testee.DeleteDirectory(Folder);

                action.ShouldThrow<DirectoryNotFoundException>();
            }

            [Fact]
            public void ReturnsSubDiretoriesRecursively()
            {
                const string SubFolder = Folder + "\\SubFolder";
                this.testee.CreateDirectory(Folder);
                this.testee.CreateDirectory(SubFolder);
                this.testee.CreateDirectory(OtherFolder);

                IEnumerable<AbsoluteFolderPath> result = this.testee.GetSubdirectoriesOfRecursive(Root);

                result.Should().HaveCount(3)
                    .And.Contain(Folder)
                    .And.Contain(SubFolder)
                    .And.Contain(OtherFolder);
            }
        }

        public class FileFacts
        {
            private readonly InMemoryFileSystem testee;

            public FileFacts()
            {
                this.testee = new InMemoryFileSystem(new TimeDoesNotMatterInMemoryDateTimeProvider());
            }

            [Fact]
            public void AddsAFile()
            {
                this.testee.CreateDirectory(Folder);

                this.testee.AddFile(FileInFolder, DummyFile);

                this.testee.FileExists(FileInFolder)
                    .Should().BeTrue();
            }

            [Fact]
            public void AddsAFile_WhenFileAlreadyExists()
            {
                this.testee.CreateDirectory(Folder);

                this.testee.AddFile(FileInFolder, new byte[] { 9, 9 });
                this.testee.AddFile(FileInFolder, DummyFile);

                this.testee.GetFile(FileInFolder)
                    .Should().Equal(DummyFile);
            }

            [Fact]
            public void ThrowsDirectoryNotFoundExceptionOnAddingAFile_WhenDirectoryDoesNotYetExist()
            {
                Action action = () => this.testee.AddFile(FileInFolder, DummyFile);

                action.ShouldThrow<DirectoryNotFoundException>();
            }

            [Fact]
            public void DeletesAFile()
            {
                this.testee.CreateDirectory(Folder);

                this.testee.AddFile(FileInFolder, DummyFile);

                this.testee.DeleteFile(FileInFolder);

                this.testee.FileExists(FileInFolder)
                    .Should().BeFalse();
            }

            [Fact]
            public void KeepsTheDirectoryWhenDeletingAFile()
            {
                this.testee.CreateDirectory(Folder);

                this.testee.AddFile(FileInFolder, DummyFile);

                this.testee.DeleteFile(FileInFolder);

                this.testee.DirectoryExists(Folder)
                    .Should().BeTrue();
            }

            [Fact]
            public void SkipsDeletingAFile_WhenDirectoryExistsButFileDoesNotExist()
            {
                this.testee.CreateDirectory(Folder);

                this.testee.DeleteFile(FileInFolder);

                this.testee.FileExists(FileInFolder)
                    .Should().BeFalse();
            }

            [Fact]
            public void ThrowsDirectoryNotFoundExceptionOnDeletingAFile_WhenDirectoryDoesNotExist()
            {
                Action action = () => this.testee.DeleteFile(FileInFolder);

                action.ShouldThrow<DirectoryNotFoundException>();
            }

            [Fact]
            public void MovesAFile()
            {
                this.testee.CreateDirectory(Folder);
                this.testee.AddFile(FileInFolder, DummyFile);
                this.testee.CreateDirectory(OtherFolder);

                this.testee.Move(FileInFolder, OtherFileInOtherFolder);

                this.testee.FileExists(OtherFileInOtherFolder)
                    .Should().BeTrue();
            }

            [Fact]
            public void ThrowsFileNotFoundExceptionOnMovingAFile_WhenFileDoesNotExist()
            {
                this.testee.CreateDirectory(Folder);
                this.testee.CreateDirectory(OtherFolder);

                Action action = () => this.testee.Move(FileInFolder, OtherFileInOtherFolder);

                action.ShouldThrow<FileNotFoundException>();
            }

            [Fact]
            public void ThrowsDirectoryNotFoundExceptionOnMovingAFile_WhenSourceFolderDoesNotExist()
            {
                this.testee.CreateDirectory(OtherFolder);

                Action action = () => this.testee.Move(FileInFolder, OtherFileInOtherFolder);

                action.ShouldThrow<DirectoryNotFoundException>();
            }

            [Fact]
            public void MovesAFile_WhenDestinationFolderDoesNotExist()
            {
                this.testee.CreateDirectory(Folder);
                this.testee.AddFile(FileInFolder, DummyFile);

                this.testee.Move(FileInFolder, OtherFileInOtherFolder);

                this.testee.FileExists(OtherFileInOtherFolder)
                    .Should().BeTrue();

                this.testee.DirectoryExists(OtherFolder)
                    .Should().BeTrue();
            }

            [Fact]
            public void MovesAFile_WhenAlreadyAFileAtTargetExists()
            {
                this.testee.CreateDirectory(Folder);
                this.testee.AddFile(FileInFolder, DummyFile);
                this.testee.CreateDirectory(OtherFolder);
                this.testee.AddFile(OtherFileInOtherFolder, new byte[] { 9, 9 });

                this.testee.Move(FileInFolder, OtherFileInOtherFolder);

                this.testee.GetFile(OtherFileInOtherFolder)
                    .Should().Equal(DummyFile);
            }

            [Fact]
            public void GetsAFile()
            {
                this.testee.CreateDirectory(Folder);
                this.testee.AddFile(FileInFolder, DummyFile);

                IEnumerable<byte> result = this.testee.GetFile(FileInFolder);

                result.Should().Equal(DummyFile);
            }

            [Fact]
            public void ThrowsAnFileNotFoundExceptionOnGettingAFile_WhenFileDoesNotExist()
            {
                this.testee.CreateDirectory(Folder);

                Action action = () => this.testee.GetFile(FileInFolder);

                action.ShouldThrow<FileNotFoundException>();
            }

            [Fact]
            public void ThrowsAnFileNotFoundExceptionOnGettingAFile_WhenDirectoryDoesNotExist()
            {
                Action action = () => this.testee.GetFile(FileInFolder);

                action.ShouldThrow<DirectoryNotFoundException>();
            }
        }
    }
}