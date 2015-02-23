using System;
using System.Diagnostics;
using System.IO;
using CcAcca.DemoDownstream;
using NUnit.Framework;

namespace CcAcca.EntityFramework.MultiMigrate.Tests.CmdLineTests
{
    [TestFixture]
    public class MultiMigrationTests
    {
        [SetUp]
        public void Setup()
        {
            var db = new DemoDbContext("DemoDbContext");
            if (db.Database.Exists())
            {
                db.Database.Delete();
            }
        }

        [Test]
        public void CanMigrateToLatestVs_SkippedMigrationsAsFile()
        {
            var exitCode = RunProgressDataImport("run-multimigrate-eg1.bat");
            Assert.That(exitCode, Is.EqualTo(0));
        }

        [Test]
        public void CanMigrateToLatestVs_SkippedMigrationsAsCommaSeperatedList()
        {
            var exitCode = RunProgressDataImport("run-multimigrate-eg2.bat");
            Assert.That(exitCode, Is.EqualTo(0));
        }

        private static int RunProgressDataImport(string batchFilename)
        {
            const int oneMinute = 60000;
            string batchFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\CmdLineTests", batchFilename);
            if (!File.Exists(batchFile))
            {
                Assert.Fail("Batch file does not exist; tried: '{0}'", batchFile);
            }

            var process = new Process
            {
                StartInfo =
                {
                    FileName = batchFile,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    WorkingDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\CmdLineTests")
                }
            };

            process.Start();
            process.WaitForExit(oneMinute);
            return process.ExitCode;
        }
    }
}