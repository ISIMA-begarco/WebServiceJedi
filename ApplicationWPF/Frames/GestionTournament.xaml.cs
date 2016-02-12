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

using ApplicationWPF.ViewModel;

namespace ApplicationWPF.Frames
{
    /// <summary>
    /// Interaction logic for GestionTournament.xaml
    /// </summary>
    public partial class GestionTournament : Page, IFrameNavigator
    {
        public event EventHandler<FrameChangedEventArgs> m_changeFrame;

        public EventHandler<FrameChangedEventArgs> OnFrameChanged
        {
            get { return m_changeFrame; }
            set { m_changeFrame = value; }
        }

        public GestionTournament()
        {
            InitializeComponent();
        }

        private void ButtonSoft_Loaded(object sender, RoutedEventArgs e)
        {
            //passage à la page newJedi depuis le boutton Ajouter
            Uri uri = new Uri("Frames/NewJediPagexaml.xaml", UriKind.Relative);
            this.NavigationService.Navigate(uri);
        }

        private void ButtonBak_Event(object sender, EventArgs e)
        {
            string nextFrame = "Frames/MainMenu.xaml";
            OnFrameChanged(this, new FrameChangedEventArgs(nextFrame));
        }

        private void JediLoaded(object sender, RoutedEventArgs e)
        {
            // Initialisation des Jedis
            BusinessLayer.JediTournamentManager jtm = new BusinessLayer.JediTournamentManager();
            IList<EntitiesLayer.Jedi> jedis = jtm.getJedis();
            ViewModel.Jedi.JedisModelView jvm = new ViewModel.Jedi.JedisModelView(jedis);
            ucJedis.DataContext = jvm;
        }

        private void JediUnloaded(object sender, RoutedEventArgs e)
        {
            BusinessLayer.JediTournamentManager jtm = new BusinessLayer.JediTournamentManager();
            ViewModel.Jedi.JedisModelView jvm = ucJedis.DataContext as ViewModel.Jedi.JedisModelView;
            List<EntitiesLayer.Jedi> jedis = new List<EntitiesLayer.Jedi>();
            foreach(ViewModel.Jedi.JediViewModel j in jvm.Jedis)
            {
                if (j.Nom != "" && j.ImageUri.OriginalString != "")
                    jedis.Add(j.Jedi);
            }
            jtm.updateJedis(jedis);
        }

        private void StadeLoaded(object sender, RoutedEventArgs e)
        {
            // Initialisation des Stade
            BusinessLayer.JediTournamentManager jtm = new BusinessLayer.JediTournamentManager();
            IList<EntitiesLayer.Stade> stades = jtm.getStades();
            ViewModel.Stade.StadesViewModel svm = new ViewModel.Stade.StadesViewModel(stades);
            ucStade.DataContext = svm;
        }

        private void StadeUnloaded(object sender, RoutedEventArgs e)
        {
            BusinessLayer.JediTournamentManager jtm = new BusinessLayer.JediTournamentManager();
            ViewModel.Stade.StadesViewModel svm = ucStade.DataContext as ViewModel.Stade.StadesViewModel;
            List<EntitiesLayer.Stade> stades = new List<EntitiesLayer.Stade>();
            foreach (ViewModel.Stade.StadeViewModel s in svm.Stades)
            {
                if (s.Planete != "" && s.ImageUri.OriginalString != "")
                    stades.Add(s.Stade);
            }
            jtm.updateStades(stades);
        }

        private void MatchLoaded(object sender, RoutedEventArgs e)
        {
            // Initialisation des Matchs
            BusinessLayer.JediTournamentManager jtm = new BusinessLayer.JediTournamentManager();
            List<EntitiesLayer.Match> matchs = jtm.getMatchsNonEmpty();

            ViewModel.Match.MatchsViewModel mvm = new ViewModel.Match.MatchsViewModel(matchs);
            ucMatchs.DataContext = mvm;
        }

        private void MatchUnloaded(object sender, RoutedEventArgs e)
        {
            BusinessLayer.JediTournamentManager jtm = new BusinessLayer.JediTournamentManager();
            ViewModel.Match.MatchsViewModel mvm = ucMatchs.DataContext as ViewModel.Match.MatchsViewModel;
            List<EntitiesLayer.Match> matchs = new List<EntitiesLayer.Match>();
            foreach (ViewModel.Match.MatchViewModel m in mvm.Matchs)
            {
                matchs.Add(m.Match);
            }
            matchs.AddRange(jtm.getMatchsEmpty());
            jtm.updateMatches(matchs);
        }

        private void TournoiLoaded(object sender, RoutedEventArgs e)
        {
            // Initialisation des Tournois
            BusinessLayer.JediTournamentManager jtm = new BusinessLayer.JediTournamentManager();
            IList<EntitiesLayer.Tournoi> tournois = jtm.getTournois();
            ViewModel.Tournament.TournamentsViewModel tvm = new ViewModel.Tournament.TournamentsViewModel(tournois);
            ucTournoi.DataContext = tvm;
        }

        private void TournoiUnloaded(object sender, RoutedEventArgs e)
        {
            BusinessLayer.JediTournamentManager jtm = new BusinessLayer.JediTournamentManager();
            ViewModel.Tournament.TournamentsViewModel tvm = ucTournoi.DataContext as ViewModel.Tournament.TournamentsViewModel;
            List<EntitiesLayer.Tournoi> tournois = new List<EntitiesLayer.Tournoi>();
            foreach (ViewModel.Tournament.TournamentViewModel t in tvm.Tournaments)
            {
                if (t.Name != "")
                    tournois.Add(t.Tournament);
            }
            jtm.updateTournois(tournois);
        }
    }
}
