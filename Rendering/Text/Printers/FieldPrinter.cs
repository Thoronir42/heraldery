using Heraldry.Blazon.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Rendering.Text.Printers
{
    class FieldPrinter : BasePrinter<Field>
    {
        public FieldPrinter(RootPrinter root) : base(root)
        {

        }

        public override void P(Field field)
        {
            if(field is ContentField)
            {
                PrintContentField(field as ContentField);
            }
            if(field is DividedField)
            {
                PrintDividedField(field as DividedField);
            }
            PrintFieldAugmentations(field);
        }

        private void PrintContentField(ContentField field)
        {
            Print.Writer.Write(field.Background);
        }

        private void PrintDividedField(DividedField field)
        {
            Print.Writer.Write(Print.Define.FieldDivision(field.Division));
            
            foreach(Field subfield in field.Subfields)
            {
                Print.Field.P(subfield);
            }

        }

        private void PrintFieldAugmentations(Field field)
        {

        }

    }
}
