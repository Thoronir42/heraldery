using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Elements
{
    /// <summary>
    /// Possible types of variations.
    /// </summary>
    public enum FieldVariationType
    {
        /// <summary>
        /// Vertical lines.
        /// </summary>
        PalyOf,

        /// <summary>
        /// Horizontal lines.
        /// </summary>
        BarryOf,

        /// <summary>
        /// Rotated checky.
        /// </summary>
        Lozengy,

        /// <summary>
        /// Something similar to checky.
        /// </summary>
        Fusilly,

        /// <summary>
        /// Diagonal lines.
        /// </summary>
        BendyOf,

        /// <summary>
        /// Crossed lines.
        /// </summary>
        Fretty
    }
}
