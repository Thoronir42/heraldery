using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Elements
{
    /// <summary>
    /// Layout of fields
    /// </summary>
    public enum FieldDivisionType
    {
        /// <summary> Split in half vertically </summary>
        PartyPerPale,
        /// <summary> Split in half horizontally </summary>
        PartyPerFess,
        /// <summary> Quarters </summary>
        Quarterly,

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

    }

    /// <summary>
    /// Some helper methods for field division types.
    /// </summary>
    public static class FieldDivisionTypeMethods
    {
        /// <summary>
        /// Returns true if the type is party per * division.
        /// </summary>
        /// <returns>True or false. Surprise, eh?</returns>
        public static bool IsPartyPerDivision(this FieldDivisionType type)
        {
            switch (type)
            {
                case FieldDivisionType.PartyPerBend:
                case FieldDivisionType.PartyPerBendSinister:
                case FieldDivisionType.PartyPerChevron:
                case FieldDivisionType.PartyPerFess:
                case FieldDivisionType.PartyPerPale:
                    return true;
                default:
                    return false;
            }
        }
    }
}
