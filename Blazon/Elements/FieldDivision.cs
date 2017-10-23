using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Elements
{

    public class FieldDivision
    {
        public string Name { get; set; }
        public FieldDivisionType Type { get; set; }
    }

    public enum FieldDivisionType
    {
        /// <summary> Split in half vertically </summary>
        PartyPerPale,
        /// <summary> Split in half horizontally </summary>
        PartyPerFess,
        /// <summary> Quarters </summary>
        Quaterly,

        /// <summary> Split diagonally sinister top </summary>
        PartyPerBendSinister,
        /// <summary> Split diagonally dexter top </summary>
        PartyPerBend,
        /// <summary> Split double diagonally </summary>
        PerSaltire,

        /// <summary> Split into thirds arrow downwards</summary>
        TiercedPerPall,
        /// <summary> Split into thirds arrow upwards </summary>
        TiercedPerPallReversed,
        /// <summary> Split into thirds vertically </summary>
        TiercedPerPale,

        /// <summary> Diagonal quarter on bottom </summary>
        PartyPerChevron,
        /// <summary> Split into N beams </summary>
        GyronyOfN

        // todo: Todd, add more divisions

    }

    public enum FieldDivisionLine
    {
        /// <summary> Squares </summary>
        Embattled,
        /// <summary> Gooey-like tears </summary>
        Nebuly,
        /// <summary> Ocean-like waves pointy </summary>
        Engrailed,
        /// <summary> T-shaped zipper </summary>
        Potenty,
        /// <summary> Pot-shaped zipper </summary>
        Dovetailed,
        /// <summary> Dessert-like waves </summary>
        Invected,
        /// <summary> Ocean-like waves calm </summary>
        Wave,
        /// <summary> Tilted stairs </summary>
        Indented,
    }
}
