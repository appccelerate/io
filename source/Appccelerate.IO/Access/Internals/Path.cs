//-------------------------------------------------------------------------------
// <copyright file="Path.cs" company="Appccelerate">
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
    using System.Linq;

    /// <summary>
    /// Wrapper class which simplifies access to paths.
    /// </summary>
    public class Path : IPath, IExtensionProvider<IPathExtension>
    {
        private readonly List<IPathExtension> extensions;

        /// <summary>
        /// Initializes a new instance of the <see cref="Path"/> class.
        /// </summary>
        /// <param name="extensions">The extensions.</param>
        public Path(IEnumerable<IPathExtension> extensions)
        {
            this.extensions = extensions.ToList();
        }

        /// <inheritdoc />
        public IEnumerable<IPathExtension> Extensions
        {
            get
            {
                return this.extensions;
            }
        }

        /// <inheritdoc />
        public string GetDirectoryName(string path)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.Path.GetDirectoryName(path),
                e => e.BeginGetDirectoryName(path),
                (e, r) => e.EndGetDirectoryName(r, path),
                (IPathExtension e, ref Exception exception) => e.FailGetDirectoryName(ref exception, path));
        }

        /// <inheritdoc />
        public string GetFileName(string path)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.Path.GetFileName(path),
                e => e.BeginGetFileName(path),
                (e, r) => e.EndGetFileName(r, path),
                (IPathExtension e, ref Exception exception) => e.FailGetFileName(ref exception, path));
        }

        /// <inheritdoc />
        public string GetFileNameWithoutExtension(string path)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.Path.GetFileNameWithoutExtension(path),
                e => e.BeginGetFileNameWithoutExtension(path),
                (e, r) => e.EndGetFileNameWithoutExtension(r, path),
                (IPathExtension e, ref Exception exception) => e.FailGetFileNameWithoutExtension(ref exception, path));
        }

        /// <inheritdoc />
        public string Combine(string path1, string path2)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.Path.Combine(path1, path2),
                e => e.BeginCombine(path1, path2),
                (e, r) => e.EndCombine(r, path1, path2),
                (IPathExtension e, ref Exception exception) => e.FailCombine(ref exception, path1, path2));
        }

        /// <inheritdoc />
        public string GetRandomFileName()
        {
            return this.EncapsulateWithExtension(
                () => System.IO.Path.GetRandomFileName(),
                e => e.BeginGetRandomFileName(),
                (e, r) => e.EndGetRandomFileName(r),
                (IPathExtension e, ref Exception exception) => e.FailGetRandomFileName(ref exception));
        }

        /// <inheritdoc />
        public string ChangeExtension(string path, string extension)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.Path.ChangeExtension(path, extension),
                e => e.BeginChangeExtension(path, extension),
                (e, r) => e.EndChangeExtension(r, path, extension),
                (IPathExtension e, ref Exception exception) => e.FailChangeExtension(ref exception, path, extension));
        }

        /// <inheritdoc />
        public string GetExtension(string path)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.Path.GetExtension(path),
                e => e.BeginGetExtension(path),
                (e, r) => e.EndGetExtension(r, path),
                (IPathExtension e, ref Exception exception) => e.FailGetExtension(ref exception, path));
        }

        /// <inheritdoc />
        public string GetFullPath(string path)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.Path.GetFullPath(path),
                e => e.BeginGetFullPath(path),
                (e, r) => e.EndGetFullPath(r, path),
                (IPathExtension e, ref Exception exception) => e.FailGetFullPath(ref exception, path));
        }

        /// <inheritdoc />
        public IEnumerable<char> GetInvalidFileNameChars()
        {
            return this.EncapsulateWithExtension(
                () => System.IO.Path.GetInvalidFileNameChars(),
                e => e.BeginGetInvalidFileNameChars(),
                (e, r) => e.EndGetInvalidFileNameChars(r),
                (IPathExtension e, ref Exception exception) => e.FailGetInvalidFileNameChars(ref exception));
        }

        /// <inheritdoc />
        public IEnumerable<char> GetInvalidPathChars()
        {
            return this.EncapsulateWithExtension(
                () => System.IO.Path.GetInvalidPathChars(),
                e => e.BeginGetInvalidPathChars(),
                (e, r) => e.EndGetInvalidPathChars(r),
                (IPathExtension e, ref Exception exception) => e.FailGetInvalidPathChars(ref exception));
        }

        /// <inheritdoc />
        public string GetPathRoot(string path)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.Path.GetPathRoot(path),
                e => e.BeginGetPathRoot(path),
                (e, r) => e.EndGetPathRoot(r, path),
                (IPathExtension e, ref Exception exception) => e.FailGetPathRoot(ref exception, path));
        }

        /// <inheritdoc />
        public string GetTempFileName()
        {
            return this.EncapsulateWithExtension(
                () => System.IO.Path.GetTempFileName(),
                e => e.BeginGetTempFileName(),
                (e, r) => e.EndGetTempFileName(r),
                (IPathExtension e, ref Exception exception) => e.FailGetTempFileName(ref exception));
        }

        /// <inheritdoc />
        public string GetTempPath()
        {
            return this.EncapsulateWithExtension(
                () => System.IO.Path.GetTempPath(),
                e => e.BeginGetTempPath(),
                (e, r) => e.EndGetTempPath(r),
                (IPathExtension e, ref Exception exception) => e.FailGetTempPath(ref exception));
        }

        /// <inheritdoc />
        public bool HasExtension(string path)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.Path.HasExtension(path),
                e => e.BeginHasExtension(path),
                (e, r) => e.EndHasExtension(r, path),
                (IPathExtension e, ref Exception exception) => e.FailHasExtension(ref exception, path));
        }

        /// <inheritdoc />
        public bool IsPathRooted(string path)
        {
            return this.EncapsulateWithExtension(
                () => System.IO.Path.IsPathRooted(path),
                e => e.BeginIsPathRooted(path),
                (e, r) => e.EndIsPathRooted(r, path),
                (IPathExtension e, ref Exception exception) => e.FailIsPathRooted(ref exception, path));
        }
    }
}