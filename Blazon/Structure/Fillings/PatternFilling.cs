using Heraldry.Blazon.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Structure.Fillings
{
    public class PatternFilling : Filling
    {
        /// <summary>
        /// Number parameter needed for some type of variation layouts.
        /// Namely: 'PalyOf', 'BarryOf', 'BendyOf'.
        /// </summary>
        public int Number { get; set; }

        public bool HasNumber { get {return Number != 0; } }

        public FieldVariationType Type { get; set; }

        public Tincture[] Tinctures { get; protected set; } = new Tincture[2];

        public Tincture PrimaryTincture {
            get { return Tinctures[0]; }
            set { Tinctures[0] = value; }
        }

        public Tincture SecondaryTincture
        {
            get { return Tinctures[1]; }
            set { Tinctures[1] = value; }
        }

        public PatternFilling(FieldVariationType type, params Tincture[] tinctures)
        {
            if(tinctures.Length != 2)
            {
                throw new ArgumentException("Tincture count != 2");
            }

            this.Type = type;
            this.Tinctures = tinctures;
        }

        public PatternFilling(FieldVariationType type, int number, params Tincture[] tinctures) : this(type, tinctures)
        {
            this.Number = number;
        }

        public override bool Equals(object obj)
        {
            var filling = obj as PatternFilling;
            return filling != null &&
                   Number == filling.Number &&
                   Type == filling.Type &&
                   EqualityComparer<Tincture>.Default.Equals(PrimaryTincture, filling.PrimaryTincture) &&
                   EqualityComparer<Tincture>.Default.Equals(SecondaryTincture, filling.SecondaryTincture);
        }

        public override int GetHashCode()
        {
            var hashCode = -1500737925;
            hashCode = hashCode * -1521134295 + Number.GetHashCode();
            hashCode = hashCode * -1521134295 + Type.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Tincture>.Default.GetHashCode(PrimaryTincture);
            hashCode = hashCode * -1521134295 + EqualityComparer<Tincture>.Default.GetHashCode(SecondaryTincture);
            return hashCode;
        }
    }
}
