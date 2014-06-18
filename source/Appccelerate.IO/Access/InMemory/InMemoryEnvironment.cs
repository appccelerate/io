// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InMemoryEnvironment.cs" company="Appccelerate">
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

    public class InMemoryEnvironment : IEnvironment
    {
        public string CommandLine
        {
            get { throw new NotImplementedException(); }
        }

        public string CurrentDirectory { get; set; }

        public int ExitCode { get; set; }

        public bool HasShutdownStarted
        {
            get { throw new NotImplementedException(); }
        }

        public bool Is64BitOperatingSystem
        {
            get { throw new NotImplementedException(); }
        }

        public bool Is64BitProcess
        {
            get { throw new NotImplementedException(); }
        }

        public string MachineName
        {
            get { throw new NotImplementedException(); }
        }

        public string NewLine
        {
            get { throw new NotImplementedException(); }
        }

        public OperatingSystem OSVersion
        {
            get { throw new NotImplementedException(); }
        }

        public int ProcessorCount
        {
            get { throw new NotImplementedException(); }
        }

        public string StackTrace
        {
            get { throw new NotImplementedException(); }
        }

        public string SystemDirectory
        {
            get { throw new NotImplementedException(); }
        }

        public int SystemPageSize
        {
            get { throw new NotImplementedException(); }
        }

        public int TickCount
        {
            get { throw new NotImplementedException(); }
        }

        public string UserDomainName
        {
            get { throw new NotImplementedException(); }
        }

        public bool UserInteractive
        {
            get { throw new NotImplementedException(); }
        }

        public string UserName
        {
            get { return "Administrator"; }
        }

        public Version Version
        {
            get { throw new NotImplementedException(); }
        }

        public long WorkingSet
        {
            get { throw new NotImplementedException(); }
        }

        public void Exit(int exitCode)
        {
            throw new NotImplementedException();
        }

        public string ExpandEnvironmentVariables(string name)
        {
            throw new NotImplementedException();
        }

        public void FailFast(string message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void FailFast(string message)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetCommandLineArgs()
        {
            throw new NotImplementedException();
        }

        public string GetEnvironmentVariable(string variable, EnvironmentVariableTarget target)
        {
            throw new NotImplementedException();
        }

        public string GetEnvironmentVariable(string variable)
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, string> GetEnvironmentVariables(EnvironmentVariableTarget target)
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, string> GetEnvironmentVariables()
        {
            throw new NotImplementedException();
        }

        public string GetFolderPath(Environment.SpecialFolder folder)
        {
            throw new NotImplementedException();
        }

        public string GetFolderPath(Environment.SpecialFolder folder, Environment.SpecialFolderOption option)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetLogicalDrives()
        {
            throw new NotImplementedException();
        }

        public void SetEnvironmentVariable(string variable, string value)
        {
            throw new NotImplementedException();
        }

        public void SetEnvironmentVariable(string variable, string value, EnvironmentVariableTarget target)
        {
            throw new NotImplementedException();
        }
    }
}