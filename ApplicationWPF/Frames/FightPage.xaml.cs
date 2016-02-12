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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ApplicationWPF.Frames
{
    /// <summary>
    /// Logique d'interaction pour FightPage.xaml
    /// </summary>
    public partial class FightPage : Page, IFrameNavigator
    {

        public event EventHandler<FrameChangedEventArgs> m_changeFrame;
        public string m_nextFrame;

        public FightPage()
        {
            InitializeComponent();
        }

        private void WindowLoaded(object sender, EventArgs args)
        {
            BusinessLayer.JediTournamentManager jtm = new BusinessLayer.JediTournamentManager();


            fightPage.DataContext = BusinessLayer.PartieManager.getCurrentGame().Current_match.Stade;

            // Gestion de la Partie
            EntitiesLayer.Partie game = BusinessLayer.PartieManager.getCurrentGame();
            ViewModel.Partie.PartieViewModel gvm = new ViewModel.Partie.PartieViewModel(game);
            Concurent1.DataContext = gvm.Current_match.Jedi1;
            Concurent2.DataContext = gvm.Current_match.Jedi2;

            Pari1.DataContext = gvm;
            Pari2.DataContext = gvm;

            game.Choice_j1 = EntitiesLayer.EShifumi.Aucun;
            game.Choice_j2 = EntitiesLayer.EShifumi.Aucun;

            game.Pari_j1 = 0;
            game.Pari_j2 = 0;

            PhaseTournoi.Text = gvm.Current_match.PhaseTournoi.ToString();

            Affiche1.Visibility = Visibility.Hidden;
            Affiche2.Visibility = Visibility.Hidden;


            if(BusinessLayer.PartieManager.getCurrentGame().Mode == EntitiesLayer.Mode.SoloPari || BusinessLayer.PartieManager.getCurrentGame().Mode == EntitiesLayer.Mode.MultiPari)
            {
                
                List<EntitiesLayer.Jedi> jedis = new List<EntitiesLayer.Jedi>();   
                jedis.Add(game.Current_match.Jedi1);
                jedis.Add(game.Current_match.Jedi2);


                IList<EntitiesLayer.Jedi> jedis2 = jedis;
                ViewModel.Jedi.JedisModelView jvm = new ViewModel.Jedi.JedisModelView(jedis);
                J1Jedi.DataContext = jvm;

                ViewModel.Jedi.JedisModelView jvm2 = new ViewModel.Jedi.JedisModelView(jedis);
                J2Jedi.DataContext = jvm2;

                if (BusinessLayer.PartieManager.getCurrentGame().J1 != null)
                {
                    Pari1.Visibility = Visibility.Visible;
                }

                if (BusinessLayer.PartieManager.getCurrentGame().J2 != null)
                {
                    Pari2.Visibility = Visibility.Visible;
                }
            }

            var window = Window.GetWindow(this);
            window.KeyDown += Page_KeyDown;

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
            if (BusinessLayer.PartieManager.getCurrentGame().Mode == EntitiesLayer.Mode.Multi || BusinessLayer.PartieManager.getCurrentGame().Mode == EntitiesLayer.Mode.Solo)
            {
                if (BusinessLayer.PartieManager.getCurrentGame().Current_match.JediVainqueur == null)
                {
                    if (((BusinessLayer.PartieManager.getCurrentGame().J1 != null && BusinessLayer.PartieManager.getCurrentGame().Current_match.Jedi1.Nom == BusinessLayer.PartieManager.getCurrentGame().Jedi_j1.Nom)
                        || (BusinessLayer.PartieManager.getCurrentGame().J2 != null && BusinessLayer.PartieManager.getCurrentGame().Current_match.Jedi1.Nom == BusinessLayer.PartieManager.getCurrentGame().Jedi_j2.Nom))
                        && (BusinessLayer.PartieManager.getCurrentGame().Mode.Equals(EntitiesLayer.Mode.Solo) || BusinessLayer.PartieManager.getCurrentGame().Mode.Equals(EntitiesLayer.Mode.Multi)))
                    {
                        Affiche1.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        BusinessLayer.PartieManager.getCurrentGame().Choice_j1 = BusinessLayer.PartieManager.getIAChoice();
                    }

                    if (((BusinessLayer.PartieManager.getCurrentGame().J1 != null && BusinessLayer.PartieManager.getCurrentGame().Current_match.Jedi2.Nom == BusinessLayer.PartieManager.getCurrentGame().Jedi_j1.Nom)
                       || (BusinessLayer.PartieManager.getCurrentGame().J2 != null && BusinessLayer.PartieManager.getCurrentGame().Current_match.Jedi2.Nom == BusinessLayer.PartieManager.getCurrentGame().Jedi_j2.Nom))
                       && (BusinessLayer.PartieManager.getCurrentGame().Mode.Equals(EntitiesLayer.Mode.Solo) || BusinessLayer.PartieManager.getCurrentGame().Mode.Equals(EntitiesLayer.Mode.Multi)))
                    {
                        Affiche2.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        BusinessLayer.PartieManager.getCurrentGame().Choice_j2 = BusinessLayer.PartieManager.getIAChoice();
                    }

                    resolve();
                }
            }
            else
            {
                Bourse_j1_LostFocus(this, null);
                Bourse_j2_LostFocus(this, null);

                if (BusinessLayer.PartieManager.getCurrentGame().Current_match.JediVainqueur == null)
                {
                    BusinessLayer.JediTournamentManager jtm = new BusinessLayer.JediTournamentManager();
                    resolve();
                    EntitiesLayer.Jedi winner = BusinessLayer.PartieManager.getCurrentGame().Current_match.JediVainqueur;
                     
                    if (BusinessLayer.PartieManager.getCurrentGame().J1 != null 
                        && (this.J1Jedi.ComboJedi.SelectedItem as ViewModel.Jedi.JediViewModel) != null
                        && BusinessLayer.PartieManager.getCurrentGame().Pari_j1 != 0)
                    {
                        if((this.J1Jedi.ComboJedi.SelectedItem as ViewModel.Jedi.JediViewModel).Jedi.Equals(winner))
                        {
                            BusinessLayer.PartieManager.getCurrentGame().Bourse_j1 += BusinessLayer.PartieManager.getCurrentGame().Pari_j1;
                        }
                        else
                        {
                            BusinessLayer.PartieManager.getCurrentGame().Bourse_j1 -= BusinessLayer.PartieManager.getCurrentGame().Pari_j1;
                        }
                    }
                        
                    if (BusinessLayer.PartieManager.getCurrentGame().J2 != null 
                        && (this.J2Jedi.ComboJedi.SelectedItem as ViewModel.Jedi.JediViewModel) != null
                        && BusinessLayer.PartieManager.getCurrentGame().Pari_j2 != 0)
                    {
                        if ((this.J2Jedi.ComboJedi.SelectedItem as ViewModel.Jedi.JediViewModel).Jedi.Equals(winner))
                        {
                            BusinessLayer.PartieManager.getCurrentGame().Bourse_j2 += BusinessLayer.PartieManager.getCurrentGame().Pari_j2;
                        }
                        else
                        {
                            BusinessLayer.PartieManager.getCurrentGame().Bourse_j2 -= BusinessLayer.PartieManager.getCurrentGame().Pari_j2;
                        }
                    }

                }
            }
        }

        private void ButtonNext_Event(object sender, EventArgs e)
        {
            if(BusinessLayer.PartieManager.getCurrentGame().Current_match.JediVainqueur != null) {
                bool ret = BusinessLayer.PartieManager.nextMatch();
                if (ret == false)
                {
                    string nextFrame = "Frames/WinnerPage.xaml";
                    OnFrameChanged(this, new FrameChangedEventArgs(nextFrame));
                }
                else {
                    this.NavigationService.Refresh();
                }
            }
        }

        private void ButtonPierre_Event(object sender, EventArgs e)
        {
            BusinessLayer.PartieManager.getCurrentGame().Choice_j1 = EntitiesLayer.EShifumi.Pierre;
            resolve();
        }

        private void ButtonPapier_Event(object sender, EventArgs e)
        {
            BusinessLayer.PartieManager.getCurrentGame().Choice_j1 = EntitiesLayer.EShifumi.Papier;
            resolve();
        }

        private void ButtonCiseau_Event(object sender, EventArgs e)
        {
            BusinessLayer.PartieManager.getCurrentGame().Choice_j1 = EntitiesLayer.EShifumi.Ciseau;
            resolve();
        }


        private void ButtonPierre2_Event(object sender, EventArgs e)
        {
            BusinessLayer.PartieManager.getCurrentGame().Choice_j2 = EntitiesLayer.EShifumi.Pierre;
            resolve();
        }

        private void ButtonPapier2_Event(object sender, EventArgs e)
        {
            BusinessLayer.PartieManager.getCurrentGame().Choice_j2 = EntitiesLayer.EShifumi.Papier;
            resolve();
        }

        private void ButtonCiseau2_Event(object sender, EventArgs e)
        {
            BusinessLayer.PartieManager.getCurrentGame().Choice_j2 = EntitiesLayer.EShifumi.Ciseau;
            resolve();
        }

        private void resolve()
        {
            if (BusinessLayer.PartieManager.getCurrentGame().Current_match.JediVainqueur == null)
            {
                DropShadowEffect o = new DropShadowEffect();
                o.Direction = 0;
                o.Color = Colors.Blue;
                o.ShadowDepth = 0;
                o.BlurRadius = 50;


                if (BusinessLayer.PartieManager.getCurrentGame().Mode == EntitiesLayer.Mode.Solo || BusinessLayer.PartieManager.getCurrentGame().Mode == EntitiesLayer.Mode.Multi)
                {                    
                    if (BusinessLayer.PartieManager.resolve())
                    {                        
                        if (BusinessLayer.PartieManager.getCurrentGame().Current_match.JediVainqueur == BusinessLayer.PartieManager.getCurrentGame().Current_match.Jedi1)
                        {
                            this.Concurent1Img.Effect = o;
                            this.fightPage.UpdateLayout();
                        }
                        else
                        {
                            this.Concurent2Img.Effect = o;
                            this.fightPage.UpdateLayout();
                        }
                    }
                }
                else
                {
                    EntitiesLayer.Jedi winner =  new BusinessLayer.JediTournamentManager().simulateMatch(BusinessLayer.PartieManager.getCurrentGame().Current_match);
                    BusinessLayer.PartieManager.getCurrentGame().Current_match.JediVainqueur = winner;

                    if (BusinessLayer.PartieManager.getCurrentGame().Current_match.JediVainqueur == BusinessLayer.PartieManager.getCurrentGame().Current_match.Jedi1)
                    {
                        this.Concurent1Img.Effect = o;
                        this.fightPage.UpdateLayout();
                    }
                    else
                    {
                        this.Concurent2Img.Effect = o;
                        this.fightPage.UpdateLayout();
                    }
                }
            }
        }


        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if( (BusinessLayer.PartieManager.getCurrentGame().Mode == EntitiesLayer.Mode.Solo 
                ||BusinessLayer.PartieManager.getCurrentGame().Mode == EntitiesLayer.Mode.Multi)
                && BusinessLayer.PartieManager.getCurrentGame().Current_match.JediVainqueur == null)
            {
                if(this.Affiche1.Visibility.Equals(Visibility.Visible)
                     && BusinessLayer.PartieManager.getCurrentGame().Choice_j1 == EntitiesLayer.EShifumi.Aucun)
                {
                    if (e.Key == Key.E)
                    {
                        BusinessLayer.PartieManager.getCurrentGame().Choice_j1 = EntitiesLayer.EShifumi.Pierre;
                        resolve();
                    }

                    if (e.Key == Key.D)
                    {
                        BusinessLayer.PartieManager.getCurrentGame().Choice_j1 = EntitiesLayer.EShifumi.Papier;
                        resolve();
                    }

                    if (e.Key == Key.X)
                    {
                        BusinessLayer.PartieManager.getCurrentGame().Choice_j1 = EntitiesLayer.EShifumi.Ciseau;
                        resolve();
                    }
                }

                if (this.Affiche2.Visibility.Equals(Visibility.Visible)
                    && BusinessLayer.PartieManager.getCurrentGame().Choice_j2 == EntitiesLayer.EShifumi.Aucun)
                {
                    if (e.Key == Key.U)
                    {
                        BusinessLayer.PartieManager.getCurrentGame().Choice_j2 = EntitiesLayer.EShifumi.Pierre;
                        resolve();
                    }

                    if (e.Key == Key.J)
                    {
                        BusinessLayer.PartieManager.getCurrentGame().Choice_j2 = EntitiesLayer.EShifumi.Papier;
                        resolve();
                    }

                    if (e.Key == Key.N)
                    {
                        BusinessLayer.PartieManager.getCurrentGame().Choice_j2 = EntitiesLayer.EShifumi.Ciseau;
                        resolve();
                    }
                }
               
            }
        }

        private void Bourse_j1_LostFocus(object sender, RoutedEventArgs e)
        {
            string val = this.mise_j1.Text;

            try
            {
                int bourse = int.Parse(val);

                if (bourse < 0)
                {
                    this.mise_j1.Text = "0";
                    BusinessLayer.PartieManager.getCurrentGame().Pari_j1 = 0;
                }
                else if (bourse > BusinessLayer.PartieManager.getCurrentGame().Bourse_j1)
                {
                    this.mise_j1.Text = BusinessLayer.PartieManager.getCurrentGame().Bourse_j1.ToString();
                    BusinessLayer.PartieManager.getCurrentGame().Pari_j1 = BusinessLayer.PartieManager.getCurrentGame().Bourse_j1;
                }
                else
                {
                    BusinessLayer.PartieManager.getCurrentGame().Pari_j1 = bourse;
                }


            }
            catch (Exception)
            {
                this.mise_j1.Text = "0";
                BusinessLayer.PartieManager.getCurrentGame().Pari_j1 = 0;
            }

        }

        private void Bourse_j2_LostFocus(object sender, RoutedEventArgs e)
        {
            string val = this.mise_j2.Text;

            try
            {
                int bourse = int.Parse(val);

                if (bourse < 0)
                {
                    this.mise_j2.Text = "0";
                    BusinessLayer.PartieManager.getCurrentGame().Pari_j2 = 0;
                }
                else if(bourse > BusinessLayer.PartieManager.getCurrentGame().Bourse_j2)
                {
                    this.mise_j2.Text = BusinessLayer.PartieManager.getCurrentGame().Bourse_j2.ToString();
                    BusinessLayer.PartieManager.getCurrentGame().Pari_j2 = BusinessLayer.PartieManager.getCurrentGame().Bourse_j2;
                }
                else
                {
                    BusinessLayer.PartieManager.getCurrentGame().Pari_j2 = bourse;
                }


            }
            catch (Exception)
            {
                this.mise_j2.Text = "0";
                BusinessLayer.PartieManager.getCurrentGame().Pari_j2 = 0;
            }

        }
    }
}
