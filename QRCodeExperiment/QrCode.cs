using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
//using Aspose.BarCode;
//using Aspose.BarCodeRecognition;
using Aspose.Words;
using Aspose.Words.Saving;
using Lpa.DocFramework.AsposeWrapper;
using ZXing;
using ZXing.Common;

namespace QRCodeExperiment
{
    /// <summary> Component Registration and Extension Methods to handle QR Codes </summary>
    /// <remarks>
    /// QR Codes support numeric[0-9], alphanumeric[0–9A–Z\ \$\%\*\+\-\.\/\:], byte/binary (interpreted as ISO 8859-1), and kanji (Shift JIS X 0208), 
    /// but Aspose supports only the first three. 
    /// Need to use binary Encoding for anything else. 
    /// See <a href="https://en.wikipedia.org/wiki/QR_code#Encoding">QR Code</a> and
    /// <a href="https://github.com/zxing/zxing/wiki/Barcode-Contents">Barcode Contents</a> is considered Text, 
    /// but internet Protocols are recognized by Clients: URLs, mailto: addresses and vCard: Data.
    /// iCal Info is possible. 
    /// </remarks>
    public static class QrCode
    {
        static QrCode()
        {
            AsposeBarCodeInit.Init();
        }

        private static Encoding ISO_8859_1 = Encoding.GetEncoding("iso-8859-1");
        private static float RESOLUTION = 100;

        //private const QREncodeMode DefaultQrEncodeMode = QREncodeMode.Binary;

        /// <summary> Level of Reed-Solomon error correction. From low to high: LevelL, LevelM, LevelQ, LevelH. </summary>
        /// <remarks>
        /// LevelL. Allows recovery of 7% of the code text
        /// LevelM. Allows recovery of 15% of the code text
        /// LevelQ. Allows recovery of 25% of the code text
        /// LevelH. Allows recovery of 30% of the code text
        /// 
        /// </remarks>
        //private const QRErrorLevel DefaultQrErrorLevel = QRErrorLevel.LevelH;

        #region default create methods

        public static Bitmap Create(string contents) => CreateBitmap(contents);

        public static Image CreateEmf(byte [] contents) => CreateImage(contents);

        #endregion

        #region qr reader


        public static byte[] ReadAsBinary(byte[] qr, BarcodeFormat type = BarcodeFormat.QR_CODE)
        {
            using (var ms = new MemoryStream(qr))
            {
                IBarcodeReader reader = new BarcodeReader();

                var result = reader.DecodeMultiple(qr, 50, 50, RGBLuminanceSource.BitmapFormat.RGB32);

                if (result != null)
                {
                    return result[0].RawBytes;
                }
                return null;

            }
        }

        public static byte[] ReadAsBinary(Bitmap bmp, BarcodeFormat type = BarcodeFormat.QR_CODE)
        {
            IBarcodeReader reader = new BarcodeReader();

            var result = reader.DecodeMultiple(bmp);

            if (result != null)
            {
                return result[0].RawBytes;
            }
            return null;
        }

        public static string ReadAsString(Bitmap bmp, BarcodeFormat type = BarcodeFormat.QR_CODE)
        {
            //BarCodeReader reader = new BarCodeReader(bmp, type) { RecognitionMode = RecognitionMode.MaxQuality };

            IBarcodeReader reader = new BarcodeReader();
            // detect and decode the barcode inside the bitmap
            var result = reader.DecodeMultiple(bmp);
            // do something with the result
            if (result != null)
            {
                return result[0].Text;
            }
            return null;
        }

        public static string ReadAsString(byte[] qr, BarcodeFormat type = BarcodeFormat.QR_CODE)
        {
            using (var ms = new MemoryStream(qr))
            {
                IBarcodeReader reader = new BarcodeReader();
                var result = reader.DecodeMultiple(qr, 50, 50, RGBLuminanceSource.BitmapFormat.RGB32);

                if (result != null)
                {
                    return result[0].Text;
                }
                return null;
            }
        }

        #endregion

        /// <summary> Allowed <paramref name="resolution"/> are  </summary>
        private static Bitmap CreateBitmap(string contents)
        {
            Bitmap bitmap = CreateBitmapImage(contents);

            return bitmap;

        }

        private static Image CreateImage(this byte[] content)
        {
            return Image.FromStream(new MemoryStream(content));
        }

        private static Bitmap CreateBitmapImage(string contents)
        {
            var qrcodeBitmap = CreateQrBuilder(contents);


            return qrcodeBitmap;
        }

        private static byte[] CreateEmfBytes(string contents)
        {
            var builder = CreateQrBuilder(contents);
            var emfImage = new MemoryStream();
            //builder.(emfImage);
            return emfImage.ToArray();
        }

        private static Bitmap CreateQrBuilder(string contents)
        {

            var bytes = Encoding.UTF8.GetBytes(contents);
            contents = ISO_8859_1.GetString(bytes);

            BarcodeWriter builder = new BarcodeWriter()
            {
                Format = BarcodeFormat.QR_CODE
            };

            Bitmap qrcodeBitmap;
            var result = builder.Write(contents);
            qrcodeBitmap = new Bitmap(result);

            return qrcodeBitmap;
        }

        public static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        public static List<Image> DocxAsImage(byte[] data, int maxNumPages)
        {
            var ret = new List<Image>();
            using (var input = new MemoryStream(data))
            {
                DocXAsImage(maxNumPages, ret, input);
            }
            return ret;
        }

        public static List<Image> DocxAsImage(string filePath, int maxNumPages)
        {
            var ret = new List<Image>();
            using (var input = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                DocXAsImage(maxNumPages, ret, input);
            }
            return ret;
        }

        private static void DocXAsImage(int maxNumPages, List<Image> ret, Stream input)
        {
            var document = new Document(input, new LoadOptions { LoadFormat = LoadFormat.Docx });
            int numPages = Math.Min(maxNumPages, document.PageCount);
            for (int i = 0; i < numPages; i++)
            {
                using (var output = new MemoryStream())
                {
                    document.Save(output, new ImageSaveOptions(SaveFormat.Png) { Resolution = RESOLUTION, PageIndex = i });
                    ret.Add(Image.FromStream(output));
                }
            }
        }
    }
}