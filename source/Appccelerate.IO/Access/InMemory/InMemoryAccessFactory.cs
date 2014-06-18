// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InMemoryAccessFactory.cs" company="Appccelerate">
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
// --------------------------------------------------------------------------------------------------------------------

namespace Appccelerate.IO.Access.InMemory
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class InMemoryAccessFactory : IAccessFactory
    {
        public InMemoryAccessFactory()
        {
            this.FileSystem = new InMemoryFileSystem();
        }

        public IInMemoryFileSystem FileSystem { get; private set; }

        public IDirectory CreateDirectory()
        {
            return new InMemoryDirectory(this.FileSystem);
        }

        public IFile CreateFile()
        {
            return new InMemoryFile(this.FileSystem);
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
            throw new NotImplementedException();
        }

        public IDirectoryInfo CreateDirectoryInfo(DirectoryInfo directoryInfo)
        {
            throw new NotImplementedException();
        }

        public IDirectoryInfo CreateDirectoryInfo(string pathToDirectory)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void RegisterDirectoryExtensionsProvider(Func<IEnumerable<IDirectoryExtension>> extensionsProvider)
        {
            throw new NotImplementedException();
        }

        public void RegisterDriveExtensionsProvider(Func<IEnumerable<IDriveExtension>> extensionsProvider)
        {
            throw new NotImplementedException();
        }

        public void RegisterPathExtensionsProvider(Func<IEnumerable<IPathExtension>> extensionsProvider)
        {
            throw new NotImplementedException();
        }

        public void RegisterEnvironmentExtensionsProvider(Func<IEnumerable<IEnvironmentExtension>> extensionsProvider)
        {
            throw new NotImplementedException();
        }
    }
}