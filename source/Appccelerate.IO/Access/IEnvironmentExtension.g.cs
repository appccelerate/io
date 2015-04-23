//-------------------------------------------------------------------------------
// <copyright file="IEnvironmentExtension.cs" company="Appccelerate">
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

namespace Appccelerate.IO.Access
{
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;

    [CompilerGenerated]
    public interface IEnvironmentExtension
    {
        void BeginExit(int exitCode);
        void EndExit(int exitCode);
        void FailExit(ref Exception exception, int exitCode);

        void BeginExpandEnvironmentVariables(string name);
        void EndExpandEnvironmentVariables(string result, string name);
        void FailExpandEnvironmentVariables(ref Exception exception, string name);

        void BeginFailFast(string message, Exception exception);
        void EndFailFast(string message, Exception exception);
        void FailFailFast(ref Exception thrownException, string message, Exception exception);

        void BeginFailFast(string message);
        void EndFailFast(string message);
        void FailFailFast(ref Exception exception, string message);

        void BeginGetCommandLineArgs();
        void EndGetCommandLineArgs(string[] result);
        void FailGetCommandLineArgs(ref Exception exception);

        void BeginGetEnvironmentVariable(string variable, EnvironmentVariableTarget target);
        void EndGetEnvironmentVariable(string result, string variable, EnvironmentVariableTarget target);
        void FailGetEnvironmentVariable(ref Exception exception, string variable, EnvironmentVariableTarget target);

        void BeginGetEnvironmentVariable(string variable);
        void EndGetEnvironmentVariable(string result, string variable);
        void FailGetEnvironmentVariable(ref Exception exception, string variable);

        void BeginGetEnvironmentVariables(EnvironmentVariableTarget target);
        void EndGetEnvironmentVariables(IDictionary result, EnvironmentVariableTarget target);
        void FailGetEnvironmentVariables(ref Exception exception, EnvironmentVariableTarget target);

        void BeginGetEnvironmentVariables();
        void EndGetEnvironmentVariables(IDictionary result);
        void FailGetEnvironmentVariables(ref Exception exception);

        void BeginGetFolderPath(Environment.SpecialFolder folder);
        void EndGetFolderPath(string result, Environment.SpecialFolder folder);
        void FailGetFolderPath(ref Exception exception, Environment.SpecialFolder folder);

        void BeginGetFolderPath(Environment.SpecialFolder folder, Environment.SpecialFolderOption option);
        void EndGetFolderPath(string result, Environment.SpecialFolder folder, Environment.SpecialFolderOption option);
        void FailGetFolderPath(ref Exception exception, Environment.SpecialFolder folder, Environment.SpecialFolderOption option);

        void BeginGetLogicalDrives();
        void EndGetLogicalDrives(string[] result);
        void FailGetLogicalDrives(ref Exception exception);

        void BeginSetEnvironmentVariable(string variable, string value);
        void EndSetEnvironmentVariable(string variable, string value);
        void FailSetEnvironmentVariable(ref Exception exception, string variable, string value);

        void BeginSetEnvironmentVariable(string variable, string value, EnvironmentVariableTarget target);
        void EndSetEnvironmentVariable(string variable, string value, EnvironmentVariableTarget target);
        void FailSetEnvironmentVariable(ref Exception exception, string variable, string value, EnvironmentVariableTarget target);
    }
}