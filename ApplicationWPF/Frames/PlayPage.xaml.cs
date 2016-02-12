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

namespace ApplicationWPF.Frames
{
    /// <summary>
    /// Logique d'interaction pour PlayPage.xaml
    /// </summary>
    public partial class PlayPage : Page,IFrameNavigator
    {
        public event EventHandler<FrameChangedEventArgs> m_changeFrame;
        public string m_nextFrame;

        public UserControl lastSelect;

        public PlayPage()
        {
            InitializeComponent();
            this.ModeChoice.NavigationService.Navigate(new System.Uri("Frames/PlayPageFrame/OnePlayerPage.xaml", UriKind.Relative));

        }

        private void WindowLoaded(object sender, EventArgs args)
        {
            BusinessLayer.JediTournamentManager jtm = new BusinessLayer.JediTournamentManager();

            // Gestion des Tournois
            IList<EntitiesLayer.Tournoi> tournaments = jtm.getTournois();
            ViewModel.Tournament.TournamentsViewModel tvm = new ViewModel.Tournament.TournamentsViewModel(tournaments);
            usrCtrlTournoiCombo.DataContext = tvm;

            BusinessLayer.PartieManager.startNewGame();
            usrCtrlTournoiCombo.cbTournoi.DropDownClosed += SetDetails;

            lastSelect = usrSolo;
            usrSolo.Margin = new System.Windows.Thickness(5);
            usrSolo.Opacity = 1;


            usrSolo.MouseDown += fixeSelection;
            usrMulti.MouseDown += fixeSelection;
            usrPariSolo.MouseDown += fixeSelection;
            usrPariMulti.MouseDown += fixeSelection;
        }

        private void fixeSelection(object sender, MouseButtonEventArgs e)
        {
            lastSelect.Margin = new System.Windows.Thickness(10);
            lastSelect.Opacity = 0.9;
            UserControl usr = sender as UserControl;
            usr.Margin = new System.Windows.Thickness(5);
            usr.Opacity = 1;
            lastSelect = usr;
        }

        public void SetDetails(object sender, EventArgs e)
        {
            if (BusinessLayer.PartieManager.getCurrentGame().Tournament != null)
            {
                List<EntitiesLayer.Match> ms = new List<EntitiesLayer.Match>();
                foreach (EntitiesLayer.Match m in BusinessLayer.PartieManager.getCurrentGame().Tournament.Matchs)
                {
                    if (m.Jedi1 != null && m.Jedi2 != null)
                        ms.Add(m);
                }

                IList<EntitiesLayer.Match> matchs = ms;
                ViewModel.Match.MatchsViewModel mvm = new ViewModel.Match.MatchsViewModel(matchs);
                tournamentDetails.DataContext = mvm;
            }
        }
        
        public string NextFrame
        {
            get { return m_nextFrame; }
            set { m_nextFrame = value; }
        }

        public EventHandler<FrameChangedEventArgs> OnFrameChanged
        {
            get { return m_changeFrame; }
            set { m_changeFrame = value; }
        }



        private void ButtonBack_Event(object sender, EventArgs e)
        {
            string nextFrame = "Frames/MainMenu.xaml";
            OnFrameChanged(this, new FrameChangedEventArgs(nextFrame));
        }

        private void ButtonStart_Event(object sender, EventArgs e)
        {

            if(this.usrCtrlTournoiCombo.cbTournoi.SelectedItem != null)
            {
                string nextFrame = "Frames/FightPage.xaml";
                bool launch = false;                

                switch (BusinessLayer.PartieManager.getCurrentGame().Mode)

                {
                    case EntitiesLayer.Mode.Solo:
                        if (BusinessLayer.PartieManager.getCurrentGame().Jedi_j1 != null)
                        {
                            BusinessLayer.PartieManager.setCurrentPlayer(new EntitiesLayer.Joueur(0, "J1", 0), 1);
                            launch = true;
                        }
                        break;
                    case EntitiesLayer.Mode.Multi:
                        if (BusinessLayer.PartieManager.getCurrentGame().Jedi_j1 != null 
                            && BusinessLayer.PartieManager.getCurrentGame().Jedi_j2 != null 
                            && BusinessLayer.PartieManager.getCurrentGame().Jedi_j1 != BusinessLayer.PartieManager.getCurrentGame().Jedi_j2)
                        {
                            BusinessLayer.PartieManager.setCurrentPlayer(new EntitiesLayer.Joueur(0, "J1", 0), 1);
                            BusinessLayer.PartieManager.setCurrentPlayer(new EntitiesLayer.Joueur(1, "J2", 0), 2);
                            launch = true;
                        }
                        break;
                    case EntitiesLayer.Mode.MultiPari:
                        if(BusinessLayer.PartieManager.getCurrentGame().Bourse_j1 != 0 
                            && BusinessLayer.PartieManager.getCurrentGame().Bourse_j2 != 0)
                        {
                            BusinessLayer.PartieManager.setCurrentPlayer(new EntitiesLayer.Joueur(0, "J1", 0), 1);
                            BusinessLayer.PartieManager.setCurrentPlayer(new EntitiesLayer.Joueur(1, "J2", 0), 2);
                            launch = true;
                        }
                        break;
                    case EntitiesLayer.Mode.SoloPari:
                        if (BusinessLayer.PartieManager.getCurrentGame().Bourse_j1 != 0)
                        {
                            BusinessLayer.PartieManager.setCurrentPlayer(new EntitiesLayer.Joueur(0, "J1", 0), 1);
                            launch = true;
                        }
                        break;
                        
                }

                if(launch)
                    OnFrameChanged(this, new FrameChangedEventArgs(nextFrame));
            }
            
        }

        private void OnPlayChoiceOnePlayer_Click(object sender, EventArgs e)
        {
            this.ModeChoice.NavigationService.Navigate(new System.Uri("Frames/PlayPageFrame/OnePlayerPage.xaml", UriKind.Relative));
            BusinessLayer.PartieManager.setCurrentGameMode(EntitiesLayer.Mode.Solo);
            BusinessLayer.PartieManager.getCurrentGame().Jedi_j1 = null;
        }

        private void OnPlayChoiceMultiplayer_Click(object sender, EventArgs e)
        {
           this.ModeChoice.NavigationService.Navigate(new System.Uri("Frames/PlayPageFrame/MultiplayerPage.xaml", UriKind.Relative));
            BusinessLayer.PartieManager.setCurrentGameMode(EntitiesLayer.Mode.Multi);
            BusinessLayer.PartieManager.getCurrentGame().Jedi_j1 = null;
            BusinessLayer.PartieManager.getCurrentGame().Jedi_j2 = null;
        }

        private void OnPlayChoiceMultiplayerPari_Click(object sender, EventArgs e)
        {
            this.ModeChoice.NavigationService.Navigate(new System.Uri("Frames/PlayPageFrame/PariMultiplayer.xaml", UriKind.Relative));
            BusinessLayer.PartieManager.setCurrentGameMode(EntitiesLayer.Mode.MultiPari);
        }

        private void OnPlayChoiceSoloPari_Click(object sender, EventArgs e)
        {
            this.ModeChoice.NavigationService.Navigate(new System.Uri("Frames/PlayPageFrame/PariOnePlayer.xaml", UriKind.Relative));
            BusinessLayer.PartieManager.setCurrentGameMode(EntitiesLayer.Mode.SoloPari);
        }
    }
}
