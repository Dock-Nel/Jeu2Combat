# Jeu de combat

Ceci est un jeu de combat au tour par tour, dans lequel vous endossez le rôle d'un personnage, qui devra affronter une IA.
Des commandes vous seront demandées par le programme, elles seront à saisir au clavier 

1. NIVEAUX DE DIFFICULTE : 
En début de partie vous pourrez choisir le niveau de difficulté, parmi les 3 suivantes :
- Facile
- Normal
- Difficile

2. CLASSES DE PERSONNAGES :

Ensuite vous pourrez choisir votre classe de personnage parmis les suivantes :

- Damager :
  - Points de vie : 3 pv
  - Force d'attaque : inflige 2 points de dégats l'IA
  - Capacité spéciale : Il peut renvoyer les dégâts qu’on lui inflige.
  
- Healer
  - Points de vie : 4 pv
  - Force d'attaque : inflige 1 point de dégat à l'IA
  - Capacité spéciale : Il peut se soigner de 2pv
 
- Tank
  - Points de vie : 5 pv
  - Force d'attaque : inflige 1 point de dégat à l'IA
  - Capacité spéciale : Il sacrifie 1pv pour augmenter son attaque de 1.
 
- Ecid
  - Points de vie : 4 pv
  - Force d'attaque : lance un D10, quand le résultat est inférieur à 8 il fait -1 pv contre l'IA sinon il double son attaque
  - Capacité spéciale : le personnage lance un D10, si son résultat est : 
        - 1 : il s'inflige lui même 1 point de dégat.
        - 2 : il ne fait aucun dégât
        - Compris entre 3 et 6 :  Il inflige 1 point de dégat à l'ennemi 
        - Compris entre 7 et 8 :  Il inflige 2 points de dégats à l'ennemi
        - Compris entre 9 et 10 : Il tue instantanément l'adversaire, y compris si ce dernier choisi de se défendre

3. ACTIONS POSSIBLES : 
Vous pourrez jouer tour par tour et choisir l'une des actions suivante :
- Attaque : elle enlève de la vie à l'énemie
- Défense : elle contre complètement les attaques de l'énemie
- Action spéciale

4. CONDITIONS DE VICTOIRE : 
La partie se termine lorsque l'un d'entre vous est tué, et donc que ses points de vie tombent à zéro. 

# Bonne chance
