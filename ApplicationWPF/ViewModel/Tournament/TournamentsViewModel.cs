using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiblioWPF.ViewModel;

namespace ApplicationWPF.ViewModel.Tournament
{
    class TournamentsViewModel : ViewModelBase
    {
        private ObservableCollection<TournamentViewModel> m_tournaments;
        private TournamentViewModel m_selectedItem;

        private RelayCommand m_addCommand;
        private RelayCommand m_removeCommand;
        
        public ObservableCollection<TournamentViewModel> Tournaments
        {
            get { return m_tournaments; }
            private set
            {
                m_tournaments = value;
                OnPropertyChanged("Tournaments");
            }
        }


        public TournamentViewModel SelectedTournoi
        {
            get { return m_selectedItem; }
            set
            {
                m_selectedItem = value;
                OnPropertyChanged("SelectedTournoi");
            }
        }

        public TournamentsViewModel(IList<EntitiesLayer.Tournoi> tournamentModel)
        {
            m_tournaments = new ObservableCollection<TournamentViewModel>();
            foreach (EntitiesLayer.Tournoi t in tournamentModel)
            {
                m_tournaments.Add(new TournamentViewModel(t));
            }
        }



        public System.Windows.Input.ICommand AddCommand
        {
            get
            {
                if (m_addCommand == null)
                {
                    m_addCommand = new RelayCommand(
                        () => this.Add(),
                        () => this.CanAdd()
                        );
                }
                return m_addCommand;
            }
        }

        private bool CanAdd()
        {
            return true;
        }

        private void Add()
        {
            BusinessLayer.JediTournamentManager jtm = new BusinessLayer.JediTournamentManager();
            EntitiesLayer.Match placeholder = (from x in jtm.getMatches()
                                               where x.PhaseTournoi == EntitiesLayer.EPhaseTournoi.HuitiemeFinale1
                                               select x).First();
            EntitiesLayer.Match[] placeholders = {placeholder, placeholder, placeholder, placeholder,
                                                  placeholder, placeholder, placeholder, placeholder };
            List<EntitiesLayer.Match> matchs = new List<EntitiesLayer.Match>(placeholders);
            matchs.AddRange(jtm.getMatchsEmpty());

            EntitiesLayer.Tournoi t = new EntitiesLayer.Tournoi(0, "<New>", matchs);
            this.SelectedTournoi = new TournamentViewModel(t);
            m_tournaments.Add(this.SelectedTournoi);
        }

        public System.Windows.Input.ICommand RemoveCommand
        {
            get
            {
                if (m_removeCommand == null)
                {
                    m_removeCommand = new RelayCommand(
                        () => this.Remove(),
                        () => this.CanRemove()
                        );
                }
                return m_removeCommand;
            }
        }

        private bool CanRemove()
        {
            return (this.SelectedTournoi != null);
        }

        private void Remove()
        {
            if (this.SelectedTournoi != null) m_tournaments.Remove(this.SelectedTournoi);
        }
    }
}
