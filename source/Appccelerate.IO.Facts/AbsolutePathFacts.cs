//-------------------------------------------------------------------------------
// <copyright file="AbsolutePathFacts.cs" company="Appccelerate">
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
namespace Appccelerate.IO
{
    using System;
    using FluentAssertions;
    using Xunit;
    using Xunit.Extensions;

    public class AbsolutePathFacts
    {
        private const string Path = @"c:\folder\file.extension";

        [Fact]
        public void RepresentsAnAbsolutePath()
        {
            var absolutePath = new AbsolutePath(Path);

            absolutePath.Value.Should().Be(Path);
        }

        [Fact]
        public void ReturnsAbsoluteFilePath()
        {
            var absolutePath = new AbsolutePath(Path);

            absolutePath.AsAbsoluteFilePath.Value.Should().Be(Path);
        }

        [Fact]
        public void ReturnsAbsoluteFolderPath()
        {
            var absolutePath = new AbsolutePath(Path);

            absolutePath.AsAbsoluteFolderPath.Value.Should().Be(Path);
        }

        [Fact]
        public void IsAssignableFromString()
        {
            AbsolutePath absolutePath = Path;

            absolutePath.Value.Should().Be(Path);
        }

        [Fact]
        public void IsAssignableToString()
        {
            AbsolutePath absolutePath = new AbsolutePath(Path);

            string path = absolutePath;

            path.Should().Be(Path);
        }

        [Fact]
        public void ThrowsException_WhenPathIsNotAbsolute()
        {
            Action action = () => new AbsolutePath("..\folder\file.ext");

            action.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData(@"c:\folder\file.ext", @"c:\folder\file.ext", true)]
        [InlineData(@"c:\folder\", @"c:\folder\", true)]
        [InlineData(@"c:\folder\file.ext", @"c:\folder\other.ext", false)]
        [InlineData(@"c:\folder\file.ext", @"c:\other\file.ext", false)]
        public void SupportsEqualityOperator(string aa, string bb, bool expected)
        {
            AbsolutePath a = aa;
            AbsolutePath b = bb;

            bool result = a == b;

            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(@"c:\folder\file.ext", @"c:\folder\file.ext", false)]
        [InlineData(@"c:\folder\", @"c:\other", true)]
        [InlineData(@"c:\folder\file.ext", @"c:\folder\other.ext", true)]
        [InlineData(@"c:\folder\file.ext", @"c:\other\file.ext", true)]
        public void SupportsInequalityOperator(string aa, string bb, bool expected)
        {
            AbsolutePath a = aa;
            AbsolutePath b = bb;

            bool result = a != b;

            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(@"c:\folder\file.ext", @"c:\folder\file.ext", true)]
        [InlineData(@"c:\folder\", @"c:\folder\", true)]
        [InlineData(@"c:\folder\file.ext", @"c:\folder\other.ext", false)]
        [InlineData(@"c:\folder\file.ext", @"c:\other\file.ext", false)]
        public void SupportsEquals(string aa, string bb, bool expected)
        {
            AbsolutePath a = aa;
            AbsolutePath b = bb;

            bool result = a.Equals(b);

            result.Should().Be(expected);
        }

        [Fact]
        public void ReturnsSameHashCodeForEqualObjects()
        {
            AbsolutePath a = @"c:\folder\file.ext";
            AbsolutePath b = @"c:\folder\file.ext";

            var hashcodeA = a.GetHashCode();
            var hashcodeB = b.GetHashCode();

            hashcodeA.Should().Be(hashcodeB);
        }

        [Fact]
        public void ReturnsDifferentHashCodeForDifferentObjects()
        {
            AbsolutePath a = @"c:\folder\file.ext";
            AbsolutePath b = @"c:\folder\other.ext";

            var hashcodeA = a.GetHashCode();
            var hashcodeB = b.GetHashCode();

            hashcodeA.Should().NotBe(hashcodeB);
        }

        [Fact]
        public void ReturnsTrue_WhenComparingEquivalentPaths()
        {
            AbsolutePath a = "c:\\folder\\file.ext";
            AbsolutePath b = "c:\\folder\\file.ext";

            bool result = a == b;

            result.Should().BeTrue();
        }

        [Fact]
        public void ReturnsFalse_WhenComparingInequivalentPaths()
        {
            AbsolutePath a = "c:\\folder\\file.ext";
            AbsolutePath b = "c:\\folder\\other.ext";

            bool result = a == b;

            result.Should().BeFalse();
        }

        [Fact]
        public void ReturnsFalse_WhenComparingEquivalentPathsForInequality()
        {
            AbsolutePath a = "c:\\folder\\file.ext";
            AbsolutePath b = "c:\\folder\\file.ext";

            bool result = a != b;

            result.Should().BeFalse();
        }

        [Fact]
        public void ReturnsTrue_WhenComparingInequivalentPathsForInequality()
        {
            AbsolutePath a = "c:\\folder\\file.ext";
            AbsolutePath b = "c:\\folder\\other.ext";

            bool result = a != b;

            result.Should().BeTrue();
        }

        [Fact]
        public void ReturnsTrue_WhenCheckingEqualityForEquivalentPaths()
        {
            AbsolutePath a = "c:\\folder\\file.ext";
            AbsolutePath b = "c:\\folder\\file.ext";

            bool result = a.Equals(b);

            result.Should().BeTrue();
        }

        [Fact]
        public void ReturnsFalse_WhenCheckingEqualityForIneequivalentPaths()
        {
            AbsolutePath a = "c:\\folder\\file.ext";
            AbsolutePath b = "c:\\folder\\other.ext";

            bool result = a.Equals(b);

            result.Should().BeFalse();
        }

        [Theory]
        [InlineData(@"c:\folder\file.ext", true)]
        [InlineData(@"c:\folder\", true)]
        [InlineData(@".\folder\", false)]
        [InlineData(@"blah", false)]
        public void ReturnsWhetherAPathIsAnAbsolutePath(string path, bool expected)
        {
            bool result = AbsolutePath.IsAbsolutePath(path);

            result.Should().Be(expected);
        }
    }
}