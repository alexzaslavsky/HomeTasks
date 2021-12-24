using NUnit.Framework;
using System;

namespace FileSystemVisitor.Tests
{
    public class FileSystemVisitorTests
    {
        const string projectName = "FileSystemVisitor";

        string startPath = string.Empty;

        FileSystemVisitor sut;

        [SetUp]
        public void Setup()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            startPath = baseDirectory.Substring(0, baseDirectory.IndexOf(projectName) + projectName.Length);
        }

        [Test]
        public void Only_One_File_Program_Cs_Exists()
        {
            sut = new FileSystemVisitor(startPath, file =>
            {
                return file.FullName.EndsWith("Program.cs");
            });

            Assert.That(GetResult(sut), Is.EqualTo(1));
        }

        [Test]
        public void GetFourCsFiles_In_MainFolder_And_StopSearch()
        {
            sut = new FileSystemVisitor(startPath, file =>
            {
                return file.FullName.EndsWith(".cs");
            });

            sut.DirectoryFinded += (sender, eventArgs) =>
            {
                if (sender is FileSystemVisitor fileSystemVisitor)
                    fileSystemVisitor.IsSearchStopped = true;
            };

            Assert.That(GetResult(sut), Is.EqualTo(4));
        }

        [Test]
        public void GetFourCsFiles_In_MainFolder_StopSearch_And_ExcludeProgramCs()
        {
            sut = new FileSystemVisitor(startPath, file =>
            {
                return file.FullName.EndsWith(".cs");
            });

            sut.FileFinded += (sender, eventArgs) =>
            {
                if (sender is FileSystemVisitor fileSystemVisitor && eventArgs.Path.EndsWith("Program.cs"))
                {
                    fileSystemVisitor.IsItemExcluded = true;
                }
            };

            sut.DirectoryFinded += (sender, eventArgs) =>
            {
                if (sender is FileSystemVisitor fileSystemVisitor)
                {
                    fileSystemVisitor.IsSearchStopped = true;
                }
            };

            Assert.That(GetResult(sut), Is.EqualTo(3));
        }

        private int GetResult(FileSystemVisitor sut)
        {
            int result = 0;

            foreach (var item in sut)
            {
                result++;
            }

            return result;
        }
    }
}