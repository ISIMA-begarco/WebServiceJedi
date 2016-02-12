using System.Runtime.InteropServices;

namespace EntitiesLayer
{
    public abstract class EntityObject
    {
        private int id = 0;
        protected static int NB = 0;

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public EntityObject()
        {
            ++NB;
        }


    }
}