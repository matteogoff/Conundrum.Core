using MongoDB.Bson;

namespace Conundrum.Model
{
    [Serializable]
    public class CipherSetting : ICipherSetting
    {
        const string COLLECTION_NAME = "Ciphers";


        /// <summary>
        /// Gets or sets the unique identifier for the object.
        /// </summary>
        public ObjectId Id { get; set; }

        /// <summary>
        /// The Date last used
        /// </summary>
        public DateOnly Date { get; set; }

        /// <summary>
        /// The Cipher Machine type
        /// </summary>
        public string Type { get; set; }


        // Added 'required' modifier to ensure non-nullable property is initialized (CS8618)
        public string Name { get; set; }

        /// <summary>
        /// The summary of the cipher settings.
        /// </summary>
        public string? Summary { get; set; }

        // Implementation of IContainerCollection.CollectionName
        public string CollectionName { get; } = COLLECTION_NAME;
    }
}
