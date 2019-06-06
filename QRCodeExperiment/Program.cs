using System;

namespace QRCodeExperiment
{
    class Program
    {
        static void Main(string[] args)
        {
            QrTests qrTests = new QrTests();
            //qrTests.TestArabic();
            //qrTests.TestChineseHanzuKanji();
            //qrTests.TestGeorgisch();
            //qrTests.TestJapaneseHiragana();
            //qrTests.TestJapaneseKatakana();
            qrTests.TestMaxTextLength();
            //qrTests.TestMaxTNumberLength();
            //qrTests.TestNewLine();
            //qrTests.TestReadHiddenQrCodeBmp();
            //qrTests.TestRussisch();
            //qrTests.TestSpecialChars();
        }
    }
}
