
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections.Generic;
using System.Linq;

namespace Game.Utility
{

    public static class PrintDevice
    {
        public class DrawSize
        {
            public DrawSize(float width, float height)
            {
                this.width = width;
                this.height = height;
            }

            public static readonly DrawSize one = new DrawSize(1f, 1f);
            public float width { get; private set; }
            public float height { get; private set; }

            public static DrawSize operator *(DrawSize s, float mul)
            {
                return new DrawSize(s.width * mul, s.height * mul);
            }
        }

        static PrintDocument _printDocument = new PrintDocument();
        static public bool isFinish
        {
            get; private set;
        }

        static PrintDevice()
        {
            if (_printDocument == null) throw new NullReferenceException("print document is null");
            _printDocument.BeginPrint += new PrintEventHandler(BeginPrint);
            _printDocument.EndPrint += new PrintEventHandler(EndPrint);
        }


        private static void BeginPrint(object sender,PrintEventArgs e)
        {
            isFinish = false;
        }

        private static void EndPrint(object sender, PrintEventArgs e)
        {
            isFinish = true;
        }


        public static bool PrintRequest(string path, string printerName, bool color)
        {
            if (!BindImage(path)) { return false; }
            PrintImage(printerName, color);
            _image.Dispose();
            _image = null;
            return true;
        }

        public static bool PrintRequest(string path, DrawSize size, string printerName, bool color)
        {
            PrintDevice.size = size;
            return PrintRequest(path, printerName, color);
        }

        static Image _image = null;
        public static DrawSize size { get; set; }

        static bool BindImage(string path)
        {
            _image = Image.FromFile(path);
            return _image != null;
        }

        static void PrintImage(string printerName, bool color)
        {
            _printDocument.DocumentName = "ar marker";
            _printDocument.DefaultPageSettings.PrinterSettings.PrinterName = printerName;
            _printDocument.DefaultPageSettings.Color = color;
            if (!_printDocument.DefaultPageSettings.PrinterSettings.IsValid) throw new NotSupportedException("有効なプリンターがないです");
            _printDocument.PrintPage += new PrintPageEventHandler(PrintEventAction);
            _printDocument.Print();
            _printDocument.PrintPage -= new PrintPageEventHandler(PrintEventAction);
        }

        static void PrintEventAction(object sender, PrintPageEventArgs args)
        {
            var image = _image;
            var margin = args.MarginBounds;
            args.Graphics.DrawImage(image, margin.Left, margin.Top, size.width, size.height);
            args.HasMorePages = false;
            image.Dispose();
        }

        public static IEnumerable<string> GetPrinterNames()
        {
            foreach (string name in PrinterSettings.InstalledPrinters)
            {
                yield return name;
            }
        }

        public static bool GetPrinterColorConfig(bool type)
        {
            var printerSettings = _printDocument.DefaultPageSettings;
            printerSettings.Color = type;
            return printerSettings.Color;
        }

        public static bool GetPrinterColorConfig()
        {
            var printerSettings = _printDocument.DefaultPageSettings;
            return printerSettings.Color;
        }

        /// <summary>
        /// プリンターが有効か
        /// </summary>
        /// <param name="printName">プリンターの名前</param>
        /// <returns></returns>
        public static bool IsValid(string printName)
        {
            var printDocument = new PrintDocument();
            var printerSettings = printDocument.PrinterSettings;
            printerSettings.PrinterName = printName;
            return printerSettings.IsValid;
        }

        /// <summary>
        /// 有効なプリンターかあるかどうか調べる
        /// </summary>
        /// <returns></returns>
        public static bool isValid
        {
            get
            {
                var name = GetPrinterNames();
                return name.Count() != 0;
            }
        }

        /// <summary>
        /// カラー印刷ができるか
        /// </summary>
        /// <param name="printName">プリンターの名前</param>
        /// <returns></returns>
        public static bool CanSupportsColor(string printName)
        {
            var printDocument = new PrintDocument();
            var printerSettings = printDocument.PrinterSettings;
            printerSettings.PrinterName = printName;
            return printerSettings.SupportsColor;
        }

        /// <summary>
        /// 使用可能な用紙サイズの名前一覧を取得
        /// </summary>
        /// <param name="printName">プリンターの名前</param>
        /// <returns></returns>
        public static IEnumerable<string> GetPaperSizes(string printName)
        {
            var printDocument = new PrintDocument();
            var printerSettings = printDocument.PrinterSettings;
            printerSettings.PrinterName = printName;

            foreach (PaperSize paperSize in printerSettings.PaperSizes)
            {
                yield return paperSize.PaperName;
            }
        }

        /// <summary>
        /// 印刷する向きを設定
        /// </summary>
        /// <param name="isBeside">
        /// true:横
        /// false:縦
        /// </param>
        public static void SetLandScape(bool isBeside)
        {
            var printDocument = new PrintDocument();
            var printerSettings = printDocument.DefaultPageSettings;
            printerSettings.Landscape = isBeside;
        }
    }
}
