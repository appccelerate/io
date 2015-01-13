// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringExtensionMethods.cs" company="Appccelerate">
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

    public static class StringExtensionMethods
    {
        public static bool EqualsIgnoringCaseCultureInvariant(this string comparable, string compareWith)
        {
            return comparable.Equals(compareWith, StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool IsLike(this string inputString, string pattern)
        {
            var regexPattern = System.Text.RegularExpressions.Regex.Escape(pattern);
            regexPattern = regexPattern.Replace(@"\*", @".*");
            regexPattern = regexPattern.Replace(@"\?", @".?");
            regexPattern = "^" + regexPattern + @"\z";

            if (System.Text.RegularExpressions.Regex.Match(inputString.Trim().ToLower(), regexPattern.Trim().ToLower()).Success)
            {
                return true;
            }

            return false;
        }
    }
}