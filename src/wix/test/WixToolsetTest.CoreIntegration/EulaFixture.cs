// Copyright (c) .NET Foundation and contributors. All rights reserved. Licensed under the Microsoft Reciprocal License. See LICENSE.TXT file in the project root for full license information.

namespace WixToolsetTest.CoreIntegration
{
    using System;
    using System.IO;
    using System.Linq;
    using WixInternal.Core.TestPackage;
    using WixInternal.TestSupport;
    using Xunit;

    public class EulaFixture
    {
        private static readonly string EulaFileFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".wix");
        private static readonly string EulaFilePath = Path.Combine(EulaFileFolder, "wix" + SomeVerInfo.Major + "-osmf-eula.txt");

        [Fact]
        public void CanAcceptEula()
        {
            CleanEulaFile();

            try
            {
                var result = WixRunner.Execute(
                [
                    "eula", "accept",
                    "wix" + SomeVerInfo.Major,
                ], out var messages, skipAcceptEula: true);
                Assert.Equal(0, result);
                Assert.Empty(messages);

                Assert.True(File.Exists(EulaFilePath), "Failed to create EULA file");
            }
            finally
            {
                CleanEulaFile();
            }
        }

        private static void CleanEulaFile()
        {
            if (Directory.Exists(EulaFileFolder))
            {
                File.Delete(EulaFilePath);
                Directory.Delete(EulaFileFolder);
            }
        }
    }
}
