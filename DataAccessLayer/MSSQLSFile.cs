using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesLayer;
using System.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
    public class MSSQLSFile : IBridge
    {
        private String connectionString;
        private DataTable[] dataTables;

        #region Enums
        public enum DTName
        {
            JEDIS = 0,
            STADES = 1,
            CARAC = 2,
            MATCHES = 3,
            TOURNOIS = 4,
            USERS = 5,
            STADECARAC = 6,
            JEDICARAC = 7,
            MATCHTOURNOI = 8
        }
        public enum JediField
        {
            ID = 0,
            NOM = 1,
            ISSITH = 2,
            IMAGE = 3
        }
        public enum CaracField
        {
            ID = 0,
            NOM = 1,
            DEF = 2,
            TYPE = 3,
            VALEUR = 4
        }
        public enum StadeField
        {
            ID = 0,
            NOM = 1,
            NBPLACES = 2,
            IMAGE = 3
        }
        public enum UserField
        {
            ID = 0,
            LOGIN = 1,
            PASSWORD = 2,
            NOM = 3,
            PRENOM = 4
        }
        public enum MatchField
        {
            ID = 0,
            JEDI1 = 1,
            JEDI2,
            STADE,
            WINNER,
            PHASE
        }
        public enum TournoiField
        {
            ID = 0,
            NOM = 1
        }
        #endregion

        public MSSQLSFile(String pConnectionString)
        {
            connectionString = pConnectionString;
            dataTables = new DataTable[9];
            for (int i = 0; i < 9; i++)
            {
                dataTables[i] = new DataTable();
            }

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(new SqlCommand("SELECT id, nom, isSith, image FROM jedis;", sqlConnection));
                dataTables[(int)DTName.JEDIS].Clear();
                sqlDataAdapter.Fill(dataTables[(int)DTName.JEDIS]);

                sqlDataAdapter = new SqlDataAdapter(new SqlCommand("SELECT idjedi, idcarac FROM JediCarac;", sqlConnection));
                dataTables[(int)DTName.JEDICARAC].Clear();
                sqlDataAdapter.Fill(dataTables[(int)DTName.JEDICARAC]);

                sqlDataAdapter = new SqlDataAdapter(new SqlCommand("SELECT C.id, C.nom, C.def, C.type, C.valeur FROM caracteristiques C;", sqlConnection));
                dataTables[(int)DTName.CARAC].Clear();
                sqlDataAdapter.Fill(dataTables[(int)DTName.CARAC]);

                sqlDataAdapter = new SqlDataAdapter(new SqlCommand("SELECT id, jedi1, jedi2, stade, vainqueur, phase FROM Matches;", sqlConnection));
                dataTables[(int)DTName.MATCHES].Clear();
                sqlDataAdapter.Fill(dataTables[(int)DTName.MATCHES]);

                sqlDataAdapter = new SqlDataAdapter(new SqlCommand("SELECT id, nom, nbplaces, image FROM stades;", sqlConnection));
                dataTables[(int)DTName.STADES].Clear();
                sqlDataAdapter.Fill(dataTables[(int)DTName.STADES]);

                sqlDataAdapter = new SqlDataAdapter(new SqlCommand("SELECT idstade, idcarac FROM StadeCarac;", sqlConnection));
                dataTables[(int)DTName.STADECARAC].Clear();
                sqlDataAdapter.Fill(dataTables[(int)DTName.STADECARAC]);

                sqlDataAdapter = new SqlDataAdapter(new SqlCommand("SELECT id, nom FROM Tournois;", sqlConnection));
                dataTables[(int)DTName.TOURNOIS].Clear();
                sqlDataAdapter.Fill(dataTables[(int)DTName.TOURNOIS]);

                sqlDataAdapter = new SqlDataAdapter(new SqlCommand("SELECT idtournoi, idmatch, idmatchtournoi FROM MatchTournoi;", sqlConnection));
                dataTables[(int)DTName.MATCHTOURNOI].Clear();
                sqlDataAdapter.Fill(dataTables[(int)DTName.MATCHTOURNOI]);

                sqlConnection.Close();
            }
        }

        #region Getters
        public List<Caracteristique> getCaracteristiques()
        {
            List<Caracteristique> carac = new List<Caracteristique>();

            using (SqlConnection sqlConnection2 = new SqlConnection(connectionString))
            {
                String request2 = "SELECT C.id, C.nom, C.def, C.type, C.valeur FROM caracteristiques C;";
                SqlCommand sqlCommand2 = new SqlCommand(request2, sqlConnection2);
                sqlConnection2.Open();

                SqlDataReader sqlDataReader2 = sqlCommand2.ExecuteReader();            

                while (sqlDataReader2.Read())
                {
                    carac.Add(new Caracteristique(  sqlDataReader2.GetInt32((int)CaracField.ID),
                                                    convertDef(sqlDataReader2.GetString((int)CaracField.DEF)),
                                                    sqlDataReader2.GetString((int)CaracField.NOM),
                                                    convertType(sqlDataReader2.GetString((int)CaracField.TYPE)),
                                                    sqlDataReader2.GetInt32((int)CaracField.VALEUR))
                    );
                }
                sqlDataReader2.Close();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand2);
                dataTables[(int)DTName.CARAC].Clear();
                sqlDataAdapter.Fill(dataTables[(int)DTName.CARAC]);

                sqlConnection2.Close();
            }

            return carac;
        }
        public List<Jedi> getJedis()
        {
            List<Jedi> allJedis = new List<Jedi>();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                String request = "SELECT id, nom, isSith, image FROM jedis;";
                SqlCommand sqlCommand = new SqlCommand(request, sqlConnection);
                sqlConnection.Open();

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    List<Caracteristique> carac = new List<Caracteristique>();

                    using (SqlConnection sqlConnection2 = new SqlConnection(connectionString))
                    {
                        String id = sqlDataReader.GetInt32((int)JediField.ID).ToString();
                        String request2 = "SELECT C.id, C.nom, C.def, C.type, C.valeur FROM jedis J, caracteristiques C, JediCarac JC WHERE J.id=" + id + " AND J.id=JC.idjedi AND C.id=JC.idcarac;";
                        SqlCommand sqlCommand2 = new SqlCommand(request2, sqlConnection2);
                        sqlConnection2.Open();

                        SqlDataReader sqlDataReader2 = sqlCommand2.ExecuteReader();
                        while (sqlDataReader2.Read())
                        {
                            carac.Add(new Caracteristique(  sqlDataReader2.GetInt32((int)CaracField.ID),
                                                            convertDef(sqlDataReader2.GetString((int)CaracField.DEF)),
                                                            sqlDataReader2.GetString((int)CaracField.NOM),
                                                            convertType(sqlDataReader2.GetString((int)CaracField.TYPE)),
                                                            sqlDataReader2.GetInt32((int)CaracField.VALEUR))
                            );
                        }
                        sqlConnection2.Close();
                    }

                    allJedis.Add(new Jedi(       sqlDataReader.GetInt32((int)JediField.ID), 
                                                 carac,
                                                 sqlDataReader.GetBoolean((int)JediField.ISSITH),
                                                 sqlDataReader.GetString((int)JediField.NOM),
                                                 sqlDataReader.GetString((int)JediField.IMAGE)));
                }
                sqlDataReader.Close();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                dataTables[(int)DTName.JEDIS].Clear();
                sqlDataAdapter.Fill(dataTables[(int)DTName.JEDIS]);
                sqlDataAdapter = new SqlDataAdapter(new SqlCommand("SELECT idjedi, idcarac FROM JediCarac;", sqlConnection));
                dataTables[(int)DTName.JEDICARAC].Clear();
                sqlDataAdapter.Fill(dataTables[(int)DTName.JEDICARAC]);
                sqlConnection.Close();
            }
            return allJedis;
        }
        public List<Match> getMatches()
        {
            List<Match> allMatches = new List<Match>();
            List<Stade> allStade = this.getStades();
            List<Jedi> allJedis = this.getJedis();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                String request = "SELECT id, jedi1, jedi2, stade, vainqueur, phase FROM Matches;";
                SqlCommand sqlCommand = new SqlCommand(request, sqlConnection);
                sqlConnection.Open();

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    List<Jedi> j1 = allJedis.Where(j => j.Id.Equals(sqlDataReader.GetInt32((int)MatchField.JEDI1))).ToList();
                    List<Jedi> j2 = allJedis.Where(j => j.Id.Equals(sqlDataReader.GetInt32((int)MatchField.JEDI2))).ToList();
                    List<Jedi> j3 = allJedis.Where(j => j.Id.Equals(sqlDataReader.GetInt32((int)MatchField.WINNER))).ToList();
                    List<Stade> s1 = allStade.Where(s => s.Id.Equals(sqlDataReader.GetInt32((int)MatchField.STADE))).ToList();
                    allMatches.Add(new Match(   sqlDataReader.GetInt32((int)MatchField.ID),
                                                (j1.Count != 0 ? j1.First() : null),
                                                (j2.Count != 0 ? j2.First() : null),
                                                (EPhaseTournoi)sqlDataReader.GetInt32((int)MatchField.PHASE),
                                                (s1.Count != 0 ? s1.First() : null),
                                                (j3.Count != 0 ? j3.First() : null)));
                }
                sqlDataReader.Close();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(new SqlCommand("SELECT id, jedi1, jedi2, stade, vainqueur, phase FROM Matches;", sqlConnection));
                dataTables[(int)DTName.MATCHES].Clear();
                sqlDataAdapter.Fill(dataTables[(int)DTName.MATCHES]);
                sqlConnection.Close();
            }
            return allMatches;
        }
        public List<Stade> getStades()
        {
            List<Stade> allStades = new List<Stade>();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                String request = "SELECT id, nom, nbplaces, image FROM stades;";
                SqlCommand sqlCommand = new SqlCommand(request, sqlConnection);
                sqlConnection.Open();

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    List<Caracteristique> carac = new List<Caracteristique>();

                    using (SqlConnection sqlConnection2 = new SqlConnection(connectionString))
                    {
                        String id = sqlDataReader.GetInt32((int)JediField.ID).ToString();
                        String request2 = "SELECT C.id, C.nom, C.def, C.type, C.valeur FROM stades s, caracteristiques C, StadeCarac SC WHERE S.id=" + id + " AND S.id=SC.idstade AND S.id=SC.idcarac;";
                        SqlCommand sqlCommand2 = new SqlCommand(request2, sqlConnection2);
                        sqlConnection2.Open();

                        SqlDataReader sqlDataReader2 = sqlCommand2.ExecuteReader();
                        while (sqlDataReader2.Read())
                        {
                            carac.Add(new Caracteristique(  sqlDataReader2.GetInt32((int)CaracField.ID),
                                                            convertDef(sqlDataReader2.GetString((int)CaracField.DEF)),
                                                            sqlDataReader2.GetString((int)CaracField.NOM),
                                                            convertType(sqlDataReader2.GetString((int)CaracField.TYPE)),
                                                            sqlDataReader2.GetInt32((int)CaracField.VALEUR))
                            );
                        }
                        sqlConnection2.Close();
                    }

                    allStades.Add(new Stade(    sqlDataReader.GetInt32((int)StadeField.ID),
                                                sqlDataReader.GetInt32((int)StadeField.NBPLACES),
                                                sqlDataReader.GetString((int)StadeField.NOM),
                                                carac,
                                                sqlDataReader.GetString((int)StadeField.IMAGE)));
                }
                sqlDataReader.Close();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                dataTables[(int)DTName.STADES].Clear();
                sqlDataAdapter.Fill(dataTables[(int)DTName.STADES]);
                sqlDataAdapter = new SqlDataAdapter(new SqlCommand("SELECT idstade, idcarac FROM StadeCarac;", sqlConnection));
                dataTables[(int)DTName.STADECARAC].Clear();
                sqlDataAdapter.Fill(dataTables[(int)DTName.STADECARAC]);
                sqlConnection.Close();
            }
            return allStades;
        }
        public Utilisateur getUtilisateurByLogin(string login)
        {
            Utilisateur us = null;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                String request = "SELECT id, login, password, nom, prenom FROM users WHERE login='" + login + "';";
                SqlCommand sqlCommand = new SqlCommand(request, sqlConnection);
                sqlConnection.Open();

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    us = new Utilisateur(sqlDataReader.GetInt32((int)UserField.ID), sqlDataReader.GetString((int)UserField.LOGIN), sqlDataReader.GetString((int)UserField.PASSWORD), sqlDataReader.GetString((int)UserField.NOM), sqlDataReader.GetString((int)UserField.PRENOM));
                }
                sqlDataReader.Close();
                sqlConnection.Close();
            }
            return us;
        }
        public List<Utilisateur> getUsers()
        {
            List<Utilisateur> us = new List<Utilisateur>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                String request = "SELECT id, login, password, nom, prenom FROM users;";
                SqlCommand sqlCommand = new SqlCommand(request, sqlConnection);
                sqlConnection.Open();

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    us.Add(new Utilisateur(sqlDataReader.GetInt32((int)UserField.ID), sqlDataReader.GetString((int)UserField.LOGIN), sqlDataReader.GetString((int)UserField.PASSWORD), sqlDataReader.GetString((int)UserField.NOM), sqlDataReader.GetString((int)UserField.PRENOM)));
                }
                sqlDataReader.Close();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                dataTables[(int)DTName.USERS].Clear();
                sqlDataAdapter.Fill(dataTables[(int)DTName.USERS]);
                sqlConnection.Close();
            }
            return us;
        }
        public List<Tournoi> getTournois()
        {
            List<Tournoi> allTournois = new List<Tournoi>();
            List<Match> allMatches = this.getMatches();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                String request = "SELECT id, nom FROM Tournois;";
                SqlCommand sqlCommand = new SqlCommand(request, sqlConnection);
                sqlConnection.Open();

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    List<Match> matches = new List<Match>();

                    using (SqlConnection sqlConnection2 = new SqlConnection(connectionString))
                    {
                        String id = sqlDataReader.GetInt32((int)TournoiField.ID).ToString();
                        String request2 = "SELECT idMatch FROM MatchTournoi WHERE idTournoi=" + id;
                        SqlCommand sqlCommand2 = new SqlCommand(request2, sqlConnection2);
                        sqlConnection2.Open();

                        SqlDataReader sqlDataReader2 = sqlCommand2.ExecuteReader();
                        while (sqlDataReader2.Read())
                        {
                            matches.Add(allMatches.Where(x => x.Id == sqlDataReader2.GetInt32(0)).First());
                        }
                        sqlConnection2.Close();
                    }
                    allTournois.Add(new Tournoi(sqlDataReader.GetInt32((int)TournoiField.ID),
                                                sqlDataReader.GetString((int)TournoiField.NOM),
                                                matches));
                }
                sqlDataReader.Close();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                dataTables[(int)DTName.MATCHES].Clear();
                sqlDataAdapter.Fill(dataTables[(int)DTName.MATCHES]);
                sqlDataAdapter = new SqlDataAdapter(new SqlCommand("SELECT idtournoi, idmatch, idmatchtournoi FROM MatchTournoi;", sqlConnection));
                dataTables[(int)DTName.MATCHTOURNOI].Clear();
                sqlDataAdapter.Fill(dataTables[(int)DTName.MATCHTOURNOI]);
                sqlConnection.Close();
            }
            return allTournois;
        }
        #endregion

        #region Updaters
        public int updateCaracteristiques(List<Caracteristique> l)
        {
            int result = 0;
            int maximum = 1;

            foreach(Caracteristique c in l)
            {
                maximum = (c.Id>=maximum?c.Id+1:maximum);
            }

            foreach (Caracteristique c in l)    // ajout et modification
            {
                DataRow row;
                bool nouveau = false;
                int numero = (c.Id == 0 ? maximum++ : c.Id);

                if (dataTables[(int)DTName.CARAC].Select("Id = "+c.Id.ToString()).Length!=0)
                {
                    row = dataTables[(int)DTName.CARAC].Select("Id = " + c.Id.ToString()).First();
                }
                else
                {
                    row = dataTables[(int)DTName.CARAC].NewRow();
                    nouveau = true;
                    c.Id = numero;
                }

                row[(int)CaracField.ID] = numero;
                row[(int)CaracField.NOM] = c.Nom;
                row[(int)CaracField.DEF] = c.Definition.ToString();
                row[(int)CaracField.TYPE] = c.Type.ToString();
                row[(int)CaracField.VALEUR] = c.Valeur;

                if (nouveau)
                {
                    dataTables[(int)DTName.CARAC].Rows.Add(row);
                }
            }
            
            foreach (DataRow ligne in dataTables[(int)DTName.CARAC].Select())   // suppression
            {
                if(l.Count(c => c.Id == (int)ligne["Id"])==0)
                {
                    Console.WriteLine(dataTables[(int)DTName.JEDICARAC]);
                    DataRow[] res = dataTables[(int)DTName.JEDICARAC].Select("IdCarac = " + ligne["Id"].ToString());
                    foreach (DataRow lineJC in res)
                    {
                        lineJC.Delete();
                    }
                    ligne.Delete();
                }
            }

            UpdateByCommandBuilder("SELECT idjedi, idcarac FROM JediCarac;", dataTables[(int)DTName.JEDICARAC]);
            UpdateByCommandBuilder("SELECT C.id, C.nom, C.def, C.type, C.valeur FROM caracteristiques C;", dataTables[(int)DTName.CARAC]);

            return result;
        }
        public int updateJedis(List<Jedi> l)
        {
            int result = 0;
            int maximum = 1;
            bool deleteOccur = false;
            
            foreach (Jedi c in l)
            {
                maximum = (c.Id >= maximum ? c.Id + 1 : maximum);
            }

            foreach (Jedi c in l)    // ajout et modification
            {
                DataRow row;
                int numero = (c.Id == 0 ? maximum++ : c.Id);

                if (dataTables[(int)DTName.JEDIS].Select("Id = " + c.Id.ToString()).Length != 0)
                {
                    row = dataTables[(int)DTName.JEDIS].Select("Id = " + c.Id.ToString()).First();
                    row[(int)JediField.ID] = numero;
                    row[(int)JediField.NOM] = c.Nom;
                    row[(int)JediField.ISSITH] = c.IsSith;
                    row[(int)JediField.IMAGE] = c.Image;
                }
                else
                {
                    row = dataTables[(int)DTName.JEDIS].NewRow();
                    dataTables[(int)DTName.JEDIS].LoadDataRow(new object[] { numero, c.Nom, c.IsSith, c.Image }, false);
                    c.Id = numero;
                }

                foreach (DataRow lineJC in dataTables[(int)DTName.JEDICARAC].Select("IdJedi = " + numero))
                {
                    lineJC.Delete();
                }
                if (c.Caracteristiques != null)
                {
                    foreach(Caracteristique ca in c.Caracteristiques)
                    {
                        DataRow newRow = dataTables[(int)DTName.JEDICARAC].NewRow();
                        newRow["IdJedi"] = c.Id;
                        newRow["IdCarac"] = ca.Id;
                        dataTables[(int)DTName.JEDICARAC].Rows.Add(newRow);
                    }
                }
            }

            foreach (DataRow ligne in dataTables[(int)DTName.JEDIS].Select())   // suppression
            {
                if (l.Count(c => c.Id == (int)ligne["Id"]) == 0)
                {
                    // suppression des references sur les caracteristiques
                    foreach (DataRow lineJC in dataTables[(int)DTName.JEDICARAC].Select("IdJedi = " + ligne["Id"].ToString()))
                    {
                        lineJC.Delete();
                    }
                    // suppression des matches ou le jedi joue
                    foreach (DataRow lineM in dataTables[(int)DTName.MATCHES].Select("jedi1 = " + ligne["Id"].ToString() + " OR jedi2 = " + ligne["Id"].ToString()))
                    {
                        // suppression de la reference
                        foreach (DataRow lineMT in dataTables[(int)DTName.MATCHTOURNOI].Select("IdMatch = " + lineM["Id"].ToString()))
                        {
                            lineMT.Delete();
                        }
                        lineM.Delete();
                    }
                    ligne.Delete();
                    deleteOccur = true;
                }
            }

            if(deleteOccur)
            {
                UpdateByCommandBuilder("SELECT id, jedi1, jedi2, stade, vainqueur, phase FROM Matches;", dataTables[(int)DTName.TOURNOIS]);
                UpdateByCommandBuilder("SELECT idtournoi, idmatch, idmatchtournoi FROM MatchTournoi;", dataTables[(int)DTName.MATCHTOURNOI]);
            }
            UpdateByCommandBuilder("SELECT id, nom, isSith, image FROM jedis;", dataTables[(int)DTName.JEDIS]);
            UpdateByCommandBuilder("SELECT idjedi, idcarac FROM JediCarac;", dataTables[(int)DTName.JEDICARAC]);

            return result;
        }
        public int updateMatches(List<Match> l)
        {
            int result = 0;
            int maximum = 1;

            foreach (Match c in l)
            {
                maximum = (c.Id >= maximum ? c.Id + 1 : maximum);
            }

            foreach (Match c in l)    // ajout et modification
            {
                DataRow row;
                int numero = (c.Id == 0 ? maximum++ : c.Id);

                if (dataTables[(int)DTName.MATCHES].Select("Id = " + c.Id.ToString()).Length != 0)
                {
                    row = dataTables[(int)DTName.MATCHES].Select("Id = " + c.Id.ToString()).First();
                    row[(int)MatchField.ID] = numero;
                    row[(int)MatchField.JEDI1] = (c.Jedi1 != null ? c.Jedi1.Id : 0);
                    row[(int)MatchField.JEDI2] = (c.Jedi2 != null ? c.Jedi2.Id : 0);
                    row[(int)MatchField.STADE] = (c.Stade != null ? c.Stade.Id : 0);
                    row[(int)MatchField.WINNER] = (c.JediVainqueur != null ? c.JediVainqueur.Id : 0);
                    row[(int)MatchField.PHASE] = (int)c.PhaseTournoi;
                }
                else
                {
                    row = dataTables[(int)DTName.MATCHES].NewRow();
                    dataTables[(int)DTName.MATCHES].LoadDataRow(new object[] { numero,
                                                        (c.Jedi1 != null ? c.Jedi1.Id : 0),
                                                        (c.Jedi2 != null ? c.Jedi2.Id : 0),
                                                        (c.Stade != null ? c.Stade.Id : 0),
                                                        (c.JediVainqueur != null ? c.JediVainqueur.Id : 0),
                                                        (int)c.PhaseTournoi }, false);
                    c.Id = numero;
                }
            }

            foreach (DataRow ligne in dataTables[(int)DTName.MATCHES].Select())   // suppression
            {
                if (l.Count(c => c.Id == (int)ligne["Id"]) == 0)
                {
                    foreach (DataRow lineJC in dataTables[(int)DTName.MATCHTOURNOI].Select("IdMatch = " + ligne["Id"].ToString()))
                    {
                        lineJC.Delete();
                    }
                    ligne.Delete();
                }
            }

            UpdateByCommandBuilder("SELECT id, jedi1, jedi2, stade, vainqueur, phase FROM Matches;", dataTables[(int)DTName.MATCHES]);
            UpdateByCommandBuilder("SELECT idtournoi, idmatch, idmatchtournoi FROM MatchTournoi;", dataTables[(int)DTName.MATCHTOURNOI]);

            return result;
        }
        public int updateStades(List<Stade> l)
        {
            int result = 0;
            int maximum = 1;
            bool deleteOccur = false;

            foreach (Stade c in l)
            {
                maximum = (c.Id >= maximum ? c.Id + 1 : maximum);
            }

            foreach (Stade c in l)    // ajout et modification
            {
                DataRow row;
                int numero = (c.Id == 0 ? maximum++ : c.Id);

                if (dataTables[(int)DTName.STADES].Select("Id = " + c.Id.ToString()).Length != 0)
                {
                    row = dataTables[(int)DTName.STADES].Select("Id = " + c.Id.ToString()).First();
                    row[(int)StadeField.ID] = numero;
                    row[(int)StadeField.NOM] = c.Planete;
                    row[(int)StadeField.NBPLACES] = c.NbPlaces;
                    row[(int)StadeField.IMAGE] = c.Image;
                }
                else
                {
                    row = dataTables[(int)DTName.STADES].NewRow();
                    dataTables[(int)DTName.STADES].LoadDataRow(new object[] { numero, c.Planete, c.NbPlaces, c.Image }, false);
                    c.Id = numero;
                }

                foreach (DataRow lineJC in dataTables[(int)DTName.STADECARAC].Select("IdStade = " + numero))
                {
                    lineJC.Delete();
                }
                if (c.Caracteristiques != null)
                {
                    foreach (Caracteristique ca in c.Caracteristiques)
                    {
                        DataRow newRow = dataTables[(int)DTName.STADECARAC].NewRow();
                        newRow["IdStade"] = c.Id;
                        newRow["IdCarac"] = ca.Id;
                        dataTables[(int)DTName.STADECARAC].Rows.Add(newRow);
                    }
                }
            }

            foreach (DataRow ligne in dataTables[(int)DTName.STADES].Select())   // suppression
            {
                if (l.Count(c => c.Id == (int)ligne["Id"]) == 0)
                {
                    foreach (DataRow lineJC in dataTables[(int)DTName.STADECARAC].Select("IdStade = " + ligne["Id"].ToString()))
                    {
                        lineJC.Delete();
                    }
                    // suppression des matches ou le stade est utilise
                    foreach (DataRow lineM in dataTables[(int)DTName.MATCHES].Select("stade = " + ligne["Id"].ToString()))
                    {
                        // suppression de la reference du match
                        foreach (DataRow lineMT in dataTables[(int)DTName.MATCHTOURNOI].Select("IdMatch = " + lineM["Id"].ToString()))
                        {
                            lineMT.Delete();
                        }
                        lineM.Delete();
                    }
                    ligne.Delete();
                    deleteOccur = true;
                }
            }

            if(deleteOccur)
            {
                UpdateByCommandBuilder("SELECT id, jedi1, jedi2, stade, vainqueur, phase FROM Matches;", dataTables[(int)DTName.TOURNOIS]);
                UpdateByCommandBuilder("SELECT idtournoi, idmatch, idmatchtournoi FROM MatchTournoi;", dataTables[(int)DTName.MATCHTOURNOI]);
            }
            UpdateByCommandBuilder("SELECT id, nom, nbplaces, image FROM stades;", dataTables[(int)DTName.STADES]);
            UpdateByCommandBuilder("SELECT idstade, idcarac FROM StadeCarac;", dataTables[(int)DTName.STADECARAC]);

            return result;
        }
        public int updateTournois(List<Tournoi> l)
        {
            int result = 0;
            int maximum = 1;

            foreach (Tournoi c in l)
            {
                maximum = (c.Id >= maximum ? c.Id + 1 : maximum);
            }

            foreach (Tournoi c in l)    // ajout et modification
            {
                DataRow row;
                int numero = (c.Id == 0 ? maximum++ : c.Id);

                if (dataTables[(int)DTName.TOURNOIS].Select("Id = " + c.Id.ToString()).Length != 0)     // modif
                {
                    row = dataTables[(int)DTName.TOURNOIS].Select("Id = " + c.Id.ToString()).First();
                    row[(int)TournoiField.ID] = numero;
                    row[(int)TournoiField.NOM] = c.Nom;
                }
                else        // ajout
                {
                    row = dataTables[(int)DTName.TOURNOIS].NewRow();
                    dataTables[(int)DTName.TOURNOIS].LoadDataRow(new object[] { numero, c.Nom }, false);
                    c.Id = numero;
                }

                foreach (DataRow lineJC in dataTables[(int)DTName.MATCHTOURNOI].Select("IdTournoi = " + numero))    // suppression des matches pour update
                {
                    lineJC.Delete();
                }
                if (c.Matchs != null)       // update des matches
                {
                    List<Match> MajMatch = getMatches();
                    foreach (Match ca in c.Matchs)
                    {
                        DataRow newRow = dataTables[(int)DTName.MATCHTOURNOI].NewRow();
                        newRow["IdTournoi"] = c.Id;
                        newRow["IdMatch"] = ca.Id;
                        dataTables[(int)DTName.MATCHTOURNOI].Rows.Add(newRow);

                        /* MAJ DES MATCHES */
                        Match toModify = MajMatch.Where(m => m.Id == ca.Id).First();
                        toModify.Jedi1 = ca.Jedi1;
                        toModify.Jedi2 = ca.Jedi2;
                        toModify.Stade = ca.Stade;
                        toModify.JediVainqueur = ca.JediVainqueur;
                        toModify.PhaseTournoi = ca.PhaseTournoi;
                    }
                    updateMatches(MajMatch);
                }
            }

            foreach (DataRow ligne in dataTables[(int)DTName.TOURNOIS].Select())   // suppression
            {
                if (l.Count(c => c.Id == (int)ligne["Id"]) == 0)
                {
                    foreach (DataRow lineJC in dataTables[(int)DTName.MATCHTOURNOI].Select("IdTournoi = " + ligne["Id"].ToString()))
                    {
                        lineJC.Delete();
                    }
                    ligne.Delete();
                }
            }

            UpdateByCommandBuilder("SELECT id, nom FROM Tournois;", dataTables[(int)DTName.TOURNOIS]);
            UpdateByCommandBuilder("SELECT idtournoi, idmatch, idmatchtournoi FROM MatchTournoi;", dataTables[(int)DTName.MATCHTOURNOI]);

            return result;
        }
        public bool addUser(Utilisateur u)
        {
            bool result = true;
            int maximum = 1;
            DataTable dt = new DataTable();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                String request = "SELECT id, login, password, nom, prenom FROM Users;";
                SqlCommand sqlCommand = new SqlCommand(request, sqlConnection);
                sqlConnection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                
                sqlDataAdapter.Fill(dt);
                sqlConnection.Close();
            }


            foreach (DataRow c in dt.Select())
            {
                if(u.Login == c.Field<string>("Login"))
                    result = false;
            }

            if (result)
            {
                foreach (DataRow dr in dt.Select())
                {
                    maximum = (dr.Field<int>("Id") >= maximum ? dr.Field<int>("Id") + 1 : maximum);
                }
                u.Id = maximum;
                DataRow row = dt.NewRow();
                row.SetField<int>("Id", u.Id);
                row.SetField<string>("Login", u.Login);
                row.SetField<string>("Password", u.Password);
                row.SetField<string>("Prenom", u.Prenom);
                row.SetField<string>("Nom", u.Nom);
                dt.Rows.Add(row);
                UpdateByCommandBuilder("SELECT id, login, password, nom, prenom FROM Users;", dt);
            }

            return result;
        }
        public bool deleteUserByLogin(string login)
        {
            bool result = true;
            DataTable dt = new DataTable();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                String request = "SELECT id, login, password, nom, prenom FROM Users;";
                SqlCommand sqlCommand = new SqlCommand(request, sqlConnection);
                sqlConnection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                sqlDataAdapter.Fill(dt);
                sqlConnection.Close();
            }


            foreach (DataRow c in dt.Select())
            {
                if (login == c.Field<string>("Login"))
                    c.Delete();
            }

            UpdateByCommandBuilder("SELECT id, login, password, nom, prenom FROM Users;", dt);
            
            return result;
        }
        #endregion

        #region Annexe
        private int UpdateByCommandBuilder(string request, DataTable table)
        {
            int result = 0;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlTransaction myTrans = sqlConnection.BeginTransaction();
                SqlCommand sqlCommand = new SqlCommand(request, sqlConnection, myTrans);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                SqlCommandBuilder sqlCommandbuild = new SqlCommandBuilder(sqlDataAdapter);

                sqlDataAdapter.UpdateCommand = sqlCommandbuild.GetUpdateCommand();
                sqlDataAdapter.InsertCommand = sqlCommandbuild.GetInsertCommand();
                sqlDataAdapter.DeleteCommand = sqlCommandbuild.GetDeleteCommand();

                sqlDataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

                try
                {
                    result = sqlDataAdapter.Update(table);
                    myTrans.Commit();
                }
                catch (DBConcurrencyException)
                {
                    myTrans.Rollback();
                }
            }
            return result;
        }
        private EDefCaracteristique convertDef(String s)
        {
            EDefCaracteristique retour;

            if (s.Equals("Strength"))
            {
                retour = EDefCaracteristique.Strength;
            }
            else if (s.Equals("Dexterity"))
            {
                retour = EDefCaracteristique.Dexterity;
            }
            else
            {
                retour = EDefCaracteristique.Perception;
            }

            return retour;
        }
        private ETypeCaracteristique convertType(String s)
        {
            ETypeCaracteristique retour = 0;

            if (s.Equals("Jedi"))
            {
                retour = ETypeCaracteristique.Jedi;
            }
            else if (s.Equals("Stade"))
            {
                retour = ETypeCaracteristique.Stade;
            }

            return retour;
        }
        #endregion
    }
}
