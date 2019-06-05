using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCodeRecognition;
using Aspose.Pdf;
using Lpa.DocFramework.AsposeWrapper;
using Lpa.DocFramework.Contracts;
using Lpa.DocFramework.DocGenCore.BarCode;
using Lpa.DocFramework.DocGenCore.Fragments;
using Lpa.DocFramework.DocGenService.UnitTest.Merger.PDF;
using Lpa.DocFramework.ExpressionEvaluator.Data;
using Lpa.DocFramework.Helper.Trace;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Image = System.Drawing.Image;

namespace Lpa.DocFramework.DocGenService.UnitTest.BarCode
{
    [TestClass]
    public class QrTests 
    {
        /// <summary> Test the maximum Character Length with Numbers only </summary>
        [TestCategory("RegressionTest")]
        [TestMethod]
        [ExpectedException(typeof (InvalidCodeException), "Max available length - 7089.")]
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
            QrCode.Create(str, QREncodeMode.AlphaNumber); str += str; //2560 max: 1852
            QrCode.Create(str, QREncodeMode.Numeric); str += str; //5120 max: 4296
            ImageFragment bmp = QrCode.Create(str, QREncodeMode.Numeric, QRErrorLevel.LevelL);
            bmp.Save(fileName: "TestMaxTNumberLength.bmp");
            ImageFragment emf = QrCode.CreateEmf(str, QREncodeMode.Numeric, QRErrorLevel.LevelL);
            emf.Save(fileName: "TestMaxTNumberLength.emf"); str += str; //10240 max: 7089.
            QrCode.Create(str, QREncodeMode.Numeric, QRErrorLevel.LevelL); //20480
        }

        /// <summary> Test the maximum Character Length with ASCII-Characters  </summary>
        /// <remarks>
        ///     nine other characters: space, $ % * + - . / :
        /// </remarks>
        [TestMethod]
        [TestCategory("RegressionTest")]
        [ExpectedException(typeof (InvalidCodeException), "Max available length - 2953.")]
        public void TestMaxTextLength()
        {
            var str = "ABCDEFGHIJKLMNOPQRSTUVWXYZ-1234567890_abcdefghijklmnopqrstuvwxyz"; //64
            QrCode.Create(str); str += str; //128
            QrCode.Create(str); str += str; //256
            QrCode.Create(str); str += str; //512
            QrCode.Create(str); str += str; //1024
            QrCode.Create(str); str += str; //2048 max: 1273.
            var bmp = QrCode.Create(str, QRErrorLevel.LevelM);
            bmp.Save(fileName: "TestMaxTextLength.bmp");
            var emf = QrCode.CreateEmf(str, QREncodeMode.Binary, QRErrorLevel.LevelM);
            emf.Save(fileName: "TestMaxTextLength.emf"); str += str; //4096 max: 2953
            QrCode.Create(str, QREncodeMode.AlphaNumber, QRErrorLevel.LevelL); //8192 max: 
        }

        [TestCategory("RegressionTest")]
        [TestMethod]
        public void TestSpecialChars()
        {
            var str = "!\"§$%&/()=??`*'Äöüä;><,.-#d+´ß";
            var bmp = QrCode.Create(str);
            bmp.Save(fileName: "TestSpecialChars.bmp");
            var emf = QrCode.CreateEmf(str, QREncodeMode.Binary, QRErrorLevel.LevelM);
            emf.Save(fileName: "TestSpecialChars.emf");
            var result = QrCode.ReadAsString(bmp.GetRawFile());
            Assert.AreEqual(str, result);
        }

        [TestCategory("RegressionTest")]
        [TestMethod]
        public void TestNewLine()
        {
            const string str = "A\r\nB";
            var bmp = QrCode.Create(str);
            bmp.Save(fileName: "TestNewLine.bmp");
            var emf = QrCode.CreateEmf(str, QREncodeMode.Binary, QRErrorLevel.LevelM);
            emf.Save(fileName: "TestNewLine.emf");
            var result = QrCode.ReadAsString(bmp.GetRawFile());
            Assert.AreEqual(str, result);
        }

        [TestCategory("RegressionTest")]
        [TestMethod]
        public void TestGeorgisch()
        {
            var str = "აზრდოებელი დედა) თანამედროვე მნიშვნელობ";
            var bmp = QrCode.Create(str);
            bmp.Save(fileName: "TestGeorgisch.bmp");
            var emf = QrCode.CreateEmf(str, QREncodeMode.Binary, QRErrorLevel.LevelM);
            emf.Save(fileName: "TestGeorgisch.emf");
            var result = QrCode.ReadAsString(bmp.GetRawFile());
            Assert.AreEqual(str, result);
        }

        [TestCategory("RegressionTest")]
        [TestMethod]
        public void TestRussisch()
        {
            var str = "ў дачыненні да іншых скончаных";
            var bmp = QrCode.Create(str);
            bmp.Save(fileName: "TestRussisch.bmp");
            var emf = QrCode.CreateEmf(str, QREncodeMode.Binary, QRErrorLevel.LevelM);
            emf.Save(fileName: "TestRussisch.emf");
            var result = QrCode.ReadAsString(bmp.GetRawFile());
            Assert.AreEqual(str, result);
        }

