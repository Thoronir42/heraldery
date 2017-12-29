using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Elements
{
    public enum Shape
    {
        /// <summary> Acute diamond </summary>
        Lozenge,
        /// <summary> A more acute lozenge </summary>
        Fusil,
        /// <summary>  </summary>
        Circle,
        /// <summary>  </summary>
        Billet,
    }

    public enum ShapeType
    {
        /// <summary> Whole shape </summary>
        Solid,
        /// <summary> Hole of same shape </summary>
        Voided,
        /// <summary> Circular hole </summary>
        Pierced,
    }
}
