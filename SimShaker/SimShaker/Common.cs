using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace SimShaker
{
    class Common
    {
        internal const float SPRING_RATE_PER_PSI = 15.00F;

        static internal Double Distance(Point3D point1, Point3D point2)
        {
            return Distance(new Double[] { point1.X, point1.Y, point1.Z }, new Double[] { point2.X, point2.Y, point2.Z });
        }
        static internal Double Distance(Double[] point1, Double[] point2)
        {
            return (Double)Math.Sqrt(point1.Zip(point2, (a, b) => (a - b) * (a - b)).Sum());
        }
    }
}
