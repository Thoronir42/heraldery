using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Elements
{
    /// <summary>
    /// Particular type of filling layout.
    /// </summary>
    public enum FillingLayoutType
    {
        /// <summary>
        /// Solid layout type - use this for single tincture background.
        /// </summary>
        Solid,
        /// <summary> Vertical poles </summary>
        PalyOf,
        /// <summary> Horizontal bars </summary>
        BarryOf,
        /// <summary> Checkered board </summary>
        Chequy,
        /// <summary> Diagonal checkered board </summary>
        Lozengy,
        /// <summary> Twice as high as wide lozengy </summary>
        Fusilly,
        /// <summary> Diagonal bars </summary>
        BendyOf,
        /// <summary> Cloth-like intertwined diagonal bars </summary>
        Fretty,
    }
}
