using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiblioWPF.ViewModel;

namespace ApplicationWPF.ViewModel.Jedi
{
    class JedisModelView : ViewModelBase
    {
        private ObservableCollection<JediViewModel> m_jedis;
        private JediViewModel m_selectedItem;

        private RelayCommand m_addCommand;
        private RelayCommand m_removeCommand;
        

        public ObservableCollection<JediViewModel> Jedis
        {
            get { return m_jedis; }
            private set
            {
                m_jedis = value;
                OnPropertyChanged("Jedis");
            }
        }

        public JediViewModel SelectedJedi
        {
            get { return m_selectedItem; }
            set
            {
                m_selectedItem = value;
                OnPropertyChanged("SelectedJedi");
            }
        }

        public JedisModelView(IList<EntitiesLayer.Jedi> jedisModel)
        {
            m_jedis = new ObservableCollection<JediViewModel>();
            foreach (EntitiesLayer.Jedi j in jedisModel)
            {
                m_jedis.Add(new JediViewModel (j));
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
            EntitiesLayer.Jedi j = new EntitiesLayer.Jedi(0, new List<EntitiesLayer.Caracteristique>(), false, "New");   
            this.SelectedJedi = new JediViewModel(j);
            m_jedis.Add(this.SelectedJedi);
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
            return (this.SelectedJedi != null);
        }

        private void Remove()
        {
            if (this.SelectedJedi != null) m_jedis.Remove(this.SelectedJedi);
        }
    }
}
