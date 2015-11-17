using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimShaker.Logic
{
    internal class Spring
    {
        internal const float DEFAULT_SPRING_RATE = 500F;
        internal const float DEFAULT_SPRING_OFFSET_INCLINATION = .85F;
        internal const float DEFAULT_SPRING_FREE_HEIGHT = 10F;
        internal const float DEFAULT_SPRING_BIND_HEIGHT = 4F;
        
        public float Load { get; set; }
        /// <summary>
        /// Force required to compress the spring one inch
        /// </summary>
        public float Rate { get; set; }
        /// <summary>
        /// Force required to compress the spring one inch, including OffsetInclination
        /// </summary>
        public float InstalledRate
        {
            get
            {
                return Rate * OffsetInclination;
            }
        }
        /// <summary>
        /// Height of free standing spring
        /// </summary>
        public float StaticHeight { get; set; }
        /// <summary>
        /// Height at which spring coil binds
        /// </summary>
        public float BindHeight { get; set; }
        /// <summary>
        /// Height of spring with load applied.
        /// </summary>
        public float DynamicHeight
        {
            get
            {
                return StaticHeight - (Load / InstalledRate);
            }
        }
        /// <summary>
        /// Degrees spring is inclined from vertical  (0.0 to 1.0)
        /// </summary>
        public float OffsetInclination { get; set; }

        public Spring() : this(0)
        {

        }
        public Spring(float staticWeight)
        {
            Rate = DEFAULT_SPRING_RATE;
            StaticHeight = DEFAULT_SPRING_FREE_HEIGHT;
            BindHeight = DEFAULT_SPRING_BIND_HEIGHT;
            OffsetInclination = DEFAULT_SPRING_OFFSET_INCLINATION;
            this.Load = staticWeight;
        }       
    }
}
