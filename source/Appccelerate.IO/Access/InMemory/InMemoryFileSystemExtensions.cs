// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InMemoryFileSystemExtensions.cs" company="Appccelerate">
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
    using System.IO;

    public static class InMemoryFileSystemExtensions
    {
        public static void EnsureParentDirectoryExists(this IInMemoryFileSystem fileSystem, string absoluteFilePath)
        {
            if (!fileSystem.DirectoryExists(Path.GetDirectoryName(absoluteFilePath)))
            {
                throw new DirectoryNotFoundException(string.Format("Could not find a part of the path '{0}'.", absoluteFilePath));
            }
        }
    }
}