//-------------------------------------------------------------------------------
// <copyright file="IFileExtension.cs" company="Appccelerate">
//   Copyright (c) 2008-2014
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
namespace Appccelerate.IO.Access
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Security.AccessControl;
    using System.Text;
    /// <summary>
    /// Interface for file access extensions
    /// </summary>
    [CompilerGenerated]
    public interface IFileExtension
    {
        void BeginGetLastWriteTime(string path);

        void EndGetLastWriteTime(DateTime result, string path);

        void FailGetLastWriteTime(ref Exception exception, string path);

        void BeginGetLastWriteTimeUtc(string path);

        void EndGetLastWriteTimeUtc(DateTime result, string path);

        void FailGetLastWriteTimeUtc(ref Exception exception, string path);

        void BeginMove(string sourceFileName, string destFileName);

        void EndMove(string sourceFileName, string destFileName);

        void FailMove(ref Exception exception, string sourceFileName, string destFileName);

        void BeginOpenRead(string path);

        void EndOpenRead(Stream result, string path);

        void FailOpenRead(ref Exception exception, string path);

        void BeginOpenText(string path);

        void EndOpenText(StreamReader result, string path);

        void FailOpenText(ref Exception exception, string path);

        void BeginOpenWrite(string path);

        void EndOpenWrite(FileStream result, string path);

        void FailOpenWrite(ref Exception exception, string path);

        void BeginReplace(string sourceFileName, string destinationFileName, string destinationBackupFileName);

        void EndReplace(string sourceFileName, string destinationFileName, string destinationBackupFileName);

        void FailReplace(ref Exception exception, string sourceFileName, string destinationFileName, string destinationBackupFileName);

        void BeginReplace(string sourceFileName, string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors);

        void EndReplace(string sourceFileName, string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors);

        void FailReplace(ref Exception exception, string sourceFileName, string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors);

        void BeginSetAccessControl(string path, FileSecurity fileSecurity);

        void EndSetAccessControl(string path, FileSecurity fileSecurity);

        void FailSetAccessControl(ref Exception exception, string path, FileSecurity fileSecurity);

        void BeginSetCreationTime(string path, DateTime creationTime);

        void EndSetCreationTime(string path, DateTime creationTime);

        void FailSetCreationTime(ref Exception exception, string path, DateTime creationTime);

        void BeginSetCreationTimeUtc(string path, DateTime creationTimeUtc);

        void EndSetCreationTimeUtc(string path, DateTime creationTimeUtc);

        void FailSetCreationTimeUtc(ref Exception exception, string path, DateTime creationTimeUtc);

        void BeginSetLastAccessTime(string path, DateTime lastAccessTime);

        void EndSetLastAccessTime(string path, DateTime lastAccessTime);

        void FailSetLastAccessTime(ref Exception exception, string path, DateTime lastAccessTime);

        void BeginSetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc);

        void EndSetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc);

        void FailSetLastAccessTimeUtc(ref Exception exception, string path, DateTime lastAccessTimeUtc);

        void BeginSetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc);

        void EndSetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc);

        void FailSetLastWriteTimeUtc(ref Exception exception, string path, DateTime lastWriteTimeUtc);

        void BeginDelete(string path);

        void EndDelete(string path);

        void FailDelete(ref Exception exception, string path);

        void BeginCopy(string sourceFileName, string destFileName);

        void EndCopy(string sourceFileName, string destFileName);

        void FailCopy(ref Exception exception, string sourceFileName, string destFileName);

        void BeginCopy(string sourceFileName, string destFileName, bool overwrite);

        void EndCopy(string sourceFileName, string destFileName, bool overwrite);

        void FailCopy(ref Exception exception, string sourceFileName, string destFileName, bool overwrite);

        void BeginCreateText(string path);

        void EndCreateText(StreamWriter result, string path);

        void FailCreateText(ref Exception exception, string path);

        void BeginGetAttributes(string path);

        void EndGetAttributes(FileAttributes result, string path);

        void FailGetAttributes(ref Exception exception, string path);

        void BeginSetLastWriteTime(string path, DateTime lastWriteTime);

        void EndSetLastWriteTime(string path, DateTime lastWriteTime);

        void FailSetLastWriteTime(ref Exception exception, string path, DateTime lastWriteTime);

        void BeginSetAttributes(string path, FileAttributes fileAttributes);

        void EndSetAttributes(string path, FileAttributes fileAttributes);

        void FailSetAttributes(ref Exception exception, string path, FileAttributes fileAttributes);

        void BeginExists(string path);

        void EndExists(bool result, string path);

        void FailExists(ref Exception exception, string path);

        void BeginReadAllBytes(string path);

        void EndReadAllBytes(byte[] result, string path);

        void FailReadAllBytes(ref Exception exception, string path);

        void BeginReadAllLines(string path, Encoding encoding);

        void EndReadAllLines(string[] result, string path, Encoding encoding);

        void FailReadAllLines(ref Exception exception, string path, Encoding encoding);

        void BeginReadAllLines(string path);

        void EndReadAllLines(string[] result, string path);

        void FailReadAllLines(ref Exception exception, string path);

        void BeginReadAllText(string path, Encoding encoding);

        void EndReadAllText(string result, string path, Encoding encoding);

        void FailReadAllText(ref Exception exception, string path, Encoding encoding);

        void BeginReadAllText(string path);

        void EndReadAllText(string result, string path);

        void FailReadAllText(ref Exception exception, string path);

        void BeginReadLines(string path);

        void EndReadLines(IEnumerable<string> result, string path);

        void FailReadLines(ref Exception exception, string path);

        void BeginReadLines(string path, Encoding encoding);

        void EndReadLines(IEnumerable<string> result, string path, Encoding encoding);

        void FailReadLines(ref Exception exception, string path, Encoding encoding);

        void BeginWriteAllLines(string path, IEnumerable<string> contents, Encoding encoding);

        void EndWriteAllLines(string path, IEnumerable<string> contents, Encoding encoding);

        void FailWriteAllLines(ref Exception exception, string path, IEnumerable<string> contents, Encoding encoding);

        void BeginWriteAllLines(string path, IEnumerable<string> contents);

        void EndWriteAllLines(string path, IEnumerable<string> contents);

        void FailWriteAllLines(ref Exception exception, string path, IEnumerable<string> contents);

        void BeginWriteAllText(string path, string contents);

        void EndWriteAllText(string path, string contents);

        void FailWriteAllText(ref Exception exception, string path, string contents);

        void BeginWriteAllText(string path, string contents, Encoding encoding);

        void EndWriteAllText(string path, string contents, Encoding encoding);

        void FailWriteAllText(ref Exception exception, string path, string contents, Encoding encoding);

        void BeginWriteAllBytes(string path, byte[] bytes);

        void EndWriteAllBytes(string path, byte[] bytes);

        void FailWriteAllBytes(ref Exception exception, string path, byte[] bytes);

        void BeginOpen(string path, FileMode mode);

        void EndOpen(FileStream result, string path, FileMode mode);

        void FailOpen(ref Exception exception, string path, FileMode mode);

        void BeginOpen(string path, FileMode mode, FileAccess access);

        void EndOpen(FileStream result, string path, FileMode mode, FileAccess access);

        void FailOpen(ref Exception exception, string path, FileMode mode, FileAccess access);

        void BeginOpen(string path, FileMode mode, FileAccess access, FileShare share);

        void EndOpen(FileStream result, string path, FileMode mode, FileAccess access, FileShare share);

        void FailOpen(ref Exception exception, string path, FileMode mode, FileAccess access, FileShare share);

        void BeginAppendAllLines(string path, IEnumerable<string> contents);

        void EndAppendAllLines(string path, IEnumerable<string> contents);

        void FailAppendAllLines(ref Exception exception, string path, IEnumerable<string> contents);

        void BeginAppendAllLines(string path, IEnumerable<string> contents, Encoding encoding);

        void EndAppendAllLines(string path, IEnumerable<string> contents, Encoding encoding);

        void FailAppendAllLines(ref Exception exception, string path, IEnumerable<string> contents, Encoding encoding);

        void BeginAppendAllText(string path, string contents);

        void EndAppendAllText(string path, string contents);

        void FailAppendAllText(ref Exception exception, string path, string contents);

        void BeginAppendAllText(string path, string contents, Encoding encoding);

        void EndAppendAllText(string path, string contents, Encoding encoding);

        void FailAppendAllText(ref Exception exception, string path, string contents, Encoding encoding);

        void BeginAppendText(string path);

        void EndAppendText(StreamWriter result, string path);

        void FailAppendText(ref Exception exception, string path);

        void BeginCreate(string path);

        void EndCreate(FileStream result, string path);

        void FailCreate(ref Exception exception, string path);

        void BeginCreate(string path, int bufferSize);

        void EndCreate(FileStream result, string path, int bufferSize);

        void FailCreate(ref Exception exception, string path, int bufferSize);

        void BeginCreate(string path, int bufferSize, FileOptions options);

        void EndCreate(FileStream result, string path, int bufferSize, FileOptions options);

        void FailCreate(ref Exception exception, string path, int bufferSize, FileOptions options);

        void BeginCreate(string path, int bufferSize, FileOptions options, FileSecurity fileSecurity);

        void EndCreate(FileStream result, string path, int bufferSize, FileOptions options, FileSecurity fileSecurity);

        void FailCreate(ref Exception exception, string path, int bufferSize, FileOptions options, FileSecurity fileSecurity);

        void BeginDecrypt(string path);

        void EndDecrypt(string path);

        void FailDecrypt(ref Exception exception, string path);

        void BeginEncrypt(string path);

        void EndEncrypt(string path);

        void FailEncrypt(ref Exception exception, string path);

        void BeginGetAccessControl(string path);

        void EndGetAccessControl(FileSecurity result, string path);

        void FailGetAccessControl(ref Exception exception, string path);

        void BeginGetAccessControl(string path, AccessControlSections includeSections);

        void EndGetAccessControl(FileSecurity result, string path, AccessControlSections includeSections);

        void FailGetAccessControl(ref Exception exception, string path, AccessControlSections includeSections);

        void BeginGetCreationTime(string path);

        void EndGetCreationTime(DateTime result, string path);

        void FailGetCreationTime(ref Exception exception, string path);

        void BeginGetCreationTimeUtc(string path);

        void EndGetCreationTimeUtc(DateTime result, string path);

        void FailGetCreationTimeUtc(ref Exception exception, string path);

        void BeginGetLastAccessTime(string path);

        void EndGetLastAccessTime(DateTime result, string path);

        void FailGetLastAccessTime(ref Exception exception, string path);

        void BeginGetLastAccessTimeUtc(string path);

        void EndGetLastAccessTimeUtc(DateTime result, string path);

        void FailGetLastAccessTimeUtc(ref Exception exception, string path);
    }
}