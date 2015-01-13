// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InMemoryFile.cs" company="Appccelerate">
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
    using System.Text;

    public class InMemoryFile : IFile
    {
        private readonly IInMemoryFileSystem fileSystem;

        public InMemoryFile(IInMemoryFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public void Delete(string path)
        {
            this.fileSystem.DeleteFile(path);
        }

        public bool Exists(string path)
        {
            return this.fileSystem.FileExists(path);
        }

        public IEnumerable<byte> ReadAllBytes(string path)
        {
            return this.fileSystem.GetFile(path);
        }

        public string ReadAllText(string path)
        {
            return Encoding.Default.GetString(this.ReadAllBytes(path).ToArray());
        }

        public void WriteAllBytes(string path, IEnumerable<byte> bytes)
        {
            this.fileSystem.CreateDirectory(Path.GetDirectoryName(path));
            this.fileSystem.AddFile(path, bytes);
        }

        public void WriteAllText(string path, string contents)
        {
            this.WriteAllBytes(path, Encoding.Default.GetBytes(contents));
        }

        public Stream OpenRead(string path)
        {
            var bytes = this.fileSystem.GetFile(path).ToArray();

            var stream = new MemoryStream(bytes);
            stream.Seek(0, SeekOrigin.Begin);

            return stream;
        }

        public void Move(string sourceFileName, string destinationFileName)
        {
            this.fileSystem.Move(sourceFileName, destinationFileName);
        }

        public void Copy(string sourceFileName, string destFileName)
        {
            throw new NotImplementedException();
        }

        public void Copy(string sourceFileName, string destFileName, bool overwrite)
        {
            throw new NotImplementedException();
        }

        public StreamWriter CreateText(string path)
        {
            throw new NotImplementedException();
        }

        public FileAttributes GetAttributes(string path)
        {
            throw new NotImplementedException();
        }

        public void SetLastWriteTime(string path, DateTime lastWriteTime)
        {
            throw new NotImplementedException();
        }

        public void SetAttributes(string path, FileAttributes fileAttributes)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> ReadAllLines(string path, Encoding encoding)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> ReadAllLines(string path)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> ReadLines(string path)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> ReadLines(string path, Encoding encoding)
        {
            throw new NotImplementedException();
        }

        public string ReadAllText(string path, Encoding encoding)
        {
            throw new NotImplementedException();
        }

        public void WriteAllLines(string path, IEnumerable<string> contents, Encoding encoding)
        {
            throw new NotImplementedException();
        }

        public void WriteAllLines(string path, IEnumerable<string> contents)
        {
            throw new NotImplementedException();
        }

        public void WriteAllText(string path, string contents, Encoding encoding)
        {
            throw new NotImplementedException();
        }

        public Stream Open(string path, FileMode mode)
        {
            throw new NotImplementedException();
        }

        public Stream Open(string path, FileMode mode, FileAccess access)
        {
            throw new NotImplementedException();
        }

        public Stream Open(string path, FileMode mode, FileAccess access, FileShare share)
        {
            throw new NotImplementedException();
        }

        public void AppendAllLines(string path, IEnumerable<string> contents)
        {
            throw new NotImplementedException();
        }

        public void AppendAllLines(string path, IEnumerable<string> contents, Encoding encoding)
        {
            throw new NotImplementedException();
        }

        public void AppendAllText(string path, string contents)
        {
            throw new NotImplementedException();
        }

        public void AppendAllText(string path, string contents, Encoding encoding)
        {
            throw new NotImplementedException();
        }

        public StreamWriter AppendText(string path)
        {
            throw new NotImplementedException();
        }

        public Stream Create(string path)
        {
            throw new NotImplementedException();
        }

        public Stream Create(string path, int bufferSize)
        {
            throw new NotImplementedException();
        }

        public Stream Create(string path, int bufferSize, FileOptions options)
        {
            throw new NotImplementedException();
        }

        public Stream Create(string path, int bufferSize, FileOptions options, FileSecurity fileSecurity)
        {
            throw new NotImplementedException();
        }

        public void Decrypt(string path)
        {
            throw new NotImplementedException();
        }

        public void Encrypt(string path)
        {
            throw new NotImplementedException();
        }

        public FileSecurity GetAccessControl(string path)
        {
            throw new NotImplementedException();
        }

        public FileSecurity GetAccessControl(string path, AccessControlSections includeSections)
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

        public StreamReader OpenText(string path)
        {
            throw new NotImplementedException();
        }

        public Stream OpenWrite(string path)
        {
            throw new NotImplementedException();
        }

        public void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName)
        {
            throw new NotImplementedException();
        }

        public void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors)
        {
            throw new NotImplementedException();
        }

        public void SetAccessControl(string path, FileSecurity fileSecurity)
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

        public void SetLastAccessTime(string path, DateTime lastAccessTime)
        {
            throw new NotImplementedException();
        }

        public void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc)
        {
            throw new NotImplementedException();
        }

        public void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc)
        {
            throw new NotImplementedException();
        }
    }
}