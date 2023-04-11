-- postgreSQL pgAdmin4
DROP DATABASE IF EXISTS "transport_logistique";

-- Créez une base de données nommée "transport_logistique".
CREATE DATABASE "transport_logistique" 
	ENCODING "UTF8";

-- Créez une table "entrepots" & "expeditions"
DROP TABLE IF EXISTS expeditions;
DROP TABLE IF EXISTS entrepots;

-- SERIAL = auto incrémente et notre valeur ne peut-être null (NOT NULL inclus dans SERIAL)
CREATE TABLE entrepots
(
	id SERIAL PRIMARY KEY,
    nom_entrepot VARCHAR(20) NOT NULL,
    adresse VARCHAR(50) NOT NULL,
    ville VARCHAR(20) NOT NULL,
    pays VARCHAR(20) NOT NULL
);

/*
 * date_expedition peut-être null si pas encore expédié
 * poids précisions de 8 chiffres et 3 décimales après la virgule
*/
CREATE TABLE expeditions
(
	id SERIAL PRIMARY KEY,
    date_expedition DATE,
    date_livraison DATE,
    id_entrepot_source INT NOT NULL,
    id_entrepot_destination INT NOT NULL,
    poids DECIMAL(8,3) NOT NULL,
    statut VARCHAR(20) NOT NULL,
	date_livraison_prevu DATE NOT NULL,
	FOREIGN KEY (id_entrepot_source) REFERENCES entrepots(id),
	FOREIGN KEY (id_entrepot_destination) REFERENCES entrepots(id)
);