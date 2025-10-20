using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    public static Random random = new Random();

    static void Main()
    {
        while (true)
        {
            Personnage persoHumain = null;
            Personnage persoIA = null;

            Console.WriteLine("+----------------------------------+");
            Console.WriteLine("| Really Fantastic Fighting Game   |");
            Console.WriteLine("+----------------------------------+");
            Console.WriteLine("\nBienvenue dans l'arène !\n");

            int choixDif = 0;
            while (choixDif < 1 || choixDif > 3)
            {
                Console.WriteLine("Avec quelle difficulte voulez vous jouez :");
                Console.WriteLine("1 - Facile");
                Console.WriteLine("2 - Nornal");
                Console.WriteLine("3 - Difficile");
                Console.Write("Choix : ");
                string saisieChoix = Console.ReadLine();
                if (!int.TryParse(saisieChoix, out choixDif) || choixDif < 1 || choixDif > 4)
                    choixDif = 0;
            }


            int choixJ = 0;
            while (choixJ < 1 || choixJ > 4)
            {
                Console.WriteLine("Catégories de personnages disponibles :");
                Console.WriteLine("1 - Damager");
                Console.WriteLine("2 - Healer");
                Console.WriteLine("3 - Tank");
                Console.WriteLine("4 - Ecid");
                Console.Write("Choix : ");
                string saisieChoix = Console.ReadLine();
                if (!int.TryParse(saisieChoix, out choixJ) || choixJ < 1 || choixJ > 4)
                    choixJ = 0;
            }

            if (choixJ == 1)
            {
                persoHumain = new Damager();
            }
            else if (choixJ == 2)
            {
                persoHumain = new Healer();
            }
            else if (choixJ == 3)
            {
                persoHumain = new Tank();
            }
            else if (choixJ == 4)
            {
                persoHumain = new Ecid();
            }

            Console.WriteLine("\n\nTu as choisi de jouer un " + persoHumain.name + ".");
            System.Threading.Thread.Sleep(1000);

            int nombreRNG = random.Next(1, 5);

            if (nombreRNG == 1)
            {
                persoIA = new Damager();
            }
            else if (nombreRNG == 2)
            {
                persoIA = new Healer();
            }
            else if (nombreRNG == 3)
            {
                persoIA = new Tank();
            }
            else if (nombreRNG == 4)
            {
                persoIA = new Ecid();
            }

            Console.WriteLine("Tu vas affronter un " + persoIA.name + " joué par une IA.");
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
                Console.WriteLine("3 - Action spéciale");
                int choixAttaque = 0;
                string saisie;
                do
                {
                    Console.Write("Choix : ");
                    saisie = Console.ReadLine();
                    if (!int.TryParse(saisie, out choixAttaque) || choixAttaque < 1 || choixAttaque > 3)
                        choixAttaque = 0;
                } while (choixAttaque < 1 || choixAttaque > 3);
                Console.WriteLine("Vous avez choisi : " + choixAttaque + "\n");

                int choixIa = random.Next(1, 4);
                switch (choixDif)
                {
                    case 1: /// FACILE 
                        if (persoIA is Healer)
                        {
                            int choixActionIa = random.Next(1, 101);
                            if (persoIA.health == persoIA.baseHealth && choixAttaque != 1)
                            {
                                if (choixActionIa >= 80)
                                {
                                    choixIa = 3;
                                }
                            }
                        }
                        else if (persoIA is Tank)
                        {
                            int choixActionIa = random.Next(1, 101);
                            if (persoIA.health == 1)
                            {
                                if (choixActionIa <= 80)
                                {
                                    choixIa = 3;
                                }
                            }
                        }
                        else if (persoIA is Damager)
                        {
                            int choixActionIa = random.Next(1, 101);
                            if (choixActionIa <= 50)
                            {
                                choixIa = 1;
                            }
                            else
                            {
                                choixIa = 2;
                            }
                        }
                        break;
                    case 2: // NORMAL 
                        choixIa = random.Next(1, 4);
                        break;
                    case 3: ///DIFFICILE 
                        if (persoIA is Healer)
                        {
                            int choixActionIa = random.Next(1, 101);
                            if (persoIA.health == persoIA.baseHealth)
                            {
                                if (choixActionIa <= 80)
                                {
                                    choixIa = random.Next(1, 3); ;
                                }
                            }
                        }
                        else if (persoIA is Tank)
                        {
                            int choixActionIa = random.Next(1, 101);
                            if (persoIA.health == 1)
                            {
                                if (choixActionIa <= 80)
                                {
                                    choixIa = random.Next(1, 3); ;
                                }
                            }
                        }
                        else if (persoIA is Damager)
                        {
                            int choixActionIa = random.Next(1, 101);
                            if (choixActionIa <= 80)
                            {
                                choixIa = 3;
                            }
                            else
                            {
                                choixIa = 1;
                            }
                        }
                        break;
                }


                // Gestion spécifique Damager vs Damager
                bool actionSpeciale = false;
                if (persoHumain is Damager && persoIA is Damager)
                {
                    Damager damagerH = (Damager)persoHumain;
                    Damager damagerIA = (Damager)persoIA;

                    if (choixAttaque == 3 && choixIa == 1)
                    {
                        damagerH.health -= damagerIA.attack;
                        damagerIA.health -= damagerIA.attack;
                        damagerH.health = Math.Max(0, damagerH.health);
                        damagerIA.health = Math.Max(0, damagerIA.health);
                        Console.WriteLine("Vous utilisez Rage pendant que l'IA attaque !");
                        actionSpeciale = true;
                    }
                    else if (choixAttaque == 1 && choixIa == 3)
                    {
                        damagerIA.health -= damagerH.attack;
                        damagerH.health -= damagerH.attack;
                        damagerH.health = Math.Max(0, damagerH.health);
                        damagerIA.health = Math.Max(0, damagerIA.health);
                        Console.WriteLine("L'IA utilise Rage pendant que vous attaquez !");
                        actionSpeciale = true;
                    }
                    else if (choixAttaque == 3 && choixIa == 3)
                    {
                        damagerH.health -= damagerIA.attack;
                        damagerIA.health -= damagerH.attack;
                        damagerH.health = Math.Max(0, damagerH.health);
                        damagerIA.health = Math.Max(0, damagerIA.health);
                        Console.WriteLine("Les deux utilisent Rage !");
                        actionSpeciale = true;
                    }

                    if (damagerH.health <= 0 || damagerIA.health <= 0)
                        break;
                }

                if (!actionSpeciale)
                {
                    // Cas spécial : Joueur Damager utilise Rage et IA attaque
                    bool rageJoueurVsAttaqueIA = false;
                    if (persoHumain is Damager && choixAttaque == 3 && choixIa == 1 && !(persoIA is Ecid))
                    {
                        Damager damager = (Damager)persoHumain;
                        damager.health -= persoIA.attack;
                        persoIA.health -= persoIA.attack;
                        damager.health = Math.Max(0, damager.health);
                        persoIA.health = Math.Max(0, persoIA.health);
                        Console.WriteLine("Vous utilisez Rage et l'IA attaque ! Vous renvoyez les dégâts !");
                        rageJoueurVsAttaqueIA = true;
                    }

                    // Cas spécial : IA Damager utilise Rage et Joueur attaque
                    bool rageIAVsAttaqueJoueur = false;
                    if (persoIA is Damager && choixIa == 3 && choixAttaque == 1 && !(persoHumain is Ecid))
                    {
                        Damager damager = (Damager)persoIA;
                        damager.health -= persoHumain.attack;
                        persoHumain.health -= persoHumain.attack;
                        damager.health = Math.Max(0, damager.health);
                        persoHumain.health = Math.Max(0, persoHumain.health);
                        Console.WriteLine("L'IA utilise Rage et vous attaquez ! L'IA renvoie les dégâts !");
                        rageIAVsAttaqueJoueur = true;
                    }

                    // Cas spécial : Joueur Damager utilise Rage et IA Tank utilise Attaque Puissante
                    bool rageJoueurVsTankIA = false;
                    if (persoHumain is Damager && choixAttaque == 3 && persoIA is Tank && choixIa == 3)
                    {
                        Damager damager = (Damager)persoHumain;
                        Tank tank = (Tank)persoIA;
                        damager.health -= 2;
                        tank.health -= 2;
                        tank.health -= 1;
                        damager.health = Math.Max(0, damager.health);
                        tank.health = Math.Max(0, tank.health);
                        Console.WriteLine("Vous utilisez Rage et l'IA utilise Attaque Puissante ! Vous renvoyez 2 dégâts !");
                        rageJoueurVsTankIA = true;
                    }

                    // Cas spécial : IA Damager utilise Rage et Joueur Tank utilise Attaque Puissante
                    bool rageIAVsTankJoueur = false;
                    if (persoIA is Damager && choixIa == 3 && persoHumain is Tank && choixAttaque == 3)
                    {
                        Damager damager = (Damager)persoIA;
                        Tank tank = (Tank)persoHumain;
                        damager.health -= 2;
                        tank.health -= 2;
                        tank.health -= 1;
                        damager.health = Math.Max(0, damager.health);
                        tank.health = Math.Max(0, tank.health);
                        Console.WriteLine("L'IA utilise Rage et vous utilisez Attaque Puissante ! L'IA renvoie 2 dégâts !");
                        rageIAVsTankJoueur = true;
                    }

                    // Cas spécial : Joueur Ecid utilise Tiafrap et IA Damager utilise Rage
                    bool tiafrapJoueurVsRageIA = false;
                    if (persoHumain is Ecid && choixAttaque == 3 && persoIA is Damager && choixIa == 3)
                    {
                        Ecid ecid = (Ecid)persoHumain;
                        Damager damager = (Damager)persoIA;
                        int roll = ecid.RollDice();
                        Console.WriteLine("Lancer de dé 10, vous faites : " + roll);

                        int degatsRecus = 0;
                        if (roll == 1)
                        {
                            ecid.health -= 1;
                            ecid.health = Math.Max(0, ecid.health);
                            Console.WriteLine("Vous utilisez Tiafrap mais vous vous blessez vous-même !");
                        }
                        else if (roll >= 3 && roll <= 6)
                        {
                            degatsRecus = 1;
                            damager.health -= degatsRecus;
                            ecid.health -= degatsRecus;
                            damager.health = Math.Max(0, damager.health);
                            ecid.health = Math.Max(0, ecid.health);
                            Console.WriteLine("Vous utilisez Tiafrap et l'IA utilise Rage ! L'IA renvoie " + degatsRecus + " dégât !");
                        }
                        else if (roll >= 7 && roll <= 8)
                        {
                            degatsRecus = 2;
                            damager.health -= degatsRecus;
                            ecid.health -= degatsRecus;
                            damager.health = Math.Max(0, damager.health);
                            ecid.health = Math.Max(0, ecid.health);
                            Console.WriteLine("Vous utilisez Tiafrap et l'IA utilise Rage ! L'IA renvoie " + degatsRecus + " dégâts !");
                        }
                        else if (roll >= 9)
                        {
                            degatsRecus = 5;
                            damager.health -= degatsRecus;
                            ecid.health -= degatsRecus;
                            damager.health = Math.Max(0, damager.health);
                            ecid.health = Math.Max(0, ecid.health);
                            Console.WriteLine("Vous utilisez Tiafrap et l'IA utilise Rage ! L'IA renvoie " + degatsRecus + " dégâts !");
                        }
                        else
                        {
                            Console.WriteLine("Vous utilisez Tiafrap mais ne faites aucun dégât ! Rage n'a aucun effet !");
                        }

                        tiafrapJoueurVsRageIA = true;
                    }

                    // Cas spécial : IA Ecid utilise Tiafrap et Joueur Damager utilise Rage
                    bool tiafrapIAVsRageJoueur = false;
                    if (persoIA is Ecid && choixIa == 3 && persoHumain is Damager && choixAttaque == 3)
                    {
                        Ecid ecid = (Ecid)persoIA;
                        Damager damager = (Damager)persoHumain;
                        int roll = ecid.RollDice();
                        Console.WriteLine("Lancer de dé 10, l'ordinateur fait : " + roll);

                        int degatsRecus = 0;
                        if (roll == 1)
                        {
                            ecid.health -= 1;
                            ecid.health = Math.Max(0, ecid.health);
                            Console.WriteLine("L'IA utilise Tiafrap mais se blesse elle-même !");
                        }
                        else if (roll >= 3 && roll <= 6)
                        {
                            degatsRecus = 1;
                            damager.health -= degatsRecus;
                            ecid.health -= degatsRecus;
                            damager.health = Math.Max(0, damager.health);
                            ecid.health = Math.Max(0, ecid.health);
                            Console.WriteLine("L'IA utilise Tiafrap et vous utilisez Rage ! Vous renvoyez " + degatsRecus + " dégât !");
                        }
                        else if (roll >= 7 && roll <= 8)
                        {
                            degatsRecus = 2;
                            damager.health -= degatsRecus;
                            ecid.health -= degatsRecus;
                            damager.health = Math.Max(0, damager.health);
                            ecid.health = Math.Max(0, ecid.health);
                            Console.WriteLine("L'IA utilise Tiafrap et vous utilisez Rage ! Vous renvoyez " + degatsRecus + " dégâts !");
                        }
                        else if (roll >= 9)
                        {
                            degatsRecus = 5;
                            damager.health -= degatsRecus;
                            ecid.health -= degatsRecus;
                            damager.health = Math.Max(0, damager.health);
                            ecid.health = Math.Max(0, ecid.health);
                            Console.WriteLine("L'IA utilise Tiafrap et vous utilisez Rage ! Vous renvoyez " + degatsRecus + " dégâts !");
                        }
                        else
                        {
                            Console.WriteLine("L'IA utilise Tiafrap mais ne fait aucun dégât ! Rage n'a aucun effet !");
                        }

                        tiafrapIAVsRageJoueur = true;
                    }

                    if (persoIA.health <= 0 || persoHumain.health <= 0)
                        break;

                    if (rageJoueurVsAttaqueIA || rageIAVsAttaqueJoueur || rageJoueurVsTankIA || rageIAVsTankJoueur || tiafrapJoueurVsRageIA || tiafrapIAVsRageJoueur)
                        continue; // On passe au tour suivant, les actions sont déjà résolues

                    // Actions du joueur
                    if (choixAttaque == 1)
                    {
                        if (choixIa != 2)
                        {
                            if (persoHumain is Ecid)
                            {
                                Ecid ecid = (Ecid)persoHumain;
                                int roll = ecid.RollDice();
                                Console.WriteLine("Lancer de dé 10, vous faites : " + roll);
                                ecid.AttaquerAvecDe(persoIA, roll);
                            }
                            else
                            {
                                persoHumain.Attaquer(persoIA);
                                persoIA.health = Math.Max(0, persoIA.health);
                            }
                            Console.WriteLine("Vous attaquez");
                        }
                        else
                        {
                            Console.WriteLine("Vous attaquez mais l'IA se défend !");
                        }
                    }
                    else if (choixAttaque == 2)
                    {
                        Console.WriteLine("Vous prenez une posture défensive.");
                    }
                    else if (choixAttaque == 3)
                    {
                        if (persoHumain is Healer)
                        {
                            Healer healer = (Healer)persoHumain;
                            if (healer.Soin())
                                Console.WriteLine("Vous vous êtes soigné !");
                            else
                                Console.WriteLine("Vous ne pouvez pas vous heal plus que votre vie de base");
                            healer.health = Math.Min(healer.health, healer.baseHealth);
                        }
                        else if (persoHumain is Damager)
                        {
                            Damager damager = (Damager)persoHumain;
                            if (choixIa != 1 && !(persoIA is Tank && choixIa == 3))
                            {
                                Console.WriteLine("Vous utilisez Rage mais l'IA ne vous attaque pas, cela n'a donc aucun effet !");
                            }
                        }
                        else if (persoHumain is Tank)
                        {
                            Tank tank = (Tank)persoHumain;
                            if (choixIa == 2)
                            {
                                persoIA.health -= 1;
                                persoHumain.health -= 1;
                                persoIA.health = Math.Max(0, persoIA.health);
                                persoHumain.health = Math.Max(0, persoHumain.health);
                                Console.WriteLine("Vous utilisez Attaque Puissante mais l'IA se défend ! 1 dégât passe quand même.");
                            }
                            else
                            {
                                tank.AttaquePuissante(persoIA);
                                tank.health = Math.Max(0, tank.health);
                                persoIA.health = Math.Max(0, persoIA.health);
                            }
                        }
                        else if (persoHumain is Ecid)
                        {
                            Ecid ecid = (Ecid)persoHumain;
                            int roll = ecid.RollDice();
                            Console.WriteLine("Lancer de dé 10, vous faites : " + roll);

                            if (choixIa == 2)
                            {
                                if (roll >= 9)
                                {
                                    Console.WriteLine("Attaque critique !");
                                    ecid.TiafrapDamages(persoIA, roll);
                                    Console.WriteLine("Vous utilisez l'attaque spéciale Tiafrap, l'IA se défend, mais vous faites un One Shot !");
                                }
                                else
                                {
                                    Console.WriteLine("Vous utilisez l'attaque spéciale Tiafrap, mais l'IA se défend !");
                                }
                            }
                            else
                            {
                                ecid.TiafrapDamages(persoIA, roll);
                            }
                        }
                    }

                    if (persoIA.health <= 0 || persoHumain.health <= 0)
                        break;

                    // Action de l'IA
                    if (choixIa == 1)
                    {
                        if (choixAttaque != 2)
                        {
                            if (persoIA is Ecid)
                            {
                                Ecid ecid = (Ecid)persoIA;
                                int roll = ecid.RollDice();
                                Console.WriteLine("Lancer de dé 10, l'ordinateur fait : " + roll);
                                ecid.AttaquerAvecDe(persoHumain, roll);
                            }
                            else
                            {
                                persoIA.Attaquer(persoHumain);
                                persoHumain.health = Math.Max(0, persoHumain.health);
                            }
                            Console.Write("L'ordinateur attaque\n");
                        }
                        else
                        {
                            Console.WriteLine("L'ordinateur attaque mais vous vous défendez !");
                        }
                    }
                    else if (choixIa == 2)
                    {
                        Console.Write("L'ordinateur se défend\n");
                    }
                    else if (choixIa == 3)
                    {
                        Console.Write("L'ordinateur utilise son attaque spéciale\n");
                        if (persoIA is Healer)
                        {
                            Healer healer = (Healer)persoIA;
                            if (healer.Soin())
                                Console.WriteLine("L'ordinateur s'est soigné !");
                            else
                                Console.WriteLine("L'ordinateur a échoué à se soigner");
                            healer.health = Math.Min(healer.health, healer.baseHealth);
                        }
                        else if (persoIA is Damager)
                        {
                            Damager damager = (Damager)persoIA;
                            if (choixAttaque != 1 && !(persoHumain is Tank && choixAttaque == 3))
                            {
                                Console.WriteLine("L'IA utilise Rage mais vous ne l'attaquez pas, cela n'a donc aucun effet !");
                            }
                        }
                        else if (persoIA is Tank)
                        {
                            Tank tank = (Tank)persoIA;
                            if (choixAttaque == 2)
                            {
                                persoHumain.health -= 1;
                                persoIA.health -= 1;
                                persoHumain.health = Math.Max(0, persoHumain.health);
                                persoIA.health = Math.Max(0, persoIA.health);
                                Console.WriteLine("L'IA utilise Attaque Puissante mais vous vous défendez ! 1 dégât passe quand même.");
                            }
                            else
                            {
                                tank.AttaquePuissante(persoHumain);
                                tank.health = Math.Max(0, tank.health);
                                persoHumain.health = Math.Max(0, persoHumain.health);
                            }
                        }
                        else if (persoIA is Ecid)
                        {
                            Ecid ecid = (Ecid)persoIA;
                            int roll = ecid.RollDice();
                            Console.WriteLine("Lancer de dé 10, l'ordinateur fait : " + roll);

                            if (choixAttaque == 2)
                            {
                                if (roll >= 9)
                                {
                                    Console.WriteLine("Attaque critique !");
                                    ecid.TiafrapDamages(persoHumain, roll);
                                    Console.WriteLine("L'ordinateur utilise l'attaque spéciale Tiafrap, vous vous êtes défendu mais vous subissez un One Shot !");
                                }
                                else
                                {
                                    Console.WriteLine("L'ordinateur utilise l'attaque spéciale Tiafrap, mais vous vous êtes défendu !");
                                }
                            }
                            else
                            {
                                ecid.TiafrapDamages(persoHumain, roll);
                            }
                        }
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
}

public class Damager : Personnage
{
    public Damager() : base("Damager", 3, 2) { }

    public void RageAction(Personnage cible)
    {
        // Le Damager subit l'attaque de la cible
        this.health -= cible.attack;
        // ET renvoie ces dégâts à la cible
        cible.health -= cible.attack;
    }
}

public class Healer : Personnage
{
    public Healer() : base("Healer", 4, 1) { }

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

    public void AttaquePuissante(Personnage cible)
    {
        cible.health -= 2;
        this.health -= 1;
    }
}

public class Ecid : Personnage
{
    public Ecid() : base("Ecid", 4, 0) { }

    public int RollDice()
    {
        return Program.random.Next(1, 11);
    }

    public void AttaquerAvecDe(Personnage cible, int rollDice)
    {
        if (rollDice >= 8)
        {
            cible.health -= 2;
        }
        else
        {
            cible.health -= 1;
        }
        cible.health = Math.Max(0, cible.health);
    }

    public void TiafrapDamages(Personnage cible, int rollDice)
    {
        int damages = 0;

        if (rollDice == 1)
        {
            this.health -= 1;
            this.health = Math.Max(0, this.health);
        }
        else if (rollDice == 2)
        {
            damages = 0;
        }
        else if (rollDice >= 3 && rollDice <= 6)
        {
            damages = 1;
        }
        else if (rollDice >= 7 && rollDice <= 8)
        {
            damages = 2;
        }
        else if (rollDice >= 9 && rollDice <= 10)
        {
            damages = 5;
        }

        if (rollDice != 1)
        {
            cible.health -= damages;
            cible.health = Math.Max(0, cible.health);
        }
    }
}