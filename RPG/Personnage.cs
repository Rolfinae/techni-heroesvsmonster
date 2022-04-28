using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class Personnage
    {
        private double _Pv;
        public int degats;
        public int Xpvaleur { get; set; }

        public int Pa { get; set; }
        public double Pv
        {
            get
            {
                return _Pv;
            }
            set
            {
                _Pv = value;
                if (_Pv <= 0)
                {
                    Mort?.Invoke(this);
                }
            }

        }
        public void Frappe(Personnage target)
        {
            Random rnd = new Random();
            double delta = (Pa * 0.2);

            degats = rnd.Next((int)(Pa - delta), (int)(Pa + delta) + 1);
            if (this is Hero h)
            {
                if (h.arme == "Poing")
                {
                    target.Pv -= degats;
                }
                else if (h.arme == "Masse")
                {
                    target.Pv -= degats + 2;
                }
                else if (h.arme == "épée")
                {
                    target.Pv -= degats + 1;
                }
                else
                {
                    Console.WriteLine(h.arme);
                    target.Pv -= degats;
                }
            }
            else
            {
                target.Pv -= degats;
            }
            


            if (target.Pv <= 0)
            {
                target.Mort?.Invoke(target);
            }
            
        }
        public event Action<Personnage> Mort;
    }
}
