using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Elements
{
    public class FurTincture : Tincture
    {
        private string[] customColors = { null, null };
        private string[] implicitColors;

        public string Pattern { get; set; }
        public string PrimaryColor
        {
            get => customColors[0] ?? implicitColors[0];
            set => customColors[0] = value;
        }
        public string SecondaryColor {
            get => customColors[1] ?? implicitColors[1];
            set => customColors[1] = value;
        }

        public FurTincture(string pattern, params string[] implicitColors) : base(TinctureType.Fur, null)
        {
            if (implicitColors.Length != 2)
            {
                throw new ArgumentException("Implicit color count != 2");
            }

            this.Pattern = pattern;
            this.implicitColors = implicitColors;
        }

        public override bool Equals(object obj)
        {
            var tincture = obj as FurTincture;
            return tincture != null &&
                   base.Equals(obj) &&
                   Pattern == tincture.Pattern &&
                   PrimaryColor == tincture.PrimaryColor &&
                   SecondaryColor == tincture.SecondaryColor;
        }

        public override int GetHashCode()
        {
            var hashCode = 1227624777;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Pattern);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PrimaryColor);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(SecondaryColor);
            return hashCode;
        }
    }
}
