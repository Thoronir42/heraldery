using Heraldry.Blazon.Structure;
using Heraldry.Blazon.Structure.Augmentations;
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
            if (field is ContentField)
            {
                PrintContentField(field as ContentField);
            }
            if (field is DividedField)
            {
                PrintDividedField(field as DividedField);
            }
            PrintAugmentations(field);
        }

        private void PrintContentField(ContentField field)
        {
            Print.Filling.P(field.Background);
            if (field.Charge != null)
            {
                Print.Charge.P(field.Charge);
            }
        }

        private void PrintDividedField(DividedField field)
        {
            // todo: optimize divided field printing
            Print.Write(Print.Define.FieldDivision(field.Division));

            for (int i = 0; i < field.Subfields.Length; i++)
            {
                if (i != 0)
                {
                    Print.Print(KeyWord.And);
                }
                var subfield = field.Subfields[i];
                Print.Field.P(subfield);
            }

        }

        private void PrintAugmentations(Field field)
        {
            // todo: implement field augmentations
            foreach (var aug in field.Augmentations)
            {

                if (aug is FieldAugmentation)
                {
                    PrintFieldAugmentation(aug as FieldAugmentation);
                }
                if (aug is PositionAugmentation)
                {
                    PrintPositionAugmentation(aug as PositionAugmentation);
                }
                if (aug is SubordinaryAugmentation)
                {
                    PrintSubordinaryAugmentation(aug as SubordinaryAugmentation);
                }
            }
        }

        private void PrintFieldAugmentation(FieldAugmentation aug)
        {

        }

        private void PrintPositionAugmentation(PositionAugmentation aug)
        {

        }

        private void PrintSubordinaryAugmentation(SubordinaryAugmentation aug)
        {

        }

    }
}
