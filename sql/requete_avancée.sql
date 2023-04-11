-- Affichez les entrepôts qui ont envoyé au moins une expédition en transit.
select * 
	from entrepots
	inner join expeditions on (id_entrepot_source = entrepots.id)
	where statut = 'en transit';

-- Affichez les entrepôts qui ont reçu au moins une expédition en transit.
select * 
	from entrepots
	inner join expeditions on (id_entrepot_destination = entrepots.id)
	where statut = 'en transit';

-- Affichez les expéditions qui ont un poids supérieur à 100 kg et qui sont en transit.
select *
	from expeditions
	where poids > 100 and statut = 'en transit';

-- Affichez le nombre d'expéditions envoyées par chaque entrepôt.
select id_entrepot_source, count(id_entrepot_source) as nbre_expe_par_entrepots
	from expeditions
	inner join entrepots on (entrepots.id = id_entrepot_source)
	group by id_entrepot_source
	order by id_entrepot_source asc;

-- Affichez le nombre total d'expéditions en transit.
select count(id_entrepot_source) as nombre_expedition_totale_en_transit
	from expeditions
	where statut = 'en transit';

-- Affichez le nombre total d'expéditions livrées.
select count(id_entrepot_source) as nombre_expedition_totale_livrée
	from expeditions
	where statut = 'livrée';

-- Affichez le nombre total d'expéditions pour chaque mois de l'année en cours.
select TO_CHAR(date_expedition, 'Month') as mois_annee_en_cours, count(id) as nombre_expedition_totale
	from expeditions
	where DATE_PART('Year', current_date) = DATE_PART('Year', date_expedition)
	group by TO_CHAR(date_expedition, 'Month');

-- Affichez les entrepôts qui ont envoyé des expéditions au cours des 30 derniers jours.
select distinct nom_entrepot
	from entrepots
	inner join expeditions on (id_entrepot_source = entrepots.id)
	where current_date - interval '30' day < expeditions.date_expedition;

-- Affichez les entrepôts qui ont reçu des expéditions au cours des 30 derniers jours.
select *
	from entrepots
	inner join expeditions on (id_entrepot_source = entrepots.id)
	where current_date - interval '30' day < expeditions.date_livraison;

-- Affichez les expéditions qui ont été livrées dans un délai de moins de 5 jours ouvrables.
select *
	from expeditions
	where 
    	case
        	when extract(ISODOW from date_expedition) in (1,2,3,4,5) 
			then date_livraison <  date_expedition + 7
        else date_livraison <  date_expedition + 5
    end;