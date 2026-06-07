using Conundrum.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conundrum.Crypto
{
    /// <summary>
    /// Interface for cipher machines.
    /// </summary>
    public interface ICipherMachine
    {

        /// <summary>
        /// List of characters that will be ignored by the cipher.
        /// </summary>
        List<char> ByPassCharacters { get; set; }


        //Object to input text to be encrypted by the settings object
        char Encode(char input);


        /// <summary>
        /// Resets the cipher to the orginal starting point.
        /// </summary>
        void Reset();

        /// <summary>
        /// Gets the settings of the cipher machine.
        /// </summary>
        /// <returns>A data model of the cipher machines current settings.</returns>
        ICipherSetting GetSettings();

        /// <summary>
        /// Lists the current positions of the rotors by name.
        /// </summary>
        /// <returns>A list of name and the current position of each rotor.</returns>
        Dictionary<string, char> GetPositions();


    }
}
