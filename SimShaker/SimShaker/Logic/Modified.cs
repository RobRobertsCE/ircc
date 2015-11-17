using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimShaker.Logic
{
    internal class Modified : ChassisDefinition
    {
        public Modified()
            : this(2800F, new Point3D() { X = -10F, Y = 0F, Z = 18F })
        {
        }

        public Modified(Point3D cg)
           : this(2800F, cg)
        {
        }

        public Modified(float staticWeight, Point3D cg)
            : base(staticWeight, cg, new List<Point3D>(){
                    new Point3D() { X = -42F, Y = 52.5F, Z = 0F },
                    new Point3D() { X = 42F, Y = 52.5F, Z = 0F },
                    new Point3D() { X = -42F, Y = -52.5F, Z = 0F },
                    new Point3D() { X = 42F, Y = -52.5F, Z = 0F }})
        {

        }
    }
}
