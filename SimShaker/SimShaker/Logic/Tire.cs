using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimShaker.Logic
{
    internal class Tire
    {
        internal const float DEFAULT_TIRE_STATIC_HEIGHT = 2F;
        /// <summary>
        /// Pounds per square inch of pressure in the tire
        /// </summary>
        public float PSI { get; set; }
        /// <summary>
        /// Force required to compress the tire one inch
        /// </summary>
        public float InstalledRate
        {
            get
            {
                return PSI * Common.SPRING_RATE_PER_PSI;
            }
        }

        public float Load { get; set; }

        public float StaticHeight { get; set; }

        public float DynamicHeight
        {
            get
            {
                return StaticHeight - (Load / InstalledRate);
            }
        }

        public Tire()
            : this(80F, 20F, DEFAULT_TIRE_STATIC_HEIGHT)
        {
        }
        public Tire(float load, float psi)
            : this(80F, 20F, DEFAULT_TIRE_STATIC_HEIGHT)
        { }
        public Tire(float load, float psi, float staticHeight)
        {
            this.Load = load;
            this.PSI = psi;
            this.StaticHeight = staticHeight;
        }
    }
}
