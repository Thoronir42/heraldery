using Heraldry.Blazon.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Vocabulary.Entries
{
    /// <summary>
    /// Definition of quaterly divided field.
    /// Doesn't have to be only 2x2, can be even 2x3, 3x3, .... depends on how many colors
    /// is specified.
    /// </summary>
    public class QuaterlyDivisionDefinition : FieldDivisionDefinition
    {
        /// <summary>
        /// Fields of the division.
        /// </summary>
        public List<Field> Fields {get; set; }

        /// <summary>
        /// Default constructor. Sets division type to Quaterly.
        /// </summary>
        public QuaterlyDivisionDefinition() 
        {
            Type = Elements.FieldDivisionType.Quarterly;
        }

        /// <summary>
        /// Constructor which allows to specify each field separately.
        /// If the map is incosistent (doesn't start at 1, not every field is defined) exception is thrown.
        /// 
        /// </summary>
        /// <param name="fieldMap">Structure which maps field number to its definition.</param>
        public QuaterlyDivisionDefinition(Dictionary<int, Field> fieldMap) : this()
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
            Fields = tmpFields.OfType<Field>().ToList();
        }

        /// <summary>
        /// Constructs divided fields from fillings array. Size of the division depends on 
        /// how many fillings is provided.
        /// </summary>
        /// <param name="tinctures">Array of fillings. Each item represents a fillings of 1 field, so for 2x2 division 4 fillings need to be provided.</param>
        public QuaterlyDivisionDefinition(Filling[] tinctures)
            : this()
        {
            // todo: accept also other divisions
            if (tinctures.Count() == 4)
            {
                Fields = new List<Field>();
                foreach(Filling tDef in tinctures) {
                    Field f = new Field(tDef);
                    Fields.Add(f);
                }
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
        public QuaterlyDivisionDefinition(Filling tincture1, Filling tincture2) :
            this(new Filling[] { tincture1, tincture2, tincture2, tincture1 })
        {
            
        }

    }
}
