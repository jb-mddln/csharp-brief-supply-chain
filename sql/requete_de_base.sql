-- Affichez tous les entrepots.
select * from entrepots;

-- Affichez toutes les expéditions.
select * from expeditions;

-- Affichez toutes les expéditions en transit.
select * 
	from expeditions
	where statut = 'en transit';

-- Affichez toutes les expéditions livrées.
select * 
	from expeditions
	where statut = 'livrée';