using Heraldry.Blazon.Elements;
using Heraldry.Blazon.Structure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Heraldry.SyntacticAnalysis.Formulas.FieldDivisions
{
    /// <summary>
    /// Definition of quaterly divided field.
    /// Doesn't have to be only 2x2, can be even 2x3, 3x3, .... depends on how many colors
    /// is specified.
    /// </summary>
    public class QuaterlyDividedField : DividedField
    {
        /// <summary>
        /// Default constructor. Sets division type to Quaterly.
        /// </summary>
        public QuaterlyDividedField() : base(FieldDivisionType.Quarterly)
        {   
        }

        /// <summary>
        /// Constructor which allows to specify each field separately.
        /// If the map is incosistent (doesn't start at 1, not every field is defined) exception is thrown.
        /// 
        /// </summary>
        /// <param name="fieldMap">Structure which maps field number to its definition.</param>
        public QuaterlyDividedField(Dictionary<int, Field> fieldMap) : this()
        {
            int min = fieldMap.Keys.Min();
            int max = fieldMap.Keys.Max();

            if (min != 1 || max != fieldMap.Count)
            {
                throw new Exception("Not all fields specified by quterly division.");
            }

            Field[] tmpFields = new Field[max];
            foreach (var keyVal in fieldMap)
            {
                // field numbering starts at 1 
                // array numbering starts at 0
                tmpFields[keyVal.Key - 1] = keyVal.Value;
            }
            Subfields = tmpFields;
        }

        /// <summary>
        /// Constructs divided fields from fillings array. Size of the division depends on 
        /// how many fillings is provided.
        /// </summary>
        /// <param name="fields">Array of fillings. Each item represents a fillings of 1 field, so for 2x2 division 4 fillings need to be provided.</param>
        public QuaterlyDividedField(params Field[] fields)
            : this()
        {
            if (fields.Count() == 2)
            {
                fields = new Field[] { fields[0], fields[1], fields[1], fields[0] };
            }

            // todo: accept also other divisions
            if (fields.Count() != 4)
            {
                throw new Exception(fields.Count() + " of tinctures is currently not supported for quaterly division.");
            }

            this.Subfields = fields;
        }

    }
}
