namespace csharp_brief_supply_chain.Database
{
    /// <summary>
    /// Attribut pour récupérer le nom de nos colonnes en database sur nos entités C#
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnAttribute :Attribute
    {
        /// <summary>
        /// Nom de la colonne
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Constructeur 
        /// </summary>
        /// <param name="name">Nom de la colonne</param>
        public ColumnAttribute(string name)
        {
            Name = name;
        }
    }
}
