//-------------------------------------------------------------------------------
// <copyright file="ShortcutPath.cs" company="Appccelerate">
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
    using System.Linq;

    public class ShortcutPath
    {
        public const string ShortcutCharacter = "%";

        public ShortcutPath(string shortcutPath)
        {
            if (ContainsInvalidShortcut(shortcutPath))
            {
                throw new ArgumentException("Expected path with shortcuts, but path contains incomplete shortcut `" + shortcutPath + "`.");
            }

            this.Value = shortcutPath;
        }

        public string Value { get; private set; }

        public static implicit operator ShortcutPath(string shortcutPath)
        {
            return new ShortcutPath(shortcutPath);
        }

        public static implicit operator string(ShortcutPath shortcutPath)
        {
            return shortcutPath != null ? shortcutPath.Value : null;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return this.Equals((ShortcutPath)obj);
        }

        public override int GetHashCode()
        {
            return this.Value != null ? this.Value.GetHashCode() : 0;
        }

        public override string ToString()
        {
            return this.Value;
        }

        protected bool Equals(ShortcutPath other)
        {
            return string.Equals(this.Value, other.Value);
        }

        public static bool operator ==(ShortcutPath a, ShortcutPath b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            // ReSharper disable RedundantCast.0 because otherwise it results in recursion.
            if (((object)a == null) || ((object)b == null))
            // ReSharper restore RedundantCast.0
            {
                return false;
            }

            return a.Value == b.Value;
        }

        public static bool operator !=(ShortcutPath a, ShortcutPath b)
        {
            return !(a == b);
        }

        private static bool ContainsInvalidShortcut(string shortcutPath)
        {
            return shortcutPath.Count(c => c == ShortcutCharacter.Single()) % 2 == 1;
        }
    }
}