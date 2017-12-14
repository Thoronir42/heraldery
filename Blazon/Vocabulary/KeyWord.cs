using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Vocabulary
{
    public enum KeyWord
    {
        /// <summary> determiner (a, an, the) </summary>
        Determiner,
        /// <summary> eg: first and fourth, ... </summary>
        And,
        /// <summary> positioner </summary>
        In,
        /// <summary> joins idea, eg: ... on a canton dickbutt Argent </summary>
        On,
        /// <summary> of the </summary>
        Reference,
        /// <summary> ... overall (current field) a feline beast </summary>
        Overall,
        /// <summary> ... as an augmentation banana </summary>
        Augmentation,
        /// <summary> comma, colon, etc... used to divide concepts, 
        /// todo: might want to split this into separate enum values </summary>
        Separator,
    }
}
