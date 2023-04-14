using csharp_brief_supply_chain.Database;
using csharp_brief_supply_chain.Database.Entities;
using csharp_brief_supply_chain.Database.Repositories;

namespace csharp_brief_supply_chain
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            DatabaseManager databaseManager = new DatabaseManager
            {
                Host = "localhost",
                Username = "postgres",
                Password = "root",
                Database = "transport_logistique"
            };
            databaseManager.OpenConnection();

            // Initialise nos différents repository
            ClientRepository clientRepository = new ClientRepository(databaseManager.Connection);
            EntrepotRepository entrepotRepository = new EntrepotRepository(databaseManager.Connection);
            ExpeditionRepository expeditionRepository = new ExpeditionRepository(databaseManager.Connection);
            ExpeditionClientRepository expeditionClientRepository = new ExpeditionClientRepository(databaseManager.Connection);

            // Récupère toutes nos valeurs en base de données grace à notre repository et notre GetAll
            var allClients = clientRepository.GetAll();
            var allEntrepots = entrepotRepository.GetAll();
            var allExpeditions = expeditionRepository.GetAll();
            var allExpeditionsClients = expeditionClientRepository.GetAll();


            // Utilisation de string.join et Linq pour joindre nos clients et appeler notre ToString
            Console.WriteLine("Clients list:");
            Console.WriteLine(string.Join("\n", allClients.Select(client => client.ToString())));
            Console.WriteLine("\n");

            Console.WriteLine("Entrepots list:");
            Console.WriteLine(string.Join("\n", allEntrepots.Select(entrepot => entrepot.ToString())));
            Console.WriteLine("\n");

            Console.WriteLine("Expeditions list:");
            Console.WriteLine(string.Join("\n", allExpeditions.Select(expedition => expedition.ToString())));
            Console.WriteLine("\n");

            Console.WriteLine("Expeditions Clients list:");
            Console.WriteLine(string.Join("\n", allExpeditionsClients.Select(expeditionClient => expeditionClient.ToString())));

            Entrepot? entrepotToFind = entrepotRepository.GetById(6);
            if (entrepotToFind != null)
                Console.WriteLine(entrepotToFind.ToString());

            Entrepot testSaveEntrepot = new Entrepot();
            testSaveEntrepot.Id = entrepotRepository.GetNextFreeId();
            testSaveEntrepot.Nom = "Test Ajout";
            testSaveEntrepot.Ville = "Lille";
            testSaveEntrepot.Adresse = "451 Rue de Lille";
            testSaveEntrepot.Pays = "France";

            // entrepotRepository.Insert(testSaveEntrepot);

            entrepotRepository.Delete(7);

            while (true)
            {
                Console.ReadLine();
            }
        }
    }
}