        [TestCategory("RegressionTest")]
        [TestMethod]
        public void TestChineseHanzuKanji()
        {
            var str = "梨阜埼茨栃";
            var bmp = QrCode.Create(str);
            bmp.Save(fileName: "TestChineseHanzuKanji.bmp");
            var emf = QrCode.CreateEmf(str, QREncodeMode.Binary, QRErrorLevel.LevelM);
            emf.Save(fileName: "TestChineseHanzuKanji.emf");
            var result = QrCode.ReadAsString(bmp.GetRawFile());
            Assert.AreEqual(str, result);
        }

        [TestCategory("RegressionTest")]
        [TestMethod]
        public void TestJapaneseKatakana()
        {
            var str = "アイウエオカキクケコサシスセソタチツテトナニヌネノハヒフヘホマミム";
            var bmp = QrCode.Create(str);
            bmp.Save(fileName: "TestJapaneseKatakana.bmp");
            var emf = QrCode.CreateEmf(str, QREncodeMode.Binary, QRErrorLevel.LevelM);
            emf.Save(fileName: "TestJapaneseKatakana.emf");
            var result = QrCode.ReadAsString(bmp.GetRawFile());
            Assert.IsNull(result, "This issue has been fixed by Aspose");
            var bytes = QrCode.ReadAsBinary(bmp.GetRawFile());
            Assert.IsNull(bytes, "This issue has been fixed by Aspose");
            //Assert.AreEqual(str, result); //works with Mobile Phone
        }

        [TestCategory("RegressionTest")]
        [TestMethod]
        public void TestJapaneseHiragana()
        {
            var str = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむ";
            var bmp = QrCode.Create(str);
            bmp.Save(fileName: "TestJapaneseHiragana.bmp");
            var emf = QrCode.CreateEmf(str, QREncodeMode.Binary, QRErrorLevel.LevelM);
            emf.Save(fileName: "TestJapaneseHiragana.emf");
            var result = QrCode.ReadAsString(bmp.GetRawFile());
            Assert.IsNull(result, "This issue has been fixed by Aspose");
            var bytes = QrCode.ReadAsBinary(bmp.GetRawFile());
            Assert.IsNull(bytes, "This issue has been fixed by Aspose");
            //Assert.AreEqual(str, result); //works with Mobile Phone
        }

        [TestCategory("RegressionTest")]
        [TestMethod]
        public void TestArabic()
        {
            var str = "ه تعبير كان بيستخدم ";
            var bmp = QrCode.Create(str);
            bmp.Save(fileName: "TestArabic.bmp");
            var emf = QrCode.CreateEmf(str, QREncodeMode.Binary, QRErrorLevel.LevelM);
            emf.Save(fileName: "TestArabic.emf");
            var result = QrCode.ReadAsString(bmp.GetRawFile());
            Assert.AreEqual(str, result);
        }

        private const string Dir = @"BarCode\";
        const string DOCX_QR_CODE = "QrCodeWord.docx";
        const string QRCODE_20MM_PDF = "QrCode_20mm.pdf";
        const string QRCODE_20MM_PNG = "QrCode_20mm.png";

        [TestCategory("RegressionTest")]
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

            var qrContentString = QrCode.ReadAsString((Bitmap)images[0]);

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

            var qrContentString = QrCode.ReadAsString((Bitmap)images[0]);

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

            var qrContentString = QrCode.ReadAsString((Bitmap)images[0]);

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

            var qrContentString = QrCode.ReadAsString((Bitmap)images[0]);

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

            var qrContentString = QrCode.ReadAsString((Bitmap)images[0]);

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

                var qrContentString = QrCode.ReadAsString((Bitmap)images[0]);

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

        [TestCategory("RegressionTest")]
        [TestMethod]
        [DeploymentItem(Dir + QRCODE_20MM_PNG)]
        public void TestReadHiddenQrCodeBmp() 
        {
            var bmp = new Bitmap(QRCODE_20MM_PNG);
            var qrContentString = QrCode.ReadAsString(bmp, BarCodeReadType.AllSupportedTypes);
            Assert.AreEqual("Hello world!", qrContentString);
        }

        /// <summary> Submitted to Aspose: https://forum.aspose.com/t/attached-bitmap-and-pdf-not-readable-by-barcodereader/192195/2 </summary>

        [TestCategory("RegressionTest")]
        [TestMethod]
        [DeploymentItem(Dir + QRCODE_20MM_PDF)]
        public void TestReadHiddenQrCode() 
        {
            var bytes = File.ReadAllBytes(QRCODE_20MM_PDF);
            var pdf   = bytes.AsPdf();

            List<Image> images = pdf.GetImages();
            
            var qrContentString = QrCode.ReadAsString((Bitmap)images[0]);

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
            
            var qrContentString = QrCode.ReadAsString((Bitmap)images[0]);

            Assert.AreEqual("Hello world!", qrContentString);
        }
    }
}