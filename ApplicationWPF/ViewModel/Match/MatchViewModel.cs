using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiblioWPF.ViewModel;

namespace ApplicationWPF.ViewModel.Match
{
    class MatchViewModel : ViewModelBase
    {
        private EntitiesLayer.Match m_match;

        public EntitiesLayer.Match Match
        {
            get { return m_match; }
            set { m_match = value; }
        }

        public MatchViewModel(EntitiesLayer.Match match)
        {
            m_match = match;
        }

        public EntitiesLayer.Jedi obj_Jedi1
        {
            get
            {
                return m_match.Jedi1;
            }
        }

        public EntitiesLayer.Jedi obj_Jedi2
        {
            get
            {
                return m_match.Jedi2;
            }
        }


        public string Jedi1
        {
            get
            {
                string res = "Inconnu";
                if (m_match.Jedi1 != null)
                    res = m_match.Jedi1.Nom;
                return res;
            }
            set
            {
                BusinessLayer.JediTournamentManager jtm = new BusinessLayer.JediTournamentManager();
                m_match.Jedi1 = (from x in jtm.getJedis() where x.Nom == value select x).FirstOrDefault();
                OnPropertyChanged("Jedi1");
            }
        }

        public string Jedi2
        {
            get
            {
                string res = "Inconnu";
                if (m_match.Jedi2 != null)
                    res = m_match.Jedi2.Nom;
                return res;
            }
            set
            {
                BusinessLayer.JediTournamentManager jtm = new BusinessLayer.JediTournamentManager();
                m_match.Jedi2 = (from x in jtm.getJedis() where x.Nom == value select x).FirstOrDefault();
                OnPropertyChanged("Jedi2");
            }
        }


        public EntitiesLayer.Stade obj_Stade
        {
            get { return m_match.Stade; }
        }


        public string Stade
        {
            get
            {
                return m_match.Stade.Planete;
            }
            set
            {
                BusinessLayer.JediTournamentManager jtm = new BusinessLayer.JediTournamentManager();
                m_match.Stade = (from x in jtm.getStades() where x.Planete == value select x).FirstOrDefault();
                OnPropertyChanged("Stade");
            }
        }

        public EntitiesLayer.EPhaseTournoi PhaseTournoi
        {
            get
            {
                return m_match.PhaseTournoi;
            }
            set
            {
                m_match.PhaseTournoi = value;
                OnPropertyChanged("PhaseTournoi");
            }
        }

        public string Vainqueur
        {
            get
            {
                string res = "Inconnu";
                if (m_match.JediVainqueur != null)
                    res = m_match.JediVainqueur.Nom;
                return res;
            }
            set
            {
                m_match.JediVainqueur = m_match.Jedi1;
                if (value == m_match.Jedi2.Nom)
                    m_match.JediVainqueur = m_match.Jedi2;
                OnPropertyChanged("Vainqueur");
            }
        }
    }
}
