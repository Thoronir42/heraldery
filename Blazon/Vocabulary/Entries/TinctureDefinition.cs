﻿using Heraldry.Blazon.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Vocabulary.Entries
{
    public class TinctureDefinition : Definition
    {
        public Tincture Tincture { get; set; }

        [System.Obsolete]
        public TinctureType TinctureType {
            get { return Tincture.TinctureType; }
            set { Tincture.TinctureType = value; }
        }

        [System.Obsolete]
        public String Value {
            get { return Tincture.Value; }
            set { Tincture.Value = value; }
        }

        /// <summary>
        /// Default constructor, does nothing.
        /// </summary>
        public TinctureDefinition()
        {
            this.Tincture = new Tincture();
        }

        /// <summary>
        /// Constructor with tincture type and text specified.
        /// </summary>
        /// <param name="tinctureType">Type of the tincture</param>
        /// <param name="tinctureValue">Tincture text.</param>
        public TinctureDefinition(TinctureType tinctureType, String tinctureValue)
        {
            this.Tincture = new Tincture { TinctureType = tinctureType, Value = tinctureValue };
        }

        public override DefinitionType GetTokenType()
        {
            return DefinitionType.Tincture;
        }

        public override bool Equals(object obj)
        {
            var definition = obj as TinctureDefinition;
            return definition != null &&
                   base.Equals(obj) &&
                   EqualityComparer<Tincture>.Default.Equals(Tincture, definition.Tincture);
        }

        public override int GetHashCode()
        {
            var hashCode = -761685877;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Tincture>.Default.GetHashCode(Tincture);
            return hashCode;
        }
    }
}
