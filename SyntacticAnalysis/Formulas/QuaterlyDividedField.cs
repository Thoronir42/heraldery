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
        public QuaterlyDividedField() 
        {
            this.Division = FieldDivisionType.Quarterly;
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

            if(min != 1 || max != fieldMap.Count)
            {
                throw new Exception("Not all fields specified by quterly division.");
            }

            Field[] tmpFields = new Field[max];
            foreach(int fieldNum in fieldMap.Keys)
            {
                Field f = fieldMap[fieldNum];
                // field numbering starts at 1 
                // array numbering starts at 0
                tmpFields[fieldNum - 1] = f;
            }
            Subfields = tmpFields;
        }

        /// <summary>
        /// Constructs divided fields from fillings array. Size of the division depends on 
        /// how many fillings is provided.
        /// </summary>
        /// <param name="tinctures">Array of fillings. Each item represents a fillings of 1 field, so for 2x2 division 4 fillings need to be provided.</param>
        public QuaterlyDividedField(Filling[] tinctures)
            : this()
        {
            // todo: accept also other divisions
            if (tinctures.Count() == 4)
            {
                List<Field> Fields = new List<Field>();
                foreach(Filling tDef in tinctures) {
                    Field f = new ContentField { Background = tDef};
                    Fields.Add(f);
                }

                Subfields = Fields.ToArray();
            }
            else
            {
                // todo: throw exception
                throw new Exception(tinctures.Count()+" of tinctures is currently not supported for quaterly division.");
            }
        }

        /// <summary>
        /// Constructs 2x2 divided field with two tinctures.
        /// </summary>
        /// <param name="tincture1">Tincture of the 1st and 4th quadrant.</param>
        /// <param name="tincture2">Tincture of the 2nd and 3rd quadrant.</param>
        public QuaterlyDividedField(Filling tincture1, Filling tincture2) :
            this(new Filling[] { tincture1, tincture2, tincture2, tincture1 })
        {
            
        }

    }
}
