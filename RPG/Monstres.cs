using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class Monstres : Personnage
    {
        public bool depecable;
        
        
        public TypeMonstres Type { get; set; }
        public enum TypeMonstres
        {
            Squellete = 1,
            Troll = 2,
            Dragonnet = 3,
            Goblin = 4,
            Brigand = 5,
            Loup = 6,
            Epouvantail = 7,
            kobold = 8,

        }
    }
}
