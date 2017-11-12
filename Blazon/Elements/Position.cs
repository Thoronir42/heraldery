using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Elements
{
    public class Position
    {
        public HorizontalPosition Horizontal { get; set; } = HorizontalPosition.Middle;
        public VerticalPosition? Vertical { get; set; }

        public override String ToString()
        {
            return this.Horizontal.ToString() + (this.Vertical != null ? "-" +  this.Vertical.ToString() : "");
        }
    }

    public enum PositionType
    {
        Horizontal,
        Vertical,
        Point,
    }

    public enum HorizontalPosition
    {
        /// <summary> Left (Bearer's right) </summary>
        Dexter,
        /// <summary> Middle </summary>
        Middle,
        /// <summary> Right (Bearer's left) </summary>
        Sinister,
    }

    public enum VerticalPosition
    {
        /// <summary> Top </summary>
        Chief,
        /// <summary> Between Chief and Fess </summary>
        Honour,
        /// <summary> Middle </summary>
        Fess,
        /// <summary> Between Fess and Base </summary>
        Nombril,
        /// <summary> Bottom </summary>
        Base,
    }
}
