using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Vocabulary
{
    public enum KeyWord
    {
        /// <summary> eg: first and fourth, ... </summary>
        And,
        /// <summary> positioner </summary>
        In,
        /// <summary> joins idea, eg: ... on a canton dickbutt Argent </summary>
        Combination,
        /// <summary> of the </summary>
        Reference,
        /// <summary> ... as an augmentation banana </summary>
        Augmentation,
        /// <summary> comma, colon, etc... used to divide concepts </summary>
        Separator,
    }
}
