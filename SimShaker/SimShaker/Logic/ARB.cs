using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimShaker.Logic
{
    internal class ARB
    {
        const Double MODULUS_OF_ELASTICITY_OF_STEEL = 500000;

        public bool IsHollow { get; set; }
        public Double Diameter { get; set; }
        public Double WallThickness { get; set; }
        public Double BarLength { get; set; }
        public Double ArmLength { get; set; }
        public Double TorqueArmLength { get; set; }
        public Double MotionRatio { get; set; }
        /// <summary>
        /// Rate in inch/pounds
        /// </summary>
        public Double Rate
        {
            get
            {
                var D = 0D;
                if (IsHollow)
                    D = Math.Pow(Diameter, 4);
                else
                    D = Math.Pow(Diameter, 4) - Math.Pow((Diameter - WallThickness), 4);
                var A = TorqueArmLength;
                var B = BarLength;
                var C = ArmLength;

                var numerator = MODULUS_OF_ELASTICITY_OF_STEEL * D;
                var denominator = (0.4244 * Math.Pow(A, 2) * B) + 0.2264 * Math.Pow(C, 3);

                return numerator / denominator;
            }
        }

        public ARB()
        {
            IsHollow = false;
            Diameter = 1.0D;
            WallThickness = .125D;
            BarLength = 36D;
            ArmLength = 16D;
            TorqueArmLength = 16D;
            MotionRatio = .55D;
        }
    }
}
