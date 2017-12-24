using Heraldry.Blazon.Charges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Elements
{
    /// <summary>
    /// Layout of filling. May describe solid tincture or some kind of variation.
    /// </summary>
    public class FillingLayout
    {
        public const int NO_NUMBER = -1;

        public FillingLayoutType FillingLayoutType { get; set; }

        /// <summary>
        /// Number parameter needed for some type of variation layouts.
        /// Namely: 'PalyOf', 'BarryOf', 'BendyOf'.
        /// For every other type NO_NUMBER constant is used.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// For 'SemeDe' variation type a charge is specified. Otherwise null.
        /// </summary>
        public Charge Charge { get; set; }

        /// <summary>
        /// Returns a Filling layout for a single solid tincture.
        /// Used as an example and for easy refactoring.
        /// </summary>
        /// <returns>Filling layout for single solid tincture.</returns>
        public static FillingLayout Solid()
        {
            return new FillingLayout { FillingLayoutType = FillingLayoutType.Solid, Number = NO_NUMBER, Charge = null };
        }

        public override bool Equals(object obj)
        {
            var layout = obj as FillingLayout;
            return layout != null &&
                   FillingLayoutType == layout.FillingLayoutType &&
                   Number == layout.Number &&
                   EqualityComparer<Charge>.Default.Equals(Charge, layout.Charge);
        }

        public override int GetHashCode()
        {
            var hashCode = 1834094023;
            hashCode = hashCode * -1521134295 + FillingLayoutType.GetHashCode();
            hashCode = hashCode * -1521134295 + Number.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Charge>.Default.GetHashCode(Charge);
            return hashCode;
        }

        /// <summary>
        /// Sets FillingLayoutType accordingly to fieldVariationType.
        /// </summary>
        /// <param name="fieldVariationType">Variation type to be transformed to FillingLayoutType.</param>
        public void SetVariationLayoutType(FieldVariationType fieldVariationType)
        {
            // todo: something better
            switch(fieldVariationType)
            {
                case FieldVariationType.BarryOf:
                    FillingLayoutType = FillingLayoutType.BarryOf;
                    break;
                case FieldVariationType.PalyOf:
                    FillingLayoutType = FillingLayoutType.PalyOf;
                    break;
                case FieldVariationType.BendyOf:
                    FillingLayoutType = FillingLayoutType.BendyOf;
                    break;
                case FieldVariationType.Fretty:
                    FillingLayoutType = FillingLayoutType.Fretty;
                    break;
                case FieldVariationType.Fusilly:
                    FillingLayoutType = FillingLayoutType.Fusilly;
                    break;
                case FieldVariationType.Lozengy:
                    FillingLayoutType = FillingLayoutType.Lozengy;
                    break;
                default:
                    FillingLayoutType = FillingLayoutType.Solid;
                    break;
            }
        }


    }
}
