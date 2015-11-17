using System;

namespace SimShaker.Logic
{
    internal class Tire
    {
        #region consts
        internal const Double DEFAULT_TIRE_STATIC_HEIGHT = 2F;
        internal const Double DEFAULT_TIRE_PSI = 20F;
        internal const Double DEFAULT_TIRE_LOAD = 80F;
        #endregion

        #region properties
        /// <summary>
        /// Pounds per square inch of pressure in the tire
        /// </summary>
        public Double PSI { get; set; }
        /// <summary>
        /// Force required to compress the tire one inch
        /// </summary>
        public Double InstalledRate
        {
            get
            {
                return PSI * Common.SPRING_RATE_PER_PSI;
            }
        }
        /// <summary>
        /// Dynamic load on this component
        /// </summary>
        public Double Load { get; set; }
        /// <summary>
        /// Height of component when unloaded
        /// </summary>
        public Double StaticHeight { get; set; }
        /// <summary>
        /// Height of component when loaded
        /// </summary>
        public Double DynamicHeight
        {
            get
            {
                return StaticHeight - (Load / InstalledRate);
            }
        }
        /// <summary>
        /// Change in height of component
        /// </summary>
        public Double Travel { get; set; }
        #endregion

        #region ctor
        public Tire()
            : this(DEFAULT_TIRE_LOAD, DEFAULT_TIRE_PSI, DEFAULT_TIRE_STATIC_HEIGHT)
        { }
        public Tire(Double load, Double psi)
            : this(load, psi, DEFAULT_TIRE_STATIC_HEIGHT)
        { }
        public Tire(Double load, Double psi, Double staticHeight)
        {
            this.Load = load;
            this.PSI = psi;
            this.StaticHeight = staticHeight;
        }
        #endregion
    }
}
