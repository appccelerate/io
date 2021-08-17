//-------------------------------------------------------------------------------
// <copyright file="ExtensionProviderExtensionsFacts.cs" company="Appccelerate">
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
    using System.Collections.Generic;
    using System.IO;

    using FakeItEasy;
    using FluentAssertions;
    using Xunit;

    public class ExtensionProviderExtensionsFacts
    {
        private const int ExpectedReturnValue = 1;

        private readonly IExtensionProvider<IExtension> provider;

        private readonly MemoryStream expectedReturnStream = new MemoryStream();

        private readonly IExtension extension;

        private Exception exception;

        private string stringParameter;

        private bool boolParameter;

        private bool throwException;

        private int intParameter;

        public ExtensionProviderExtensionsFacts()
        {
            this.provider = A.Fake<IExtensionProvider<IExtension>>();
            this.extension = A.Fake<IExtension>();

            this.exception = new Exception();
        }

        public interface IExtension
        {
            void BeginDo(bool s);

            void EndDo(bool s);

            void BeginDo(string s);

            void EndDo(string s);

            void FailDo(ref Exception exception);

            void BeginDoReturn(string s);

            void EndDoReturn(int result, string s);

            void BeginDoReturn(bool s);

            void EndDoReturn(int result, bool s);

            void BeginDoReturn(int s);

            void EndDoReturn(Stream result, int s);

            void FailDoReturn(ref Exception exception);
        }

        [Fact]
        public void CallsBeginWithCorrectParameters_WhenUsingAction()
        {
            const string ExpectedParameter = "Test";

            this.SetupExtensions();

            this.provider.EncapsulateWithExtension(
                () => this.Do(ExpectedParameter),
                e => e.BeginDo(ExpectedParameter),
                e => e.EndDo(ExpectedParameter),
                (IExtension e, ref Exception ex) => e.FailDo(ref ex));

            A.CallTo(() => this.extension.BeginDo(ExpectedParameter)).MustHaveHappened();
        }

        [Fact]
        public void CallsEndWithCorrectParameters_WhenUsingAction()
        {
            const string ExpectedParameter = "Test";

            this.SetupExtensions();

            this.provider.EncapsulateWithExtension(
                () => this.Do(ExpectedParameter),
                e => e.BeginDo(ExpectedParameter),
                e => e.EndDo(ExpectedParameter),
                (IExtension e, ref Exception ex) => e.FailDo(ref ex));

            A.CallTo(() => this.extension.EndDo(ExpectedParameter)).MustHaveHappened();
        }

        [Fact]
        public void CallsActionWithCorrectParameters_WhenUsingAction()
        {
            const string ExpectedParameter = "Test";

            this.SetupExtensions();

            this.provider.EncapsulateWithExtension(
                () => this.Do(ExpectedParameter),
                e => e.BeginDo(ExpectedParameter),
                e => e.EndDo(ExpectedParameter),
                (IExtension e, ref Exception ex) => e.FailDo(ref ex));

            this.stringParameter.Should().Be(ExpectedParameter);
        }

        [Fact]
        public void CallsFailWithCorrectExceptionAndRethrowsException_WhenUsingAction()
        {
            var exception = new Exception();

            const string ExpectedParameter = "Test";

            this.RegisterExtensions(this.extension);

            Action action = () =>
                            this.provider.EncapsulateWithExtension(
                                () =>
                                    {
                                        throw exception;
                                    },
                                e => e.BeginDo(ExpectedParameter),
                                e => e.EndDo(ExpectedParameter),
                                (IExtension e, ref Exception ex) => e.FailDo(ref ex));

            action.Should().Throw<Exception>();
            A.CallTo(() => this.extension.FailDo(ref exception)).MustHaveHappened();
        }

        [Fact]
        public void RethrowsExchangedException_WhenUsingActionAndExtensionExchangesException()
        {
            const string ExpectedParameter = "Test";

            this.RegisterExtensions(new NewExceptionExchangingExtension(new InvalidOperationException()));

            Action action = () =>
                            this.provider.EncapsulateWithExtension(
                                () =>
                                    {
                                        throw new ArgumentException();
                                    },
                                e => e.BeginDo(ExpectedParameter),
                                e => e.EndDo(ExpectedParameter),
                                (IExtension e, ref Exception ex) => e.FailDo(ref ex));

            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void RethrowsLastExchangedException_WhenUsingActionAndMultipleExtensionsExchangeException()
        {
            const string ExpectedParameter = "Test";

            this.RegisterExtensions(
                new NewExceptionExchangingExtension(new OutOfMemoryException()),
                new NewExceptionExchangingExtension(new InvalidOperationException()));

            Action action = () =>
                            this.provider.EncapsulateWithExtension(
                                () =>
                                    {
                                        throw new ArgumentException();
                                    },
                                e => e.BeginDo(ExpectedParameter),
                                e => e.EndDo(ExpectedParameter),
                                (IExtension e, ref Exception ex) => e.FailDo(ref ex));

            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void CallsBeginWithCorrectParameters_WhenUsingActionWithOverload()
        {
            const bool ExpectedParameter = true;

            this.SetupExtensions();

            this.provider.EncapsulateWithExtension(
                () => this.Do(ExpectedParameter),
                e => e.BeginDo(ExpectedParameter),
                e => e.EndDo(ExpectedParameter),
                (IExtension e, ref Exception ex) => e.FailDo(ref ex));

            A.CallTo(() => this.extension.BeginDo(ExpectedParameter)).MustHaveHappened();
        }

        [Fact]
        public void CallsEndWithCorrectParameters_WhenUsingActionWithOverload()
        {
            const bool ExpectedParameter = true;

            this.SetupExtensions();

            this.provider.EncapsulateWithExtension(
                () => this.Do(ExpectedParameter),
                e => e.BeginDo(ExpectedParameter),
                e => e.EndDo(ExpectedParameter),
                (IExtension e, ref Exception ex) => e.FailDo(ref ex));

            A.CallTo(() => this.extension.EndDo(ExpectedParameter)).MustHaveHappened();
        }

        [Fact]
        public void CallsActionWithCorrectParameters_WhenUsingActionWithOverload()
        {
            const bool ExpectedParameter = true;

            this.SetupExtensions();

            this.provider.EncapsulateWithExtension(
                () => this.Do(ExpectedParameter),
                e => e.BeginDo(ExpectedParameter),
                e => e.EndDo(ExpectedParameter),
                (IExtension e, ref Exception ex) => e.FailDo(ref ex));

            this.boolParameter.Should().Be(ExpectedParameter);
        }

        [Fact]
        public void CallsFailWithCorrectExceptionAndRethrowsException_WhenUsingActionWithOverload()
        {
            const bool ExpectedParameter = true;

            this.SetupExtensions();

            Action action = () =>
                            this.provider.EncapsulateWithExtension(
                                () =>
                                    {
                                        throw new ArgumentException();
                                    },
                                e => e.BeginDo(ExpectedParameter),
                                e => e.EndDo(ExpectedParameter),
                                (IExtension e, ref Exception ex) => e.FailDo(ref ex));

            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void RethrowsExchangedException_WhenUsingActionWithOverloadAndExtensionExchangesException()
        {
            const bool ExpectedParameter = true;

            this.RegisterExtensions(new NewExceptionExchangingExtension(new InvalidOperationException()));

            Action action = () =>
                            this.provider.EncapsulateWithExtension(
                                () =>
                                    {
                                        throw new ArgumentException();
                                    },
                                e => e.BeginDo(ExpectedParameter),
                                e => e.EndDo(ExpectedParameter),
                                (IExtension e, ref Exception ex) => e.FailDo(ref ex));

            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void RethrowsLastExchangedException_WhenUsingActionWithOverloadAndMultipleExtensionsExchangeException()
        {
            const bool ExpectedParameter = true;

            this.RegisterExtensions(
                new NewExceptionExchangingExtension(new OutOfMemoryException()),
                new NewExceptionExchangingExtension(new InvalidOperationException()));

            Action action = () =>
                            this.provider.EncapsulateWithExtension(
                                () =>
                                    {
                                        throw new ArgumentException();
                                    },
                                e => e.BeginDo(ExpectedParameter),
                                e => e.EndDo(ExpectedParameter),
                                (IExtension e, ref Exception ex) => e.FailDo(ref ex));

            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void ReturnsValue_WhenUsingFunc()
        {
            const string ExpectedParameter = "Test";

            this.RegisterExtensions(this.extension);

            int result = this.provider.EncapsulateWithExtension(
                () => this.DoReturn(ExpectedParameter),
                e => e.BeginDoReturn(ExpectedParameter),
                (e, r) => e.EndDoReturn(r, ExpectedParameter),
                (IExtension e, ref Exception ex) => e.FailDo(ref ex));

            result.Should().Be(ExpectedReturnValue);
        }

        [Fact]
        public void CallsBeginWithCorrectParameters_WhenUsingFunc()
        {
            const string ExpectedParameter = "Test";

            this.SetupExtensions();

            this.provider.EncapsulateWithExtension(
                () => this.DoReturn(ExpectedParameter),
                e => e.BeginDoReturn(ExpectedParameter),
                (e, r) => e.EndDoReturn(r, ExpectedParameter),
                (IExtension e, ref Exception ex) => e.FailDo(ref ex));

            A.CallTo(() => this.extension.BeginDoReturn(ExpectedParameter)).MustHaveHappened();
        }

        [Fact]
        public void CallsEndWithCorrectParameters_WhenUsingFunc()
        {
            const string ExpectedParameter = "Test";

            this.SetupExtensions();

            this.provider.EncapsulateWithExtension(
                () => this.DoReturn(ExpectedParameter),
                e => e.BeginDoReturn(ExpectedParameter),
                (e, r) => e.EndDoReturn(r, ExpectedParameter),
                (IExtension e, ref Exception ex) => e.FailDo(ref ex));

            A.CallTo(() => this.extension.EndDoReturn(ExpectedReturnValue, ExpectedParameter)).MustHaveHappened();
        }

        [Fact]
        public void CallsActionWithCorrectParameters_WhenUsingFunc()
        {
            const string ExpectedParameter = "Test";

            this.SetupExtensions();

            this.provider.EncapsulateWithExtension(
                () => this.DoReturn(ExpectedParameter),
                e => e.BeginDoReturn(ExpectedParameter),
                (e, r) => e.EndDoReturn(r, ExpectedParameter),
                (IExtension e, ref Exception ex) => e.FailDo(ref ex));

            this.stringParameter.Should().Be(ExpectedParameter);
        }

        [Fact]
        public void CallsFailWithCorrectExceptionAndRethrowsException_WhenUsingFunc()
        {
            const string ExpectedParameter = "Test";

            this.SetupExtensions();

            Action action = () =>
                            this.provider.EncapsulateWithExtension(
                                (Func<int>)(() =>
                                    {
                                        throw new ArgumentException();
                                    }),
                                e => e.BeginDoReturn(ExpectedParameter),
                                (e, r) => e.EndDoReturn(r, ExpectedParameter),
                                (IExtension e, ref Exception ex) => e.FailDo(ref ex));

            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void RethrowsExchangedException_WhenUsingFuncAndExtensionExchangesException()
        {
            const string ExpectedParameter = "Test";

            this.RegisterExtensions(new NewExceptionExchangingExtension(new InvalidOperationException()));

            Action action = () =>
                            this.provider.EncapsulateWithExtension(
                                (Func<int>)(() =>
                                    {
                                        throw new ArgumentException();
                                    }),
                                e => e.BeginDoReturn(ExpectedParameter),
                                (e, r) => e.EndDoReturn(r, ExpectedParameter),
                                (IExtension e, ref Exception ex) => e.FailDoReturn(ref ex));

            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void RethrowsLastExchangedException_WhenUsingFuncAndMultipleExtensionsExchangeException()
        {
            const string ExpectedParameter = "Test";

            this.RegisterExtensions(
                new NewExceptionExchangingExtension(new OutOfMemoryException()),
                new NewExceptionExchangingExtension(new InvalidOperationException()));

            Action action = () =>
                            this.provider.EncapsulateWithExtension(
                                (Func<int>)(() =>
                                {
                                    throw new ArgumentException();
                                }),
                                e => e.BeginDoReturn(ExpectedParameter),
                                (e, r) => e.EndDoReturn(r, ExpectedParameter),
                                (IExtension e, ref Exception ex) => e.FailDo(ref ex));

            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void CallsBeginWithCorrectParameters_WhenUsingFuncWithOverload()
        {
            const bool ExpectedParameter = true;

            this.SetupExtensions();

            this.provider.EncapsulateWithExtension(
                () => this.DoReturn(ExpectedParameter),
                e => e.BeginDoReturn(ExpectedParameter),
                (e, r) => e.EndDoReturn(r, ExpectedParameter),
                (IExtension e, ref Exception ex) => e.FailDo(ref ex));

            A.CallTo(() => this.extension.BeginDoReturn(ExpectedParameter)).MustHaveHappened();
        }

        [Fact]
        public void CallsEndWithCorrectParameters_WhenUsingFuncWithOverload()
        {
            const bool ExpectedParameter = true;

            this.SetupExtensions();

            this.provider.EncapsulateWithExtension(
                () => this.DoReturn(ExpectedParameter),
                e => e.BeginDoReturn(ExpectedParameter),
                (e, r) => e.EndDoReturn(r, ExpectedParameter),
                (IExtension e, ref Exception ex) => e.FailDo(ref ex));

            A.CallTo(() => this.extension.EndDoReturn(ExpectedReturnValue, ExpectedParameter)).MustHaveHappened();
        }

        [Fact]
        public void CallsActionWithCorrectParameters_WhenUsingFuncWithOverload()
        {
            const bool ExpectedParameter = true;

            this.SetupExtensions();

            this.provider.EncapsulateWithExtension(
                () => this.DoReturn(ExpectedParameter),
                e => e.BeginDoReturn(ExpectedParameter),
                (e, r) => e.EndDoReturn(r, ExpectedParameter),
                (IExtension e, ref Exception ex) => e.FailDo(ref ex));

            this.boolParameter.Should().Be(ExpectedParameter);
        }

        [Fact]
        public void CallsFailWithCorrectExceptionAndRethrowsException_WhenUsingFuncWithOverload()
        {
            const bool ExpectedParameter = true;

            this.SetupExtensions();

            Action action = () =>
                            this.provider.EncapsulateWithExtension(
                                (Func<int>)(() =>
                                {
                                    throw new ArgumentException();
                                }),
                                e => e.BeginDoReturn(ExpectedParameter),
                                (e, r) => e.EndDoReturn(r, ExpectedParameter),
                                (IExtension e, ref Exception ex) => e.FailDo(ref ex));

            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void RethrowsExchangedException_WhenUsingFuncWithOverloadAndExtensionExchangesException()
        {
            const bool ExpectedParameter = true;

            this.RegisterExtensions(new NewExceptionExchangingExtension(new InvalidOperationException()));

            Action action = () =>
                            this.provider.EncapsulateWithExtension(
                                (Func<int>)(() =>
                                {
                                    throw new ArgumentException();
                                }),
                                e => e.BeginDoReturn(ExpectedParameter),
                                (e, r) => e.EndDoReturn(r, ExpectedParameter),
                                (IExtension e, ref Exception ex) => e.FailDo(ref ex));

            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void RethrowsLastExchangedException_WhenUsingFuncWithOverloadAndMultipleExtensionsExchangeException()
        {
            const bool ExpectedParameter = true;

            this.RegisterExtensions(
                new NewExceptionExchangingExtension(new OutOfMemoryException()),
                new NewExceptionExchangingExtension(new InvalidOperationException()));

            Action action = () =>
                            this.provider.EncapsulateWithExtension(
                                (Func<int>)(() =>
                                {
                                    throw new ArgumentException();
                                }),
                                e => e.BeginDoReturn(ExpectedParameter),
                                (e, r) => e.EndDoReturn(r, ExpectedParameter),
                                (IExtension e, ref Exception ex) => e.FailDo(ref ex));

            action.Should().Throw<InvalidOperationException>();
        }
        
        [Fact]
        public void CallsBeginWithCorrectParameters_WhenUsingFuncWithOverloadAndExtensionUsingReturnTypeMapping()
        {
            const int ExpectedParameter = 42;

            this.RegisterExtensions(this.extension);
            
            this.provider.EncapsulateWithExtension(
                () => this.DoReturn(ExpectedParameter),
                e => e.BeginDoReturn(ExpectedParameter),
                (e, r) => e.EndDoReturn(r, ExpectedParameter),
                (IExtension e, ref Exception ex) => e.FailDo(ref ex));

            A.CallTo(() => this.extension.BeginDoReturn(ExpectedParameter)).MustHaveHappened();
        }

        [Fact]
        public void CallsEndWithCorrectParameters_WhenUsingFuncWithOverloadAndExtensionUsingReturnTypeMapping()
        {
            const int ExpectedParameter = 42;

            this.RegisterExtensions(this.extension);

            this.provider.EncapsulateWithExtension(
                () => this.DoReturn(ExpectedParameter),
                e => e.BeginDoReturn(ExpectedParameter),
                (e, r) => e.EndDoReturn(r, ExpectedParameter),
                (IExtension e, ref Exception ex) => e.FailDo(ref ex));

            A.CallTo(() => this.extension.EndDoReturn(this.expectedReturnStream, ExpectedParameter)).MustHaveHappened();
        }

        [Fact]
        public void CallsActionWithCorrectParameters_WhenUsingFuncWithOverloadAndExtensionUsingReturnTypeMapping()
        {
            const int ExpectedParameter = 42;

            this.RegisterExtensions(this.extension);

            Stream result = this.provider.EncapsulateWithExtension(
                () => this.DoReturn(ExpectedParameter),
                e => e.BeginDoReturn(ExpectedParameter),
                (e, r) => e.EndDoReturn(r, ExpectedParameter),
                (IExtension e, ref Exception ex) => e.FailDo(ref ex));
            
            this.intParameter.Should().Be(ExpectedParameter);
            result.Should().BeSameAs(this.expectedReturnStream);
        }

        [Fact]
        public void CallsFailWithCorrectParameters_WhenUsingFuncWithOverloadAndExtensionUsingReturnTypeMapping()
        {
            var exception = new Exception();
            const int ExpectedParameter = 42;

            this.RegisterExtensions(this.extension);

            Action action = () => this.provider.EncapsulateWithExtension(
                (Func<Stream>)(() =>
                    {
                        throw exception;
                    }),
                e => e.BeginDoReturn(ExpectedParameter),
                (e, r) => e.EndDoReturn(r, ExpectedParameter),
                (IExtension e, ref Exception ex) => e.FailDoReturn(ref ex));

            action.Should().Throw<Exception>();
            A.CallTo(() => this.extension.FailDoReturn(ref exception)).MustHaveHappened();
        }

        [Fact]
        public void RethrowsExchangedException_WhenUsingFuncWithOverloadAndExtensionUsingReturnTypeMappingAndExtensionExchangesException()
        {
            const int ExpectedParameter = 42;

            this.RegisterExtensions(new NewExceptionExchangingExtension(new InvalidOperationException()));

            Action action = () =>
                            this.provider.EncapsulateWithExtension(
                                (Func<Stream>)(() =>
                                {
                                    throw new ArgumentException();
                                }),
                                e => e.BeginDoReturn(ExpectedParameter),
                                (e, r) => e.EndDoReturn(r, ExpectedParameter),
                                (IExtension e, ref Exception ex) => e.FailDo(ref ex));

            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void RethrowsLastExchangedException_WhenUsingFuncWithOverloadAndExtensionUsingReturnTypeMappingAndMultipleExtensionsExchangeException()
        {
            const int ExpectedParameter = 42;

            this.RegisterExtensions(
                new NewExceptionExchangingExtension(new OutOfMemoryException()),
                new NewExceptionExchangingExtension(new InvalidOperationException()));

            Action action = () =>
                            this.provider.EncapsulateWithExtension(
                                (Func<Stream>)(() =>
                                {
                                    throw new ArgumentException();
                                }),
                                e => e.BeginDoReturn(ExpectedParameter),
                                (e, r) => e.EndDoReturn(r, ExpectedParameter),
                                (IExtension e, ref Exception ex) => e.FailDo(ref ex));

            action.Should().Throw<InvalidOperationException>();
        }

        private void RegisterExtensions(params IExtension[] extensions)
        {
            A.CallTo(() => this.provider.Extensions).Returns(extensions);
        }

        private void Do(bool parameter)
        {
            if (this.throwException)
            {
                throw this.exception;
            }

            this.boolParameter = parameter;
        }

        private void Do(string parameter)
        {
            if (this.throwException)
            {
                throw this.exception;
            }

            this.stringParameter = parameter;
        }

        private int DoReturn(string parameter)
        {
            if (this.throwException)
            {
                throw this.exception;
            }

            this.stringParameter = parameter;

            return ExpectedReturnValue;
        }

        private int DoReturn(bool parameter)
        {
            if (this.throwException)
            {
                throw this.exception;
            }

            this.boolParameter = parameter;

            return ExpectedReturnValue;
        }

        private MemoryStream DoReturn(int parameter)
        {
            if (this.throwException)
            {
                throw this.exception;
            }

            this.intParameter = parameter;

            return this.expectedReturnStream;
        }

        private void SetupThrowsException()
        {
            this.throwException = true;
        }

        private void SetupExtensions()
        {
            A.CallTo(() => this.provider.Extensions).Returns(new List<IExtension> { this.extension });
        }

        private void SetupExtensionsWithExceptionExchangingExtension()
        {
            A.CallTo(() => this.provider.Extensions).Returns(new List<IExtension> { new ExceptionExchangingExtension(this.exception), new SecondExceptionExchangingExtension(this.exception) });
        }

        private class ExceptionThrowingExtension : IExtension
        {
            public Exception BeginException { get; set; }

            public Exception EndException { get; set; }
            
            public void BeginDo(bool s)
            {
                this.Begin();
            }

            public void EndDo(bool s)
            {
                this.End();
            }

            public void BeginDo(string s)
            {
                this.Begin();
            }

            public void EndDo(string s)
            {
                this.End();
            }

            public void FailDo(ref Exception exception)
            {
            }

            public void BeginDoReturn(string s)
            {
                this.Begin();
            }

            public void EndDoReturn(int result, string s)
            {
                this.End();
            }

            public void BeginDoReturn(bool s)
            {
                this.Begin();
            }

            public void EndDoReturn(int result, bool s)
            {
                this.End();
            }

            public void BeginDoReturn(int s)
            {
                this.Begin();
            }

            public void EndDoReturn(Stream result, int s)
            {
                this.End();
            }

            public void FailDoReturn(ref Exception exception)
            {
            }

            private void Begin()
            {
                if (this.BeginException != null)
                {
                    throw this.BeginException;
                }
            }

            private void End()
            {
                if (this.EndException != null)
                {
                    throw this.EndException;
                }
            }
        }

        private class NewExceptionExchangingExtension : IExtension
        {
            private readonly Exception replacement;

            public NewExceptionExchangingExtension(Exception replacement)
            {
                this.replacement = replacement;
            }

            public void BeginDo(bool s)
            {
            }

            public void EndDo(bool s)
            {
            }

            public void BeginDo(string s)
            {
            }

            public void EndDo(string s)
            {
            }

            public void FailDo(ref Exception exception)
            {
                exception = this.replacement;
            }

            public void BeginDoReturn(string s)
            {
            }

            public void EndDoReturn(int result, string s)
            {
            }

            public void BeginDoReturn(bool s)
            {
            }

            public void EndDoReturn(int result, bool s)
            {
            }

            public void BeginDoReturn(int s)
            {
            }

            public void EndDoReturn(Stream result, int s)
            {
            }

            public void FailDoReturn(ref Exception exception)
            {
                exception = this.replacement;
            }
        }

        private class ExceptionExchangingExtension : IExtension
        {
            private readonly Exception exceptionToThrow;

            public ExceptionExchangingExtension(Exception exceptionToThrow)
            {
                this.exceptionToThrow = exceptionToThrow;
            }

            public void BeginDo(bool s)
            {
                throw this.exceptionToThrow;
            }

            public void EndDo(bool s)
            {
                throw this.exceptionToThrow;
            }

            public void BeginDo(string s)
            {
                throw this.exceptionToThrow;
            }

            public void EndDo(string s)
            {
                throw this.exceptionToThrow;
            }

            public void FailDo(ref Exception exception)
            {
                exception = new InvalidOperationException();
            }

            public void BeginDoReturn(string s)
            {
                throw this.exceptionToThrow;
            }

            public void EndDoReturn(int result, string s)
            {
                throw this.exceptionToThrow;
            }

            public void BeginDoReturn(bool s)
            {
                throw this.exceptionToThrow;
            }

            public void EndDoReturn(int result, bool s)
            {
                throw this.exceptionToThrow;
            }

            public void BeginDoReturn(int s)
            {
                throw this.exceptionToThrow;
            }

            public void EndDoReturn(Stream result, int s)
            {
                throw this.exceptionToThrow;
            }

            public void FailDoReturn(ref Exception exception)
            {
                exception = new InvalidOperationException();
            }
        }

        private class SecondExceptionExchangingExtension : IExtension
        {
            private readonly Exception exceptionToThrow;

            public SecondExceptionExchangingExtension(Exception exceptionToThrow)
            {
                this.exceptionToThrow = exceptionToThrow;
            }

            public void BeginDo(bool s)
            {
                throw this.exceptionToThrow;
            }

            public void EndDo(bool s)
            {
                throw this.exceptionToThrow;
            }

            public void BeginDo(string s)
            {
                throw this.exceptionToThrow;
            }

            public void EndDo(string s)
            {
                throw this.exceptionToThrow;
            }

            public void FailDo(ref Exception exception)
            {
                exception = new ArgumentNullException();
            }

            public void BeginDoReturn(string s)
            {
                throw this.exceptionToThrow;
            }

            public void EndDoReturn(int result, string s)
            {
                throw this.exceptionToThrow;
            }

            public void BeginDoReturn(bool s)
            {
                throw this.exceptionToThrow;
            }

            public void EndDoReturn(int result, bool s)
            {
                throw this.exceptionToThrow;
            }

            public void BeginDoReturn(int s)
            {
                throw this.exceptionToThrow;
            }

            public void EndDoReturn(Stream result, int s)
            {
                throw this.exceptionToThrow;
            }

            public void FailDoReturn(ref Exception exception)
            {
                exception = new ArgumentNullException();
            }
        }
    }
}