using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using Aspose.BarCode;
using Faker;
//using Lpa.DocFramework.DocGenCore.BarCode;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Image = System.Drawing.Image;

namespace QRCodeReader_Aspose_19_5
{
    [TestClass]
    public class QrTests195 
    {
        ///// <summary> Test the maximum Character Length with Numbers only </summary>
        //[TestCategory("RegressionTest")]
        //[TestMethod]
        //[ExpectedException(typeof (InvalidCodeException), "Max available length - 7089.")]
        //public void TestMaxTNumberLength()
        //{
        //    var str = "1234567890";
        //    QrCode195.Create(str); str += str; //20
        //    QrCode195.Create(str); str += str; //40
        //    QrCode195.Create(str); str += str; //80
        //    QrCode195.Create(str); str += str; //160
        //    QrCode195.Create(str); str += str; //320
        //    QrCode195.Create(str); str += str; //640
        //    QrCode195.Create(str); str += str; //1280
        //    QrCode195.Create(str, QREncodeMode.AlphaNumber); str += str; //2560 max: 1852
        //    QrCode195.Create(str, QREncodeMode.Numeric); str += str; //5120 max: 4296
        //    Image bmp = QrCode195.Create(str, QREncodeMode.Numeric, QRErrorLevel.LevelL);
        //    bmp.Save("TestMaxTNumberLength.bmp", ImageFormat.Bmp);
        //    Image emf = QrCode195.CreateEmf(str, QREncodeMode.Numeric, QRErrorLevel.LevelL);
        //    emf.Save("TestMaxTNumberLength.emf", ImageFormat.Emf); str += str; //10240 max: 7089.
        //    QrCode195.Create(str, QREncodeMode.Numeric, QRErrorLevel.LevelL); //20480
        //}

        ///// <summary> Test the maximum Character Length with ASCII-Characters  </summary>
        ///// <remarks>
        /////     nine other characters: space, $ % * + - . / :
        ///// </remarks>
        //[TestMethod]
        //[TestCategory("RegressionTest")]
        //[ExpectedException(typeof (InvalidCodeException), "Max available length - 2953.")]
        //public void TestMaxTextLength()
        //{
        //    var str = "ABCDEFGHIJKLMNOPQRSTUVWXYZ-1234567890_abcdefghijklmnopqrstuvwxyz"; //64
        //    QrCode195.Create(str); str += str; //128
        //    QrCode195.Create(str); str += str; //256
        //    QrCode195.Create(str); str += str; //512
        //    QrCode195.Create(str); str += str; //1024
        //    QrCode195.Create(str); str += str; //2048 max: 1273.
        //    var bmp = QrCode195.Create(str, QRErrorLevel.LevelM);
        //    bmp.Save("TestMaxTextLength.bmp", ImageFormat.Bmp);
        //    var emf = QrCode195.CreateEmf(str, QREncodeMode.Binary, QRErrorLevel.LevelM);
        //    emf.Save("TestMaxTextLength.emf", ImageFormat.Emf); str += str; //4096 max: 2953
        //    QrCode195.Create(str, QREncodeMode.AlphaNumber, QRErrorLevel.LevelL); //8192 max: 
        //}

        //[TestCategory("RegressionTest")]
        //[TestMethod]
        //public void TestSpecialChars()
        //{
        //    var str = "!\"§$%&/()=??`*'Äöüä;><,.-#d+´ß";
        //    var bmp = QrCode195.Create(str);
        //    bmp.Save("TestSpecialChars.bmp", ImageFormat.Bmp);
        //    var emf = QrCode195.CreateEmf(str, QREncodeMode.Binary, QRErrorLevel.LevelM);
        //    emf.Save("TestSpecialChars.emf", ImageFormat.Emf);
        //    var result = QrCode195.ReadAsString(bmp);
        //    Assert.AreEqual(str, result);
        //}

