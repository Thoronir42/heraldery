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
        /// <summary> Vertical lines. </summary>
        PalyOf,
        /// <summary> Horizontal lines. </summary>
        BarryOf,
        /// <summary> Diagonal lines. </summary>
        BendyOf,

        /// <summary> Checboard </summary>
        Chequy,
        /// <summary> Rotated checky. </summary>
        Lozengy,
        /// <summary> Horizontally stretched Lozengy. </summary>
        Fusilly,
        /// <summary> Crossed lines. </summary>
        Fretty,

        /// <summary> Charge spread over the field </summary>
        SemeOf,
    }
}
