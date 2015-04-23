//-------------------------------------------------------------------------------
// <copyright file="Environment.cs" company="Appccelerate">
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

namespace Appccelerate.IO.Access.Internals
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class Environment : IEnvironment, IExtensionProvider<IEnvironmentExtension>
    {
        private readonly List<IEnvironmentExtension> extensions;

        public Environment(IEnumerable<IEnvironmentExtension> extensions)
        {
            this.extensions = extensions.ToList();
        }

        public IEnumerable<IEnvironmentExtension> Extensions
        {
            get
            {
                return this.extensions;
            }
        }

        public string CommandLine
        {
            get
            {
                return System.Environment.CommandLine;
            }
        }

        public string CurrentDirectory
        {
            get
            {
                return System.Environment.CurrentDirectory;
            }

            set
            {
                System.Environment.CurrentDirectory = value;
            }
        }

        public int ExitCode
        {
            get
            {
                return System.Environment.ExitCode;
            }

            set
            {
                System.Environment.ExitCode = value;
            }
        }

        public bool HasShutdownStarted
        {
            get
            {
                return System.Environment.HasShutdownStarted;
            }
        }

        public bool Is64BitOperatingSystem
        {
            get
            {
                return System.Environment.Is64BitOperatingSystem;
            }
        }

        public bool Is64BitProcess
        {
            get
            {
                return System.Environment.Is64BitProcess;
            }
        }

        public string MachineName
        {
            get
            {
                return System.Environment.MachineName;
            }
        }

        public string NewLine
        {
            get
            {
                return System.Environment.NewLine;
            }
        }

        public OperatingSystem OSVersion
        {
            get
            {
                return System.Environment.OSVersion;
            }
        }

        public int ProcessorCount
        {
            get
            {
                return System.Environment.ProcessorCount;
            }
        }

        public string StackTrace
        {
            get
            {
                return System.Environment.StackTrace;
            }
        }

        public string SystemDirectory
        {
            get
            {
                return System.Environment.SystemDirectory;
            }
        }

        public int SystemPageSize
        {
            get
            {
                return System.Environment.SystemPageSize;
            }
        }

        public int TickCount
        {
            get
            {
                return System.Environment.TickCount;
            }
        }

        public string UserDomainName
        {
            get
            {
                return System.Environment.UserDomainName;
            }
        }

        public bool UserInteractive
        {
            get
            {
                return System.Environment.UserInteractive;
            }
        }

        public string UserName
        {
            get
            {
                return System.Environment.UserName;
            }
        }

        public Version Version
        {
            get
            {
                return System.Environment.Version;
            }
        }

        public long WorkingSet
        {
            get
            {
                return System.Environment.WorkingSet;
            }
        }

        public void Exit(int exitCode)
        {
            this.EncapsulateWithExtension(
                () => System.Environment.Exit(exitCode),
                e => e.BeginExit(exitCode),
                e => e.EndExit(exitCode),
                (IEnvironmentExtension e, ref Exception exception) => e.FailExit(ref exception, exitCode));
        }

        public string ExpandEnvironmentVariables(string name)
        {
            return this.EncapsulateWithExtension(
                () => System.Environment.ExpandEnvironmentVariables(name),
                e => e.BeginExpandEnvironmentVariables(name),
                (e, r) => e.EndExpandEnvironmentVariables(r, name),
                (IEnvironmentExtension e, ref Exception exception) => e.FailExpandEnvironmentVariables(ref exception, name));
        }

        public void FailFast(string message, Exception exception)
        {
            this.EncapsulateWithExtension(
                () => System.Environment.FailFast(message, exception),
                e => e.BeginFailFast(message, exception),
                e => e.EndFailFast(message, exception),
                (IEnvironmentExtension e, ref Exception ex) => e.FailFailFast(ref ex, message, exception));
        }

        public void FailFast(string message)
        {
            this.EncapsulateWithExtension(
                () => System.Environment.FailFast(message),
                e => e.BeginFailFast(message),
                e => e.EndFailFast(message),
                (IEnvironmentExtension e, ref Exception exception) => e.FailFailFast(ref exception, message));
        }

        public IEnumerable<string> GetCommandLineArgs()
        {
            return this.EncapsulateWithExtension(
                () => System.Environment.GetCommandLineArgs(),
                e => e.BeginGetCommandLineArgs(),
                (e, r) => e.EndGetCommandLineArgs(r),
                (IEnvironmentExtension e, ref Exception exception) => e.FailGetCommandLineArgs(ref exception));
        }

        public string GetEnvironmentVariable(string variable, EnvironmentVariableTarget target)
        {
            return this.EncapsulateWithExtension(
                () => System.Environment.GetEnvironmentVariable(variable, target),
                e => e.BeginGetEnvironmentVariable(variable, target),
                (e, r) => e.EndGetEnvironmentVariable(r, variable, target),
                (IEnvironmentExtension e, ref Exception exception) => e.FailGetEnvironmentVariable(ref exception, variable, target));
        }

        public string GetEnvironmentVariable(string variable)
        {
            return this.EncapsulateWithExtension(
                () => System.Environment.GetEnvironmentVariable(variable),
                e => e.BeginGetEnvironmentVariable(variable),
                (e, r) => e.EndGetEnvironmentVariable(r, variable),
                (IEnvironmentExtension e, ref Exception exception) => e.FailGetEnvironmentVariable(ref exception, variable));
        }

        public IDictionary<string, string> GetEnvironmentVariables(EnvironmentVariableTarget target)
        {
            return this.EncapsulateWithExtension(
                () => System.Environment.GetEnvironmentVariables(target).OfType<DictionaryEntry>().ToDictionary(e => (string)e.Key, v => (string)v.Value),
                e => e.BeginGetEnvironmentVariables(target),
                (e, r) => e.EndGetEnvironmentVariables(r, target),
                (IEnvironmentExtension e, ref Exception exception) => e.FailGetEnvironmentVariables(ref exception, target));
        }

        public IDictionary<string, string> GetEnvironmentVariables()
        {
            return this.EncapsulateWithExtension(
                () => System.Environment.GetEnvironmentVariables().OfType<DictionaryEntry>().ToDictionary(e => (string)e.Key, v => (string)v.Value),
                e => e.BeginGetEnvironmentVariables(),
                (e, r) => e.EndGetEnvironmentVariables(r),
                (IEnvironmentExtension e, ref Exception exception) => e.FailGetEnvironmentVariables(ref exception));
        }

        public string GetFolderPath(System.Environment.SpecialFolder folder)
        {
            return this.EncapsulateWithExtension(
                () => System.Environment.GetFolderPath(folder),
                e => e.BeginGetFolderPath(folder),
                (e, r) => e.EndGetFolderPath(r, folder),
                (IEnvironmentExtension e, ref Exception exception) => e.FailGetFolderPath(ref exception, folder));
        }

        public string GetFolderPath(System.Environment.SpecialFolder folder, System.Environment.SpecialFolderOption option)
        {
            return this.EncapsulateWithExtension(
                () => System.Environment.GetFolderPath(folder, option),
                e => e.BeginGetFolderPath(folder, option),
                (e, r) => e.EndGetFolderPath(r, folder, option),
                (IEnvironmentExtension e, ref Exception exception) => e.FailGetFolderPath(ref exception, folder, option));
        }

        public IEnumerable<string> GetLogicalDrives()
        {
            return this.EncapsulateWithExtension(
                () => System.Environment.GetLogicalDrives(),
                e => e.BeginGetLogicalDrives(),
                (e, r) => e.EndGetLogicalDrives(r),
                (IEnvironmentExtension e, ref Exception exception) => e.FailGetLogicalDrives(ref exception));
        }

        public void SetEnvironmentVariable(string variable, string value)
        {
            this.EncapsulateWithExtension(
                () => System.Environment.SetEnvironmentVariable(variable, value),
                e => e.BeginSetEnvironmentVariable(variable, value),
                e => e.EndSetEnvironmentVariable(variable, value),
                (IEnvironmentExtension e, ref Exception exception) => e.FailSetEnvironmentVariable(ref exception, variable, value));

            this.SurroundWithExtension(() => System.Environment.SetEnvironmentVariable(variable, value), variable, value);
        }

        public void SetEnvironmentVariable(string variable, string value, EnvironmentVariableTarget target)
        {
            this.EncapsulateWithExtension(
                () => System.Environment.SetEnvironmentVariable(variable, value, target),
                e => e.BeginSetEnvironmentVariable(variable, value, target),
                e => e.EndSetEnvironmentVariable(variable, value, target),
                (IEnvironmentExtension e, ref Exception exception) => e.FailSetEnvironmentVariable(ref exception, variable, value, target));

            this.SurroundWithExtension(() => System.Environment.SetEnvironmentVariable(variable, value, target), variable, value, target);
        }
    }
}