using System;
using System.Globalization;
using System.Windows;
using System.IO;
using Microsoft.Win32;

namespace CVEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Pdf pdf;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            pdf = new Pdf();
            SetScreen(Stages.Intro);

            RegistryHandler.CheckMainSettings();

            try
            {
                txtDisclaimer.Text = RegistryHandler.ReadKey(Constants.RegistryKeys.Disclaimer);
                sliderSize.Value = double.Parse(RegistryHandler.ReadKey(Constants.RegistryKeys.FontSize));
                txtPosX.Text = RegistryHandler.ReadKey(Constants.RegistryKeys.PosX);
                txtPosY.Text = RegistryHandler.ReadKey(Constants.RegistryKeys.PosY);
                txtLineHeight.Text = RegistryHandler.ReadKey(Constants.RegistryKeys.LineHeight);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                txtDisclaimer.Text = string.Empty;
                sliderSize.Value = 0;
                txtPosX.Text = "0";
                txtPosY.Text = "0";
                txtLineHeight.Text = "0";
            }
        }

        private void SetScreen(Stages newScreen)
        {
            gridIntro.Visibility = Visibility.Hidden;
            gridLoader.Visibility = Visibility.Hidden;
            gridDisclaimer.Visibility = Visibility.Hidden;
            gridFinish.Visibility = Visibility.Hidden;

            switch (newScreen)
            {
                case Stages.Intro:
                    gridIntro.Visibility = Visibility.Visible;
                    break;
                case Stages.Loader:
                    gridLoader.Visibility = Visibility.Visible;
                    break;
                case Stages.Disclaimer:
                    gridDisclaimer.Visibility = Visibility.Visible;
                    break;
                case Stages.Finish:
                    gridFinish.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            SetScreen(Stages.Loader);
        }

        private void btnPickPdf_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var openFileDlg = new OpenFileDialog
                {
                    Multiselect = false,
                    Filter = "PDFy (.pdf)|*.pdf"
                };
                bool? result = openFileDlg.ShowDialog();

                if (result.HasValue && result.Value)
                {
                    pdf.FileName = openFileDlg.SafeFileName;
                    pdf.FilePath = openFileDlg.FileName;
                }

                SetScreen(Stages.Disclaimer);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void btnShowPreview_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                pdf.Disclaimer = txtDisclaimer.Text;
                pdf.PosX = float.Parse(txtPosX.Text);
                pdf.PosY = float.Parse(txtPosY.Text);
                pdf.FontSize = (float)Math.Round(sliderSize.Value, 0);
                pdf.LineHeight = int.Parse(txtLineHeight.Text);
                pdf.FontName = RegistryHandler.ReadKey(Constants.RegistryKeys.FontName);
                pdf.AddDisclaimer();
                System.Diagnostics.Process.Start(Path.Combine("Preview", pdf.PreviewFileName));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void sliderSize_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            txtSize.Content = Math.Round(sliderSize.Value, 0).ToString(CultureInfo.InvariantCulture);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var saveFileDlg = new SaveFileDialog
                {
                    Filter = "PDFy (.pdf)|*.pdf"
                };
                bool? result = saveFileDlg.ShowDialog();

                if (result.HasValue && result.Value)
                {
                    string sourceFileName = Path.Combine("Preview", pdf.PreviewFileName);
                    string newFileName = saveFileDlg.FileName;
                    File.Copy(sourceFileName, newFileName);
                }

                SetScreen(Stages.Finish);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
