//-------------------------------------------------------------------------------
// <copyright file="InMemoryDirectoryInfoFacts.cs" company="Appccelerate">
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
    using Xunit.Extensions;

    public class InMemoryDirectoryInfoFacts
    {
        private const string Drive = "C:";
        private const string FolderName = "folder";
        private const string Folder = Drive + "\\" + FolderName;
        private const string FolderInFolder = Folder + "\\subfolder";

        private readonly InMemoryFileSystem fileSystem;

        public InMemoryDirectoryInfoFacts()
        {
            this.fileSystem = new InMemoryFileSystem(new TimeDoesNotMatterDateTimeProvider());
        }

        [Theory]
        [InlineData(FolderInFolder, Folder)]
        [InlineData(FolderInFolder + "\\", Folder)]
        public void ReturnsParentFolder(string folder, string parent)
        {
            var testee = new InMemoryDirectoryInfo(this.fileSystem, folder);

            IDirectoryInfo result = testee.Parent;

            result.FullName.Should().Be(parent);
        }

        [Theory]
        [InlineData(Drive)]
        [InlineData(Drive + "\\")]
        public void ReturnsNull_WhenDirectoryIsRoot(string root)
        {
            var testee = new InMemoryDirectoryInfo(this.fileSystem, root);

            IDirectoryInfo result = testee.Parent;

            result.Should().BeNull();
        }

        [Fact]
        public void ReturnsRoot()
        {
            var testee = new InMemoryDirectoryInfo(this.fileSystem, FolderInFolder);

            IDirectoryInfo result = testee.Root;

            result.FullName.Should().Be(Drive);
        }
    }
}