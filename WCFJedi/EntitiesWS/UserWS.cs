using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntitiesLayer;
using System.Runtime.Serialization;

namespace WCFJedi
{
    [DataContract]
    public class UserWS
    {
        [DataMember]
		public int Id { get; set; }
        [DataMember]
        public string Nom { get; set; }
        [DataMember]
        public string Prenom { get; set; }
        [DataMember]
        public string Login { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public int Points { get; set; }

        public UserWS(Utilisateur u)
        {
			this.Id = u.Id;
            this.Nom = u.Nom;
            this.Prenom = u.Prenom;
            this.Login = u.Login;
            this.Password = u.Password;
            this.Points = u.Points;
        }

        public UserWS(int pId, string pLogin, string pPassword, int pPoints, string pNom, string pPrenom)
        {
            this.Id = pId;
            this.Nom = pNom;
            this.Prenom = pPrenom;
            this.Login = pLogin;
            this.Password = pPassword;
            this.Points = pPoints;
        }
    }
}