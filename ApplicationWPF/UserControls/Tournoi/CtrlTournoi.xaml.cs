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
using Microsoft.Win32;

namespace ApplicationWPF.UserControls
{
    /// <summary>
    /// Interaction logic for CtrlTournoi.xaml
    /// </summary>
    public partial class CtrlTournoi : UserControl
    {
        public CtrlTournoi()
        {
            InitializeComponent();

            // Recuperation des matchs
            BusinessLayer.JediTournamentManager jtm = new BusinessLayer.JediTournamentManager();
            List<EntitiesLayer.Jedi> jedis1 = (from x in jtm.getMatches() select x.Jedi1).ToList();
            List<EntitiesLayer.Jedi> jedis2 = (from x in jtm.getMatches() select x.Jedi2).ToList();

            List<string> matchs = new List<string>();
            for (int i = 0; i < jedis1.Count; i++)
            {
                if (jedis1[i] != null && jedis2[i] != null)
                {
                    string jedi1 = "Inconnu";
                    string jedi2 = "Inconnu";
                    string stade = jtm.getMatches()[i].Stade.Planete;

                    if (jedis1[i] != null)
                        jedi1 = jedis1[i].Nom;
                    if (jedis1[i] != null)
                        jedi2 = jedis2[i].Nom;

                    matchs.Add(jedi1 + " VS " + jedi2 + " (" + stade + ")");
                }
            }

            Match1.ItemsSource = matchs;
            Match2.ItemsSource = matchs;
            Match3.ItemsSource = matchs;
            Match4.ItemsSource = matchs;
            Match5.ItemsSource = matchs;
            Match6.ItemsSource = matchs;
            Match7.ItemsSource = matchs;
            Match8.ItemsSource = matchs;
        }
    }
}
