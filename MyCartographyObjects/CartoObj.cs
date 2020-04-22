using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCartographyObjects 
{
    [Serializable]
    public abstract class CartoObj : ICartoObj
    {
        private int _id;
        protected static int id_generator = 0;

        public CartoObj()
        {
            id_generator++;
            Id = id_generator;
        }

        /*public CartoObj(int num)
        {
            Id = num;
        }*/
        public void Affiche()
        {
            Console.WriteLine("Id :" + Id);
        }
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public override string ToString()
        {
            return this.Id + " ";
        }

        public virtual void Draw()
        {
            Console.WriteLine(ToString());
        }

    }
}
