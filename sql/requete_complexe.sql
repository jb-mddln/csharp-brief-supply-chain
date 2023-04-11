-- Affichez les expéditions en transit qui ont été initiées par un entrepôt situé en Europe et à destination d'un entrepôt situé en Asie.
select * from expeditions
	inner join entrepots e on (e.id = expeditions.id_entrepot_source)
	inner join entrepots d on (d.id = expeditions.id_entrepot_destination)
	where statut = 'en transit' and e.pays in ('France', 'Espagne', 'Allemagne', 'Suède')
	and d.pays in ('Chine');

-- Affichez les entrepôts qui ont envoyé des expéditions à destination d'un entrepôt situé dans le même pays.
select *
    from expeditions
    inner join entrepots e on (e.id = expeditions.id_entrepot_source)
    inner join entrepots d on (d.id = expeditions.id_entrepot_destination)
    where e.pays = d.pays;
	
-- Affichez les entrepôts qui ont envoyé des expéditions à destination d'un entrepôt situé dans un pays différent.
select *
    from expeditions
    inner join entrepots e on (e.id = expeditions.id_entrepot_source)
    inner join entrepots d on (d.id = expeditions.id_entrepot_destination)
    where e.pays != d.pays;

-- Affichez les expéditions en transit qui ont été initiées par un entrepôt situé dans un pays dont le nom commence par la lettre "F" et qui pèsent plus de 500 kg.
select *
	from entrepots
	inner join expeditions on (id_entrepot_source = entrepots.id)
	where expeditions.statut = 'en transit' and starts_with(entrepots.pays, 'F')
	and expeditions.poids > 500;

-- Affichez le nombre total d'expéditions pour chaque combinaison de pays d'origine et de destination.
select (id_entrepot_source, id_entrepot_destination) as combinaison, count(*)
	from expeditions
	group by combinaison;

-- Affichez les entrepôts qui ont envoyé des expéditions au cours des 30 derniers jours et dont le poids total des expéditions est supérieur à 1000 kg.
select entrepots.id as entrepot_expediteur, sum(expeditions.poids) as total_poids_expediés
	from entrepots
	inner join expeditions on (id_entrepot_source = entrepots.id)
	where expeditions.date_expedition > current_date - interval '30' day 
	group by entrepots.id
	having sum(expeditions.poids) > 1000;

-- Affichez les expéditions qui ont été livrées avec un retard de plus de 2 jours ouvrables.
select *
	from expeditions
	where statut = 'livrée'
	and date_livraison > (date_expedition + 9);

-- Affichez le nombre total d'expéditions pour chaque jour du mois en cours, trié par ordre décroissant.
select DATE_PART('Day', date_expedition) as jour_du_mois, count(id) as nombre_expedition_totale
    from expeditions
    where DATE_PART('Month', current_date) = DATE_PART('Month', date_expedition)
    group by jour_du_mois
	order by jour_du_mois desc;