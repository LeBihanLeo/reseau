# TD : Exploitation des champs d'en-tête HTTP.
Afin de lancer le projet il faut dans un premier temps ouvrir la soolution dans un ide lancer le fichier run, puis ouvrir la page index.html
# Scénario
J'ai 3 scénario de tests, deux qui ont un but de sécurité logiciel et un de performance.

## Scenario 1
Mon premier scénario va sur un jeux de serveur différent regarder s''ils possèdent dans leurs entête http l'entête "X-Content-Type-Options". La header HTTP X-Content-Type-Options est utilisée pour indiquer aux navigateurs web comment ils doivent traiter les réponses HTTP qui contiennent des informations de type de contenu (Content-Type) incorrectes ou incohérentes. Cette header permet de prévenir les attaques d'injection de contenu (Content-Type Sniffing), qui peuvent permettre à des attaquants de faire exécuter du code malveillant sur le navigateur de l'utilisateur.

Lorsque la header X-Content-Type-Options est définie avec la valeur "nosniff", le navigateur est informé qu'il ne doit pas tenter de deviner le type de contenu en analysant le contenu de la réponse. Si le type de contenu est incorrect ou manquant, le navigateur bloquera simplement le chargement de la réponse.

Par exemple, une réponse HTTP qui devrait être de type texte (Content-Type: text/plain) mais qui est renvoyée avec un en-tête de type de contenu incorrect (Content-Type: application/json) pourrait être considérée comme une attaque de type de contenu sniffing. En utilisant la header X-Content-Type-Options, vous pouvez protéger vos utilisateurs contre ces types d'attaques.

## Scenario 2
Tout comme le scénario précédent, dans ce scénario on cherche une entête particulière. Ici c'est l'entête "Strict-Transport-Security". Cet en-tête HTTP indique que le site Web doit être accédé uniquement via HTTPS. Si cet en-tête est mal configuré, il peut ouvrir la porte à des attaques de type "Man-in-the-Middle" (MITM). Il est donc important de configurer correctement cet en-tête pour garantir une communication sécurisée avec le site Web.
Il est donc interessant de voir quels serveur affichent ou non cette informations.

## Scénario 3
Dans ce scénario on mesure le temps de réponse d'une liste de serveur. Le temps de réponse est exprimé en millisecondes. On réalise 5 requête par serveur puis on en fait une moyenne. Même si 5 requête peut paraitre faible c'est avant tout pour ne causer aucune gène aux serveurs.
