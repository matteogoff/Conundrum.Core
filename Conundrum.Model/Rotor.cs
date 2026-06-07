using MongoDB.Bson;

namespace Conundrum.Model
{
    [Serializable]
    public class Rotor: IContainerCollection
    {
        const string COLLECTION_NAME = "Rotors";

        /// <summary>
        /// Gets or sets the unique identifier for the object.
        /// </summary>
        public ObjectId Id { get; set; }

        /// <summary>
        ///  The name of the rotor, accessible publicly for identification purposes.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        // The character mapping of the rotor, defining its substitution logic.
        /// </summary>
        public string Map { get; set; }

        /// <summary>
        // The notches on the rotor, indicating positions where turnover occurs.
        /// </summary>
        public string Notches { get; set; }

        // Implementation of IContainerCollection.CollectionName
        public string CollectionName { get; } = COLLECTION_NAME;
    }
}
