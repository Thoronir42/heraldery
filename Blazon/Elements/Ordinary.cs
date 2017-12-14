using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Elements
{
    public enum Ordinary
    {
        /// <summary> A cross Sinister to Dexter, Chief to Base </summary>
        Cross,
        /// <summary> Horizontal stripe in chief </summary>
        Chief,
        /// <summary> Vertical stripe in center </summary>
        Pale,
        /// <summary> Diagonal stripe dexter chief to sinister base </summary>
        Bend,
        /// <summary> Diagonal stripe sinister chief to dexter base </summary>
        BendSinister,
        /// <summary> Horizontal stripe in middle </summary>
        Fess,
        /// <summary> Inner escutcheon </summary>
        Inescutcheon,
        /// <summary> Diagonal cross</summary>
        Saltire,
        /// <summary> Reverse V </summary>
        Chevron,
        /// <summary> Along the border </summary>
        Bordure,
    }

    public enum OrdinarySize
    {
        Honourable,
        Dim,
    }
}
