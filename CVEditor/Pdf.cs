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
            byte[] imageToByteArray(System.Drawing.Image imageori)
            {
                using (var ms = new MemoryStream())
                {
                    imageori.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    return ms.ToArray();
                }
            }

            try
            {
                PdfReader reader = new PdfReader(FilePath);
                //select pages from the original document
                reader.SelectPages("1");
                //create PdfStamper object to write to get the pages from reader 
                PreviewFileName = $"{Guid.NewGuid()}.pdf";
                PdfStamper stamper = new PdfStamper(reader, new FileStream(PreviewFileName, FileMode.Create));
                // PdfContentByte from stamper to add content to the pages over the original content
                PdfContentByte pbover = stamper.GetOverContent(1);
                //add content to the page using ColumnText
 
                string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), FontName);
                BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                Font font = new Font(baseFont, FontSize, Font.NORMAL);

                ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(Disclaimer, font), PosX, PosY, 0);
                // PdfContentByte from stamper to add content to the pages under the original content
                PdfContentByte pbunder = stamper.GetUnderContent(1);
                //add image from a file 
                //iTextSharp.text.Image img = new Jpeg(imageToByteArray(System.Drawing.Image.FromFile("d:/baby.jpg")));
                //add the image under the original content
                //pbunder.AddImage(img, img.Width / 2, 0, 0, img.Height / 2, 0, 0);

                //close the stamper
                stamper.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
