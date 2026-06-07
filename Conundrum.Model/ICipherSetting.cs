using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conundrum.Model
{
    public interface ICipherSetting : IContainerCollection
    {
        /// <summary>
        /// The Date last used
        /// </summary>
        DateOnly Date { get; set; }

        /// <summary>
        /// The Cipher Machine type
        /// </summary>
        string Type { get; set; }


        // Added 'required' modifier to ensure non-nullable property is initialized (CS8618)
        string Name { get; set; }

        /// <summary>
        /// The summary of the cipher settings.
        /// </summary>
        string? Summary { get; set; }
    }
}
