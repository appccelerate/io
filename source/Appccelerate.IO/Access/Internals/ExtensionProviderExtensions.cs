//-------------------------------------------------------------------------------
// <copyright file="ExtensionProviderExtensions.cs" company="Appccelerate">
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
    
    public static class ExtensionProviderExtensions
    {
        public delegate void FailAction<TExtension>(TExtension extension, ref Exception exception);

        public static void EncapsulateWithExtension<TExtension>(
            this IExtensionProvider<TExtension> provider,
            Action f,
            Action<TExtension> begin,
            Action<TExtension> end,
            FailAction<TExtension> fail)
        {
            foreach (TExtension extension in provider.Extensions)
            {
                begin(extension);
            }

            try
            {
                f();
            }
            catch (Exception exception)
            {
                var exceptionToRethrow = exception;
                foreach (TExtension fileExtension in provider.Extensions)
                {
                    var e = exception;
                    fail(fileExtension, ref e);

                    exceptionToRethrow = e;
                }

                throw exceptionToRethrow;
            }

            foreach (TExtension fileExtension in provider.Extensions)
            {
                end(fileExtension);
            }
        }

        public static TResult EncapsulateWithExtension<TExtension, TResult>(
            this IExtensionProvider<TExtension> provider, 
            Func<TResult> f, 
            Action<TExtension> begin, 
            Action<TExtension, TResult> end,
            FailAction<TExtension> fail)
        {
            foreach (TExtension extension in provider.Extensions)
            {
                begin(extension);
            }

            try
            {
                TResult result = f();

                foreach (TExtension fileExtension in provider.Extensions)
                {
                    end(fileExtension, result);
                }

                return result;
            }
            catch (Exception exception)
            {
                var exceptionToRethrow = exception;
                foreach (TExtension fileExtension in provider.Extensions)
                {
                    var e = exception;
                    fail(fileExtension, ref e);

                    exceptionToRethrow = e;
                }

                throw exceptionToRethrow;
            }
        }
    }
}