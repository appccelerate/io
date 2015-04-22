// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InMemoryFileInfo.cs" company="Appccelerate">
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
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Security.AccessControl;

    public class InMemoryFileInfo : IFileInfo
    {
        private readonly IInMemoryFileSystem fileSystem;

        private readonly string pathToFile;

        public InMemoryFileInfo(IInMemoryFileSystem fileSystem, string pathToFile)
        {
            this.fileSystem = fileSystem;
            this.pathToFile = pathToFile;
        }

        public FileAttributes Attributes { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime CreationTimeUtc { get; set; }

        public bool Exists
        {
            get
            {
                return this.fileSystem.FileExists(this.pathToFile);
            }
        }

        public string FullName
        {
            get
            {
                return this.pathToFile;  
            } 
        }

        public string Name
        {
            get
            {
                return Path.GetFileName(this.pathToFile);
            }
        }

        public string Extension
        {
            get
            {
                return Path.GetExtension(this.pathToFile);
            }
        }

        public DateTime LastAccessTime { get; set; }

        public DateTime LastAccessTimeUtc { get; set; }

        public DateTime LastWriteTime { get; set; }

        public DateTime LastWriteTimeUtc
        {
            get
            {
                return this.fileSystem.GetFileProperties(this.pathToFile).LastWriteTimeUtc;
            }

            set
            {
                this.fileSystem.GetFileProperties(this.pathToFile).LastWriteTimeUtc = value;
            }
        }

        public IDirectoryInfo Directory
        {
            get
            {
                string parentDirectory = this.pathToFile.Substring(0, this.pathToFile.Length - (Path.GetFileName(this.pathToFile).Length + 1));

                return new InMemoryDirectoryInfo(this.fileSystem, parentDirectory);
            }
        }

        public string DirectoryName
        {
            get
            {
                return this.Directory.Name;
            }
        }

        public bool IsReadOnly { get; set; }

        public long Length
        {
            get
            {
                return this.fileSystem.GetFile(this.pathToFile).LongCount();
            }
        }

        public void Refresh()
        {
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
        }

        public void Delete()
        {
            this.fileSystem.DeleteFile(this.pathToFile);
        }

        public StreamWriter AppendText()
        {
            return null;
        }

        public IFileInfo CopyTo(string destFileName)
        {
            if (this.fileSystem.FileExists(destFileName))
            {
                throw new IOException("file " + destFileName + " already exists.");
            }

            this.fileSystem.AddFile(destFileName, this.fileSystem.GetFile(this.pathToFile));
            return new InMemoryFileInfo(this.fileSystem, destFileName);
        }

        public IFileInfo CopyTo(string destFileName, bool overwrite)
        {
            if (!overwrite)
            {
                return this.CopyTo(destFileName);
            }

            this.fileSystem.AddFile(destFileName, this.fileSystem.GetFile(this.pathToFile));

            return new InMemoryFileInfo(this.fileSystem, destFileName);
        }

        public Stream Create()
        {
            return null;
        }

        public StreamWriter CreateText()
        {
            return null;
        }

        public void Decrypt()
        {
        }

        public void Encrypt()
        {
        }

        public FileSecurity GetAccessControl()
        {
            return null;
        }

        public FileSecurity GetAccessControl(AccessControlSections includeSections)
        {
            return null;
        }

        public void MoveTo(string destFileName)
        {
            this.fileSystem.Move(this.pathToFile, destFileName);
        }

        public Stream Open(FileMode mode)
        {
            return null;
        }

        public Stream Open(FileMode mode, FileAccess access)
        {
            return null;
        }

        public Stream Open(FileMode mode, FileAccess access, FileShare share)
        {
            return null;
        }

        public Stream OpenRead()
        {
            return null;
        }

        public StreamReader OpenText()
        {
            return null;
        }

        public Stream OpenWrite()
        {
            return null;
        }

        public IFileInfo Replace(string destinationFileName, string destinationBackupFileName)
        {
            return null;
        }

        public IFileInfo Replace(string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors)
        {
            return null;
        }

        public void SetAccessControl(FileSecurity fileSecurity)
        {
        }
    }
}