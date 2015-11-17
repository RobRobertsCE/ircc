﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimShaker.Logic
{
    internal class VehiclePlatform
    {
        public Point3D LFContactPatch { get; set; }
        public Point3D RFContactPatch { get; set; }
        public Point3D LRContactPatch { get; set; }
        public Point3D RRContactPatch { get; set; }

        public Point3D LFShakerMount { get; set; }
        public Point3D RFShakerMount { get; set; }
        public Point3D RearShakerMount { get; set; }

        public VehiclePlatform()
        {
            LFContactPatch = new Point3D();
            RFContactPatch = new Point3D();
            LRContactPatch = new Point3D();
            RRContactPatch = new Point3D();
            LFShakerMount = new Point3D();
            RFShakerMount = new Point3D();
            RearShakerMount = new Point3D();
        }
    }
}
