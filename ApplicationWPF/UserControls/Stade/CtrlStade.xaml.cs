using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ApplicationWPF.UserControls
{
    /// <summary>
    /// Interaction logic for CtrlStade.xaml
    /// </summary>
    public partial class CtrlStade : UserControl
    {
        public CtrlStade()
        {
            InitializeComponent();

            // Recuperation des caracteristiques
            BusinessLayer.JediTournamentManager jtm = new BusinessLayer.JediTournamentManager();
            List<EntitiesLayer.Caracteristique> caracs = jtm.getCaracteristiques();

            caracBox.ItemsSource = (from x in caracs
                                    where x.Type == EntitiesLayer.ETypeCaracteristique.Stade
                                    select x.Nom).ToList();
        }

        private void caracBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext != null)
            {
                BusinessLayer.JediTournamentManager jtm = new BusinessLayer.JediTournamentManager();
                string carac = caracBox.SelectedItem as string;

                EntitiesLayer.Caracteristique car = (from x in jtm.getCaracteristiques()
                                                     where x.Nom == carac &&
                                                     x.Type == EntitiesLayer.ETypeCaracteristique.Stade
                                                     select x).FirstOrDefault();

                ViewModel.Stade.StadeViewModel svm = DataContext as ViewModel.Stade.StadeViewModel;
                List<EntitiesLayer.Caracteristique> stadeCarac = svm.Caracteristiques;
                stadeCarac.Add(car);
                svm.Caracteristiques = stadeCarac;
                displayCarac.Items.Refresh();
            }
        }

        private void displayCarac_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext != null)
            {
                EntitiesLayer.Caracteristique car = displayCarac.SelectedItem as EntitiesLayer.Caracteristique;

                ViewModel.Stade.StadeViewModel svm = DataContext as ViewModel.Stade.StadeViewModel;
                List<EntitiesLayer.Caracteristique> stadeCarac = svm.Caracteristiques;
                stadeCarac.Remove(car);
                svm.Caracteristiques = stadeCarac;
                displayCarac.Items.Refresh();
            }
        }
    }
}
