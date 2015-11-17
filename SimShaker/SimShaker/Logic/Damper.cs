using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimShaker.Logic
{
    internal class Damper
    {
        /// <summary>
        /// Height at which damper binds when extended
        /// </summary>
        public float ExtendHeight { get; set; }
        /// <summary>
        /// Height at which damper binds when compressed
        /// </summary>
        public float BindHeight { get; set; }
        /// <summary>
        /// Degrees damper is inclined from vertical (0.0 to 1.0)
        /// </summary>
        public float OffsetInclination { get; set; }
        /// <summary>
        /// Rate at which damper resists compression
        /// </summary>
        public float CompressionRate { get; set; }
        /// <summary>
        /// Rate at which damper resists expansion
        /// </summary>
        public float ReboundRate { get; set; }
        /// <summary>
        /// Rate at which damper resists compression, factoring OffsetInclination.
        /// </summary>
        public float InstalledCompressionRate
        {
            get
            {
                return CompressionRate * OffsetInclination;
            }
        }
        /// <summary>
        /// Rate at which damper resists expansion, factoring OffsetInclination.
        /// </summary>
        public float InstalledReboundRate { get
            {
                return ReboundRate * OffsetInclination;
            }
        }
    }
}
