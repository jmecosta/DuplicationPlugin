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

namespace StandaloneDuplicationTracker
{
    using ExtensionTypes;

    using ProjectDuplicationTracker;

    /// <summary>
    /// Interaction logic for DuplicationUserControl.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonLogin_OnClick(object sender, RoutedEventArgs e)
        {
            this.Hide();

            var password = this.Password.Password;
            var hostname = this.Host.Text;
            var login = this.Login.Text;

            var model = new ProjectDuplicationTrackerModel();
            model.Login();
            var win = new Window();
            var view = new DuplicationUserControl(model);
            win.Content = view;
            win.SizeToContent = SizeToContent.WidthAndHeight;
            win.ShowDialog();
            this.Close();
        }
    }
}
