using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using static System.Net.Mime.MediaTypeNames;

class Program
{
    static void Main()
    {
        while (true)
        {
            Personnage persoHumain = null;
            Personnage persoIA = null;

            Console.WriteLine("+----------------------------------+");
            Console.WriteLine("| Realy Fantastic Fighting Game    |");
            Console.WriteLine("+----------------------------------+");
            Console.WriteLine("\nBienvenue dans l'arène !\n");

            int choixJ = 0;
            while (choixJ < 1 || choixJ > 3)
            {
                Console.WriteLine("Catégories de personnages disponibles :");
                Console.WriteLine("1 - Damager ");
                Console.WriteLine("2 - Healer");
                Console.WriteLine("3 - Tank");
                Console.Write("Choix : ");
                string saisieChoix = Console.ReadLine();
                if (!int.TryParse(saisieChoix, out choixJ) || choixJ < 1 || choixJ > 3)
                    choixJ = 0;
            }

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
            }

            Console.WriteLine("\n\nTu as choisis de jouer un " + persoHumain.name + ".");
            System.Threading.Thread.Sleep(1000);

            Random random = new Random();
            int nombreRNG = random.Next(1, 4);

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
            }

            Console.WriteLine("Tu vas affronter un  " + persoIA.name + " joué par une IA.");
            System.Threading.Thread.Sleep(1000);

            int nbrIteration = 0;

            while (persoHumain.health > 0 && persoIA.health > 0)
            {
                nbrIteration++;

                Console.WriteLine("\n\n+----------+");
                Console.WriteLine("| Manche " + nbrIteration + " |");
                Console.WriteLine("+----------+");

                Console.WriteLine("[" + persoHumain.health + "pv] " + persoHumain.name + "       " + "[" + persoIA.health + "pv] " + persoIA.name);

                Console.WriteLine("\nActions possibles :");
                Console.WriteLine("1 - Attaquer");
                Console.WriteLine("2 - Défendre");
                Console.WriteLine("3 - Actions spéciale");
                int choixAttaque = 0;
                string saisie;
                do
                {
                    Console.Write("Choix : ");
                    saisie = Console.ReadLine();
                    if (!int.TryParse(saisie, out choixAttaque) || choixAttaque < 1 || choixAttaque > 3)
                        choixAttaque = 0;
                } while (choixAttaque < 1 || choixAttaque > 3);
                Console.WriteLine($"vous avez choisi : {choixAttaque}\n");

                int choixIa = random.Next(1, 4);

                // Gestion spéciale des interactions Damager vs Damager
                bool actionSpeciale = false;
                if (persoHumain is Damager damagerH && persoIA is Damager damagerIA)
                {
                    if (choixAttaque == 3 && choixIa == 1)
                    {
                        damagerH.health -= damagerIA.attack;
                        damagerIA.health -= damagerIA.attack;
                        damagerH.health = Math.Max(0, damagerH.health);
                        damagerIA.health = Math.Max(0, damagerIA.health);
                        Console.WriteLine("Vous utilisez Rage pendant que l'IA attaque !");
                        actionSpeciale = true;
                        if (damagerH.health <= 0 || damagerIA.health <= 0) break;
                    }
                    else if (choixAttaque == 1 && choixIa == 3)
                    {
                        damagerIA.health -= damagerH.attack;
                        damagerH.health -= damagerH.attack;
                        damagerH.health = Math.Max(0, damagerH.health);
                        damagerIA.health = Math.Max(0, damagerIA.health);
                        Console.WriteLine("L'IA utilise Rage pendant que vous attaquez !");
                        actionSpeciale = true;
                        if (damagerH.health <= 0 || damagerIA.health <= 0) break;
                    }
                    else if (choixAttaque == 3 && choixIa == 3)
                    {
                        damagerH.health -= damagerIA.attack;
                        damagerIA.health -= damagerH.attack;
                        damagerH.health = Math.Max(0, damagerH.health);
                        damagerIA.health = Math.Max(0, damagerIA.health);
                        Console.WriteLine("Les deux utilisent Rage !");
                        actionSpeciale = true;
                        if (damagerH.health <= 0 || damagerIA.health <= 0) break;
                    }
                }

                if (!actionSpeciale)
                {
                    // Action du joueur
                    switch (choixAttaque)
                    {
                        case 1:
                            if (choixIa != 2)
                            {
                                persoHumain.Attaquer(persoIA);
                                persoIA.health = Math.Max(0, persoIA.health);
                            }
                            else
                            {
                                Console.WriteLine("Vous attaquez mais l'IA se défend !");
                            }
                            break;
                        case 2:
                            Console.WriteLine("Vous prenez une posture défensive.");
                            break;
                        case 3:
                            if (persoHumain is Healer healer)
                            {
                                if (healer.Soin())
                                    Console.WriteLine("Vous vous êtes soigné !");
                                else
                                    Console.WriteLine("Vous ne pouvez pas vous heal plus que votre vie de base");
                                healer.health = Math.Min(healer.health, healer.baseHealth);
                            }
                            else if (persoHumain is Damager damager)
                            {
                                if (choixIa == 2)
                                {
                                    Console.WriteLine("Vous utilisez Rage mais l'IA se défend, cela n'a donc aucun effet !");
                                }
                                else
                                {
                                    damager.RageAction(persoIA);
                                }
                                damager.health = Math.Max(0, damager.health);
                                persoIA.health = Math.Max(0, persoIA.health);
                            }
                            else if (persoHumain is Tank tank)
                            {
                                if (choixIa == 2)
                                {
                                    persoIA.health -= 1;
                                    persoHumain.health -= 1;
                                    persoIA.health = Math.Max(0, persoIA.health);
                                    persoHumain.health = Math.Max(0, persoHumain.health);
                                    Console.WriteLine("Vous utilisez Attaque Puissante mais l'IA défend ! 1 dégât passe quand même.");
                                }
                                else
                                {
                                    tank.AttaquePuissante(persoIA);
                                    tank.health = Math.Max(0, tank.health);
                                    persoIA.health = Math.Max(0, persoIA.health);
                                }
                            }
                            break;
                    }

                    if (persoIA.health <= 0 || persoHumain.health <= 0) break;

                    // Action de l'IA
                    switch (choixIa)
                    {
                        case 1:
                            if (choixAttaque != 2)
                            {
                                persoIA.Attaquer(persoHumain);
                                persoHumain.health = Math.Max(0, persoHumain.health);
                                Console.Write("L'ordinateur attaque\n");
                            }
                            else
                            {
                                Console.WriteLine("L'ordinateur attaque mais vous vous défendez !");
                            }
                            break;
                        case 2:
                            Console.Write("L'ordinateur se defend\n");
                            break;
                        case 3:
                            Console.Write("L'ordinateur utilise son attaque special\n");
                            if (persoIA is Healer healer)
                            {
                                if (healer.Soin())
                                    Console.WriteLine("L'ordinateur s'est soigné !");
                                else
                                    Console.WriteLine("L'ordinateur a échoué à se soigner");
                                healer.health = Math.Min(healer.health, healer.baseHealth);
                            }
                            else if (persoIA is Damager damager)
                            {
                                if (choixAttaque == 2)
                                {
                                    damager.health -= damager.attack;
                                    Console.WriteLine("L'IA utilise Rage mais vous vous défendez ! L'IA subit ses propres dégâts.");
                                }
                                else
                                {
                                    damager.RageAction(persoHumain);
                                }
                                damager.health = Math.Max(0, damager.health);
                                persoHumain.health = Math.Max(0, persoHumain.health);
                            }
                            else if (persoIA is Tank tank)
                            {
                                if (choixAttaque == 2)
                                {
                                    persoHumain.health -= 1;
                                    persoIA.health -= 1;
                                    persoHumain.health = Math.Max(0, persoHumain.health);
                                    persoIA.health = Math.Max(0, persoIA.health);
                                    Console.WriteLine("L'IA utilise Attaque Puissante mais vous défendez ! 1 dégât passe quand même.");
                                }
                                else
                                {
                                    tank.AttaquePuissante(persoHumain);
                                    tank.health = Math.Max(0, tank.health);
                                    persoHumain.health = Math.Max(0, persoHumain.health);
                                }
                            }
                            break;
                    }
                }
            }

