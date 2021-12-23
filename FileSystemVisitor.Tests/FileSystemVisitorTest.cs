using NUnit.Framework;
using System;

namespace FileSystemVisitor.Tests
{
    public class FileSystemVisitorTest
    {
        string startPath = string.Empty;

        [SetUp]
        public void Setup()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var projectName = "FileSystemVisitor";
            startPath = baseDirectory.Substring(0, baseDirectory.IndexOf(projectName) + projectName.Length);
        }

        [Test]
        public void Only_One_File_Program_Cs_Exists()
        {
            var fsv = new FileSystemVisitor(startPath, file =>
            {
                return file.FullName.EndsWith("Program.cs");
            });

            int result = 0;
            foreach (var item in fsv)
            {
                result++;
            }

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void GetFourCsFiles_In_MainFolder_And_StopSearch()
        {
            var fsv = new FileSystemVisitor(startPath, file =>
            {
                return file.FullName.EndsWith(".cs");
            });

            fsv.DirectoryFinded += (s, e) =>
            {
                if (s is FileSystemVisitor fileSystemVisitor)
                    fileSystemVisitor.IsSearchStopped = true;
            };

            int result = 0;
            foreach (var item in fsv)
            {
                result++;
            }

            Assert.That(result, Is.EqualTo(4));
        }

        [Test]
        public void GetFourCsFiles_In_MainFolder_StopSearch_And_ExcludeProgramCs()
        {
            var fsv = new FileSystemVisitor(startPath, file =>
            {
                return file.FullName.EndsWith(".cs");
            });

            fsv.FileFinded += (s, e) =>
            {
                if (s is FileSystemVisitor fileSystemVisitor && e.Path.EndsWith("Program.cs"))
                {
                    fileSystemVisitor.IsItemExcluded = true;
                }
            };

            fsv.DirectoryFinded += (s, e) =>
            {
                if (s is FileSystemVisitor fileSystemVisitor)
                {
                    fileSystemVisitor.IsSearchStopped = true;
                }
            };

            int result = 0;
            foreach (var item in fsv)
            {
                result++;
            }

            Assert.That(result, Is.EqualTo(3));
        }
    }
}