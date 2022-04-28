using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RPG.Hero;

namespace RPG
{
    public class Programme
    {
        private Action<Monstres, Hero> CombatAction;
        public static void Main(params string[] args)
        {
            Programme p = new Programme();
            p.Execute();
        }
        public void Execute()
        {
            Hero Hero = new Hero();
            Hero.Mort += GameOver;

            Console.WriteLine("Votre Nom :");
            Hero.Nom = Console.ReadLine();

            Console.WriteLine("Choississez Votre Race :");
            Console.WriteLine("1. Humain");
            Console.WriteLine("2. Nain");
            string raceHero = Console.ReadLine();
            int raceHeroNbr = int.Parse(raceHero);
            ChoixRace(in raceHeroNbr, Hero);
            Affichage(Hero);
            bool prem = false;
            Jouer(Hero,prem);
            
        }

        private void Jouer(Hero hero, bool prem)
        {
            Console.WriteLine("Visitez le donjon ?");
            Console.WriteLine("1. Oui");
            Console.WriteLine("2. Non");
            string visiteDj = Console.ReadLine();
            int visiteDjNbr = int.Parse(visiteDj);
            if (visiteDjNbr == 1)
            {
                Generation_Map(hero);
            }
            else
            {
                Console.WriteLine("Fuyard");
                return;
            }
            while (hero.Pv > 0)
            {
                Console.WriteLine("Continuez? ");
                Console.WriteLine("1. Oui");
                Console.WriteLine("2. Non");
                visiteDj = Console.ReadLine();
                visiteDjNbr = int.Parse(visiteDj);
                if (visiteDjNbr == 1)
                {
                    Generation_Map(hero);
                }
                else
                {
                    Console.WriteLine("Fuyard");
                    return;
                }
            }

        }

        private void Generation_Map(Hero hero)
        {
            Random random = new Random();
            int ChoixObstacle = random.Next(1, 8);
            switch(ChoixObstacle)
                {
                case 1:
                    Console.WriteLine("Vous êtes tombé dans un Trou");
                    int piegePv = (int)(hero.Pv * 0.1);
                    hero.Pv = hero.Pv - piegePv;
                    Console.WriteLine($"Vous perdez {piegePv} points de vie. il vous reste {hero.Pv} ");
                    break;
                case 2:
                    Console.WriteLine("Vous êtes ralentis par la boue");
                    break;
                case 3:
                    Console.WriteLine("Vous rencontrez un monstre. Tenez vous prêt au combat");
                    Monstres m = SelectMonstre();
                    m.Mort += hero.Loot;
                    CombatAction(m, (Hero)hero);
                    break;
                case 4:
                    Console.WriteLine("Tu as trouvé au sol une magnifique potion de points de vie");
                    int soin = (int)(hero.Pv * 0.2);
                    hero.BoirePotion(soin);
                    break;
                case 5:
                    Console.WriteLine("Rien ne ce passe");
                    break;
                case 6:
                    Console.WriteLine("Rien ne ce passe");
                    break;
                case 7:
                    Console.WriteLine("Rien ne ce passe");
                    break;
                case 8:
                    Console.WriteLine("Rien ne ce passe");
                    break;

            }
          
        }
        public void AffichageCombat(Monstres monstre, Hero hero)
        {
            Console.WriteLine($"Vous avez devant vous un monstre {monstre.Type}");
            Console.WriteLine("Point de vie du monstre : " + monstre.Pv);
            Console.WriteLine("Point d'attaque du monstre : " + monstre.Pa);

        }
        public void Combat(Monstres monstre, Hero hero)
        {
            
            Console.WriteLine("Que voulez-vous faire?");
            Console.WriteLine("1. Frapper l'ennemi");
            Console.WriteLine("2. Fuir");
             string action = Console.ReadLine();
            int actionNbr = int.Parse(action);
            if (actionNbr == 1)
            {
                hero.Frappe(monstre);
                if (monstre.Pv > 0)
                {
                    monstre.Frappe(hero);
                   
                    if (hero.Pv > 0)
                    {
                        Console.WriteLine($"Il te reste {hero.Pv} points de vie et il reste {monstre.Pv} au {monstre.Type} ");
                        Combat(monstre, hero);
                    }
                }
            }
            else if (actionNbr == 2)
            {
                hero.Fuir(monstre);

            }
        }

