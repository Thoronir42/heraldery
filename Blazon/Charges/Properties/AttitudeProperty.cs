using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Charges.Properties
{
    [DebuggerDisplay("Attitude = {Attitude}, {Direction}  (Exclusive to: {ExclusiveToList})")]
    public class AttitudeProperty : ChargeProperty
    {
        public Attitude Attitude { get; }
        public AttitudeDirection Direction { get; }

        private ChargeType[] exclusiveTo;

        public AttitudeProperty(Attitude attitude, params ChargeType[] exclusiveTo) : this(attitude, AttitudeDirection.Forward, exclusiveTo)
        {
        }

        public AttitudeProperty(Attitude attitude, AttitudeDirection direction, params ChargeType[] exclusiveTo) : base(PropertyType.Attitude)
        {
            this.Attitude = attitude;
            Direction = direction;
            this.exclusiveTo = exclusiveTo;
        }

        public bool IsFor(ChargeType type)
        {
            if (exclusiveTo == null || exclusiveTo.Length == 0)
            {
                return true;
            }

            return exclusiveTo.Contains(type);
        }

        public override bool Equals(object obj)
        {
            var property = obj as AttitudeProperty;
            return property != null &&
                   Attitude == property.Attitude;
        }

        public override int GetHashCode()
        {
            var hashCode = 2100320753;
            hashCode = hashCode * -1521134295 + Attitude.GetHashCode();
            return hashCode;
        }


        private string ExclusiveToList { get { return String.Join(", ", exclusiveTo.Select(t => t.ToString())); } }
    }

    public enum Attitude
    {
        // Beast attitude

        /// <summary> Standing on hindpaws with front paws raised </summary>
        Rampant,
        /// <summary> Standing on three paws with one front paw raised </summary>
        Passant,
        /// <summary> Sitting </summary>
        Sejant,
        /// <summary> Lying </summary>
        Couchant,
        /// <summary> Running  </summary>
        Courant,
        /// <summary> Sleeping </summary>
        Dormant,
        /// <summary> Standing on hindpaws, leaping </summary>
        Salient,
        /// <summary> Standing on all fours </summary>
        Statant,


        // Bird attitude

        Displayed,
        Overt,
        Rising,
        Volant,
        Recursant,
        Vigilant,
        Vulning,

    }

    public enum AttitudeDirection
    {
        /// <summary> Usually not mentioned </summary>
        Forward,
        /// <summary> Facing viewer </summary>
        Guardant,
        /// <summary> Facing backwards </summary>
        Regardant,
    }
}
