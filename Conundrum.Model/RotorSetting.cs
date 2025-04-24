namespace Conundrum.Model
{
    /// <summary>
    /// Represents the settings for a rotor in the Enigma machine.
    /// </summary>
    public class RotorSetting
    {
        /// <summary>
        ///  The name of the rotor, accessible publicly for identification purposes.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        // The starting position of the rotor, represented as a character.
        /// </summary>
        public char StartingPosition { get; set; }

        /// <summary>
        // The current position of the rotor, represented as an integer.
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        // The ring setting of the rotor, used to configure encryption behavior.
        /// </summary>
        public int RingSetting { get; set; }

        /// <summary>
        // The character mapping of the rotor, defining its substitution logic.
        /// </summary>
        public string Map { get; set; }

        /// <summary>
        // The notches on the rotor, indicating positions where turnover occurs.
        /// </summary>
        public string Notches { get; set; }
    }
}
