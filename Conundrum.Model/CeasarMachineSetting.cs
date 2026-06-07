using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conundrum.Model
{
    [Serializable]
    public class CeasarMachineSetting : CipherSetting, IContainerCollection
    {
        /// <summary>
        /// Gets or sets the mapping configuration for the Ceasar machine.
        /// </summary>
        public string Map { get; set; }

        /// <summary>
        /// Gets or sets the starting position of the Ceasar machine.
        /// </summary>
        public char StartingPosition { get; set; } = 'A';

        /// <summary>
        /// Gets the name of the collection.
        /// </summary>
        public string CollectionName { get; set; } = "Ciphers";
    }
}
