using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CVEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetScreen(Stages.Intro);
        }

        private void SetScreen(Stages newScreen)
        {
            gridIntro.Visibility = Visibility.Hidden;
            gridLoader.Visibility = Visibility.Hidden;
            gridDisclaimer.Visibility = Visibility.Hidden;
            gridPreview.Visibility = Visibility.Hidden;

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
                case Stages.Preview:
                    gridPreview.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SetScreen(Stages.Loader);
        }
    }
}
