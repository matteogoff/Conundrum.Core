using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Conundrum.Model
{
    /// <summary>
    /// Represents a collection of containers with a specific name.
    /// </summary>
    /// <remarks>This interface defines a contract for accessing the name of a container collection.
    /// Implementations may provide additional functionality for managing or interacting with the collection.</remarks>
    public interface IContainerCollection
    {
        /// <summary>
        /// Gets the name of the collection associated with the current instance.
        /// </summary>
        string CollectionName { get; }

    }
}
