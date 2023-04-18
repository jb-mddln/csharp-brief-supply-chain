using Npgsql;
using System.Data;
using System.Reflection;

namespace csharp_brief_supply_chain.Database
{
    /// <summary>
    /// Représente un répertoire abstrait, regroupe toutes nos requêtes crud de base
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AbstractRepository<T>
    {
        /// <summary>
        /// Instance de notre connection à notre database
        /// </summary>
        public NpgsqlConnection? Connection { get; set; }

        protected AbstractRepository(NpgsqlConnection? connection)
        {
            Connection = connection;
        }

        /// <summary>
        /// Le nom de notre table, à 'override' / redéfinir dans nos différents répertoire
        /// </summary>
        protected abstract string TableName { get; set; }

        /// <summary>
        /// Lis les données récupérer via notre requête et les convertit dans notre entité
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected abstract T SqlToEntity(IDataRecord data);

        public IEnumerable<T> GetAll()
        {
            var all = new List<T>();
            if (Connection == null)
                return all;

            using (NpgsqlCommand cmd = new NpgsqlCommand($"select * from {TableName};", Connection))
            {
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        all.Add(SqlToEntity(reader));
                    }
                }
            }
            return all;
        }

        public T? GetById(int id)
        {
            if (Connection == null)
                return default;

            using (NpgsqlCommand cmd = new NpgsqlCommand($"select * from {TableName} where id = @id;", Connection))
            {
                cmd.Parameters.AddWithValue("@id", id);

                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return SqlToEntity(reader);
                    }
                }
            }

            return default;
        }

        public void Insert(T entity)
        {
            if (Connection == null)
                return;

            // Récupérer le type de l'entité passée en paramètre
            Type entityType = typeof(T);

            // Récupérer toutes les propriétés publiques de l'entité qui ont l'attribut Column
            IEnumerable<PropertyInfo> properties = entityType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(property => property.GetCustomAttributes(typeof(ColumnAttribute), true).OfType<ColumnAttribute>()
                    .Any());

            string queryColumn = string.Empty;
            List<NpgsqlParameter> queryParameters = new List<NpgsqlParameter>();


            int count = 1;
            int countTotal = properties.Count();

            // Pour chaque propriété, afficher son nom, son type et sa valeur
            foreach (PropertyInfo property in properties)
            {
                // Récupérer l'attribut Column associé à la propriété
                ColumnAttribute? columnAttribute = property.GetCustomAttribute<ColumnAttribute>();
                if (columnAttribute == null)
                    return;

                // Récupérer la valeur de l'attribut Column
                string columnName = columnAttribute.Name; // Nom de la colonne

                string name = property.Name; // Nom de la propriété
                Type type = property.PropertyType; // Type de la propriété
                object? value = property.GetValue(entity); // Valeur de la propriété

                // Construit le string '@id, @nom_entrepot, @adresse, @ville, @pays'
                queryColumn += $"@{columnName}{(count != countTotal ? ", " : "")}";

                queryParameters.Add(new NpgsqlParameter(columnName, value));

                count++;
            }

            using (var cmd = new NpgsqlCommand($"insert into {TableName} ({queryColumn.Replace("@", "")}) VALUES ({queryColumn})", Connection))
            {
                cmd.Parameters.AddRange(queryParameters.ToArray());
                int rowAffected = cmd.ExecuteNonQuery();
                if (rowAffected > 0)
                {
                    Console.WriteLine($"{entityType.Name} inserted in database");
                }
                else
                {
                    Console.WriteLine($"Error while trying to insert {entityType.Name} in database");
                }
            }
        }
    }
}