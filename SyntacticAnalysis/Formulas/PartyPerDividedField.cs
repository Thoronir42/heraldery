using Heraldry.Blazon.Structure;
using Heraldry.SyntacticAnalysis.Formulas.FieldDivisions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heraldry.Blazon.Elements;

namespace Heraldry.SyntacticAnalysis.Formulas
{
    /// <summary>
    /// Class for fields with party per * division.
    /// Those divisions consists of exactly two fields.
    /// </summary>
    public class PartyPerDividedField : DividedField
    {
        /// <summary>
        /// Constructor which takes the first and the second field as arguemnts.
        /// </summary>
        /// <param name="field1">First field.</param>
        /// <param name="field2">Second field.</param>
        public PartyPerDividedField(FieldDivisionType division, Field field1, Field field2) : base(division)
        {
            if (!division.IsPartyPerDivision())
            {
                throw new ArgumentException(String.Format("%s is not a valid division type for party per * divided field!", division));
            }
            Subfields = new Field[] { field1, field2 };
        }


    }
}
