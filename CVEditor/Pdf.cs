using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;
using System.Windows;

namespace CVEditor
{
    public class Pdf
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string PreviewFileName { get; set; }
        public string FontName { get; set; }
        public float FontSize { get; set; } 
        public string Disclaimer { get; set; }
        public float PosX { get; set; } 
        public float PosY { get; set; }

        public void AddDisclaimer()
        {
            try
            {
                PdfReader reader = new PdfReader(FilePath);
                reader.SelectPages("1");
                PreviewFileName = $"{Guid.NewGuid()}.pdf";
                PdfStamper stamper = new PdfStamper(reader, new FileStream(PreviewFileName, FileMode.Create));
                PdfContentByte pbover = stamper.GetOverContent(1);

                string fontPath = Path.Combine("Fonts", FontName);
                BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                Font font = new Font(baseFont, FontSize, Font.NORMAL, BaseColor.WHITE);

                ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(Disclaimer, font), PosX, PosY, 0);
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
