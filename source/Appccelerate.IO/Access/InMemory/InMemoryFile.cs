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

    using Appccelerate.IO.Access.Internals;

    using Path = System.IO.Path;

    public class InMemoryFile : IFile, IExtensionProvider<IFileExtension>
    {
        private readonly IInMemoryFileSystem fileSystem;

        private readonly IEnumerable<IFileExtension> extensions;

        public InMemoryFile(IInMemoryFileSystem fileSystem, IEnumerable<IFileExtension> extensions)
        {
            this.fileSystem = fileSystem;
            this.extensions = extensions;
        }

        public IEnumerable<IFileExtension> Extensions
        {
            get
            {
                return this.extensions;
            }
        }

        public void Delete(string path)
        {
            this.EncapsulateWithExtension(
                () => this.fileSystem.DeleteFile(path),
                extension => extension.BeginDelete(path),
                extension => extension.EndDelete(path),
                (IFileExtension extension, ref Exception exception) => extension.FailDelete(ref exception, path));
        }

        public bool Exists(string path)
        {
            return this.EncapsulateWithExtension(
                () => this.fileSystem.FileExists(path),
                extension => extension.BeginExists(path),
                (extension, result) => extension.EndExists(result, path),
                (IFileExtension extension, ref Exception exception) => extension.FailExists(ref exception, path));
        }

        public IEnumerable<byte> ReadAllBytes(string path)
        {
            return this.EncapsulateWithExtension(
                () => this.fileSystem.GetFile(path),
                extension => extension.BeginReadAllBytes(path),
                (extension, result) => extension.EndReadAllBytes(result.ToArray(), path),
                (IFileExtension extension, ref Exception exception) => extension.FailReadAllBytes(ref exception, path));
        }

        public string ReadAllText(string path)
        {
            return this.EncapsulateWithExtension(
                () => Encoding.Default.GetString(this.fileSystem.GetFile(path).ToArray()),
                extension => extension.BeginReadAllText(path),
                (extension, result) => extension.EndReadAllText(result, path),
                (IFileExtension extension, ref Exception exception) => extension.FailReadAllText(ref exception, path));
        }

        public void WriteAllBytes(string path, IEnumerable<byte> bytes)
        {
            this.EncapsulateWithExtension(
                () =>
                    {
                        this.fileSystem.CreateDirectory(Path.GetDirectoryName(path));
                        this.fileSystem.AddFile(path, bytes);
                    },
                extension => extension.BeginWriteAllBytes(path, bytes.ToArray()),
                extension => extension.EndWriteAllBytes(path, bytes.ToArray()),
                (IFileExtension extension, ref Exception exception) => extension.FailWriteAllBytes(ref exception, path, bytes.ToArray()));
        }

        public void WriteAllText(string path, string contents)
        {
            this.EncapsulateWithExtension(
                () =>
                {
                    this.fileSystem.CreateDirectory(Path.GetDirectoryName(path));
                    this.fileSystem.AddFile(path, Encoding.Default.GetBytes(contents));
                },
                extension => extension.BeginWriteAllText(path, contents),
                extension => extension.EndWriteAllText(path, contents),
                (IFileExtension extension, ref Exception exception) => extension.FailWriteAllText(ref exception, path, contents));
        }

        public Stream OpenRead(string path)
        {
            return this.EncapsulateWithExtension(
                () =>
                    {
                        var bytes = this.fileSystem.GetFile(path).ToArray();

                        var stream = new MemoryStream(bytes);
                        stream.Seek(0, SeekOrigin.Begin);

                        return stream;
                    },
                extension => extension.BeginOpenRead(path),
                (extension, result) => extension.EndOpenRead(result, path),
                (IFileExtension extension, ref Exception exception) => extension.FailOpenRead(ref exception, path));
        }

        public void Move(string sourceFileName, string destinationFileName)
        {
            this.EncapsulateWithExtension(
                () => this.fileSystem.Move(sourceFileName, destinationFileName),
                extension => extension.BeginMove(sourceFileName, destinationFileName),
                extension => extension.EndMove(sourceFileName, destinationFileName),
                (IFileExtension extension, ref Exception exception) => extension.FailMove(ref exception, sourceFileName, destinationFileName));
        }

        public void Copy(string sourceFileName, string destFileName)
        {
            this.EncapsulateWithExtension(
                () => this.fileSystem.Copy(sourceFileName, destFileName),
                extension => extension.BeginCopy(sourceFileName, destFileName),
                extension => extension.EndCopy(sourceFileName, destFileName),
                (IFileExtension extension, ref Exception exception) => { });
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