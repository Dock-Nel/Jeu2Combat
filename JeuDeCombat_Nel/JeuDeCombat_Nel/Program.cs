using System;
using System.Collections.Generic;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

class Program
{
    static void Main()
    {

        // choix des persos et methode de tour par tour

        //  3 persos demander au joueur avec un choix 1 2 3, afficher le perso choisit

        // Choisir entre un des trois new classe pour l'IA 

        Personnage persoHumain = null;
        Personnage persoIA = null;

        Console.WriteLine("+----------------------------------+");
        Console.WriteLine("| Realy Fantastic Fighting Game    |");
        Console.WriteLine("+----------------------------------+");
        Console.WriteLine("\nBienvenue dans l'arène !\n");



        Console.WriteLine("Catégories de personnages disponibles :");
        Console.WriteLine("1 - Damager ");
        Console.WriteLine("2 - Healer");
        Console.WriteLine("3 - Tank");
        Console.WriteLine("Choix : ");

        int choixJ = int.Parse(Console.ReadLine());

        switch (choixJ)
        {
            case 1:
                persoHumain = new Damager();
                break;
            case 2:
                persoHumain = new Healer();
                break;
            case 3:
                persoHumain = new Tank();
                break;
            default:
                Console.WriteLine("Veuillez choisir un personnage disponible.");
                break;
        }
        Console.WriteLine("Veuillez choisir un personnage disponible.");

        Console.WriteLine("\n\nTu as choisis de jouer un " + persoHumain + ".");
        System.Threading.Thread.Sleep(1000);

        Random random = new Random();
        // Générer un nombre entre 0 et 3
        int nombreRNG = random.Next(1, 3);

        switch (nombreRNG)
        {
            case 1:
                persoIA = new Damager();
                break;
            case 2:
                persoIA = new Healer();
                break;
            case 3:
                persoIA = new Tank();
                break;
            default:
                Console.WriteLine("Erreur");
                break;
        }

        Console.WriteLine("Tu vas affronter un  " + persoIA + " joué par une IA.");
        System.Threading.Thread.Sleep(1000);

        // --------------------------------- JEU TOUR PAR TOUR BOUCLE ----------------------------------

        //persoIA.health = 0;
        int nbrIteration = 1;
        while (persoHumain.health >= 1 && persoIA.health >= 1)
        {
            Console.WriteLine("\n\n+----------+");
            Console.WriteLine("| Manche " + nbrIteration + " |");
            Console.WriteLine("+----------+");

            nbrIteration++;

            string subRoleJoueur = persoHumain.name.Substring(0, 1);
            string subRoleIa = persoIA.name.Substring(0, 1);

            Console.WriteLine("[" + persoHumain.health + "pv] " + subRoleJoueur + "       " + "[" + persoIA.health + "pv] " + subRoleIa);

            Console.WriteLine("\nActions possibles :");
            Console.WriteLine("1 - Attaquer");
            Console.WriteLine("2 - Défendre");
            Console.WriteLine("3 - Actions spéciale");
            int choixAttaque;

            string saisie;
            do
            {
                Console.Write("Choix : ");
                saisie = Console.ReadLine();
                System.Threading.Thread.Sleep(1000);
                if (!int.TryParse(saisie, out choixAttaque) || (choixAttaque < 1 || choixAttaque > 3))
                {
                    Console.WriteLine("Veuillez saisir un nombre valide (1, 2 ou 3).");
                }
            } while (choixAttaque < 1 || choixAttaque > 3);

            //A ajouter dans la boucle si lia attaque 
            bool iaAttaque = true;

            persoIA.Attaquer(persoHumain);

            switch (choixAttaque)
            {
                case 1:
                    persoHumain.Attaquer(persoIA);
                    break;
                case 2:
                    persoHumain.Defense(persoIA, iaAttaque);
                    break;
                case 3:
                    if (persoHumain is Healer healer)
                    {
                        if (healer.Soin())
                        {
                            healer.Soin();
                        }
                        else
                        {
                            Console.Write("Vous ne pouvez pas vous heal plus que votre vie de base");
                        }
                    }
                    else if (persoHumain is Damager damager)
                    {
                        damager.Rage(persoIA);
                    }
                    else if (persoHumain is Tank tank)
                    {
                        tank.AttaquePuissante(persoIA);
                    }
                    break;
                case 4:
                    break;
            }

        }

        // ------------------------------------- FIN DU JEU ---------------------------------------

        if (persoHumain.health != 0)
        {
            Console.WriteLine("\n\n+-------------------+");
            Console.WriteLine("| VOUS AVEZ GAGNE ! |");
            Console.WriteLine("+-------------------+\n");
            Console.WriteLine("        d8b        888                           ");
            Console.WriteLine("        Y8P        888                           ");
            Console.WriteLine("                   888                           ");
            Console.WriteLine("888  888888 .d8888b888888 .d88b. 888d888888  888 ");
            Console.WriteLine("888  888888d88P\"   888   d88\"\"88b888P\"  888  888 ");
            Console.WriteLine("Y88  88P888888     888   888  888888    888  888 ");
            Console.WriteLine(" Y8bd8P 888Y88b.   Y88b. Y88..88P888    Y88b 888 ");
            Console.WriteLine("  Y88P  888 \"Y8888P \"Y888 \"Y88P\" 888     \"Y88888 ");
            Console.WriteLine("                                             888 ");
            Console.WriteLine("                                        Y8b d88P ");
            Console.WriteLine("                                         \"Y88P\"  ");
        }
        else
        {
            Console.WriteLine("\n\n+------------------+");
            Console.WriteLine("| VICTOIRE DE L'IA |");
            Console.WriteLine("+------------------+");

            Console.WriteLine("Try again ? : ");
            Console.WriteLine("y - yes");
            Console.WriteLine("n - no");
            string newGame = Console.ReadLine();
            //if( newGame == "y")
            //{

            //}
            //else
            //{
            //    break;
            //}
        }



    }
}

public class Personnage
{
    public string name { get; set; }
    public int health { get; set; }
    public int attack { get; set; }
    public int baseHealth { get; set; }
    public Personnage(string name, int health, int attack)
    {
        this.name = name;
        this.health = health;
        this.attack = attack;
    }

    public void Attaquer(Personnage cible)
    {
        cible.health -= this.attack;
    }

    public void Defense(Personnage attaquant, bool iaAttaque)
    {
        if (iaAttaque)
        {
            this.health += 1;
        }
    }
}

public class Damager : Personnage
{
    public Damager() : base("Damager", 3, 2) { }

    public string SpecialAttackName { get; } = "Rage";

    public void Rage(Personnage cible)
    {
        this.health -= cible.attack;
        cible.health -= cible.attack;
    }
}

public class Healer : Personnage
{

    public Healer() : base("Healer", 4, 1) { }

    public string SpecialAttackName { get; } = "Soin";

    public bool Soin()
    {
        bool res = true;
        if (health < baseHealth)
        {
            this.health += 2;
        }
        else
        {
            res = false;
        }
        return res;
    }
}

public class Tank : Personnage
{
    public Tank() : base("Tank", 5, 1) { }
    public string SpecialAttackName { get; } = "Attaque puissante";

    public void AttaquePuissante(Personnage cible)
    {
        cible.health -= 2;
        this.health = -1;
    }
}
