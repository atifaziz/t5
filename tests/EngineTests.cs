//
// EngineTests.cs
//
// Author:
//       Mikayla Hutchinson <m.j.hutchinson@gmail.com>
//
// Copyright (c) 2016 Xamarin Inc.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace T5.TextTemplating.Tests
{
    [TestFixture]
    public class EngineTests
    {
        #pragma warning disable 414
        static object [] ParameterParsingCases = {
            new object [] { "foo=bar",             true,  "",     "",    "foo", "bar"     },
            new object [] { "a=b",                 true,  "",     "",    "a",   "b"       },
            new object [] { "a=b=c",               true,  "",     "",    "a",   "b=c"     },
            new object [] { "!!c!d",               true,  "",     "",    "c",   "d"       },
            new object [] { "!!!",                 false, "",     "",    "",    ""        },
            new object [] { "a=",                  true,  "",     "",    "a",   ""        },
            new object [] { "=",                   false, "",     "",    "",    ""        },
            new object [] { "",                    false, "",     "",    "",    ""        },
            new object [] { "!",                   false, "",     "",    "",    ""        },
            new object [] { "a!",                  true,  "",     "",    "a",   ""        },
            new object [] { "!b!c!d",              true,  "",     "b",   "c",   "d"       },
            new object [] { "a!b!c!d",             true,  "a",    "b",   "c",   "d"       },
            new object [] { "a=b!c!d!e",           true,  "",     "",    "a",   "b!c!d!e" },
            new object [] { "a!b!c!d!e",           true,  "a",    "b",   "c",   "d!e"     },
            new object [] { "foo!bar!baz!wibb!le", true,  "foo", "bar", "baz",  "wibb!le" },
        };
        #pragma warning restore 414

        [Test]
        [TestCaseSource(nameof (ParameterParsingCases))]
        public void ParameterParsing (
            string parameter, bool valid,
            string expectedProcessor, string expectedDirective,
            string expectedName, string expectedValue)
        {
            string processor, directive, name, value;
            var success = TemplateGenerator.TryParseParameter (parameter, out processor, out directive, out name, out value);

            Assert.AreEqual (valid, success);
            Assert.AreEqual (expectedProcessor, processor);
            Assert.AreEqual (expectedDirective, directive);
            Assert.AreEqual (expectedName, name);
            Assert.AreEqual (expectedValue, value);
        }

#pragma warning disable 414
        static object[] ParameterExpandCases = {
            new object [] { "thisIsATest",                  "thisIsATest",                      new string[] { }                                            },
            new object [] { "thisIs$(Not)ATest",            "thisIs$(Not)ATest",                new string[] { }                                            },
            new object [] { "thisIs$(NotATest",             "thisIs$(NotATest",                 new string[] { }                                            },
            new object [] { "this$(T1)Is$(NotATest",        "thisForSureIs$(NotATest",          new string[] { "T1=ForSure" }                               },
            new object [] { "$(Not)thisIsATest",            "NotAtAllthisIsATest",              new string[] { "Not=NotAtAll" }                             },
            new object [] { "thisIs$(Not)ATest",            "thisIsNotAtAllATest",              new string[] { "Not=NotAtAll" }                             },
            new object [] { "thisIsATest$(Not)",            "thisIsATestNotAtAll",              new string[] { "Not=NotAtAll" }                             },
            new object [] { "this$(T1)IsA$(T2)Test$(T3)",   "thisOneIsAnActualTestInTheEnd",    new string[] { "T1=One", "T2=nActual", "T3=InTheEnd" }      },
        };
        #pragma warning restore 414

        [Test]
        [TestCaseSource(nameof(ParameterExpandCases))]
        public void ParameterExpand(
            string toExpand, string expected,
            string[] parametersString)
        {
            var parameters = new Dictionary<TemplateGenerator.ParameterKey, string>();
            string processor, directive, name, value;
            for (int i = 0; i < parametersString.Length; ++i)
            {
                var success = TemplateGenerator.TryParseParameter(parametersString[i], out processor, out directive, out name, out value);
                Assert.True(success, $"Invalid test parameter input: {parametersString[i]}");
                parameters.Add(new TemplateGenerator.ParameterKey(processor, directive, name), value);
            }

            var actual = TemplateGenerator.ExpandParameters(toExpand, parameters);
            Assert.AreEqual(expected, actual);
        }
    }
}
