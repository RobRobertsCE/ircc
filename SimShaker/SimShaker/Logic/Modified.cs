using System;
using System.Collections.Generic;
using System.Windows.Media.Media3D;

namespace SimShaker.Logic
{
    internal class Modified : ChassisDefinition
    {
        #region ctor
        const Double DEFAULT_MODIFIED_STATIC_WEIGHT = 2800F;
        #endregion

        #region ctor
        public Modified()
            : this(DEFAULT_MODIFIED_STATIC_WEIGHT, new Point3D() { X = -10F, Y = 0F, Z = 18F })
        {
        }

        public Modified(Point3D cg)
           : this(DEFAULT_MODIFIED_STATIC_WEIGHT, cg)
        {
        }

        public Modified(Double staticWeight, Point3D cg)
            : base(staticWeight, cg, new List<Point3D>(){
                    new Point3D() { X = -42F, Y = 52.5F, Z = 0F },
                    new Point3D() { X = 42F, Y = 52.5F, Z = 0F },
                    new Point3D() { X = -42F, Y = -52.5F, Z = 0F },
                    new Point3D() { X = 42F, Y = -52.5F, Z = 0F }})
        {
            RCf = new Point3D(0, 52.5F, 2);
            RCr = new Point3D(0, -52.5F, 10);
        }
        #endregion
    }
}
