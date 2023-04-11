# csharp-brief-supply-chain
 Exercice Console C# .NET 6.0, gestion d'une base de donnée de manière simpliste avec PostgreSQL et Npgsql (Connexion, requêtes CRUD)
 
### Contexte du projet
Dans un langage de programmation serveur de votre choix, créer des classes entités correspondantes aux tables de votre base de données.
* Ne pas utiliser d'ORM, partez avec une connexion standard à la BDD type JDBC (java), PDO (php), MysqlClient (c#) ou mysql2 (node.js)
* Créer des DAO/Repository pour les requêtes du CRUD ainsi que quelques requêtes spécifiques qui avaient été demandées
## Bonus
* Modifier la base de données pour ajouter des modes d'expédition (naval, aérien, ferroviaire, routier) qui aura leurs propres valeurs selon le type (par exemple le nombre de wagons et la compagnie pour le ferroviaire, le poids max pour l'aérien, ce genre de chose) mais aussi des valeurs communes (prix au km, émission C02 au kilo, un temps moyen au km)
* Gérer les relations d'une manière ou d'une autre dans les DAO
* Faire un DAO/Repository générique qui nous éviterait de répéter le  CRUD de base pour chaque entité