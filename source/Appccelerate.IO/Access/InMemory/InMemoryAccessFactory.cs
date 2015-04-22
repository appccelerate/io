// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InMemoryAccessFactory.cs" company="Appccelerate">
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

    public class InMemoryAccessFactory : IAccessFactory
    {
        private Func<IEnumerable<IFileExtension>> fileExtensionsProvider = Enumerable.Empty<IFileExtension>;

        private Func<IEnumerable<IDirectoryExtension>> directoryExtensionsProvider = Enumerable.Empty<IDirectoryExtension>;

        private Func<IEnumerable<IPathExtension>> pathExtensionsProvider = Enumerable.Empty<IPathExtension>;

        private Func<IEnumerable<IEnvironmentExtension>> environmentExtensionsProvider = Enumerable.Empty<IEnvironmentExtension>;

        private Func<IEnumerable<IDriveExtension>> driveExtensionsProvider = Enumerable.Empty<IDriveExtension>;

        public InMemoryAccessFactory() : this(new TimeDoesNotMatterDateTimeProvider())
        {
        }

        public InMemoryAccessFactory(IInMemoryDateTimeProvider dateTimeProvider)
        {
            this.FileSystem = new InMemoryFileSystem(dateTimeProvider);
        }

        public IInMemoryFileSystem FileSystem { get; private set; }

        public IDirectory CreateDirectory()
        {
            return new InMemoryDirectory(this.FileSystem, this.directoryExtensionsProvider());
        }

        public IFile CreateFile()
        {
            return new InMemoryFile(this.FileSystem, this.fileExtensionsProvider());
        }

        public IPath CreatePath()
        {
            throw new NotImplementedException();
        }

        public IEnvironment CreateEnvironment()
        {
            return new InMemoryEnvironment();
        }

        public IDrive CreateDrive()
        {
            throw new NotImplementedException();
        }

        public IFileInfo CreateFileInfo(FileInfo fileInfo)
        {
            throw new NotImplementedException();
        }

        public IFileInfo CreateFileInfo(string pathToFile)
        {
            return new InMemoryFileInfo(this.FileSystem, pathToFile);
        }

        public IDirectoryInfo CreateDirectoryInfo(DirectoryInfo directoryInfo)
        {
            throw new NotImplementedException();
        }

        public IDirectoryInfo CreateDirectoryInfo(string pathToDirectory)
        {
            return new InMemoryDirectoryInfo(this.FileSystem, pathToDirectory);
        }

        public IDriveInfo CreateDriveInfo(DriveInfo driveInfo)
        {
            throw new NotImplementedException();
        }

        public IDriveInfo CreateDriveInfo(string driveName)
        {
            throw new NotImplementedException();
        }

        public void RegisterFileExtensionsProvider(Func<IEnumerable<IFileExtension>> extensionsProvider)
        {
            this.fileExtensionsProvider = extensionsProvider;
        }

        public void RegisterDirectoryExtensionsProvider(Func<IEnumerable<IDirectoryExtension>> extensionsProvider)
        {
            this.directoryExtensionsProvider = extensionsProvider;
        }

        public void RegisterDriveExtensionsProvider(Func<IEnumerable<IDriveExtension>> extensionsProvider)
        {
            this.driveExtensionsProvider = extensionsProvider;
        }

        public void RegisterPathExtensionsProvider(Func<IEnumerable<IPathExtension>> extensionsProvider)
        {
            this.pathExtensionsProvider = extensionsProvider;
        }

        public void RegisterEnvironmentExtensionsProvider(Func<IEnumerable<IEnvironmentExtension>> extensionsProvider)
        {
            this.environmentExtensionsProvider = extensionsProvider;
        }
    }
}