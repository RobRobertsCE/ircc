using System;

namespace SimShaker.Logic
{
    internal class Damper
    {
        #region properties
        /// <summary>
        /// Height at which damper binds when extended
        /// </summary>
        public Double ExtendHeight { get; set; }
        /// <summary>
        /// Height at which damper binds when compressed
        /// </summary>
        public Double BindHeight { get; set; }
        /// <summary>
        /// Degrees damper is inclined from vertical (0.0 to 1.0)
        /// </summary>
        public Double OffsetInclination { get; set; }
        /// <summary>
        /// Rate at which damper resists compression
        /// </summary>
        public Double CompressionRate { get; set; }
        /// <summary>
        /// Rate at which damper resists expansion
        /// </summary>
        public Double ReboundRate { get; set; }
        /// <summary>
        /// Rate at which damper resists compression, factoring OffsetInclination.
        /// </summary>
        public Double InstalledCompressionRate
        {
            get
            {
                return CompressionRate * OffsetInclination;
            }
        }
        /// <summary>
        /// Rate at which damper resists expansion, factoring OffsetInclination.
        /// </summary>
        public Double InstalledReboundRate
        {
            get
            {
                return ReboundRate * OffsetInclination;
            }
        }
        #endregion
    }
}
