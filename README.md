# Jeu de combat


Ce jeu se joue uniquement au clavier


Il y a 3 actions
- attaque : elle enlève de la vie à l'énemie
- défense : elle contre complètement les attaques de l'énemie
- l'action spéciale

Il existe 4 classes :
- damager
  - vie : 3 pv
  - attaque : -2 pv contre l'IA
  - action spéciale : Il peut renvoyer les dégâts qu’on lui inflige.
- healer
  - vie : 4 pv
  - attaque : -1 pv contre l'IA
  - action spéciale : Il peut se soigner de 2pv
- tank
  - vie : 5 pv
  - attaque : -1 pv contre l'IA
  - action spéciale : Il sacrifie 1pv pour augmenter son attaque de 1.
- ecid
  - vie : 4 pv
  - attaque : lance un D10, quand le résultat est inférieur à 8 il fait -1 pv contre l'IA sinon il double son attaque
  - action spéciale : lance un D10, 4 faces font 1 pv contre l'IA, 2 faces font 2 pv contre l'IA, 2 faces tue instantanément l'enemie en face, 1 face ne fait rien et 1 face nous enlève 1 pv
