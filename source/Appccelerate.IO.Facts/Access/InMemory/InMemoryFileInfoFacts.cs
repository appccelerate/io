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
    using FluentAssertions;
    using Xunit;

    public class InMemoryFileInfoFacts
    {
        private const string Root = "C:\\";
        private const string FolderName = "folder";
        private const string Folder = Root + FolderName;
        private const string FileInFolder = Folder + "\\file.ext";

        private readonly InMemoryFileInfo testee;

        public InMemoryFileInfoFacts()
        {
            this.testee = new InMemoryFileInfo(new InMemoryFileSystem(), FileInFolder);
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
    }
}