using ApplicationWPF.Frames;
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

namespace ApplicationWPF.UserControls
{
    /// <summary>
    /// Logique d'interaction pour ComboBoxTournoi.xaml
    /// </summary>
    public partial class ComboBoxTournoi : UserControl
    {
        public ComboBoxTournoi()
        {
            InitializeComponent();
        }

        private void cbTournoi_MouseLeave(object sender, EventArgs e)
        {
            ViewModel.Tournament.TournamentViewModel t = this.cbTournoi.SelectedItem as ViewModel.Tournament.TournamentViewModel;
            if (t != null)
            {
                BusinessLayer.PartieManager.setCurrentGameTournament(t.Tournament);
                
            }
        }
    }
}
