namespace Conundrum.Model
{
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

        public string? Summary { get; set; }
    }
}
