// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IInMemoryFileSystem.cs" company="Appccelerate">
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
    using System.Collections.Generic;

    public interface IInMemoryFileSystem
    {
        void AddFile(AbsoluteFilePath absoluteFilePath, IEnumerable<byte> fileContent);

        IEnumerable<byte> GetFile(AbsoluteFilePath absoluteFilePath);

        FileProperties GetFileProperties(AbsoluteFilePath absoluteFilePath);

        void DeleteFile(AbsoluteFilePath absoluteFilePath);

        void DeleteDirectory(AbsoluteFolderPath absoluteFolderPath);

        IEnumerable<AbsoluteFolderPath> GetSubdirectoriesOf(AbsoluteFolderPath absoluteFolderPath);

        IEnumerable<AbsoluteFolderPath> GetSubdirectoriesOfRecursive(AbsoluteFolderPath absoluteFolderPath);
        
        IEnumerable<AbsoluteFilePath> GetFilesOf(AbsoluteFolderPath absoluteFolderPath);

        IEnumerable<AbsoluteFilePath> GetFilesOfRecursive(AbsoluteFolderPath absoluteFolderPath);

        bool FileExists(AbsoluteFilePath absoluteFilePath);

        bool DirectoryExists(AbsoluteFolderPath absoluteFolderPath);

        void CreateDirectory(AbsoluteFolderPath absoluteFolderPath);

        string DumpFileSystem();

        void Move(AbsoluteFilePath absoluteSourceFilePath, AbsoluteFilePath absoluteDestinationFilePath);
    }
}