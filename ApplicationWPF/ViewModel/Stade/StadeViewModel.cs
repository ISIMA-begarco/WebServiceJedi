using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiblioWPF.ViewModel;

namespace ApplicationWPF.ViewModel.Stade
{
    class StadeViewModel : ViewModelBase
    {
        private EntitiesLayer.Stade m_stade;

        public EntitiesLayer.Stade Stade
        {
            get { return m_stade; }
            set { m_stade = value; }
        }

        public StadeViewModel(EntitiesLayer.Stade stade)
        {
            m_stade = stade;
        }

        public string Planete
        {
            get { return m_stade.Planete; }
            set
            {
                m_stade.Planete = value;
                OnPropertyChanged("Planete");
            }
        }

        public int NbPlaces
        {
            get { return m_stade.NbPlaces; }
            set
            {
                m_stade.NbPlaces = value;
                OnPropertyChanged("NbPlaces");
            }
        }


        public Uri ImageUri
        {
            get {return  m_stade.ImageUri; }
        }

        public string CaracteristiquesString
        {
            get
            {
                string res = "";
                foreach (EntitiesLayer.Caracteristique carac in m_stade.Caracteristiques)
                    res += carac.Nom + ',';
                return res;
            }
        }

        public List<EntitiesLayer.Caracteristique> Caracteristiques
        {
            get { return m_stade.Caracteristiques; }
            set
            {
                m_stade.Caracteristiques = value;
                OnPropertyChanged("Caracteristiques");
                OnPropertyChanged("CaracteristiquesString");
            }
        }
    }
}
