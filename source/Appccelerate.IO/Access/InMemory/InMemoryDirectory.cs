// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InMemoryDirectory.cs" company="Appccelerate">
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

    public class InMemoryDirectory : IDirectory
    {
        private readonly IInMemoryFileSystem fileSystem;

        public InMemoryDirectory(IInMemoryFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public bool Exists(string path)
        {
            return this.fileSystem.DirectoryExists(path);
        }

        public IEnumerable<string> GetFiles(string path)
        {
            return this.fileSystem.GetFilesOf(path).Select(x => x.Value);
        }

        public IEnumerable<string> GetFiles(string path, string searchPattern, SearchOption searchOption)
        {
            switch (searchOption)
            {
                case SearchOption.TopDirectoryOnly:
                    return this.GetFiles(path).Where(x => x.IsLike(searchPattern));

                case SearchOption.AllDirectories:
                    return this.GetFilesRecursive(path, searchPattern);

                default:
                    throw new ArgumentOutOfRangeException(searchOption + "is not a valid search option.");
            }
        }

        public IEnumerable<string> GetDirectories(string path)
        {
            return this.fileSystem.GetSubdirectoriesOf(path).Select(x => x.Value);
        }

        public IDirectoryInfo CreateDirectory(string path)
        {
            this.fileSystem.CreateDirectory(path);

            return null;
        }

        public void Delete(string path)
        {
            this.fileSystem.DeleteDirectory(path);
        }

        public void Delete(string path, bool recursive)
        {
            this.Delete(path);
        }

        public IDirectoryInfo CreateDirectory(string path, DirectorySecurity directorySecurity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetFiles(string path, string searchPattern)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetDirectories(string path, string searchPattern)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetDirectories(string path, string searchPattern, SearchOption searchOption)
        {
            throw new NotImplementedException();
        }

        public DirectorySecurity GetAccessControl(string path)
        {
            throw new NotImplementedException();
        }

        public DirectorySecurity GetAccessControl(string path, AccessControlSections includeSections)
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

        public string GetCurrentDirectory()
        {
            throw new NotImplementedException();
        }

        public string GetDirectoryRoot(string path)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetFileSystemEntries(string path)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetFileSystemEntries(string path, string searchPattern)
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

        public IEnumerable<string> GetLogicalDrives()
        {
            throw new NotImplementedException();
        }

        public IDirectoryInfo GetParent(string path)
        {
            throw new NotImplementedException();
        }

        public void Move(string sourceDirName, string destDirName)
        {
            throw new NotImplementedException();
        }

        public void SetAccessControl(string path, DirectorySecurity directorySecurity)
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

        public void SetCurrentDirectory(string path)
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

        public void SetLastWriteTime(string path, DateTime lastWriteTime)
        {
            throw new NotImplementedException();
        }

        public void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<string> GetFilesRecursive(string path, string searchPattern)
        {
            var paths = new List<string>();

            paths.AddRange(this.GetFiles(path).Where(x => x.IsLike(searchPattern)));

            var subDirectories = this.GetDirectories(path);
            foreach (string subDirectory in subDirectories)
            {
                paths.AddRange(this.GetFilesRecursive(subDirectory, searchPattern));
            }

            return paths;
        }
    }
}