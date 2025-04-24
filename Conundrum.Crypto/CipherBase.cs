namespace Conundrum.Crypto
{
    /// <summary>
    /// Base class for cipher machines.
    /// </summary>
    public class CipherBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CipherBase"/> class.
        /// </summary>
        public CipherBase()
        {
            this.ByPassCharacters = new List<char>();
        }

        /// <summary>
        /// List of characters that will be ignored by the cipher.
        /// </summary>
        public List<char> ByPassCharacters { get; set; }
    }
}
