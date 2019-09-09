using GUI.Pages;
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

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Pages

        Main mainPage = new Main();
        LeagueBans leagueBansPage = new LeagueBans();
        Settings settingsPage = new Settings();

        #endregion


        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnNavHome_Click(object sender, RoutedEventArgs e)
        {
            Main.Navigate(mainPage);
        }

        private void BtnNavBans_Click(object sender, RoutedEventArgs e)
        {
            Main.Navigate(leagueBansPage);
        }

        private void BtnNavSettings_Click(object sender, RoutedEventArgs e)
        {
            Main.Navigate(settingsPage);
        }
    }
}
