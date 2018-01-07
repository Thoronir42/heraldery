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
    public class FieldPrinter : BasePrinter<Field>
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

            if (field.Comment != null)
            {
                Print.Format("({0})", field.Comment);
            }
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
            if (field.Division == Blazon.Elements.FieldDivisionType.Quarterly)
            {
                PrintQuarterlyDividedField(field);
                return;
            }

            // todo: optimize divided field printing
            Print.Write(Print.Define.FieldDivision(field.Division));

            for (int i = 0; i < field.Subfields.Length; i++)
            {
                if (i != 0)
                {
                    Print.Write(KeyWord.And);
                }
                var subfield = field.Subfields[i];
                Print.Field.P(subfield);
            }
        }

        private void PrintQuarterlyDividedField(DividedField field)
        {
            Print.Write(Print.Define.FieldDivision(field.Division));
            Print.Write(Separator.Colon, SpaceRule.Never);

            bool[] definedBitmap = new bool[field.Subfields.Length];
            int toBeDefined = field.Subfields.Length;

            for (int i = 0; i < field.Subfields.Length; i++)
            {
                if (definedBitmap[i])
                {
                    continue;
                }
                Field f = field.Subfields[i];

                List<string> fieldList = new List<string>();

                for (int j = i; j < field.Subfields.Length; j++)
                {
                    if(field.Subfields[j] == f)
                    {
                        fieldList.Add(Define.Number(j + 1, Blazon.Vocabulary.Numbers.NumberType.Ordinal));
                        definedBitmap[j] = true; toBeDefined--;
                    }
                }

                Print.WriteList(fieldList);
                Print.Field.P(field.Subfields[i]);
                if(toBeDefined > 0)
                {
                    Print.Write(Separator.Semicolon, SpaceRule.Never);
                }
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
