using System;

namespace SimShaker.Logic
{
    internal class SuspensionCorner
    {
        #region consts
        internal const Double DEFAULT_SUSPENSION_WHEEL_RATIO = 1.0F;
        #endregion

        #region properties
        /// <summary>
        /// Static weight at this suspension corner
        /// </summary>
        public Double StaticWeight { get; set; }
        /// <summary>
        /// Static height at this suspension corner
        /// </summary>
        public Double StaticHeight
        {
            get
            {
                return this.Spring.StaticHeight + this.Tire.StaticHeight;
            }
        }
        /// <summary>
        /// Tire model for this suspension corner
        /// </summary>
        public Tire Tire { get; set; }
        /// <summary>
        /// Damper model for this suspension corner
        /// </summary>
        public Damper Damper { get; set; }
        /// <summary>
        /// Spring model for this suspension corner
        /// </summary>
        public Spring Spring { get; set; }
        /// <summary>
        /// Mechanical ratio of chassis-to-lower spring mount length versus chassis-to-tire contact patch length. (0.0-1.0)
        /// </summary>
        public Double WheelRatio { get; set; }
        /// <summary>
        /// Summary mathematical spring rate for for this suspension corner
        /// </summary>
        public Double WheelRate
        {
            get
            {
                return Tire.InstalledRate + Spring.InstalledRate;
            }
        }
        Double _load;
        /// <summary>
        /// Dynamic weight on this suspension corner
        /// </summary>
        public Double Load
        {
            get
            {
                return _load;
            }
            set
            {
                _load = value;
                DistributeLoad(value);
            }
        }
        /// <summary>
        /// Change in height of suspension corner
        /// </summary>
        public Double Travel
        {
            get
            {
                return DynamicHeight - StaticHeight;
            }
        }
        /// <summary>
        /// Height of the suspension with dynamic loads applied
        /// </summary>
        public Double DynamicHeight
        {
            get
            {
                return this.Spring.DynamicHeight + this.Tire.DynamicHeight;
            }
        }
        #endregion

        #region ctor
        public SuspensionCorner(Double staticWeight, Double SpringRate)
        {
            this.StaticWeight = staticWeight;
            this.Spring = new Spring(staticWeight, SpringRate);
            this.Tire = new Tire(staticWeight, 20F);
            this.Damper = new Damper();
            this.Load = StaticWeight;
            this.WheelRatio = DEFAULT_SUSPENSION_WHEEL_RATIO;
        }
        #endregion

        #region private
        void DistributeLoad(Double F)
        {
            this.Tire.Load = F * WheelRatio;
            this.Spring.Load = F;
        }
        #endregion
    }
}
