using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class PartieManager
    {
        private static EntitiesLayer.Partie game;
        private static BusinessLayer.JediTournamentManager jtm;
        private static Random rd;

        public static void startNewGame()
        {
            jtm = new BusinessLayer.JediTournamentManager();
            game = new EntitiesLayer.Partie();
            rd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
        }

        public static bool nextMatch()
        {

            
            bool nextExist = false;
            List<EntitiesLayer.Match> matchRestant = game.Tournament.Matchs.Where(m => m.JediVainqueur == null).OrderBy(m => m,new JediTournamentManager.MatchOrderComparer()).ToList();
            if(matchRestant.Count > 0)
            {
                game.Current_match = matchRestant.First();
                if(game.Current_match.Jedi1 == null)
                {
                    int hep = (int)game.Current_match.PhaseTournoi * 2 + 1;
                    List<EntitiesLayer.Match> hi = game.Tournament.Matchs.OrderBy(m => m, new JediTournamentManager.MatchOrderComparer()).Reverse().ToList();
                    game.Current_match.Jedi1 = hi[hep].JediVainqueur;
                    
                }

                if (game.Current_match.Jedi2 == null)
                {
                    int hep = (int)game.Current_match.PhaseTournoi * 2 + 2;
                    List<EntitiesLayer.Match> hi = game.Tournament.Matchs.OrderBy(m => m, new JediTournamentManager.MatchOrderComparer()).Reverse().ToList();
                    game.Current_match.Jedi2 = hi[hep].JediVainqueur;
                   
                }
                nextExist = true;

            }

            return nextExist;
        }

        public static EntitiesLayer.EShifumi getIAChoice()
        {
            EntitiesLayer.EShifumi ret = EntitiesLayer.EShifumi.Aucun;
            int rand = rd.Next();
            if (rand % 3 == 0)
            {
                ret =  EntitiesLayer.EShifumi.Ciseau;
            }
            if (rand % 3 == 1)
            {
                ret = EntitiesLayer.EShifumi.Papier;
            }
            if (rand % 3 == 2)
            {
                ret = EntitiesLayer.EShifumi.Pierre;
            }

            return ret;
        } 

            

        public static bool resolve()
        {
            bool solved = false;
            if(game.Choice_j1 != EntitiesLayer.EShifumi.Aucun && game.Choice_j2 != EntitiesLayer.EShifumi.Aucun)
            {
                int res = jtm.playRound(game.Choice_j1, game.Choice_j2);
                if(res == 0)
                {
                    Random rd = new Random();
                    if(rd.NextDouble()%2 == 1)
                    {
                        game.Current_match.JediVainqueur = game.Current_match.Jedi1;
                    }
                    else
                    {
                        game.Current_match.JediVainqueur = game.Current_match.Jedi2;
                    }
                }
                if(res == -1)
                {
                    game.Current_match.JediVainqueur = game.Current_match.Jedi1;
                }
                if(res == 1)
                {
                    game.Current_match.JediVainqueur = game.Current_match.Jedi2;
                }

                solved = true;
            }

            return solved;
        }

        public static void setCurrentPlayer(EntitiesLayer.Joueur j,int num_j)
        {
            if(num_j == 1)
                game.J1 = j;
            if(num_j == 2)
                game.J2 = j;
        }
        
        public static void setCurrentGameMode(EntitiesLayer.Mode m)
        {
            game.Mode = m;
        }

        public static void setCurrentGameTournament(EntitiesLayer.Tournoi t)
        {

            
            game.Tournament = t;
            int i = (int)EntitiesLayer.EPhaseTournoi.HuitiemeFinale1;
            foreach(EntitiesLayer.Match ma in game.Tournament.Matchs.Where(m => (int)m.PhaseTournoi >= (int)EntitiesLayer.EPhaseTournoi.HuitiemeFinale8).ToList())
            {
                ma.PhaseTournoi = (EntitiesLayer.EPhaseTournoi) i;
                i--;
            }           

            game.Current_match = t.Matchs.First();
        }

        public static void setCurrentGameOptions()
        {

        }

        public static EntitiesLayer.Partie getCurrentGame()
        {
            return game;
        }
    }
}
