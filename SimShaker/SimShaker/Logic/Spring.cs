using System;
using System.Windows.Media.Media3D;

namespace SimShaker.Logic
{
    internal class Spring
    {
        #region consts
        internal const Double DEFAULT_SPRING_RATE = 500F;
        internal const Double DEFAULT_SPRING_OFFSET_INCLINATION = .85F;
        internal const Double DEFAULT_SPRING_FREE_HEIGHT = 8F;
        internal const Double DEFAULT_SPRING_BIND_HEIGHT = 3F;
        #endregion

        #region properties
        public Point3D LowerMount { get; set; }
        public Point3D UpperMount { get; set; }

        /// <summary>
        /// Dynamic load on this component
        /// </summary>
        public Double Load { get; set; }
        /// <summary>
        /// Force required to compress the spring one inch
        /// </summary>
        public Double Rate { get; set; }
        /// <summary>
        /// Force required to compress the spring one inch, including OffsetInclination
        /// </summary>
        public Double InstalledRate
        {
            get
            {
                var cosOfAngle = Math.Cos(OffsetInclination);
                return Rate * (cosOfAngle * cosOfAngle);
            }
        }
        /// <summary>
        /// Height of free standing spring
        /// </summary>
        public Double StaticHeight { get; set; }
        /// <summary>
        /// Height at which spring coil binds
        /// </summary>
        public Double BindHeight { get; set; }
        /// <summary>
        /// Height of spring with load applied.
        /// </summary>
        public Double DynamicHeight
        {
            get
            {
                return StaticHeight - (Load / InstalledRate);
            }
        }
        /// <summary>
        /// Degrees spring is inclined from vertical  (0.0 to 1.0)
        /// </summary>
        public Double OffsetInclination { get; set; }
        /// <summary>
        /// The ratio that movement is factored against.
        /// </summary>
        public Double MotionRatio { get; set; }
        #endregion

        #region ctor
        public Spring()
            : this(0)
        {

        }
        public Spring(Double staticWeight)
            : this(staticWeight, DEFAULT_SPRING_RATE)
        {
        }
        public Spring(Double staticWeight, Double springRate)
        {
            Rate = springRate;
            StaticHeight = DEFAULT_SPRING_FREE_HEIGHT;
            BindHeight = DEFAULT_SPRING_BIND_HEIGHT;
            OffsetInclination = DEFAULT_SPRING_OFFSET_INCLINATION;
            Load = staticWeight;
            MotionRatio = .55F;
        }

        #endregion
    }
}
