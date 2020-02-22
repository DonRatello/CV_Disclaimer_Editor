using iTextSharp.text;
using iTextSharp.text.pdf;

using System;
using System.Globalization;
using System.IO;
using System.Windows;

namespace CVEditor
{
    public class Pdf
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string PreviewFileName { get; set; }

        string _fontName;
        public string FontName
        {
            get => _fontName;
            set
            {
                _fontName = value;
                RegistryHandler.WriteKey(Constants.RegistryKeys.FontName, _fontName);
            }
        }

        float _fontSize;
        public float FontSize
        {
            get => _fontSize;
            set
            {
                _fontSize = value;
                RegistryHandler.WriteKey(Constants.RegistryKeys.FontSize, _fontSize.ToString(CultureInfo.InvariantCulture));
            }
        }

        string _disclaimer;
        public string Disclaimer
        {
            get => _disclaimer;
            set
            {
                _disclaimer = value;
                RegistryHandler.WriteKey(Constants.RegistryKeys.Disclaimer, _disclaimer);
            }
        }

        float _posX;
        public float PosX
        {
            get => _posX;
            set
            {
                _posX = value;
                RegistryHandler.WriteKey(Constants.RegistryKeys.PosX, _posX.ToString(CultureInfo.InvariantCulture));
            }
        }

        float _posY;
        public float PosY
        {
            get => _posY;
            set
            {
                _posY = value;
                RegistryHandler.WriteKey(Constants.RegistryKeys.PosY, _posY.ToString(CultureInfo.InvariantCulture));
            }
        }

        int _lineHeight;
        public int LineHeight
        {
            get => _lineHeight;
            set
            {
                _lineHeight = value;
                RegistryHandler.WriteKey(Constants.RegistryKeys.LineHeight, _lineHeight.ToString(CultureInfo.InvariantCulture));
            }
        }

        public Pdf()
        {
            try
            {
                if (Directory.Exists("Preview"))
                {
                    Directory.Delete("Preview", true);
                    
                }

                Directory.CreateDirectory("Preview");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void AddDisclaimer()
        {
            try
            {
                PdfReader reader = new PdfReader(FilePath);
                reader.SelectPages("1");
                PreviewFileName = $"{Guid.NewGuid()}.pdf";
                var stamper = new PdfStamper(reader, new FileStream(Path.Combine("Preview", PreviewFileName), FileMode.Create));
                PdfContentByte pbover = stamper.GetOverContent(1);

                string fontPath = Path.Combine("Fonts", FontName);
                BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                Font font = new Font(baseFont, FontSize, Font.NORMAL, BaseColor.WHITE);

                float previousLineY = PosY;
                foreach(string line in Disclaimer.Split(new[] { '\r', '\n' }))
                {
                    previousLineY -= LineHeight;
                    ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(line, font), PosX, previousLineY, 0);
                }

                PdfContentByte pbunder = stamper.GetUnderContent(1);
                stamper.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
