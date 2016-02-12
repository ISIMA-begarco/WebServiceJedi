using BusinessLayer;
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

namespace JediTournamentWPF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class ConnexionWindow : Window
    {
        public ConnexionWindow()
        {
            InitializeComponent();
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Width/2 - this.Width/2;
            this.Top = desktopWorkingArea.Height / 2 - this.Height/2;
        }

        public void Connexion_Button_OnClick(object sender, RoutedEventArgs e) {

            if (!Login_TextBox.Text.Equals("") && !Password_TextBox.Password.Equals(""))
            {
                if (JediTournamentManager.CheckConnexionUser(Login_TextBox.Text, Password_TextBox.Password))
                {
                    ApplicationWPF.MainWindow mw = new ApplicationWPF.MainWindow();
                    mw.Show();
                    this.Close();
                }
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
