using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class Hero : Personnage
    {
        public string Nom { get; set; }
        public TypePersonnage Type { get; set; }
        public int Endurance { get; set; }
        public int Force { get; set; }
        private double PvBase;
        public int  level = 1;
        public double Po;
        public double Xp;
        public double StockDeCuir;
        public string arme = "Poing";
        
     

        public void Init()
        {
            Random rnd = new Random();
            List<int> eDices = new List<int>();
            List<int> sDices = new List<int>();
            for (int i = 0; i < 4; i++)
            {
                
                Endurance = rnd.Next(1, 7);
                Force = rnd.Next(1, 7);
                eDices.Add(Endurance);
                sDices.Add(Force);
            }

            Endurance = eDices.OrderByDescending(x => x).Take(3).Sum();
            Force = sDices.OrderByDescending(x => x).Take(3).Sum();

            int e = Modifier(Force);
            Pv = Endurance * (e > 0 ? e : 1);
            int a = Modifier(Force);
            Pa = Force / (a > 0 ? a : 1);
            

            //Endurance += Modifier(Endurance);
            //Force += Modifier(Force);

            PvBase = Pv;
        }
        

        private int Modifier(int caracteristique)
        {
            if (caracteristique > 15) { return 4; }
            if (caracteristique > 10) { return  3; }
            if (caracteristique > 5) { return 2; }
            else { return 1; }
        }

        public void Fuir(Personnage monstre)
        {
            Random RndFuite = new Random();
            int fuir = RndFuite.Next(0,4);
            switch (fuir)
            {
                case 1:

                    Console.WriteLine("Tu as réussis à fuir");
                    break;
                case 2:
                    Pv -= monstre.Pa/5;
                    Console.WriteLine("Tu as réussis à fuir mais vous avez perdu des points de vie");
                    break;

                case 3:
                    Pv -= monstre.Pa/2;
                    Console.WriteLine("Tu n'as pas réussis à fuir");
                    return;
                    break;


            }
                
        }

        public void BoirePotion(double pv)
        {
            
            if (Pv + pv >= PvBase)
            {
                Pv = PvBase;
                Console.WriteLine($"Vous avez {Pv} / {PvBase}");
                return;

            }
            Pv += pv;
            Console.WriteLine($"Vous avez {Pv} / {PvBase}");
        }
        public enum TypePersonnage
        {
            Nain = 2,
            Humain = 1,
            Orc = 3
        }

        public void Loot(Personnage personnage)
        {
            personnage.Mort -= Loot;
            Xp = Xp + personnage.Xpvaleur;
            Console.WriteLine($"Vous avez gagnés {personnage.Xpvaleur} points d'experience");
            if (Xp > level * 100)
            {
                Console.WriteLine("Vous Gagnez un Niveau");
                level++;
                Console.WriteLine("Vous êtes actuelement de niveau " + level);
                Xp = Xp - level * 100;
            }
            Random rdnPo = new Random();
            int curentPo = rdnPo.Next(1,100);
            Po += curentPo;
            Console.WriteLine($"Vous avez gagné {curentPo} Pièces d'Or et votre total s'élève à {Po}");
            if (personnage is Monstres m && m.depecable)
            {
                StockDeCuir++;
            }
            Random chanceloot = new Random();
            int chLoot = chanceloot.Next(1,20);
            if(chLoot == 1 || chLoot == 2)
            {
                Console.WriteLine("Vous avez Lootez une masse !!!!!");
                Console.WriteLine("Que voulez-vous faire ?");
                Console.WriteLine("1. équiper la masse");
                Console.WriteLine("2. Vendre la masse");
                string action = Console.ReadLine();
                int actionNbr = int.Parse(action);
                if(actionNbr == 1)
                {
                    arme = "masse";
                }
                else if(actionNbr == 2)
                {
                    Po =+ 15;
                }
            }
            if(chLoot == 3 || chLoot == 4)
            {
                Console.WriteLine("Vous avez Lootez une épée !!!!!");
                Console.WriteLine("Que voulez-vous faire ?");
                Console.WriteLine("1. équiper l'épée");
                Console.WriteLine("2. Vendre l'épée");
                string action = Console.ReadLine();
                int actionNbr = int.Parse(action);
                if (actionNbr == 1)
                {
                    arme = "épée";
                }
                else if (actionNbr == 2)
                {
                    Po = +10;
                }
            }
            else
            {
                Console.WriteLine("Po d'Chance");
            }
            
        }
    }
}