        //[TestCategory("RegressionTest")]
        //[TestMethod]
        //public void TestNewLine()
        //{
        //    const string str = "A\r\nB";
        //    var bmp = QrCode195.Create(str);
        //    bmp.Save("TestNewLine.bmp", ImageFormat.Bmp);
        //    var emf = QrCode195.CreateEmf(str, QREncodeMode.Binary, QRErrorLevel.LevelM);
        //    emf.Save("TestNewLine.emf", ImageFormat.Emf);
        //    var result = QrCode195.ReadAsString(bmp);
        //    Assert.AreEqual(str, result);
        //}

        //[TestCategory("RegressionTest")]
        //[TestMethod]
        //public void TestGeorgisch()
        //{
        //    var str = "აზრდოებელი დედა) თანამედროვე მნიშვნელობ";
        //    var bmp = QrCode195.Create(str);
        //    bmp.Save("TestGeorgisch.bmp", ImageFormat.Bmp);
        //    var emf = QrCode195.CreateEmf(str, QREncodeMode.Binary, QRErrorLevel.LevelM);
        //    emf.Save("TestGeorgisch.emf", ImageFormat.Emf);
        //    var result = QrCode195.ReadAsString(bmp);
        //    Assert.AreEqual(str, result);
        //}

        //[TestCategory("RegressionTest")]
        //[TestMethod]
        //public void TestRussisch()
        //{
        //    var str = "ў дачыненні да іншых скончаных";
        //    var bmp = QrCode195.Create(str);
        //    bmp.Save("TestRussisch.bmp", ImageFormat.Bmp);
        //    var emf = QrCode195.CreateEmf(str, QREncodeMode.Binary, QRErrorLevel.LevelM);
        //    emf.Save("TestRussisch.emf", ImageFormat.Emf);
        //    var result = QrCode195.ReadAsString(bmp);
        //    Assert.AreEqual(str, result);
        //}

        //[TestCategory("RegressionTest")]
        //[TestMethod]
        //public void TestChineseHanzuKanji()
        //{
        //    var str = "梨阜埼茨栃";
        //    var bmp = QrCode195.Create(str);
        //    bmp.Save("TestChineseHanzuKanji.bmp", ImageFormat.Bmp);
        //    var emf = QrCode195.CreateEmf(str, QREncodeMode.Binary, QRErrorLevel.LevelM);
        //    emf.Save("TestChineseHanzuKanji.emf", ImageFormat.Emf);
        //    var result = QrCode195.ReadAsString(bmp);
        //    Assert.AreEqual(str, result);
        //}

        //[TestCategory("RegressionTest")]
        //[TestMethod]
        //public void TestJapaneseKatakana()
        //{
        //    var str = "アイウエオカキクケコサシスセソタチツテトナニヌネノハヒフヘホマミム";
        //    var bmp = QrCode195.Create(str);
        //    bmp.Save("TestJapaneseKatakana.bmp", ImageFormat.Bmp);
        //    var emf = QrCode195.CreateEmf(str, QREncodeMode.Binary, QRErrorLevel.LevelM);
        //    emf.Save("TestJapaneseKatakana.emf", ImageFormat.Emf);
        //    var result = QrCode195.ReadAsString(bmp);
        //    Assert.IsNull(result, "This issue has been fixed by Aspose");
        //    var bytes = QrCode195.ReadAsBinary(bmp);
        //    Assert.IsNull(bytes, "This issue has been fixed by Aspose");
        //    //Assert.AreEqual(str, result); //works with Mobile Phone
        //}

        //[TestCategory("RegressionTest")]
        //[TestMethod]
        //public void TestJapaneseHiragana()
        //{
        //    var str = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむ";
        //    var bmp = QrCode195.Create(str);
        //    bmp.Save("TestJapaneseHiragana.bmp", ImageFormat.Bmp);
        //    var emf = QrCode195.CreateEmf(str, QREncodeMode.Binary, QRErrorLevel.LevelM);
        //    emf.Save("TestJapaneseHiragana.emf", ImageFormat.Emf);
        //    var result = QrCode195.ReadAsString(bmp);
        //    Assert.IsNull(result, "This issue has been fixed by Aspose");
        //    var bytes = QrCode195.ReadAsBinary(bmp);
        //    Assert.IsNull(bytes, "This issue has been fixed by Aspose");
        //    //Assert.AreEqual(str, result); //works with Mobile Phone
        //}

