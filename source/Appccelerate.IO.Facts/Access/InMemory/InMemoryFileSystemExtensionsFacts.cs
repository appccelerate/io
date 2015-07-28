// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InMemoryFileSystemExtensionsFacts.cs" company="Appccelerate">
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
    using System.IO;
    using FluentAssertions;
    using Xunit;

    public class InMemoryFileSystemExtensionsFacts
    {
        private readonly IInMemoryFileSystem fileSystem;

        public InMemoryFileSystemExtensionsFacts()
        {
            this.fileSystem = new InMemoryFileSystem();
        }

        [Fact]
        public void EnsuresDirectoryForFilePathExists()
        {
            var directoryPath = @"C:\some\directory\path";
            this.fileSystem.CreateDirectory(directoryPath);

            this.fileSystem.EnsureParentDirectoryExists(Path.Combine(directoryPath, "test.txt"));
        }

        [Fact]
        public void ThrowsDirectoryNotFoundException_WhenDirectoryOfFilePathDoesNotExist()
        {
            var directoryPath = @"C:\some\directory\path";
            this.fileSystem.CreateDirectory(directoryPath);

            Action action = () => this.fileSystem.EnsureParentDirectoryExists(Path.Combine(directoryPath, "butDifferent", "test.txt"));

            action.ShouldThrow<DirectoryNotFoundException>();
        }
    }
}