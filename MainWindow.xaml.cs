using System.Windows;

namespace CVAssistant
{
    /// <summary>
    /// Main View
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModels.MainViewModel();
            DocViewer.IsScrollViewEnabled = false;
        }

    }
}
