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

namespace ApplicationWPF.Frames.PlayPageFrame
{
    /// <summary>
    /// Logique d'interaction pour PariMultiplayer.xaml
    /// </summary>
    public partial class PariMultiplayer : Page
    {
        public PariMultiplayer()
        {
            InitializeComponent();
        }

        private void Bourse_j1_LostFocus(object sender, RoutedEventArgs e)
        {
            string val = this.Bourse_j1.Text;

            try
            {
                int bourse = int.Parse(val);

                if (bourse < 0)
                {
                    this.Bourse_j1.Text = "0";
                    BusinessLayer.PartieManager.getCurrentGame().Bourse_j1 = 0;
                }
                else
                {
                    BusinessLayer.PartieManager.getCurrentGame().Bourse_j1 = bourse;
                }


            }
            catch (Exception)
            {
                this.Bourse_j1.Text = "0";
                BusinessLayer.PartieManager.getCurrentGame().Bourse_j1 = 0;
            }

        }

        private void Bourse_j2_LostFocus(object sender, RoutedEventArgs e)
        {
            string val = this.Bourse_j2.Text;

            try
            {
                int bourse = int.Parse(val);

                if (bourse < 0)
                {
                    this.Bourse_j2.Text = "0";
                    BusinessLayer.PartieManager.getCurrentGame().Bourse_j2 = 0;
                }
                else
                {
                    BusinessLayer.PartieManager.getCurrentGame().Bourse_j2 = bourse;
                }


            }
            catch (Exception)
            {
                this.Bourse_j2.Text = "0";
                BusinessLayer.PartieManager.getCurrentGame().Bourse_j2 = 0;
            }

        }
    }
}