        //[TestCategory("RegressionTest")]
        //[TestMethod]
        //public void TestArabic()
        //{
        //    var str = "ه تعبير كان بيستخدم ";
        //    var bmp = QrCode195.Create(str);
        //    bmp.Save("TestArabic.bmp", ImageFormat.Bmp);
        //    var emf = QrCode195.CreateEmf(str, QREncodeMode.Binary, QRErrorLevel.LevelM);
        //    emf.Save("TestArabic.emf", ImageFormat.Emf);
        //    var result = QrCode195.ReadAsString(bmp);
        //    Assert.AreEqual(str, result);
        //}

        private const string Dir = @"BarCode\";
        const string DOCX_QR_CODE = "QrCodeWord.docx";
        const string QRCODE_20MM_PDF = "QrCode_20mm.pdf";
        const string QRCODE_20MM_PNG = "QrCode_20mm.png";

        [TestCategory("RegressionTest")]
        [TestMethod]
        [DeploymentItem(Dir + QRCODE_20MM_PNG)]
        public void TestReadHiddenQrCodeBmp()
        {
            var bmp = new Bitmap(QRCODE_20MM_PNG);
            var qrContentString = QrCode195.ReadAsString(bmp);
            Assert.AreEqual("Hello world!", qrContentString);
        }


        [TestCategory("RegressionTest")]
        [TestMethod]
        public void TestRandomCharWithPattern()
        {
            CheckRandomCharWithPattern(QRErrorLevel.LevelL);
            CheckRandomCharWithPattern(QRErrorLevel.LevelM);
            CheckRandomCharWithPattern(QRErrorLevel.LevelQ);
            CheckRandomCharWithPattern(QRErrorLevel.LevelH);

            CheckRandomCharWithPattern(QRErrorLevel.LevelL);
            CheckRandomCharWithPattern(QRErrorLevel.LevelM);
            CheckRandomCharWithPattern(QRErrorLevel.LevelQ);
            CheckRandomCharWithPattern(QRErrorLevel.LevelH);
        }

        public void CheckRandomCharWithPattern(QRErrorLevel errLevel)
        {
            Trace.WriteLine(errLevel);
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
                    var bmp = QrCode195.Create(str, errLevel);
                    bmp.Save("TestRandomCharWithPattern.bmp", ImageFormat.Bmp);
                    var emf = QrCode195.CreateEmf(str, QREncodeMode.Binary, QRErrorLevel.LevelM);
                    emf.Save("TestRandomCharWithPattern.emf", ImageFormat.Emf);
                    var result = QrCode195.ReadAsString(bmp);
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
            var mandant = FakerRandom.Rand.Next(999);
            int anzSeiten = FakerRandom.Rand.Next(99);
            var numSeite = FakerRandom.Rand.Next(anzSeiten);
            string codGoTyp = Lorem.GetWord();
            string codGoNr = Lorem.GetWord();
            int codHuelle = FakerRandom.Rand.Next(9999);
            //var str = "SAMDMS: MANDANT =181; NUMSEITE = 4; ANZSEITEN =7; ABLAGE = (COD_GO_TYP = facilis; COD_GO_NR =tempore;COD_HUELLE =3258)";
            var str = "SAMDMS: MANDANT =922; NUMSEITE = 6; ANZSEITEN =76; ABLAGE = (COD_GO_TYP = dignissimos; COD_GO_NR =qui;COD_HUELLE =6834)";
            var bmp = QrCode195.Create(str);
            bmp.Save("C:\\Users\\taod\\Downloads\\TestRandomCharWithPattern.bmp", ImageFormat.Bmp);
            var emf = QrCode195.CreateEmf(str, QREncodeMode.Binary, QRErrorLevel.LevelM);
            emf.Save("TestRandomCharWithPattern.emf", ImageFormat.Emf);
            var result = QrCode195.ReadAsString(bmp);
            Assert.AreEqual(str, result);
        }

