using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.Generation;
//using Lpa.DocFramework.AsposeWrapper;

namespace QRCodeReader_Aspose_19_5
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
    
    public static class QrCode195
    {
        static QrCode195()
        {
            Aspose.BarCode.License license = new Aspose.BarCode.License();
            license.SetLicense("Aspose.BarCode.lic");
        }
        
        private static Encoding ISO_8859_1 = Encoding.GetEncoding("iso-8859-1");
        private const QREncodeMode DefaultQrEncodeMode = QREncodeMode.Bytes;

        /// <summary> Level of Reed-Solomon error correction. From low to high: LevelL, LevelM, LevelQ, LevelH. </summary>
        /// <remarks>
        /// LevelL. Allows recovery of 7% of the code text
        /// LevelM. Allows recovery of 15% of the code text
        /// LevelQ. Allows recovery of 25% of the code text
        /// LevelH. Allows recovery of 30% of the code text
        /// 
        /// </remarks>
        private const QRErrorLevel DefaultQrErrorLevel = QRErrorLevel.LevelH;

        #region default create methods

        public static Bitmap Create(string contents) => CreateBitmap(contents);

        public static Bitmap Create(string contents, QREncodeMode mode) => CreateBitmap(contents, mode: mode);

        public static Bitmap Create(string contents, QRErrorLevel errorLevel) => CreateBitmap(contents, errorLevel: errorLevel);

        public static Bitmap Create(string contents, QREncodeMode mode, QRErrorLevel errorLevel) => CreateBitmap(contents, mode: mode, errorLevel: errorLevel);

        public static Bitmap Create(string contents, int resolution) => CreateBitmap(contents, resolution);

        public static Bitmap Create(string contents, int resolution, QREncodeMode mode) => CreateBitmap(contents, resolution, mode);

        public static Bitmap Create(string contents, int resolution, QRErrorLevel errorLevel) => CreateBitmap(contents, resolution, errorLevel: errorLevel);

        public static Bitmap Create(string contents, int resolution, QREncodeMode mode, QRErrorLevel errorLevel)
            => CreateBitmap(contents, resolution, mode, errorLevel);

        public static Image CreateEmf(string contents, QREncodeMode mode, QRErrorLevel errorLevel)
            => CreateImage(CreateEmfBytes(contents, mode, errorLevel));

        #endregion

        #region qr reader


        //public static byte[] ReadAsBinary(byte[] qr, BarCodeReadType type = BarCodeReadType.QR)
        //{
        //    using (var ms = new MemoryStream(qr))
        //    {
        //        BarCodeReader reader = new BarCodeReader(ms, type);
        //        if (reader.Read())
        //        {
        //            return reader.GetCodeBytes();
        //        }
        //        return null;
        //    }
        //}

        //public static byte[] ReadAsBinary(Bitmap bmp, BarCodeReadType type = BarCodeReadType.QR)
        //{
        //    BarCodeReader reader = new BarCodeReader(bmp, type);
        //    if (reader.Read())
        //    {
        //        return reader.GetCodeBytes();
        //    }
        //    return null;
        //}

        public static string ReadAsString(Bitmap bmp, BaseDecodeType type = null)
        {
            if (null == type)
            {
                type = DecodeType.QR;
            }
            BarCodeReader reader = new BarCodeReader(bmp, type);
            if (reader.Read())
            {
                return reader.GetCodeText();
            }
            return null;
        }

        //public static string ReadAsString(byte[] qr, BarCodeReadType type = BarCodeReadType.QR)
        //{
        //    using (var ms = new MemoryStream(qr))
        //    {
        //        BarCodeReader reader = new BarCodeReader(ms, type);
        //        if (reader.Read())
        //        {
        //            return reader.GetCodeText();
        //        }
        //        return null;
        //    }
        //}

        #endregion

        /// <summary> Allowed <paramref name="resolution"/> are  </summary>
        private static Bitmap CreateBitmap(string contents
            , int? resolution = 150, QREncodeMode mode = DefaultQrEncodeMode
            , QRErrorLevel errorLevel = DefaultQrErrorLevel)
        {
            Bitmap bitmap = CreateBitmapImage(contents, resolution, mode, errorLevel);
            return bitmap;
            /*using (var ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                var content = ms.ToArray();
                return CreateImage(content);
                //return new Image(content, "QR.png");
            }*/
        }

        private static Image CreateImage(this byte[] content)
        {
            return Image.FromStream(new MemoryStream(content));
        }

        private static Bitmap CreateBitmapImage(string contents, int? resolution, QREncodeMode mode, QRErrorLevel errorLevel)
        {
            var builder = CreateQrBuilder(contents, mode, errorLevel);

            if (resolution.HasValue)
                return builder.GetCustomSizeBarCodeImage(new Size(resolution.Value, resolution.Value), false);

            return builder.GenerateBarCodeImage();
        }

        private static byte[] CreateEmfBytes(string contents, QREncodeMode mode, QRErrorLevel errorLevel)
        {
            var builder = CreateQrBuilder(contents, mode, errorLevel);
            var emfImage = new MemoryStream();
            builder.Save(emfImage, BarCodeImageFormat.Emf);
            return emfImage.ToArray();
        }

        private static BarCodeBuilder CreateQrBuilder(string contents, QREncodeMode mode, QRErrorLevel errorLevel)
        {
            if (mode == QREncodeMode.Bytes)
            {
                var bytes = Encoding.UTF8.GetBytes(contents);
                contents = ISO_8859_1.GetString(bytes);
            }

            BarCodeBuilder builder = new BarCodeBuilder(contents, EncodeTypes.QR)
            {
                QRErrorLevel = errorLevel,
                QREncodeMode = mode,
                EnableEscape = true,
                CaptionAbove = new Aspose.BarCode.Caption { Visible = false },
                CaptionBelow = new Aspose.BarCode.Caption { Visible = false }
            };
            return builder;
        }
    }
}