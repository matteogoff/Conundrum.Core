namespace Conundrum.Model
{
    [Serializable]
    public class CipherSettingBase
    {
        /// <summary>
        /// The Date last used
        /// </summary>
        public DateOnly Date { get; set; }

        /// <summary>
        /// The Cipher Machine type
        /// </summary>
        public string Type { get; set; }


        // Added 'required' modifier to ensure non-nullable property is initialized (CS8618)
        public required string Name { get; set; }


        /// <summary>
        /// List of characters that will be ignored by the cipher.
        /// </summary>
        public string BypassCharacters { get; set; } = string.Empty;

        /// <summary>
        /// The summary of the cipher settings.
        /// </summary>
        public string? Summary { get; set; }
    }
}
