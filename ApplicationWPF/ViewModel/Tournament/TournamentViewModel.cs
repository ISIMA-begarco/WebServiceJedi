using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiblioWPF.ViewModel;

namespace ApplicationWPF.ViewModel.Tournament
{
    class TournamentViewModel : ViewModelBase
    {
        private EntitiesLayer.Tournoi m_tournament;

        public EntitiesLayer.Tournoi Tournament
        {
            get { return m_tournament; }
            set
            {
                m_tournament = value;
                OnPropertyChanged("Tournament");
            }
        }

        public TournamentViewModel (EntitiesLayer.Tournoi tournament)
        {
            m_tournament = tournament;
        }

        public string Name
        {
            get { return m_tournament.Nom; }
            set 
            { 
                m_tournament.Nom = value;
                OnPropertyChanged("Name");
            }
        }

        public string MatchsString
        {
            get
            {
                string res = "";
                for (int i = 0; i < 8; i++)
                    res += getMatchText(i) + ", ";
                return res;
            }
        }

        public List<EntitiesLayer.Match> Matchs
        {
            get { return m_tournament.Matchs; }
            set
            {
                m_tournament.Matchs = value;
                OnPropertyChanged("Matchs");
            }
        }


        public string Match1
        {
            get { return getMatchText(0); }
            set { setMatch(0, value); }
        }

        public string Match2
        {
            get { return getMatchText(1); }
            set { setMatch(1, value); }
        }

        public string Match3
        {
            get { return getMatchText(2); }
            set { setMatch(2, value); }
        }

        public string Match4
        {
            get { return getMatchText(3); }
            set { setMatch(3, value); }
        }

        public string Match5
        {
            get { return getMatchText(4); }
            set { setMatch(4, value); }
        }

        public string Match6
        {
            get { return getMatchText(5); }
            set { setMatch(5, value); }
        }

        public string Match7
        {
            get { return getMatchText(6); }
            set { setMatch(6, value); }
        }

        public string Match8
        {
            get { return getMatchText(7); }
            set { setMatch(7, value); }
        }

        private string getMatchText(int index)
        {
            EntitiesLayer.Match match = m_tournament.Matchs[index];
            string jedi1 = "Inconnu";
            string jedi2 = "Inconnu";
            //string stade = "Inconnu";

            if (match.Jedi1 != null)
                jedi1 = match.Jedi1.Nom;
            if (match.Jedi2 != null)
                jedi2 = match.Jedi2.Nom;
            //if (match.Stade != null)
                string stade = match.Stade.Planete;

            return jedi1 + " VS " + jedi2 + " (" + stade + ")";
        }

        private void setMatch(int index, string value)
        {
            BusinessLayer.JediTournamentManager jtm = new BusinessLayer.JediTournamentManager();
            string jedi1 = value.Split(new string[] { " VS " }, StringSplitOptions.None)[0];
            string jedi2 = value.Split(new string[] { " VS " }, StringSplitOptions.None)[1]
                            .Split(new string[] { " (" }, StringSplitOptions.None)[0];
            string stade = value.Split(new string[] { " (" }, StringSplitOptions.None)[1]
                            .Split(new string[] { ")" }, StringSplitOptions.None)[0];

            if (jedi1 != "Inconnu" && jedi2 != "Inconnu")
            {
                m_tournament.Matchs[index] = (from x in jtm.getMatchsNonEmpty()
                                          where x.Jedi1.Nom == jedi1
                                          && x.Jedi2.Nom == jedi2
                                          && x.Stade.Planete == stade
                                          select x).FirstOrDefault();
            }
            else if (jedi1 == "Inconnu" && jedi2 != "Inconnu")
            {
                m_tournament.Matchs[index] = (from x in jtm.getMatchsNonEmpty()
                                          where x.Jedi1 == null
                                          && x.Jedi2.Nom == jedi2
                                          && x.Stade.Planete == stade
                                          select x).FirstOrDefault();
            }
            else if (jedi1 != "Inconnu" && jedi2 == "Inconnu")
            {
                m_tournament.Matchs[index] = (from x in jtm.getMatchsNonEmpty()
                                          where x.Jedi1.Nom == jedi1
                                          && x.Jedi2 == null
                                          && x.Stade.Planete == stade
                                          select x).FirstOrDefault();
            }
            else // if (jedi1 == "Inconnu" && jedi2 == "Inconnu")
            {
                m_tournament.Matchs[index] = (from x in jtm.getMatchsNonEmpty()
                                          where x.Jedi1 == null
                                          && x.Jedi2 == null
                                          && x.Stade.Planete == stade
                                          select x).FirstOrDefault();
            }

            OnPropertyChanged("Matchs");
            OnPropertyChanged("Match" + (index+1).ToString());
            OnPropertyChanged("MatchsString");
        }
    }
}