        /*[TestCategory("RegressionTest")]
        [TestMethod]
        [DeploymentItem(Dir + DOCX_QR_CODE)]
        public void TestGenerateQrCodeWord_DownsampleTrue()
        {
            var result = CallMergerCorePositive(DOCX_QR_CODE, out _);
            var pdf = PdfUtils.DocxAsPdf(result.GetRawFile(), true, PdfFormat.PDF_A_1B, TraceFactory.CreateTraceObject(false), true, 0, CultureInfo.InvariantCulture, PasswordConfiguration.None);

            List<Image> images;
            using (var stream = new MemoryStream(pdf))
            {
                images = new Document(stream).GetImages();
            }

            var qrContentString = QrCode195.ReadAsString((Bitmap)images[0]);

            Assert.AreEqual("Hello world!", qrContentString);
        }

        [TestCategory("RegressionTest")]
        [TestMethod]
        [DeploymentItem(Dir + DOCX_QR_CODE)]
        public void TestGenerateQrCodeWord_DownsampleTrue_300()
        {
            var result = CallMergerCorePositive(DOCX_QR_CODE, out _);
            var pdf = PdfUtils.DocxAsPdf(result.GetRawFile(), true, PdfFormat.PDF_A_1B, TraceFactory.CreateTraceObject(false), true, 300, CultureInfo.InvariantCulture, PasswordConfiguration.None);

            List<Image> images;
            using (var stream = new MemoryStream(pdf))
            {
                images = new Document(stream).GetImages();
            }

            var qrContentString = QrCode195.ReadAsString((Bitmap)images[0]);

            Assert.AreEqual("Hello world!", qrContentString);
        }

        [TestCategory("RegressionTest")]
        [TestMethod]
        [DeploymentItem(Dir + DOCX_QR_CODE)]
        public void TestGenerateQrCodeWord_DownsampleTrue_negativ100()
        {
            var result = CallMergerCorePositive(DOCX_QR_CODE, out _);
            var pdf = PdfUtils.DocxAsPdf(result.GetRawFile(), true, PdfFormat.PDF_A_1B, TraceFactory.CreateTraceObject(false), true, -100, CultureInfo.InvariantCulture, PasswordConfiguration.None);

            List<Image> images;
            using (var stream = new MemoryStream(pdf))
            {
                images = new Document(stream).GetImages();
            }

            var qrContentString = QrCode195.ReadAsString((Bitmap)images[0]);

            Assert.AreEqual("Hello world!", qrContentString);
        }

        [TestCategory("RegressionTest")]
        [TestMethod]
        [DeploymentItem(Dir + DOCX_QR_CODE)]
        public void TestGenerateQrCodeWord_DownsampleFalse()
        {
            var result = CallMergerCorePositive(DOCX_QR_CODE, out _);
            var pdf = PdfUtils.DocxAsPdf(result.GetRawFile(), true, PdfFormat.PDF_A_1B, TraceFactory.CreateTraceObject(false), false, 0, CultureInfo.InvariantCulture, PasswordConfiguration.None);

            List<Image> images;
            using (var stream = new MemoryStream(pdf))
            {
                images = new Document(stream).GetImages();
            }

            var qrContentString = QrCode195.ReadAsString((Bitmap)images[0]);

            Assert.AreEqual("Hello world!", qrContentString);
        }

        [TestCategory("RegressionTest")]
        [TestMethod]
        [DeploymentItem(Dir + DOCX_QR_CODE)]
        public void TestGenerateQrCodeWord_DownsampleFalse_100()
        {
            var result = CallMergerCorePositive(DOCX_QR_CODE, out _);
            var pdf = PdfUtils.DocxAsPdf(result.GetRawFile(), true, PdfFormat.PDF_A_1B, TraceFactory.CreateTraceObject(false), true, 100, CultureInfo.InvariantCulture, PasswordConfiguration.None);

            List<Image> images;
            using (var stream = new MemoryStream(pdf))
            {
                images = new Document(stream).GetImages();
            }

            var qrContentString = QrCode195.ReadAsString((Bitmap)images[0]);

            Assert.AreEqual("Hello world!", qrContentString);
        }

        [TestCategory("RegressionTest")]
        [TestMethod]
        [DeploymentItem(Dir + "QrCodeWord2.docx")]
        public void TestGenerateQrCodeWord2()
        {
            var variables = new Variables();
            


            var str = "1234567890";
            ITrace trace;
            int notResolved;
            for (int i = 0; i < 7; i++)
            {
                variables["QrContent"] = new FormattableValue(str);

                AFragment result = CallMergerCoreOnly("QrCodeWord2.docx", 1000, out trace, out notResolved, variables);

                Assert.IsNotNull(result, "the merger returns a document");
                Assert.IsFalse(trace.ContainsErrors, "No error expecetd");
                Assert.AreEqual(0, notResolved, "No unresoved links");
                
                var pdf = PdfUtils.DocxAsPdf(result.GetRawFile(), true, PdfFormat.PDF_A_1B, TraceFactory.CreateTraceObject(false), true, 0, CultureInfo.InvariantCulture, PasswordConfiguration.None);

                List<Image> images;
                using (var stream = new MemoryStream(pdf))
                {
                    images = new Document(stream).GetImages();
                }

                var qrContentString = QrCode195.ReadAsString((Bitmap)images[0]);

                Assert.AreEqual(str, qrContentString);

                str += str;
            }

            variables["QrContent"] = new FormattableValue(str);
            var errorResult = CallMergerCoreOnly("QrCodeWord2.docx", 1000, out trace, out notResolved, variables);

            Assert.IsNotNull(errorResult, "the merger returns a document");
            Assert.IsTrue(trace.ContainsErrors, "Error expecetd");

            var dump = trace.DumpError();

            StringAssert.Contains(dump, "Maximum characters limit reached for Binary encode mode and LevelH error correction level. Encoded data length - 1280. Max available length - 1273.");
        }

        /// <summary> Submitted to Aspose: https://forum.aspose.com/t/attached-bitmap-and-pdf-not-readable-by-barcodereader/192195/2 </summary>

        /// <summary> Submitted to Aspose: https://forum.aspose.com/t/attached-bitmap-and-pdf-not-readable-by-barcodereader/192195/2 </summary>

        [TestCategory("RegressionTest")]
        [TestMethod]
        [DeploymentItem(Dir + QRCODE_20MM_PDF)]
        public void TestReadHiddenQrCode() 
        {
            var bytes = File.ReadAllBytes(QRCODE_20MM_PDF);
            var pdf   = bytes.AsPdf();

            List<Image> images = pdf.GetImages();
            
            var qrContentString = QrCode195.ReadAsString((Bitmap)images[0]);

            Assert.AreEqual("Hello world!", qrContentString);
        }

        [TestCategory("RegressionTest")]
        [TestMethod]
        [DeploymentItem(Dir + "QrCodePowerpoint.pptx")]
        public void TestGenerateQrCodePowerpoint()
        {
            var result = CallMergerCorePositive("QrCodePowerpoint.pptx", out _);

            var pdf = PdfUtils.PptxAsPdf(result.GetRawFile(), true, PdfFormat.PDF_A_1B, PasswordConfiguration.None, TraceFactory.CreateTraceObject(false));

            List<Image> images;
            using (var stream = new MemoryStream(pdf))
            {
                images = new Document(stream).GetImages();
            }
            
            var qrContentString = QrCode195.ReadAsString((Bitmap)images[0]);

            Assert.AreEqual("Hello world!", qrContentString);
        }*/
    }
}