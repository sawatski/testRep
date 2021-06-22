using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IIG.FileWorker;

namespace BlackBoxReworked
{
    [TestClass]
    public class BlackBoxReworked
    {
        private string currentDirectory = Path.GetDirectoryName
            (Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())));

        [TestMethod]
        public void ReadLinesOfFile()
        {
            //testing ReadLines method
            string path = $"{currentDirectory}\\TestedFiles\\Reading.txt";
            string[] actual = BaseFileWorker.ReadLines(path);
            string[] expected = File.ReadAllLines(path);
            string randomLine = "wegqrbasdbqer";

            // length checking

            Assert.IsTrue(actual.Length > 0 && expected.Length > 0);


            //multiple cases testing

            Assert.AreEqual("Lorem Ipsum has been the industry's standard dummy text ever since the 1500s,", actual[1]);
            Assert.AreEqual("when an unknown printer took a galley of type and scrambled it to make a type specimen book.", actual[2]);
            Assert.AreNotEqual(actual, randomLine);

            Assert.IsTrue(actual.Length == 6);

            Assert.AreEqual(actual.Length, expected.Length);
            Assert.AreEqual(actual[4], expected[4]);

            Assert.AreNotEqual(actual[1], randomLine);
            Assert.AreNotEqual(expected[5], randomLine);

        }

        [TestMethod]
        public void ReadFullFile()
        {
            //testing ReadAll method
            string path = $"{currentDirectory}\\TestedFiles\\Reading.txt";
            string actual = BaseFileWorker.ReadAll(path);
            string expected = File.ReadAllText(path);

            // multiple cases testing

            Assert.IsTrue(actual.Length > 0 && expected.Length > 0);
            Assert.IsTrue(actual.Contains("Lorem Ipsum has been the industry's standard dummy text ever since the 1500s,"));
            Assert.IsFalse(actual.Contains("NoWayItCanBeInThisText"));
            Assert.AreEqual(actual.Length, expected.Length);
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void WriteDataIntoFile()
        {
            //testing Write method
            string path1 = $"{currentDirectory}\\TestedFiles\\Writing.txt";
            string path2 = $"{currentDirectory}\\TestedFiles\\UkrainianText.txt";
            string incorrectPath = $"{currentDirectory}\\TestedFiles\\incorrectDatdandfgfmndt1";
            string data = "Easy texttT";
            string randomData = "blablablaqebqrbbeb";

            string ukrainian = "Всякому городу нрав і права" + "\n";
            //checking multiple cases

            Assert.IsTrue(BaseFileWorker.Write(data, path1));
            Assert.IsTrue(BaseFileWorker.Write(ukrainian, path2));
            Assert.IsTrue(BaseFileWorker.Write(data, incorrectPath));

            Assert.AreEqual(File.ReadAllText(path1), data);
            Assert.AreEqual(BaseFileWorker.ReadAll(path1), data);
            Assert.AreNotEqual(File.ReadAllText(path1), randomData);
        }



        [TestMethod]
        public void TryWriteTextGivenNumberOfTimes()
        {
            //testing TryWrite method

            string path = $"{currentDirectory}\\TestedFiles\\Writing.txt";
            string data = "try texT";
            string incorrectPath = $"{currentDirectory}\\TestedFiles\\incorrectData1";
            int number1 = 15;
            int number2 = 3;
            int number3 = 987654321;
            int number4 = 0;
            int number5 = -20;

            // testing cases
            Assert.IsTrue(BaseFileWorker.TryWrite(data, path, number1));
            Assert.IsTrue(BaseFileWorker.TryWrite(data, path, number3));
            Assert.IsFalse(BaseFileWorker.TryWrite(data, path, number4));
            Assert.IsFalse(BaseFileWorker.TryWrite(data, path, number5));

            Assert.IsTrue(BaseFileWorker.TryWrite(data, path, number2));
            Assert.IsTrue(BaseFileWorker.TryWrite(data, incorrectPath, number2));
            Assert.AreEqual(File.ReadAllText(path), data); 
        }

        [TestMethod]
        public void AddDataIntoFile()
        {
            //testing adding text to file

            string path = $"{currentDirectory}\\TestedFiles\\Adding.txt";
            string previousText = BaseFileWorker.ReadAll(path);
            string addedText = "werhw4rh45h4" + "\n";
            string fullText = previousText + addedText;

            //testing cases

            BaseFileWorker.Write(fullText, path);
            Assert.AreNotEqual(previousText, fullText);
        }

        [TestMethod]
        public void TryCopyTextFromFile()
        {
            //testing TryCopy method

            string sourcePath = $"{currentDirectory}\\TestedFiles\\Reading.txt";
            string expectedPath1 = $"{currentDirectory}\\TestedFiles\\RewriteCopying.txt";
            string expectedPath2 = $"{currentDirectory}\\TestedFiles\\NoRewriteCopying.txt";
            int number1 = 4;
            int number2 = 987654321;
            int number3 = 0;
            int number4 = -3;

            Assert.IsTrue(BaseFileWorker.TryCopy(sourcePath, expectedPath1, true, number1));
            Assert.IsTrue(BaseFileWorker.TryCopy(sourcePath, expectedPath1, true, number2));
            Assert.IsFalse(BaseFileWorker.TryCopy(sourcePath, expectedPath1, false, number3));
            Assert.IsFalse(BaseFileWorker.TryCopy(sourcePath, expectedPath1, false, number4));
            Assert.AreEqual(File.ReadAllText(sourcePath), File.ReadAllText(expectedPath1));
            Assert.ThrowsException<IOException>(() => BaseFileWorker.TryCopy(sourcePath, expectedPath2, false, number1));
        }


        [TestMethod]
        public void GetFileName()
        {
            //testing GetFileName method

            string path = $"{currentDirectory}\\TestedFiles\\Writing.txt";
            string incorrectPath = "blablabla\\Writing.txt";
            string fileName = "Writing.txt";
            string expectedPathByTestedLibrary1 = BaseFileWorker.GetFileName(path);
            string expectedPathByTestedLibrary2 = BaseFileWorker.GetFileName(incorrectPath);

            Assert.AreEqual(fileName, expectedPathByTestedLibrary1);
            Assert.AreNotEqual(fileName, expectedPathByTestedLibrary2);
            Assert.IsNull(expectedPathByTestedLibrary2);
        }


        [TestMethod]
        public void GetPathToFile()
        {
            // testing GetPath method

            string path = $"{currentDirectory}\\TestedFiles\\Writing.txt";
            string incorrectPath = "Nowhere\\anywhere\\Writing.txt";
            string expectedPathByTestedLibrary1 = BaseFileWorker.GetPath(path);
            string expectedPathByTestedLibrary2 = BaseFileWorker.GetPath(incorrectPath);
            string expectedPath = $"{currentDirectory}\\TestedFiles";

            //testing cases

            Assert.AreEqual(expectedPath, expectedPathByTestedLibrary1);
            Assert.AreNotEqual(expectedPathByTestedLibrary1, expectedPathByTestedLibrary2);
            Assert.IsNull(expectedPathByTestedLibrary2);
        }

        [TestMethod]
        public void GetFullPathToFile()
        {
            //testing GetFullPath method

            string path = $"{currentDirectory}\\TestedFiles\\Writing.txt";
            string incorrectPath = "blablabla\\Writing.txt";
            string expectedPath1 = BaseFileWorker.GetFullPath(path);
            string expectedPath2 = BaseFileWorker.GetFullPath(incorrectPath);

            //testing cases

            Assert.IsNotNull(expectedPath1);
            Assert.AreEqual(path, expectedPath1);
            Assert.AreNotEqual(path, expectedPath2);
            Assert.IsNull(expectedPath2);
        }



        [TestMethod]
        public void MkDirManyCases()
        {
            //testing MkDir method

            string correctPath = $"{currentDirectory}\\TestedFiles";
            string incorrectPath1 = $"{currentDirectory}\\aa.,?gvwqrb";
            string incorrectPath2 = $"{currentDirectory}ykyssherntertxt";
            string incorrectPath3 = "ykyssherntertxt";

            //testing multiple cases

            Assert.AreEqual(correctPath, BaseFileWorker.MkDir(correctPath));
            Assert.IsTrue(Directory.Exists(correctPath));
            Assert.IsFalse(Directory.Exists(incorrectPath1));
            Assert.ThrowsException<IOException>(() => BaseFileWorker.MkDir(incorrectPath1));
            Assert.AreNotEqual(BaseFileWorker.MkDir(incorrectPath2), null);

            BaseFileWorker.MkDir(incorrectPath3);
            Assert.IsTrue(Directory.Exists(incorrectPath3));
        }

    }
}
