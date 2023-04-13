--Créez une vue qui affiche les informations suivantes pour chaque entrepôt : nom de l'entrepôt, adresse complète, nombre d'expéditions envoyées au cours des 30 derniers jours.
DROP VIEW IF EXISTS view_entrepot;

CREATE VIEW view_entrepot AS
	SELECT DISTINCT entrepots.nom_entrepot as entrepot_expediteur, entrepots.adresse, entrepots.ville, entrepots.pays as adresse_entrepot, COUNT(*) as nb_expeditions_envoyees
	FROM entrepots
	INNER JOIN expeditions ON expeditions.id_entrepot_source = entrepots.id
	WHERE expeditions.date_expedition > current_date - interval '30' day 
GROUP BY entrepots.id, entrepots.adresse;

select * from view_entrepot;

--Créez une procédure stockée qui prend en entrée l'ID d'un entrepôt et renvoie le nombre total d'expéditions envoyées par cet entrepôt au cours du dernier mois.
CREATE OR REPLACE PROCEDURE compte_expeditions_par_entrepot(IN id_entrepot INTEGER, OUT total_expedition INTEGER)
LANGUAGE plpgsql
AS $BODY$
    BEGIN
		SELECT COUNT(*) FROM expeditions into total_expedition
		WHERE id_entrepot_source = id_entrepot and current_date - interval '30' day < expeditions.date_expedition;
    END;
$BODY$;

-- appelle notre procédure := 0 pour définir une valeur de base de 0 sur notre valeur de retour total_expedition 
call compte_expeditions_par_entrepot(3, total_expedition := 0);

--Créez une fonction qui prend en entrée une date et renvoie le nombre total d'expéditions livrées ce jour-là.
create or replace function livraison_jour(IN param1 date)
returns integer
LANGUAGE plpgsql
AS $BODY$
	declare total_expedition integer;
    BEGIN
	select count(date_livraison) into total_expedition
	from expeditions
	where date_livraison = param1;
	return total_expedition;
	END;
$BODY$;

select livraison_jour('2021-11-23');

--Ajoutez une table "clients" contenant les colonnes suivantes :
--id (entier auto-incrémenté, clé primaire) nom (chaîne de caractères) adresse (chaîne de caractères) ville (chaîne de caractères) pays (chaîne de caractères)
create table clients (
	id serial primary key,
	nom varchar(20),
	adresse varchar(50),
	ville varchar(20),
	pays varchar(30)
);

--Ajoutez une table de jointure "expeditions_clients" contenant les colonnes suivantes :
--id_expedition (entier, clé étrangère faisant référence à la table "expeditions") id_client (entier, clé étrangère faisant référence à la table "clients")
create table expeditions_clients (
	id_expedition int,
	id_client int,
	primary key(id_expedition,id_client),
	constraint fk_expedition foreign key (id_expedition) references expeditions,
	constraint fk_client foreign key (id_client) references clients
);

--Modifiez la table "expeditions" pour y ajouter une colonne "id_client" (entier, clé étrangère faisant référence à la table "clients").
ALTER TABLE expeditions ADD COLUMN id_client INT
	CONSTRAINT fk_id_client REFERENCES clients (id)
	ON UPDATE CASCADE ON DELETE CASCADE;

--Ajoutez des données aux tables "clients" et "expeditions_clients".
INSERT INTO clients (nom, adresse, ville, pays) 
		VALUES ('Jb', '33 Gran Vida, 28013', 'Madrid', 'Espagne'),
		('Toto', '27 Rue du Chêne, 78000', 'Paris', 'France'),
		('Florence', '142 Rue du Bois, 78000', 'Paris', 'France');

INSERT INTO expeditions_clients (id_expedition, id_client) 
		VALUES (3, 1),
		(4, 2),
		(1, 3);
		
-- Update expeditions pour mettre nos id_client
UPDATE expeditions
	SET id_client = 1
	WHERE id = 3;
	
UPDATE expeditions
	SET id_client = 2
	WHERE id = 4;

UPDATE expeditions
	SET id_client = 3
	WHERE id = 1;

-- Écrivez des requêtes pour extraire les informations suivantes : 
-- Pour chaque client, affichez son nom, son adresse complète, le nombre total d'expéditions qu'il a envoyées et le nombre total d'expéditions qu'il a reçues
select table1, count(table2) as total_exp, sum(case when table3.date_livraison is not null then 1 else 0 end) as total_reçues
	from clients as table1
    inner join expeditions_clients as table2 on table1.id = table2.id_client
    inner join expeditions as table3 on table3.id_client = table2.id_client
	group by table1;
	
--Pour chaque expédition, affichez son ID, son poids, le nom du client qui l'a envoyée, le nom du client qui l'a reçue et le statut