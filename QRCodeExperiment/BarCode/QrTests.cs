using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
//using Aspose.BarCode;
//using Aspose.BarCodeRecognition;
using Faker;
using Lpa.DocFramework.AsposeWrapper;
//using Lpa.DocFramework.DocGenCore.BarCode;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Image = System.Drawing.Image;

namespace QRCodeExperiment
{
    [TestClass]
    public class QrTests
    {
        /// <summary> Test the maximum Character Length with Numbers only </summary>
        [TestCategory("RegressionTest")]
        [TestMethod]
        public void TestMaxTNumberLength()
        {
            var str = "1234567890";
            QrCode.Create(str); str += str; //20
            QrCode.Create(str); str += str; //40
            QrCode.Create(str); str += str; //80
            QrCode.Create(str); str += str; //160
            QrCode.Create(str); str += str; //320
            QrCode.Create(str); str += str; //640
            QrCode.Create(str); str += str; //1280
            QrCode.Create(str); str += str; //2560 max: 1852
            QrCode.Create(str); str += str; //5120 max: 4296
            Image bmp = QrCode.Create(str);
            bmp.Save("TestMaxTNumberLength.bmp", ImageFormat.Bmp);
            QrCode.Create(str); //20480
        }

        /// <summary> Test the maximum Character Length with ASCII-Characters  </summary>
        /// <remarks>
        ///     nine other characters: space, $ % * + - . / :
        /// </remarks>
        [TestMethod]
        [TestCategory("RegressionTest")]
        public void TestMaxTextLength()
        {
            var str = "ABCDEFGHIJKLMNOPQRSTUVWXYZ-1234567890_abcdefghijklmnopqrstuvwxyz"; //64
            QrCode.Create(str); str += str; //128
            QrCode.Create(str); str += str; //256
            QrCode.Create(str); str += str; //512
            QrCode.Create(str); str += str; //1024
            QrCode.Create(str); str += str; //2048 max: 1273.
            var bmp = QrCode.Create(str);
            bmp.Save("TestMaxTextLength.bmp", ImageFormat.Bmp);
            QrCode.Create(str); //8192 max: 
        }

        [TestCategory("RegressionTest")]
        [TestMethod]
        public void TestSpecialChars()
        {
            var str = "!\"§$%&/()=??`*'Äöüä;><,.-#d+´ß";
            var bmp = QrCode.Create(str);
            bmp.Save("C:/TestSpecialChars.jpg", ImageFormat.Bmp);
            var result = QrCode.ReadAsString(bmp);
            Assert.AreEqual(str, result);
        }

        [TestCategory("RegressionTest")]
        [TestMethod]
        public void TestNewLine()
        {
            const string str = "A\r\nB";
            var bmp = QrCode.Create(str);
            bmp.Save("TestNewLine.bmp", ImageFormat.Bmp);
            var result = QrCode.ReadAsString(bmp);
            Assert.AreEqual(str, result);
        }

        [TestCategory("RegressionTest")]
        [TestMethod]
        public void TestGeorgisch()
        {
            var str = "აზრდოებელი დედა) თანამედროვე მნიშვნელობ";
            var bmp = QrCode.Create(str);
            bmp.Save("TestGeorgisch.bmp", ImageFormat.Bmp);
            var result = QrCode.ReadAsString(bmp);
            Assert.AreEqual(str, result);
        }

        [TestCategory("RegressionTest")]
        [TestMethod]
        public void TestRussisch()
        {
            var str = "ў дачыненні да іншых скончаных";
            var bmp = QrCode.Create(str);
            bmp.Save("TestRussisch.bmp", ImageFormat.Bmp);
            var result = QrCode.ReadAsString(bmp);
            Assert.AreEqual(str, result);
        }

        [TestCategory("RegressionTest")]
        [TestMethod]
        public void TestChineseHanzuKanji()
        {
            var str = "梨阜埼茨栃";
            var bmp = QrCode.Create(str);
            bmp.Save("TestChineseHanzuKanji.bmp", ImageFormat.Bmp);
            var result = QrCode.ReadAsString(bmp);
            Assert.AreEqual(str, result);
        }

        [TestCategory("RegressionTest")]
        [TestMethod]
        public void TestJapaneseKatakana()
        {
            var str = "アイウエオカキクケコサシスセソタチツテトナニヌネノハヒフヘホマミム";
            var bmp = QrCode.Create(str);
            bmp.Save("TestJapaneseKatakana.bmp", ImageFormat.Bmp);
            var result = QrCode.ReadAsString(bmp);
            //Assert.IsNull(result, "This issue has been fixed by Aspose");
            var bytes = QrCode.ReadAsBinary(bmp);
            //Assert.IsNull(bytes, "This issue has been fixed by Aspose");
            Assert.AreEqual(str, result); //works with Mobile Phone
        }

