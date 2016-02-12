using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiblioWPF.ViewModel;

namespace ApplicationWPF.ViewModel.Partie
{
    class PartieViewModel : ViewModelBase
    {
        private EntitiesLayer.Partie m_partie;

        public PartieViewModel(EntitiesLayer.Partie partie)
        {
            m_partie = partie;
        }

        public EntitiesLayer.Mode Mode
        {
            get { return m_partie.Mode; }
            set
            {
                m_partie.Mode = value;
                OnPropertyChanged("Mode");
            }
        }

        public EntitiesLayer.Match Current_match
        {
            get { return m_partie.Current_match; }
            set
            {
                m_partie.Current_match = value;
                OnPropertyChanged("Current_match");
            }
        }

        public EntitiesLayer.Tournoi Tournament
        {
            get { return m_partie.Tournament; }
            set
            {
                m_partie.Tournament = value;
                OnPropertyChanged("Tournament");
            }
        }

        public EntitiesLayer.Joueur J1
        {
            get { return m_partie.J1; }
            set
            {
                m_partie.J1 = value;
                OnPropertyChanged("J1");
            }
        }

        public EntitiesLayer.Joueur J2
        {
            get { return m_partie.J2; }
            set
            {
                m_partie.J2 = value;
                OnPropertyChanged("J2");
            }
        }

        public EntitiesLayer.Jedi Jedi_j1
        {
            get { return m_partie.Jedi_j1; }
            set
            {
                m_partie.Jedi_j1 = value;
                OnPropertyChanged("Jedi_j1");
            }
        }

        public EntitiesLayer.Jedi Jedi_j2
        {
            get { return m_partie.Jedi_j2; }
            set
            {
                m_partie.Jedi_j2 = value;
                OnPropertyChanged("Jedi_j2");
            }
        }

        public EntitiesLayer.EShifumi Choice_j1
        {
            get { return m_partie.Choice_j1; }
            set
            {
                m_partie.Choice_j1 = value;
                OnPropertyChanged("Choice_j1");
            }
        }

        public EntitiesLayer.EShifumi Choice_j2
        {
            get { return m_partie.Choice_j2; }
            set
            {
                m_partie.Choice_j2 = value;
                OnPropertyChanged("Choice_j2");
            }
        }

        public int Bourse_j1
        {
            get { return m_partie.Bourse_j1; }
            set
            {
                m_partie.Bourse_j1 = value;
                OnPropertyChanged("Bourse_j1");
            }
        }

        public int Bourse_j2
        {
            get { return m_partie.Bourse_j2; }
            set
            {
                m_partie.Bourse_j2 = value;
                OnPropertyChanged("Bourse_j2");
            }
        }

        public int Pari_j1
        {
            get { return m_partie.Pari_j1; }
            set
            {
                m_partie.Pari_j1 = value;
                OnPropertyChanged("Pari_j1");
            }
        }

        public int Pari_j2
        {
            get { return m_partie.Pari_j2; }
            set
            {
                m_partie.Pari_j2 = value;
                OnPropertyChanged("Pari_j2");
            }
        }
    }
}
