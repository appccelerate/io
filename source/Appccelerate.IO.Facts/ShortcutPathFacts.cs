//-------------------------------------------------------------------------------
// <copyright file="ShortcutPathFacts.cs" company="Appccelerate">
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

namespace Appccelerate.IO
{
    using System;
    using FluentAssertions;
    using Xunit;
    using Xunit.Extensions;

    public class ShortcutPathFacts
    {
        private const string Path = ShortcutPath.ShortcutCharacter + "Shortcut" + ShortcutPath.ShortcutCharacter + @"\file.extension";

        [Fact]
        public void RepresentsAShortcutPath()
        {
            var shortcutPath = new ShortcutPath(Path);

            shortcutPath.Value.Should().Be(Path);
        }

        [Fact]
        public void IsAssignableFromString()
        {
            ShortcutPath shortcutPath = Path;

            shortcutPath.Value.Should().Be(Path);
        }

        [Fact]
        public void IsAssignableToString()
        {
            ShortcutPath shortcutPath = new ShortcutPath(Path);

            string path = shortcutPath;

            path.Should().Be(Path);
        }

        [Fact]
        public void AcceptsPathWithoutShortcut()
        {
            const string PathWithoutShortcut = @"C:\path\without\shortcut";

            ShortcutPath shortcutPath = PathWithoutShortcut;

            shortcutPath.Value.Should().Be(PathWithoutShortcut);
        }

        [Fact]
        public void ThrowsException_WhenInvalidShortcutPathIsAssigned()
        {
            const string InvalidShortcutPath = ShortcutPath.ShortcutCharacter + "ShortcutT" + ShortcutPath.ShortcutCharacter + "ff" + ShortcutPath.ShortcutCharacter + @"\file.ext";
            Action action = () => new ShortcutPath(InvalidShortcutPath);

            action.ShouldThrow<ArgumentException>();
        }

        [Theory]
        [InlineData("c:\\folder\\file.extension", false)]
        [InlineData("%Shortcut%", false)]
        [InlineData("%Shortcut%\\%AnotherShortcut%", false)]
        [InlineData("%Short%cut%", true)]
        [InlineData("%Shortcut", true)]
        [InlineData("Shortcut%\\folder", true)]
        public void ReturnsWhetherpathContainsInvalidShortcut(string path, bool expected)
        {
            bool result = ShortcutPath.ContainsInvalidShortcut(path);

            result.Should().Be(expected);
        }
    }
}