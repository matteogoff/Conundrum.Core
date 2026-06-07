using MongoDB.Bson;

namespace Conundrum.Model
{
    [Serializable]
    public class User : IContainerCollection
    {
        const string COLLECTION_NAME = "Users";

        /// <summary>
        /// Gets or sets the unique identifier for the object.
        /// </summary>
        public ObjectId Id { get; set; }

        /// <summary>
        /// Gets or sets the Azure Active Directory Object Id for SSO linkage.
        /// </summary>
        public string AzureAdObjectId { get; set; }

        /// <summary>
        /// Gets or sets the user's display name from Azure AD.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the user's email address from Azure AD.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the user's profile picture URL from Azure AD.
        /// </summary>
        public string ProfilePictureUrl { get; set; }

        // Implementation of IContainerCollection.CollectionName
        public string CollectionName { get; } = COLLECTION_NAME;
    }
}
