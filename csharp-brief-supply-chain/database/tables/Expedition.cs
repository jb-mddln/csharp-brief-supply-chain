using Npgsql;

namespace csharp_brief_supply_chain.database.tables
{
    public class Expedition
    {
        public int Id { get; set; }

        public DateTime DateExpedition { get; set; }

        public DateTime? DateLivraison { get; set; }

        public int EntrepotSourceId { get; set; }

        public int EntrepotDestinationId { get; set; }

        public decimal Poids { get; set; }

        public string Statut { get; set; }

        public DateTime DateLivraisonPrevu { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Id: {Id}, Date Expedition: {DateExpedition}, Date Livraison: {DateLivraison}, Entrepot Source Id: {EntrepotSourceId}, Entrepot Destination Id: {EntrepotDestinationId}, Poids: {Poids}, Statut: {Statut}, DateLivraisonPrevu: {DateLivraisonPrevu}";
        }
    }
}
