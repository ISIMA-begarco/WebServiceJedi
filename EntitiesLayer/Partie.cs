using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer
{
    public enum Mode { Solo, Multi, SoloPari, MultiPari};
    public class Partie
    {

        private Mode m_mode;
        private Tournoi m_tournament;
        private Match m_current_match;

        private Joueur m_j1;
        private Joueur m_j2;

        private Jedi m_jedi_j1;
        private Jedi m_jedi_j2;

        private EShifumi m_choice_j1;
        private EShifumi m_choice_j2;


        private int m_bourse_j1;
        private int m_bourse_j2;

        private int m_pari_j1;
        private int m_pari_j2;
        
        public Partie()
        {
            J1 = null;
            J2 = null;
            Jedi_j1 = null;
            Jedi_j2 = null;
            Tournament = null;
            Mode = Mode.Solo;
            Choice_j1 = EShifumi.Aucun;
            Choice_j2 = EShifumi.Aucun;
            Bourse_j1 = 0;
            Bourse_j2 = 0;
            Pari_j1 = 0;
            Pari_j2 = 0;
        }

        public Mode Mode
        {
            get { return m_mode; }
            set { m_mode = value; }
        }
        public Match Current_match
        {
            get { return m_current_match; }
            set { m_current_match = value; }
        }

        public Tournoi Tournament
        {
            get { return m_tournament; }
            set { m_tournament = value; }
        }

        public Joueur J1
        {
            get { return m_j1; }
            set { m_j1 = value; }
        }
        public Joueur J2
        {
            get { return m_j2; }
            set { m_j2 = value; }
        }

        public Jedi Jedi_j1
        {
            get { return m_jedi_j1; }
            set { m_jedi_j1 = value; }
        }
        public Jedi Jedi_j2
        {
            get { return m_jedi_j2; }
            set { m_jedi_j2 = value; }
        }

        public EShifumi Choice_j1
        {
            get { return m_choice_j1; }
            set { m_choice_j1 = value; }
        }
        public EShifumi Choice_j2
        {
            get { return m_choice_j2; }
            set { m_choice_j2 = value; }
        }

        public int Bourse_j1
        {
            get { return m_bourse_j1; }
            set { m_bourse_j1 = value; }
        }
        public int Bourse_j2
        {
            get { return m_bourse_j2; }
            set { m_bourse_j2 = value; }
        }

        public int Pari_j1
        {
            get { return m_pari_j1; }
            set { m_pari_j1 = value; }
        }

        public int Pari_j2
        {
            get { return m_pari_j2; }
            set { m_pari_j2 = value; }
        }


    }
}