        private void GameOver(Personnage personnage)
        {
            Console.WriteLine("Personnage mort");
            return;
        }

        private static void RendreVisible(Monstres m, Hero p)
        {
            Console.WriteLine("BLOP");

        }

        public Monstres SelectMonstre()
        {
            CombatAction = null;

            Random random = new Random();
            int ChoixMonstre = random.Next(1, 9);
            Monstres m = new Monstres();
            m.Type = (Monstres.TypeMonstres)ChoixMonstre;
            double delta = (m.Pa * 0.2);


            switch (ChoixMonstre)
                {
                case  1:
                    CombatAction += RendreVisible;
                    m.Pv = 42;
                    m.Pa = 4;
                    m.Xpvaleur = 20;
                    

                    break;
                case 2:
                    m.Pv = 100;
                    m.Pa = 10;
                    m.Xpvaleur = 35;
                    break;
                case 3:
                    m.Pv = 26;
                    m.Pa = 3;
                    m.Xpvaleur = 8;
                    m.depecable = true;
                    break;

                case 4:
                    m.Pv = 12;
                    m.Pa = 1;
                    m.Xpvaleur = 3;
                    
                    break;
                case 5:
                    m.Pv = 20;
                    m.Pa = 2;
                    m.Xpvaleur = 4;
                    
                    break;
                case 6:
                    m.Pv = 5;
                    m.Pa = 1;
                    m.Xpvaleur = 1;
                    m.depecable = true;

                    break;
                case 7:
                    m.Pv = 1;
                    m.Pa = 0;
                    m.Xpvaleur = 1;
                    
                    break;
                case 8:
                    m.Pv = 30;
                    m.Pa = 3;
                    m.Xpvaleur = 12;
                    
                    break;

            }

            CombatAction += AffichageCombat;
            CombatAction += Combat;
            return m;
        }

        //void ChoixRace(in int raceHeroNbr, Hero_Info info)
        void ChoixRace(in int raceHeroNbr, Hero p)

        {
            if (raceHeroNbr == 1)
            {
                p.Type = TypePersonnage.Humain;
                p.Endurance += 1;
                p.Force += 1;
                p.Init();
                Console.WriteLine($"Endurance = {p.Endurance}, Force= {p.Force}, Pv= {p.Pv}, pa={p.Pa}");
                //Humain h = new Humain();
                //Stats(info, h.endurance, h.force);
            }
            else if (raceHeroNbr == 2)
            {
                p.Force += 2;
                p.Init();

                Console.WriteLine($"Endurance = {p.Endurance}, Force= {p.Force}, Pv= {p.Pv}, pa={p.Pa}");
                //Nain n = new Nain();
                //Stats(info, n.endurance,n.force);
            }
            else
            {
                Console.WriteLine("Kidding");
                Execute();
            }
        }
        //public void Stats(Hero_Info info, int endurance, int force, int pv = 0)
        //{

        //    Random random = new Random();
        //    //Hero_Info Ajout = new Hero_Info();
        //    for (int i = 0; i < 4; i++)
        //    {
        //        endurance = endurance + random.Next(1, 6);
        //        force = force + random.Next(1, 6);
        //        info.endurance = endurance;
        //        info.force = force;
        //    }
        //    if (force > 15)
        //    {
        //        pv = 4 * endurance;
        //        info.pv = pv;
        //    }
        //    else if (force <= 15 && force > 10)
        //    {
        //        pv = 3 * endurance;
        //        info.pv = pv;
        //    }
        //    else if (force <= 10 && force > 5)
        //    {
        //        pv = 2 * endurance;
        //        info.pv = pv;
        //    }
        //    else
        //    {
        //        pv = endurance;
        //        info.pv = pv;
        //    }


        //}

        public void Affichage(Hero info)
        {
            Console.WriteLine(info.Nom);
            Console.WriteLine(info.Type);
            Console.WriteLine("Point de vie : " + info.Pv);
            Console.WriteLine("Endurance : " + info.Endurance);
            Console.WriteLine("Force : " + info.Force);
            Console.WriteLine("Point d'attaque : " + info.Pa);
        }
    }
}