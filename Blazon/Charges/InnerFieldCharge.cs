﻿using Heraldry.Blazon.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Charges
{
    public class InnerFieldCharge : Charge
    {
        public override Filling Filling
        {
            get { return Content.Background; }
            set { Content.Background = value; }
        }

        public ContentField Content { get; set; } = new ContentField();

        protected InnerFieldCharge(ChargeType type) : base(type)
        {
        }
    }
}