            if (persoHumain.health > 0 && persoIA.health <= 0)
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
            else if (persoIA.health > 0 && persoHumain.health <= 0)
            {
                Console.WriteLine("\n\n+------------------+");
                Console.WriteLine("| VICTOIRE DE L'IA |");
                Console.WriteLine("+------------------+");
            }
            else
            {
                Console.WriteLine("\n\n+------------------+");
                Console.WriteLine("|    EGALITE !     |");
                Console.WriteLine("+------------------+");
            }

            Console.WriteLine("Try again ? : ");
            Console.WriteLine("y - yes");
            Console.WriteLine("n - no");
            string newGame = Console.ReadLine();
            if (newGame != "y")
                break;
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
        this.baseHealth = health;
    }

    public void Attaquer(Personnage cible)
    {
        cible.health -= this.attack;
    }

    public void Defense(Personnage attaquant, bool ennemiAttack)
    {
        if (ennemiAttack)
            this.health -= Math.Max(0, attaquant.attack - 1);
    }
}

public class Damager : Personnage
{
    public Damager() : base("Damager", 3, 2) { }

    public string SpecialAttackName { get; } = "Rage";

    public void RageAction(Personnage cible)
    {
        this.health -= cible.attack;
        cible.health -= this.attack;
    }
}

public class Healer : Personnage
{
    public Healer() : base("Healer", 4, 1) { }

    public string SpecialAttackName { get; } = "Soin";

    public bool Soin()
    {
        bool res = true;
        if (health < baseHealth - 1)
        {
            this.health += 2;
        }
        else if (health < baseHealth)
        {
            this.health += 1;
        }
        else
        {
            res = false;
        }
        this.health = Math.Min(this.health, baseHealth);
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
        this.health -= 1;
    }
}