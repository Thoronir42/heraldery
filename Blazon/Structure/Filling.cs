using Heraldry.Blazon.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Structure
{
    public abstract class Filling
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Filling() {  }

        public static FillingType TypeByVariation(FieldVariationType variationType)
        {
            switch(variationType)
            {
                case FieldVariationType.Fretty:
                case FieldVariationType.Fusily:
                case FieldVariationType.Lozengy:
                case FieldVariationType.Chequy:
                    return FillingType.Pattern;

                case FieldVariationType.BarryOf:
                case FieldVariationType.BendyOf:
                case FieldVariationType.PalyOf:
                    return FillingType.NPattern;

            }

            throw new NotImplementedException("Field variation type " + variationType.ToString() + " not implemented");
        }
    }

    public enum FillingType
    {
        Solid,
        Pattern,
        NPattern,
        Seme,
        Counterchanged,
    }
}
