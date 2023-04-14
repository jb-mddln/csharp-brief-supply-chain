-- Ajoutez 6 entrepôts dans différentes villes et pays dont un hors europe.
INSERT INTO entrepots (nom_entrepot, adresse, ville, pays) 
		VALUES ('Entrepot #1', '27 Gran Via, 28013', 'Madrid', 'Espagne'),
		('Entrepot #2', '10 Rue du Chêne, 75001', 'Paris', 'France'),
		('Entrepot #3', '10 rue de la Lampe, 62200', 'Boulogne-sur-mer', 'France'),
		('Entrepot #4', '5 Kungsgatan, 111 43', 'Stockholm', 'Suède'),
		('Entrepot #5', '7 Unter den Linden, 10117', 'Berlin', 'Allemagne'),
		('Entrepot #6', '150 Kennedy Road thoroughfare, WAN CHAI','HONGKONG', 'Chine');
		
-- Ajoutez 10 expéditions de différents poids, en provenance de différents entrepôts et à destination de différents entrepôts. Assurez-vous que certaines expéditions ont été livrées et d'autres sont en transit.
INSERT INTO expeditions (date_expedition, date_livraison, id_entrepot_source, id_entrepot_destination, poids, statut, date_livraison_prevu) 
		VALUES ('2022-12-04', '2023-04-04', 1, 2, 152.475, 'livrée','2023-01-25'),
		('2021-11-23', '2021-11-27', 2, 3, 52.475, 'livrée','2021-11-26'),
		('2021-05-23', '2021-05-27', 2, 2, 02.475, 'livrée','2021-05-23'),
		('2023-02-21', '2023-03-15', 3, 4, 07.275, 'livrée','2023-03-21'),
		('2023-01-15', null, 4, 5, 00.875, 'en transit','2023-02-15'),
		('2023-02-11', '2023-02-22', 5, 5, 00.942, 'livrée','2023-04-04'),
		('2023-02-03', '2023-02-26', 5, 5, 01.942, 'livrée','2023-02-28'),
		('2023-03-13', null, 1, 5, 03.942, 'en transit','2023-05-12'),
		('2023-03-23', null, 3, 2, 04.821, 'en transit','2023-03-29'),
		('2023-01-22', '2023-02-14', 2, 4, 94.121, 'livrée','2023-01-26'),
		('2023-01-05', null, 2, 4, 194.121, 'en transit','2023-01-17'),
		('2023-03-24', null, 1, 6, 347.541, 'en transit', '2023-04-17'),
		('2023-04-04', '2023-04-06', 3, 5, 600, 'livrée', '2023-04-05'),
		('2023-04-01', '2023-04-04', 2, 1, 450, 'livrée', '2023-04-04'),
		('2023-03-27', null, 2, 6, 1650.250, 'en transit', '2023-05-24');