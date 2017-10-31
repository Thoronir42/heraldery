using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Elements
{
    enum TinctureLayout
    {
        Solid,
        /// <summary> Vertical poles </summary>
        Paly,
        /// <summary> Horizontal bars </summary>
        Barry,
        /// <summary> Checkered board </summary>
        Chequy,
        /// <summary> Diagonal checkered board </summary>
        Lozengy,
        /// <summary> Twice as high as wide lozengy </summary>
        Fusilly,
        /// <summary> Diagonal bars </summary>
        Bendy,
        /// <summary> Cloth-like intertwined diagonal bars </summary>
        Fretty
    }
}
