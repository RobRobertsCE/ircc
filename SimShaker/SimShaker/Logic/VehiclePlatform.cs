using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace SimShaker.Logic
{
    internal class VehiclePlatform
    {
        public Point3D LFContactPatch { get; set; }
        public Point3D RFContactPatch { get; set; }
        public Point3D LRContactPatch { get; set; }
        public Point3D RRContactPatch { get; set; }
        
        public VehiclePlatform()
        {
            LFContactPatch = new Point3D();
            RFContactPatch = new Point3D();
            LRContactPatch = new Point3D();
            RRContactPatch = new Point3D();           
        }

      
    }
}
