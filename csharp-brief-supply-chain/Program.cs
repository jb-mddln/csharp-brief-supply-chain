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

            EntrepotRepository entrepotRepository = new EntrepotRepository(databaseManager.Connection);

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