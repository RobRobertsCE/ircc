using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimShaker.Logic
{
    internal class SuspensionCorner
    {
        internal const float DEFAULT_SUSPENSION_STATIC_HEIGHT = 2F;
        /// <summary>
        /// Static weight at this suspension corner
        /// </summary>
        public float StaticWeight { get; set; }
        /// <summary>
        /// Static height at this suspension corner
        /// </summary>
        public float StaticHeight
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
        public float WheelRatio { get; set; }
        /// <summary>
        /// Summary mathematical spring rate for for this suspension corner
        /// </summary>
        public float WheelRate
        {
            get
            {
                return Tire.InstalledRate + Spring.InstalledRate;
            }
        }
        /// <summary>
        /// Dynamic weight on this suspension corner
        /// </summary>
        public float Load
        {
            get
            {
                //var kN = this.Tire.InstalledRate + this.Spring.InstalledRate;
                //var k1 = this.Tire.InstalledRate / kN;
                //var k2 = this.Spring.InstalledRate / kN;
                //var l1 = this.Tire.Load * k1;
                //var l2 = this.Spring.Load * k2;
                //return l1 + l2;
                return this.Tire.Load + this.Spring.Load;
            }
            set
            {
                DistributeLoad(value);
            }
        }
        /// <summary>
        /// Change in height of suspension corner
        /// </summary>
        public float Travel
        {
            get
            {
                return StaticHeight;
            }
        }

        public float DynamicHeight
        {
            get
            {
                return this.Spring.DynamicHeight + this.Tire.DynamicHeight;
            }
        }

        public SuspensionCorner(float staticWeight)
        {
            this.StaticWeight = staticWeight;
            this.Spring = new Spring(staticWeight);
            this.Tire = new Tire(staticWeight, 20F);
            this.Damper = new Damper();
        }

        void DistributeLoad(float F)
        {
            // f/k1 + f/k2
            // divide the force among the two 'springs', the spring and the tire.
            var kN = this.Tire.InstalledRate + this.Spring.InstalledRate;
            var k1 = this.Tire.InstalledRate / kN;
            var k2 = this.Spring.InstalledRate / kN;
            this.Tire.Load += F * k1;
            this.Spring.Load += F * k2;
        }
    }
}
