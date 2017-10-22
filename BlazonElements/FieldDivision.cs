using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.BlazonElements
{
    enum FieldDivision
    {
        PartyPerPale, // Split in half vertically
        PartyPerFess, // Split in half horizontally
        PartyPerBendSinister, // Split diagonally sinister top
        PartyPerBend,           // Split diagonally dexter top

        Quaterly,
        PerSaltire,      // Split double diagonally
        TiercedPerpall,  // Split into thirds
        TiercedPerpallReversed,  // Split into thirds

        // todo: Todd, add more divisions

    }
}
