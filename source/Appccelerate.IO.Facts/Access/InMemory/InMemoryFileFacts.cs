//-------------------------------------------------------------------------------
// <copyright file="InMemoryFileFacts.cs" company="Appccelerate">
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
    using System.Text;
    using FakeItEasy;
    using FluentAssertions;
    using Xunit;

    public class InMemoryFileFacts
    {
        private const string SourceFolder = @"T:\Source";
        private const string DestinationFolder = @"T:\Destination";
        private const string SourceFile = SourceFolder + @"\someFile.txt";
        private const string DestinationFile = DestinationFolder + @"\clone.txt";

        private static readonly byte[] DummyContent = Encoding.UTF8.GetBytes("Beep Beep");

        private readonly InMemoryFileSystem fileSystem;
        private readonly IFileExtension extension;
        private readonly InMemoryFile testee;

        public InMemoryFileFacts()
        {
            this.fileSystem = new InMemoryFileSystem();
            this.extension = A.Fake<IFileExtension>();

            this.testee = new InMemoryFile(this.fileSystem, new[] { this.extension });
        }

        [Fact]
        public void CreatesNewFileAndCopiesContent()
        {
            this.fileSystem.CreateDirectory(SourceFolder);
            this.fileSystem.AddFile(SourceFile, DummyContent);
            this.fileSystem.CreateDirectory(DestinationFolder);

            this.testee.Copy(SourceFile, DestinationFile);

            this.fileSystem.FileExists(DestinationFile)
                .Should().BeTrue();
            this.fileSystem.GetFile(DestinationFile)
                .Should().Equal(DummyContent);
        }

        [Fact]
        public void CallsBeginAndEndCopyOnExtensionsOnCopyingAFile()
        {
            this.fileSystem.CreateDirectory(SourceFolder);
            this.fileSystem.AddFile(SourceFile, DummyContent);
            this.fileSystem.CreateDirectory(DestinationFolder);

            this.testee.Copy(SourceFile, DestinationFile);

            A.CallTo(() => this.extension.BeginCopy(SourceFile, DestinationFile)).MustHaveHappened();
            A.CallTo(() => this.extension.EndCopy(SourceFile, DestinationFile)).MustHaveHappened();
        }
    }
}