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
    /// Logique d'interaction pour MultiplayerPage.xaml
    /// </summary>
    public partial class MultiplayerPage : Page
    {
        public MultiplayerPage()
        {
            InitializeComponent();
        }


        private void J1Jedi_MouseEnter(object sender, MouseEventArgs e)
        {
            EntitiesLayer.Partie game = BusinessLayer.PartieManager.getCurrentGame();
            if (game.Tournament != null && (this.J1Jedi.ComboJedi.SelectedItem as ViewModel.Jedi.JediViewModel) == null 
                && (this.J2Jedi.ComboJedi.SelectedItem as ViewModel.Jedi.JediViewModel) == null)
            {
                BusinessLayer.JediTournamentManager jtm = new BusinessLayer.JediTournamentManager();                

                List<EntitiesLayer.Jedi> jedis = new List<EntitiesLayer.Jedi>();
                foreach (EntitiesLayer.Match m in game.Tournament.Matchs)
                {
                    if(m.Jedi1 != null)
                        jedis.Add(m.Jedi1);
                    if(m.Jedi2 != null)
                        jedis.Add(m.Jedi2);
                }

                IList<EntitiesLayer.Jedi> jedis2 = jedis;
                ViewModel.Jedi.JedisModelView jvm = new ViewModel.Jedi.JedisModelView(jedis);
                J1Jedi.DataContext = jvm;

                ViewModel.Jedi.JedisModelView jvm2 = new ViewModel.Jedi.JedisModelView(jedis);
                J2Jedi.DataContext = jvm2;
            }
        }

        private void J1Jedi_MouseLeave(object sender, MouseEventArgs e)
        {
            if ((this.J1Jedi.ComboJedi.SelectedItem as ViewModel.Jedi.JediViewModel) != null)
                BusinessLayer.PartieManager.getCurrentGame().Jedi_j1 = (this.J1Jedi.ComboJedi.SelectedItem as ViewModel.Jedi.JediViewModel).Jedi;
        }

        private void J2Jedi_MouseLeave(object sender, MouseEventArgs e)
        {
            if ((this.J2Jedi.ComboJedi.SelectedItem as ViewModel.Jedi.JediViewModel) != null)
                BusinessLayer.PartieManager.getCurrentGame().Jedi_j2 = (this.J2Jedi.ComboJedi.SelectedItem as ViewModel.Jedi.JediViewModel).Jedi;
        }
    }
}
