//-------------------------------------------------------------------------------
// <copyright file="Directory.cs" company="Appccelerate">
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

    /// <summary>
    /// Wrapper class which simplifies the access to directories.
    /// </summary>
    public class Directory : IDirectory, IExtensionProvider<IDirectoryExtension>
    {
        private readonly List<IDirectoryExtension> extensions;

        /// <summary>
        /// Initializes a new instance of the <see cref="Directory"/> class.
        /// </summary>
        /// <param name="extensions">The extensions.</param>
        public Directory(IEnumerable<IDirectoryExtension> extensions)
        {
            this.extensions = extensions.ToList();
        }

        /// <inheritdoc />
        public IEnumerable<IDirectoryExtension> Extensions
        {
            get
            {
                return this.extensions;
            }
        }

        /// <inheritdoc />
        public bool Exists(string path)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.Directory.Exists(path),
                e => e.BeginExists(path),
                (e, r) => e.EndExists(r, path),
                (IDirectoryExtension e, ref Exception exception) => e.FailExists(ref exception, path));
        }

        /// <inheritdoc />
        public IDirectoryInfo CreateDirectory(string path)
        {
            return this.EncapsulateWithExtension(
                () => new DirectoryInfo(System.IO.Directory.CreateDirectory(path)),
                e => e.BeginCreateDirectory(path),
                (e, r) => e.EndCreateDirectory(r, path),
                (IDirectoryExtension e, ref Exception exception) => e.FailCreateDirectory(ref exception, path));
        }

        /// <inheritdoc />
        public IDirectoryInfo CreateDirectory(string path, DirectorySecurity directorySecurity)
        {
            return this.EncapsulateWithExtension(
                () => new DirectoryInfo(System.IO.Directory.CreateDirectory(path, directorySecurity)),
                e => e.BeginCreateDirectory(path, directorySecurity),
                (e, r) => e.EndCreateDirectory(r, path, directorySecurity),
                (IDirectoryExtension e, ref Exception exception) => e.FailCreateDirectory(ref exception, path, directorySecurity));
        }

        /// <inheritdoc />
        public void Delete(string path, bool recursive)
        {
            this.EncapsulateWithExtension(
                () => System.IO.Directory.Delete(path, recursive),
                e => e.BeginDelete(path, recursive),
                e => e.EndDelete(path, recursive),
                (IDirectoryExtension e, ref Exception exception) => e.FailDelete(ref exception, path, recursive));
        }

        /// <inheritdoc />
        public void Delete(string path)
        {
            this.EncapsulateWithExtension(
                () => System.IO.Directory.Delete(path),
                e => e.BeginDelete(path),
                e => e.EndDelete(path),
                (IDirectoryExtension e, ref Exception exception) => e.FailDelete(ref exception, path));
        }

        /// <inheritdoc />
        public IEnumerable<string> GetFiles(string path)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.Directory.GetFiles(path),
                e => e.BeginGetFiles(path),
                (e, r) => e.EndGetFiles(r, path),
                (IDirectoryExtension e, ref Exception exception) => e.FailGetFiles(ref exception, path));
        }

        /// <inheritdoc />
        public IEnumerable<string> GetFiles(string path, string searchPattern)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.Directory.GetFiles(path, searchPattern),
                e => e.BeginGetFiles(path, searchPattern),
                (e, r) => e.EndGetFiles(r, path, searchPattern),
                (IDirectoryExtension e, ref Exception exception) => e.FailGetFiles(ref exception, path, searchPattern));
        }

        /// <inheritdoc />
        public IEnumerable<string> GetFiles(string path, string searchPattern, SearchOption searchOption)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.Directory.GetFiles(path, searchPattern, searchOption),
                e => e.BeginGetFiles(path, searchPattern, searchOption),
                (e, r) => e.EndGetFiles(r, path, searchPattern, searchOption),
                (IDirectoryExtension e, ref Exception exception) => e.FailGetFiles(ref exception, path, searchPattern, searchOption));
        }

        /// <inheritdoc />
        public IEnumerable<string> GetDirectories(string path)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.Directory.GetDirectories(path),
                e => e.BeginGetDirectories(path),
                (e, r) => e.EndGetDirectories(r, path),
                (IDirectoryExtension e, ref Exception exception) => e.FailGetDirectories(ref exception, path));
        }

        /// <inheritdoc />
        public DirectorySecurity GetAccessControl(string path)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.Directory.GetAccessControl(path),
                e => e.BeginGetAccessControl(path),
                (e, r) => e.EndGetAccessControl(r, path),
                (IDirectoryExtension e, ref Exception exception) => e.FailGetAccessControl(ref exception, path));
        }

        /// <inheritdoc />
        public DirectorySecurity GetAccessControl(string path, AccessControlSections includeSections)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.Directory.GetAccessControl(path, includeSections),
                e => e.BeginGetAccessControl(path, includeSections),
                (e, r) => e.EndGetAccessControl(r, path, includeSections),
                (IDirectoryExtension e, ref Exception exception) => e.FailGetAccessControl(ref exception, path, includeSections));
        }

        /// <inheritdoc />
        public DateTime GetCreationTime(string path)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.Directory.GetCreationTime(path),
                e => e.BeginGetCreationTime(path),
                (e, r) => e.EndGetCreationTime(r, path),
                (IDirectoryExtension e, ref Exception exception) => e.FailGetCreationTime(ref exception, path));
        }

        /// <inheritdoc />
        public DateTime GetCreationTimeUtc(string path)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.Directory.GetCreationTimeUtc(path),
                e => e.BeginGetCreationTimeUtc(path),
                (e, r) => e.EndGetCreationTimeUtc(r, path),
                (IDirectoryExtension e, ref Exception exception) => e.FailGetCreationTimeUtc(ref exception, path));
        }

        /// <inheritdoc />
        public string GetCurrentDirectory()
        {
            return this.EncapsulateWithExtension(
                () => System.IO.Directory.GetCurrentDirectory(),
                e => e.BeginGetCurrentDirectory(),
                (e, r) => e.EndGetCurrentDirectory(r),
                (IDirectoryExtension e, ref Exception exception) => e.FailGetCurrentDirectory(ref exception));
        }

        /// <inheritdoc />
        public IEnumerable<string> GetDirectories(string path, string searchPattern)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.Directory.GetDirectories(path, searchPattern),
                e => e.BeginGetDirectories(path, searchPattern),
                (e, r) => e.EndGetDirectories(r, path, searchPattern),
                (IDirectoryExtension e, ref Exception exception) => e.FailGetDirectories(ref exception, path, searchPattern));
        }

        /// <inheritdoc />
        public IEnumerable<string> GetDirectories(string path, string searchPattern, SearchOption searchOption)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.Directory.GetDirectories(path, searchPattern, searchOption),
                e => e.BeginGetDirectories(path, searchPattern, searchOption),
                (e, r) => e.EndGetDirectories(r, path, searchPattern, searchOption),
                (IDirectoryExtension e, ref Exception exception) => e.FailGetDirectories(ref exception, path, searchPattern, searchOption));
        }

        /// <inheritdoc />
        public string GetDirectoryRoot(string path)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.Directory.GetDirectoryRoot(path),
                e => e.BeginGetDirectoryRoot(path),
                (e, r) => e.EndGetDirectoryRoot(r, path),
                (IDirectoryExtension e, ref Exception exception) => e.FailGetDirectoryRoot(ref exception, path));
        }

        /// <inheritdoc />
        public IEnumerable<string> GetFileSystemEntries(string path)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.Directory.GetFileSystemEntries(path),
                e => e.BeginGetFileSystemEntries(path),
                (e, r) => e.EndGetFileSystemEntries(r, path),
                (IDirectoryExtension e, ref Exception exception) => e.FailGetFileSystemEntries(ref exception, path));
        }

        /// <inheritdoc />
        public IEnumerable<string> GetFileSystemEntries(string path, string searchPattern)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.Directory.GetFileSystemEntries(path, searchPattern),
                e => e.BeginGetFileSystemEntries(path, searchPattern),
                (e, r) => e.EndGetFileSystemEntries(r, path, searchPattern),
                (IDirectoryExtension e, ref Exception exception) => e.FailGetFileSystemEntries(ref exception, path, searchPattern));
        }

        /// <inheritdoc />
        public DateTime GetLastAccessTime(string path)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.Directory.GetLastAccessTime(path),
                e => e.BeginGetLastAccessTime(path),
                (e, r) => e.EndGetLastAccessTime(r, path),
                (IDirectoryExtension e, ref Exception exception) => e.FailGetLastAccessTime(ref exception, path));
        }

        /// <inheritdoc />
        public DateTime GetLastAccessTimeUtc(string path)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.Directory.GetLastAccessTimeUtc(path),
                e => e.BeginGetLastAccessTimeUtc(path),
                (e, r) => e.EndGetLastAccessTimeUtc(r, path),
                (IDirectoryExtension e, ref Exception exception) => e.FailGetLastAccessTimeUtc(ref exception, path));
        }

        /// <inheritdoc />
        public DateTime GetLastWriteTime(string path)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.Directory.GetLastWriteTime(path),
                e => e.BeginGetLastWriteTime(path),
                (e, r) => e.EndGetLastWriteTime(r, path),
                (IDirectoryExtension e, ref Exception exception) => e.FailGetLastWriteTime(ref exception, path));
        }

        /// <inheritdoc />
        public DateTime GetLastWriteTimeUtc(string path)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.Directory.GetLastWriteTimeUtc(path),
                e => e.BeginGetLastWriteTimeUtc(path),
                (e, r) => e.EndGetLastWriteTimeUtc(r, path),
                (IDirectoryExtension e, ref Exception exception) => e.FailGetLastWriteTimeUtc(ref exception, path));
        }

        /// <inheritdoc />
        public IEnumerable<string> GetLogicalDrives()
        {
            return this.EncapsulateWithExtension(
                () => System.IO.Directory.GetLogicalDrives(),
                e => e.BeginGetLogicalDrives(),
                (e, r) => e.EndGetLogicalDrives(r),
                (IDirectoryExtension e, ref Exception exception) => e.FailGetLogicalDrives(ref exception));
        }

        /// <inheritdoc />
        public IDirectoryInfo GetParent(string path)
        {
            var directoryInfo = this.EncapsulateWithExtension(
                () => System.IO.Directory.GetParent(path),
                e => e.BeginGetParent(path),
                (e, r) => e.EndGetParent(r, path),
                (IDirectoryExtension e, ref Exception exception) => e.FailGetParent(ref exception, path));
            return new DirectoryInfo(directoryInfo);
        }

        /// <inheritdoc />
        public void Move(string sourceDirName, string destDirName)
        {
            this.EncapsulateWithExtension(
                () => System.IO.Directory.Move(sourceDirName, destDirName),
                e => e.BeginMove(sourceDirName, destDirName),
                e => e.EndMove(sourceDirName, destDirName),
                (IDirectoryExtension e, ref Exception exception) => e.FailMove(ref exception, sourceDirName, destDirName));
        }

        /// <inheritdoc />
        public void SetAccessControl(string path, DirectorySecurity directorySecurity)
        {
            this.EncapsulateWithExtension(
                () => System.IO.Directory.SetAccessControl(path, directorySecurity),
                e => e.BeginSetAccessControl(path, directorySecurity),
                e => e.EndSetAccessControl(path, directorySecurity),
                (IDirectoryExtension e, ref Exception exception) => e.FailSetAccessControl(ref exception, path, directorySecurity));
        }

        /// <inheritdoc />
        public void SetCreationTime(string path, DateTime creationTime)
        {
            this.EncapsulateWithExtension(
                () => System.IO.Directory.SetCreationTime(path, creationTime),
                e => e.BeginSetCreationTime(path, creationTime),
                e => e.EndSetCreationTime(path, creationTime),
                (IDirectoryExtension e, ref Exception exception) => e.FailSetCreationTime(ref exception, path, creationTime));
        }

        /// <inheritdoc />
        public void SetCreationTimeUtc(string path, DateTime creationTimeUtc)
        {
            this.EncapsulateWithExtension(
                () => System.IO.Directory.SetCreationTimeUtc(path, creationTimeUtc),
                e => e.BeginSetCreationTimeUtc(path, creationTimeUtc),
                e => e.EndSetCreationTimeUtc(path, creationTimeUtc),
                (IDirectoryExtension e, ref Exception exception) => e.FailSetCreationTimeUtc(ref exception, path, creationTimeUtc));
        }

        /// <inheritdoc />
        public void SetCurrentDirectory(string path)
        {
            this.EncapsulateWithExtension(
                () => System.IO.Directory.SetCurrentDirectory(path),
                e => e.BeginSetCurrentDirectory(path),
                e => e.EndSetCurrentDirectory(path),
                (IDirectoryExtension e, ref Exception exception) => e.FailSetCurrentDirectory(ref exception, path));
        }

        /// <inheritdoc />
        public void SetLastAccessTime(string path, DateTime lastAccessTime)
        {
            this.EncapsulateWithExtension(
                () => System.IO.Directory.SetLastAccessTime(path, lastAccessTime),
                e => e.BeginSetLastAccessTime(path, lastAccessTime),
                e => e.EndSetLastAccessTime(path, lastAccessTime),
                (IDirectoryExtension e, ref Exception exception) => e.FailSetLastAccessTime(ref exception, path, lastAccessTime));
        }

        /// <inheritdoc />
        public void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc)
        {
            this.EncapsulateWithExtension(
                () => System.IO.Directory.SetLastAccessTimeUtc(path, lastAccessTimeUtc),
                e => e.BeginSetLastAccessTimeUtc(path, lastAccessTimeUtc),
                e => e.EndSetLastAccessTimeUtc(path, lastAccessTimeUtc),
                (IDirectoryExtension e, ref Exception exception) => e.FailSetLastAccessTimeUtc(ref exception, path, lastAccessTimeUtc));
        }

        /// <inheritdoc />
        public void SetLastWriteTime(string path, DateTime lastWriteTime)
        {
            this.EncapsulateWithExtension(
                () => System.IO.Directory.SetLastWriteTime(path, lastWriteTime),
                e => e.BeginSetLastWriteTime(path, lastWriteTime),
                e => e.EndSetLastWriteTime(path, lastWriteTime),
                (IDirectoryExtension e, ref Exception exception) => e.FailSetLastWriteTime(ref exception, path, lastWriteTime));
        }

        /// <inheritdoc />
        public void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc)
        {
            this.EncapsulateWithExtension(
                () => System.IO.Directory.SetLastWriteTimeUtc(path, lastWriteTimeUtc),
                e => e.BeginSetLastWriteTimeUtc(path, lastWriteTimeUtc),
                e => e.EndSetLastWriteTimeUtc(path, lastWriteTimeUtc),
                (IDirectoryExtension e, ref Exception exception) => e.FailSetLastWriteTimeUtc(ref exception, path, lastWriteTimeUtc));
        }
    }
}