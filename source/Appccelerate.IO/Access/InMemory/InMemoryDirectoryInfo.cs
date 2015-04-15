// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InMemoryDirectoryInfo.cs" company="Appccelerate">
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
    using System.Runtime.Serialization;

    public class InMemoryDirectoryInfo : IDirectoryInfo
    {
        private readonly IInMemoryFileSystem fileSystem;

        private readonly string pathToDirectory;

        public InMemoryDirectoryInfo(IInMemoryFileSystem fileSystem, string pathToDirectory)
        {
            this.fileSystem = fileSystem;
            this.pathToDirectory = pathToDirectory;
        }

        public IDirectoryInfo Root { get; private set; }

        public IDirectoryInfo Parent { get; private set; }

        public FileAttributes Attributes { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime CreationTimeUtc { get; set; }

        public bool Exists
        {
            get
            {
                return this.fileSystem.DirectoryExists(this.pathToDirectory);
            }
        }

        public string FullName
        {
            get
            {
                return this.pathToDirectory;
            }
        }

        public string Name { get; private set; }

        public string Extension { get; private set; }

        public DateTime LastAccessTime { get; set; }

        public DateTime LastAccessTimeUtc { get; set; }

        public DateTime LastWriteTime { get; set; }

        public DateTime LastWriteTimeUtc { get; set; }

        public void Refresh()
        {
        }

        public void Delete()
        {
            this.fileSystem.DeleteDirectory(this.pathToDirectory);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
        }

        public void Create()
        {
            this.fileSystem.CreateDirectory(this.pathToDirectory);
        }
    }
}