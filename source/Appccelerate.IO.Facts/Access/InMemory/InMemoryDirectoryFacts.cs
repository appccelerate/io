// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InMemoryDirectoryFacts.cs" company="Appccelerate">
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
    using System.IO;

    using FluentAssertions;

    using Xunit;

    public class InMemoryDirectoryFacts
    {
        private static readonly byte[] Data = { 1, 2, 3 };

        private readonly InMemoryDirectory testee;

        private readonly IInMemoryFileSystem fileSystem;

        public InMemoryDirectoryFacts()
        {
            this.fileSystem = new InMemoryFileSystem();
            this.testee = new InMemoryDirectory(this.fileSystem);
        }

        [Fact]
        public void GetsFilesWithSearchPattern()
        {
            this.fileSystem.CreateDirectory(@"c:\other");
            this.fileSystem.CreateDirectory(@"c:\top");
            this.fileSystem.CreateDirectory(@"c:\top\folder");
            this.fileSystem.CreateDirectory(@"c:\top\folder\sub");
            this.fileSystem.CreateDirectory(@"c:\top\folder\othersub\sub");

            this.fileSystem.AddFile(@"c:\other\a.txt", Data);                      // not in search directory
            this.fileSystem.AddFile(@"c:\top\folder\1.pdf", Data);                 // not a pattern match
            this.fileSystem.AddFile(@"c:\top\folder\2.txt", Data);                 // MATCH
            this.fileSystem.AddFile(@"c:\top\folder\3.xtxt", Data);                // not a pattern match
            this.fileSystem.AddFile(@"c:\top\folder\4.txtx", Data);                // not a pattern match
            this.fileSystem.AddFile(@"c:\top\folder\5.txt", Data);                 // MATCH
            this.fileSystem.AddFile(@"c:\top\folder\sub\x.pdf", Data);             // not a pattern match
            this.fileSystem.AddFile(@"c:\top\folder\sub\y.txt", Data);             // not in search directory (sub directory)
            this.fileSystem.AddFile(@"c:\top\folder\othersub\sub\u.txt", Data);    // not in search directory (sub directory)
            this.fileSystem.AddFile(@"c:\top\folder\othersub\sub\v.txt", Data);    // not in search directory (sub directory)
            this.fileSystem.AddFile(@"c:\top\folder\othersub\sub\w.pdf", Data);    // not a pattern match

            IEnumerable<string> result = this.testee.GetFiles(@"c:\top\folder\", "*.txt", SearchOption.TopDirectoryOnly);

            result.Should().BeEquivalentTo(
                @"c:\top\folder\2.txt",
                @"c:\top\folder\5.txt");
        }

        [Fact]
        public void GetsFilesRecursivelyWithSearchPattern()
        {
            this.fileSystem.CreateDirectory(@"c:\other");
            this.fileSystem.CreateDirectory(@"c:\top");
            this.fileSystem.CreateDirectory(@"c:\top\folder");
            this.fileSystem.CreateDirectory(@"c:\top\folder\sub");
            this.fileSystem.CreateDirectory(@"c:\top\folder\othersub\sub");

            this.fileSystem.AddFile(@"c:\other\a.txt", Data);                      // not in search directory
            this.fileSystem.AddFile(@"c:\top\folder\1.pdf", Data);                 // not a pattern match
            this.fileSystem.AddFile(@"c:\top\folder\2.txt", Data);                 // MATCH
            this.fileSystem.AddFile(@"c:\top\folder\3.xtxt", Data);                // not a pattern match
            this.fileSystem.AddFile(@"c:\top\folder\4.txtx", Data);                // not a pattern match
            this.fileSystem.AddFile(@"c:\top\folder\5.txt", Data);                 // MATCH
            this.fileSystem.AddFile(@"c:\top\folder\sub\x.pdf", Data);             // not a pattern match
            this.fileSystem.AddFile(@"c:\top\folder\sub\y.txt", Data);             // MATCH
            this.fileSystem.AddFile(@"c:\top\folder\othersub\sub\u.txt", Data);    // MATCH
            this.fileSystem.AddFile(@"c:\top\folder\othersub\sub\v.txt", Data);    // MATCH
            this.fileSystem.AddFile(@"c:\top\folder\othersub\sub\w.pdf", Data);    // not a pattern match
            
            IEnumerable<string> result = this.testee.GetFiles(@"c:\top\folder\", "*.txt", SearchOption.AllDirectories);

            result.Should().BeEquivalentTo(
                @"c:\top\folder\2.txt",
                @"c:\top\folder\5.txt",
                @"c:\top\folder\sub\y.txt",
                @"c:\top\folder\othersub\sub\u.txt",
                @"c:\top\folder\othersub\sub\v.txt");
        }
    }
}