        [TestCategory("RegressionTest")]
        [TestMethod]
        public void TestJapaneseHiragana()
        {
            var str = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむ";
            var bmp = QrCode.Create(str);
            bmp.Save("TestJapaneseHiragana.bmp", ImageFormat.Bmp);
            var result = QrCode.ReadAsString(bmp);
            //Assert.IsNull(result, "This issue has been fixed by Aspose");
            var bytes = QrCode.ReadAsBinary(bmp);
            //Assert.IsNull(bytes, "This issue has been fixed by Aspose");
            Assert.AreEqual(str, result); //works with Mobile Phone
        }

        [TestCategory("RegressionTest")]
        [TestMethod]
        public void TestArabic()
        {
            var str = "ه تعبير كان بيستخدم ";
            var bmp = QrCode.Create(str);
            bmp.Save("C:/TestArabic.png", ImageFormat.Bmp);
            var result = QrCode.ReadAsString(bmp);
            Assert.AreEqual(str, result);
        }

        [TestCategory("RegressionTest")]
        [TestMethod]
        public void TestRandomCharWithPattern()
        {
            CheckRandomCharWithPattern();
        }

        public void CheckRandomCharWithPattern()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            int numSuccess = 0;
            int numFails = 0;
            int sampleFail = 0;
            double successRate = 0;
            double elapsedMs = 0;

            for (int i = 999; i >= 0; i--)
            {
                string str = "";
                try
                {
                    var mandant = FakerRandom.Rand.Next(999);
                    int anzSeiten = FakerRandom.Rand.Next(99);
                    var numSeite = FakerRandom.Rand.Next(anzSeiten);
                    string codGoTyp = Lorem.GetWord();
                    string codGoNr = Lorem.GetWord();
                    int codHuelle = FakerRandom.Rand.Next(9999);
                    str = $"SAMDMS: MANDANT ={mandant}; NUMSEITE = {numSeite}; ANZSEITEN ={anzSeiten}; ABLAGE = (COD_GO_TYP = {codGoTyp}; COD_GO_NR ={codGoNr};COD_HUELLE ={codHuelle})";
                    var bmp = QrCode.Create(str);
                    bmp.Save("TestRandomCharWithPattern.bmp", ImageFormat.Bmp);
                    var result = QrCode.ReadAsString(bmp);
                    Assert.AreEqual(str, result);
                    numSuccess++;
                }
                catch (System.Exception x)
                {
                    numFails++;
                    if (sampleFail == 0)
                    {
                        Trace.WriteLine(message: $"{x.Message} with this Input: \n{str}");
                    }
                    sampleFail = 1;
                }
            }
            successRate = System.Math.Round((double)numSuccess / (numSuccess + numFails) * 100, 2);
            Trace.WriteLine("Success: " + numSuccess + ", Fail: " + numFails);
            Trace.WriteLine("Successrate: " + successRate + "%");
            watch.Stop();

            elapsedMs = watch.ElapsedMilliseconds / 1000;
            Trace.WriteLine("Elapsed Time: " + elapsedMs);
            Trace.WriteLine("");
        }

        [TestCategory("RegressionTest")]
        [TestMethod]
        public void TestRandomCharWithPattern2()
        {
            //var str = "SAMDMS: MANDANT =656; NUMSEITE = 7; ANZSEITEN =14; ABLAGE = (COD_GO_TYP = consectetur; COD_GO_NR =et;COD_HUELLE =1844)";
            var str = "COMMERZBANK AG\r\n244628794\r\n1956121CF\r\n12\r\n4484892";
            var bmp = QrCode.Create(str);
            bmp.Save("C:\\Users\\taod\\Downloads\\TestRandomCharWithPattern.bmp", ImageFormat.Bmp);
            var img = QrCode.DocxAsImage("C:\\Users\\taod\\Downloads\\TestBitmap.docx", 1);
            bmp = (Bitmap)img[0];
            bmp.Save("C:\\Users\\taod\\Downloads\\TestDocFile.bmp", ImageFormat.Bmp);
            var result = QrCode.ReadAsString(bmp);
            Assert.AreEqual(str, result);
        }

        private const string Dir = @"BarCode\";
        const string DOCX_QR_CODE = "QrCodeWord.docx";
        const string QRCODE_20MM_PDF = "QrCode_20mm.pdf";
        const string QRCODE_20MM_PNG = "NewTest.png";

        [TestCategory("RegressionTest")]
        [TestMethod]
        [DeploymentItem(Dir + QRCODE_20MM_PNG)]
        public void TestReadHiddenQrCodeBmp()
        {
            var bmp = new Bitmap(QRCODE_20MM_PNG);
            var qrContentString = QrCode.ReadAsString(bmp);
            Assert.AreEqual("SAMDMS:MANDANT={Spec~Cond3~DisplayValue};NUMSEITE=1;ANZSEITEN={12};ABLAGE=(COD_GO_TYP={COD_GO_TYPA};COD_GO_NR={CustomerID};COD_HUELLE={COD_HUELLEA};NAM_DOKUMENT={NAM_DOKUMENTA})", qrContentString);
        }

    }
}