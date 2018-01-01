using Heraldry.Blazon.Structure;
using Heraldry.Blazon.Vocabulary;
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
            Print.Filling.P(field.Background);
            // todo: print charge and other stuff
        }

        private void PrintDividedField(DividedField field)
        {
            Print.Write(Print.Define.FieldDivision(field.Division));

            for (int i = 0; i < field.Subfields.Length; i++)
            {
                if(i != 0)
                {
                    Print.Print(KeyWord.And);
                }
                var subfield = field.Subfields[i];
                Print.Field.P(subfield);
            }

        }

        private void PrintFieldAugmentations(Field field)
        {

        }

    }
}
