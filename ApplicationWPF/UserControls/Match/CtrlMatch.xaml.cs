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
    /// Interaction logic for CtrlMatch.xaml
    /// </summary>
    public partial class CtrlMatch : UserControl
    {
        public CtrlMatch()
        {
            InitializeComponent();

            BusinessLayer.JediTournamentManager jtm = new BusinessLayer.JediTournamentManager();

            // Recuperation des jedis
            jedi1.ItemsSource = (from x in jtm.getJedis() select x.Nom).ToList();
            jedi2.ItemsSource = (from x in jtm.getJedis() select x.Nom).ToList();

            // Recuperation des stades
            stade.ItemsSource = (from x in jtm.getStades() select x.Planete).ToList();

            // Recuperation des phases
            phaseTournoi.ItemsSource = Enum.GetValues(typeof(EntitiesLayer.EPhaseTournoi)).Cast<EntitiesLayer.EPhaseTournoi>();
        }
    }
}
