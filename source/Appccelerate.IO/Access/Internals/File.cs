//-------------------------------------------------------------------------------
// <copyright file="File.cs" company="Appccelerate">
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

namespace Appccelerate.IO.Access.Internals
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.AccessControl;
    using System.Text;

    /// <summary>
    /// Wrapper class which simplifies the access to the file layer.
    /// </summary>
    public class File : IFile, IExtensionProvider<IFileExtension>
    {
        private readonly List<IFileExtension> extensions;

        /// <summary>
        /// Initializes a new instance of the <see cref="File"/> class.
        /// </summary>
        /// <param name="extensions">The extensions.</param>
        public File(IEnumerable<IFileExtension> extensions)
        {
            this.extensions = extensions.ToList();
        }

        /// <inheritdoc />
        public IEnumerable<IFileExtension> Extensions
        {
            get
            {
                return this.extensions;
            }
        }

        /// <inheritdoc />
        public void Delete(string path)
        {
            this.EncapsulateWithExtension(
                () => System.IO.File.Delete(path),
                e => e.BeginDelete(path),
                e => e.EndDelete(path),
                (IFileExtension e, ref Exception exception) => e.FailDelete(ref exception, path));
        }

        /// <inheritdoc />
        public void Copy(string sourceFileName, string destFileName)
        {
            this.EncapsulateWithExtension(
                () => System.IO.File.Copy(sourceFileName, destFileName),
                e => e.BeginCopy(sourceFileName, destFileName),
                e => e.EndCopy(sourceFileName, destFileName),
                (IFileExtension e, ref Exception exception) => e.FailCopy(ref exception, sourceFileName, destFileName));
        }

        /// <inheritdoc />
        public void Copy(string sourceFileName, string destFileName, bool overwrite)
        {
            this.EncapsulateWithExtension(
                () => System.IO.File.Copy(sourceFileName, destFileName, overwrite),
                e => e.BeginCopy(sourceFileName, destFileName, overwrite),
                e => e.EndCopy(sourceFileName, destFileName, overwrite),
                (IFileExtension e, ref Exception exception) => e.FailCopy(ref exception, sourceFileName, destFileName, overwrite));
        }

        /// <inheritdoc />
        public StreamWriter CreateText(string path)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.File.CreateText(path),
                e => e.BeginCreateText(path),
                (e, r) => e.EndCreateText(r, path),
                (IFileExtension e, ref Exception exception) => e.FailCreateText(ref exception, path));
        }

        /// <inheritdoc />
        public FileAttributes GetAttributes(string path)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.File.GetAttributes(path),
                e => e.BeginGetAttributes(path),
                (e, r) => e.EndGetAttributes(r, path),
                (IFileExtension e, ref Exception exception) => e.FailGetAttributes(ref exception, path));
        }

        /// <inheritdoc />
        public void SetLastWriteTime(string path, DateTime lastWriteTime)
        {
            this.EncapsulateWithExtension(
                () => System.IO.File.SetLastWriteTime(path, lastWriteTime),
                e => e.BeginSetLastWriteTime(path, lastWriteTime),
                e => e.EndSetLastWriteTime(path, lastWriteTime),
                (IFileExtension e, ref Exception exception) => e.FailSetLastWriteTime(ref exception, path, lastWriteTime));
        }

        /// <inheritdoc />
        public void SetAttributes(string path, FileAttributes fileAttributes)
        {
            this.EncapsulateWithExtension(
                () => System.IO.File.SetAttributes(path, fileAttributes),
                e => e.BeginSetAttributes(path, fileAttributes),
                e => e.EndSetAttributes(path, fileAttributes),
                (IFileExtension e, ref Exception exception) => e.FailSetAttributes(ref exception, path, fileAttributes));
        }

        /// <inheritdoc />
        public bool Exists(string path)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.File.Exists(path),
                e => e.BeginExists(path),
                (e, r) => e.EndExists(r, path),
                (IFileExtension e, ref Exception exception) => e.FailExists(ref exception, path));
        }

        /// <inheritdoc />
        public IEnumerable<byte> ReadAllBytes(string path)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.File.ReadAllBytes(path),
                e => e.BeginReadAllBytes(path),
                (e, r) => e.EndReadAllBytes(r, path),
                (IFileExtension e, ref Exception exception) => e.FailReadAllBytes(ref exception, path));
        }

        /// <inheritdoc />
        public IEnumerable<string> ReadAllLines(string path, Encoding encoding)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.File.ReadAllLines(path, encoding),
                e => e.BeginReadAllLines(path, encoding),
                (e, r) => e.EndReadAllLines(r, path, encoding),
                (IFileExtension e, ref Exception exception) => e.FailReadAllLines(ref exception, path, encoding));
        }

        /// <inheritdoc />
        public IEnumerable<string> ReadAllLines(string path)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.File.ReadAllLines(path),
                e => e.BeginReadAllLines(path),
                (e, r) => e.EndReadAllLines(r, path),
                (IFileExtension e, ref Exception exception) => e.FailReadAllLines(ref exception, path));
        }

        /// <inheritdoc />
        public string ReadAllText(string path, Encoding encoding)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.File.ReadAllText(path, encoding),
                e => e.BeginReadAllText(path, encoding),
                (e, r) => e.EndReadAllText(r, path, encoding),
                (IFileExtension e, ref Exception exception) => e.FailReadAllText(ref exception, path, encoding));
        }

        /// <inheritdoc />
        public string ReadAllText(string path)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.File.ReadAllText(path),
                e => e.BeginReadAllText(path),
                (e, r) => e.EndReadAllText(r, path),
                (IFileExtension e, ref Exception exception) => e.FailReadAllText(ref exception, path));
        }

        /// <inheritdoc />
        public IEnumerable<string> ReadLines(string path)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.File.ReadLines(path),
                e => e.BeginReadLines(path),
                (e, r) => e.EndReadLines(r, path),
                (IFileExtension e, ref Exception exception) => e.FailReadLines(ref exception, path));
        }

        /// <inheritdoc />
        public IEnumerable<string> ReadLines(string path, Encoding encoding)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.File.ReadLines(path, encoding),
                e => e.BeginReadLines(path, encoding),
                (e, r) => e.EndReadLines(r, path, encoding),
                (IFileExtension e, ref Exception exception) => e.FailReadLines(ref exception, path, encoding));
        }

        /// <inheritdoc />
        public void WriteAllLines(string path, IEnumerable<string> contents, Encoding encoding)
        {
            this.EncapsulateWithExtension(
                () => System.IO.File.WriteAllLines(path, contents, encoding),
                e => e.BeginWriteAllLines(path, contents, encoding),
                e => e.EndWriteAllLines(path, contents, encoding),
                (IFileExtension e, ref Exception exception) => e.FailWriteAllLines(ref exception, path, contents, encoding));
        }

        /// <inheritdoc />
        public void WriteAllLines(string path, IEnumerable<string> contents)
        {
            this.EncapsulateWithExtension(
                () => System.IO.File.WriteAllLines(path, contents),
                e => e.BeginWriteAllLines(path, contents),
                e => e.EndWriteAllLines(path, contents),
                (IFileExtension e, ref Exception exception) => e.FailWriteAllLines(ref exception, path, contents));
        }

        /// <inheritdoc />
        public void WriteAllText(string path, string contents)
        {
            this.EncapsulateWithExtension(
                () => System.IO.File.WriteAllText(path, contents),
                e => e.BeginWriteAllText(path, contents),
                e => e.EndWriteAllText(path, contents),
                (IFileExtension e, ref Exception exception) => e.FailWriteAllText(ref exception, path, contents));
        }

        /// <inheritdoc />
        public void WriteAllText(string path, string contents, Encoding encoding)
        {
            this.EncapsulateWithExtension(
                () => System.IO.File.WriteAllText(path, contents, encoding),
                e => e.BeginWriteAllText(path, contents, encoding),
                e => e.EndWriteAllText(path, contents, encoding),
                (IFileExtension e, ref Exception exception) => e.FailWriteAllText(ref exception, path, contents, encoding));
        }

        /// <inheritdoc />
        public void WriteAllBytes(string path, IEnumerable<byte> bytes)
        {
            byte[] bytesAsArray = bytes.ToArray();

            this.EncapsulateWithExtension(
                () => System.IO.File.WriteAllBytes(path, bytesAsArray),
                e => e.BeginWriteAllBytes(path, bytesAsArray),
                e => e.EndWriteAllBytes(path, bytesAsArray),
                (IFileExtension e, ref Exception exception) => e.FailWriteAllBytes(ref exception, path, bytesAsArray));
        }

        /// <inheritdoc />
        public Stream Open(string path, FileMode mode)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.File.Open(path, mode),
                e => e.BeginOpen(path, mode),
                (e, r) => e.EndOpen(r, path, mode),
                (IFileExtension e, ref Exception exception) => e.FailOpen(ref exception, path, mode));
        }

        /// <inheritdoc />
        public Stream Open(string path, FileMode mode, FileAccess access)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.File.Open(path, mode, access),
                e => e.BeginOpen(path, mode, access),
                (e, r) => e.EndOpen(r, path, mode, access),
                (IFileExtension e, ref Exception exception) => e.FailOpen(ref exception, path, mode, access));
        }

        /// <inheritdoc />
        public Stream Open(string path, FileMode mode, FileAccess access, FileShare share)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.File.Open(path, mode, access, share),
                e => e.BeginOpen(path, mode, access, share),
                (e, r) => e.EndOpen(r, path, mode, access, share),
                (IFileExtension e, ref Exception exception) => e.FailOpen(ref exception, path, mode, access, share));
        }

        /// <inheritdoc />
        public void AppendAllLines(string path, IEnumerable<string> contents)
        {
            this.EncapsulateWithExtension(
                () => System.IO.File.AppendAllLines(path, contents),
                e => e.BeginAppendAllLines(path, contents),
                e => e.EndAppendAllLines(path, contents),
                (IFileExtension e, ref Exception exception) => e.FailAppendAllLines(ref exception, path, contents));
        }

        /// <inheritdoc />
        public void AppendAllLines(string path, IEnumerable<string> contents, Encoding encoding)
        {
            this.EncapsulateWithExtension(
                () => System.IO.File.AppendAllLines(path, contents, encoding),
                e => e.BeginAppendAllLines(path, contents, encoding),
                e => e.EndAppendAllLines(path, contents, encoding),
                (IFileExtension e, ref Exception exception) => e.FailAppendAllLines(ref exception, path, contents, encoding));
        }

        /// <inheritdoc />
        public void AppendAllText(string path, string contents)
        {
            this.EncapsulateWithExtension(
                () => System.IO.File.AppendAllText(path, contents),
                e => e.BeginAppendAllText(path, contents),
                e => e.EndAppendAllText(path, contents),
                (IFileExtension e, ref Exception exception) => e.FailAppendAllText(ref exception, path, contents));
        }

        /// <inheritdoc />
        public void AppendAllText(string path, string contents, Encoding encoding)
        {
            this.EncapsulateWithExtension(
                () => System.IO.File.AppendAllText(path, contents, encoding),
                e => e.BeginAppendAllText(path, contents, encoding),
                e => e.EndAppendAllText(path, contents, encoding),
                (IFileExtension e, ref Exception exception) => e.FailAppendAllText(ref exception, path, contents, encoding));
        }

        /// <inheritdoc />
        public StreamWriter AppendText(string path)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.File.AppendText(path),
                e => e.BeginAppendText(path),
                (e, r) => e.EndAppendText(r, path),
                (IFileExtension e, ref Exception exception) => e.FailAppendText(ref exception, path));
        }

        /// <inheritdoc />
        public Stream Create(string path)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.File.Create(path),
                e => e.BeginCreate(path),
                (e, r) => e.EndCreate(r, path),
                (IFileExtension e, ref Exception exception) => e.FailCreate(ref exception, path));
        }

        /// <inheritdoc />
        public Stream Create(string path, int bufferSize)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.File.Create(path, bufferSize),
                e => e.BeginCreate(path, bufferSize),
                (e, r) => e.EndCreate(r, path, bufferSize),
                (IFileExtension e, ref Exception exception) => e.FailCreate(ref exception, path, bufferSize));
        }

        /// <inheritdoc />
        public Stream Create(string path, int bufferSize, FileOptions options)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.File.Create(path, bufferSize, options),
                e => e.BeginCreate(path, bufferSize, options),
                (e, r) => e.EndCreate(r, path, bufferSize, options),
                (IFileExtension e, ref Exception exception) => e.FailCreate(ref exception, path, bufferSize, options));
        }

        /// <inheritdoc />
        public Stream Create(string path, int bufferSize, FileOptions options, FileSecurity fileSecurity)
        {
            return this.EncapsulateWithExtension(
                () => new System.IO.FileInfo(path).Create(FileMode.Create, FileSystemRights.Read | FileSystemRights.Write, FileShare.None, bufferSize, options, fileSecurity),
                e => e.BeginCreate(path, bufferSize, options, fileSecurity),
                (e, r) => e.EndCreate(r, path, bufferSize, options, fileSecurity),
                (IFileExtension e, ref Exception exception) => e.FailCreate(ref exception, path, bufferSize, options, fileSecurity));
        }

        /// <inheritdoc />
        public void Decrypt(string path)
        {
            this.EncapsulateWithExtension(
                () => System.IO.File.Decrypt(path),
                e => e.BeginDecrypt(path),
                e => e.EndDecrypt(path),
                (IFileExtension e, ref Exception exception) => e.FailDecrypt(ref exception, path));
        }

        /// <inheritdoc />
        public void Encrypt(string path)
        {
            this.EncapsulateWithExtension(
                () => System.IO.File.Encrypt(path),
                e => e.BeginEncrypt(path),
                e => e.EndEncrypt(path),
                (IFileExtension e, ref Exception exception) => e.FailEncrypt(ref exception, path));
        }

        /// <inheritdoc />
        public FileSecurity GetAccessControl(string path)
        {
            return  this.EncapsulateWithExtension(
                () => new System.IO.FileInfo(path).GetAccessControl(),
                e => e.BeginGetAccessControl(path),
                (e, r) => e.EndGetAccessControl(r, path),
                (IFileExtension e, ref Exception exception) => e.FailGetAccessControl(ref exception, path));
        }

        /// <inheritdoc />
        public FileSecurity GetAccessControl(string path, AccessControlSections includeSections)
        {
            return this.EncapsulateWithExtension(
                () => new System.IO.FileInfo(path).GetAccessControl(includeSections),
                e => e.BeginGetAccessControl(path, includeSections),
                (e, r) => e.EndGetAccessControl(r, path, includeSections),
                (IFileExtension e, ref Exception exception) => e.FailGetAccessControl(ref exception, path, includeSections));
        }

        /// <inheritdoc />
        public DateTime GetCreationTime(string path)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.File.GetCreationTime(path),
                e => e.BeginGetCreationTime(path),
                (e, r) => e.EndGetCreationTime(r, path),
                (IFileExtension e, ref Exception exception) => e.FailGetCreationTime(ref exception, path));
        }

        /// <inheritdoc />
        public DateTime GetCreationTimeUtc(string path)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.File.GetCreationTimeUtc(path),
                e => e.BeginGetCreationTimeUtc(path),
                (e, r) => e.EndGetCreationTimeUtc(r, path),
                (IFileExtension e, ref Exception exception) => e.FailGetCreationTimeUtc(ref exception, path));
        }

        /// <inheritdoc />
        public DateTime GetLastAccessTime(string path)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.File.GetLastAccessTime(path),
                e => e.BeginGetLastAccessTime(path),
                (e, r) => e.EndGetLastAccessTime(r, path),
                (IFileExtension e, ref Exception exception) => e.FailGetLastAccessTime(ref exception, path));
        }

        /// <inheritdoc />
        public DateTime GetLastAccessTimeUtc(string path)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.File.GetLastAccessTimeUtc(path),
                e => e.BeginGetLastAccessTimeUtc(path),
                (e, r) => e.EndGetLastAccessTimeUtc(r, path),
                (IFileExtension e, ref Exception exception) => e.FailGetLastAccessTimeUtc(ref exception, path));
        }

        /// <inheritdoc />
        public DateTime GetLastWriteTime(string path)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.File.GetLastWriteTime(path),
                e => e.BeginGetLastWriteTime(path),
                (e, r) => e.EndGetLastWriteTime(r, path),
                (IFileExtension e, ref Exception exception) => e.FailGetLastWriteTime(ref exception, path));
        }

        /// <inheritdoc />
        public DateTime GetLastWriteTimeUtc(string path)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.File.GetLastWriteTimeUtc(path),
                e => e.BeginGetLastWriteTimeUtc(path),
                (e, r) => e.EndGetLastWriteTimeUtc(r, path),
                (IFileExtension e, ref Exception exception) => e.FailGetLastWriteTimeUtc(ref exception, path));
        }

        /// <inheritdoc />
        public void Move(string sourceFileName, string destFileName)
        {
            this.EncapsulateWithExtension(
                () => System.IO.File.Move(sourceFileName, destFileName),
                e => e.BeginMove(sourceFileName, destFileName),
                e => e.EndMove(sourceFileName, destFileName),
                (IFileExtension e, ref Exception exception) => e.FailMove(ref exception, sourceFileName, destFileName));
        }

        /// <inheritdoc />
        public Stream OpenRead(string path)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.File.OpenRead(path),
                e => e.BeginOpenRead(path),
                (e, r) => e.EndOpenRead(r, path),
                (IFileExtension e, ref Exception exception) => e.FailOpenRead(ref exception, path));
        }

        /// <inheritdoc />
        public StreamReader OpenText(string path)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.File.OpenText(path),
                e => e.BeginOpenText(path),
                (e, r) => e.EndOpenText(r, path),
                (IFileExtension e, ref Exception exception) => e.FailOpenText(ref exception, path));
        }

        /// <inheritdoc />
        public Stream OpenWrite(string path)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.File.OpenWrite(path),
                e => e.BeginOpenWrite(path),
                (e, r) => e.EndOpenWrite(r, path),
                (IFileExtension e, ref Exception exception) => e.FailOpenWrite(ref exception, path));
        }

        /// <inheritdoc />
        public void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName)
        {
            this.EncapsulateWithExtension(
                () => System.IO.File.Replace(sourceFileName, destinationFileName, destinationBackupFileName),
                e => e.BeginReplace(sourceFileName, destinationFileName, destinationBackupFileName),
                e => e.EndReplace(sourceFileName, destinationFileName, destinationBackupFileName),
                (IFileExtension e, ref Exception exception) => e.FailReplace(ref exception, sourceFileName, destinationFileName, destinationBackupFileName));
        }

        /// <inheritdoc />
        public void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors)
        {
            this.EncapsulateWithExtension(
                () => System.IO.File.Replace(sourceFileName, destinationFileName, destinationBackupFileName, ignoreMetadataErrors),
                e => e.BeginReplace(sourceFileName, destinationFileName, destinationBackupFileName, ignoreMetadataErrors),
                e => e.EndReplace(sourceFileName, destinationFileName, destinationBackupFileName, ignoreMetadataErrors),
                (IFileExtension e, ref Exception exception) => e.FailReplace(ref exception, sourceFileName, destinationFileName, destinationBackupFileName, ignoreMetadataErrors));
        }

        /// <inheritdoc />
        public void SetAccessControl(string path, FileSecurity fileSecurity)
        {
            this.EncapsulateWithExtension(
                () => new System.IO.FileInfo(path).SetAccessControl(fileSecurity),
                e => e.BeginSetAccessControl(path, fileSecurity),
                e => e.EndSetAccessControl(path, fileSecurity),
                (IFileExtension e, ref Exception exception) => e.FailSetAccessControl(ref exception, path, fileSecurity));
        }

        /// <inheritdoc />
        public void SetCreationTime(string path, DateTime creationTime)
        {
            this.EncapsulateWithExtension(
                () => System.IO.File.SetCreationTime(path, creationTime),
                e => e.BeginSetCreationTime(path, creationTime),
                e => e.EndSetCreationTime(path, creationTime),
                (IFileExtension e, ref Exception exception) => e.FailSetCreationTime(ref exception, path, creationTime));
        }

        /// <inheritdoc />
        public void SetCreationTimeUtc(string path, DateTime creationTimeUtc)
        {
            this.EncapsulateWithExtension(
                () => System.IO.File.SetCreationTimeUtc(path, creationTimeUtc),
                e => e.BeginSetCreationTimeUtc(path, creationTimeUtc),
                e => e.EndSetCreationTimeUtc(path, creationTimeUtc),
                (IFileExtension e, ref Exception exception) => e.FailSetCreationTimeUtc(ref exception, path, creationTimeUtc));
        }

        /// <inheritdoc />
        public void SetLastAccessTime(string path, DateTime lastAccessTime)
        {
            this.EncapsulateWithExtension(
                () => System.IO.File.SetLastAccessTime(path, lastAccessTime),
                e => e.BeginSetLastAccessTime(path, lastAccessTime),
                e => e.EndSetLastAccessTime(path, lastAccessTime),
                (IFileExtension e, ref Exception exception) => e.FailSetLastAccessTime(ref exception, path, lastAccessTime));
        }

        /// <inheritdoc />
        public void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc)
        {
            this.EncapsulateWithExtension(
                () => System.IO.File.SetLastAccessTimeUtc(path, lastAccessTimeUtc),
                e => e.BeginSetLastAccessTimeUtc(path, lastAccessTimeUtc),
                e => e.EndSetLastAccessTimeUtc(path, lastAccessTimeUtc),
                (IFileExtension e, ref Exception exception) => e.FailSetLastAccessTimeUtc(ref exception, path, lastAccessTimeUtc));
        }

        /// <inheritdoc />
        public void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc)
        {
            this.EncapsulateWithExtension(
                () => System.IO.File.SetLastWriteTimeUtc(path, lastWriteTimeUtc),
                e => e.BeginSetLastWriteTimeUtc(path, lastWriteTimeUtc),
                e => e.EndSetLastWriteTimeUtc(path, lastWriteTimeUtc),
                (IFileExtension e, ref Exception exception) => e.FailSetLastWriteTimeUtc(ref exception, path, lastWriteTimeUtc));
        }
    }
}