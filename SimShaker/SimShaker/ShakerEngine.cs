using SimShaker.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace SimShaker
{
    internal class ShakerEngine
    {
        int cycles = 100;
        Double loadConst = 10;

        public void Run(ChassisDefinition c)
        {
            for (int i = 0; i < cycles; i++)
            {
                c.EvaluateChange(i * loadConst);
                Console.WriteLine(c.LFSuspension.DynamicHeight);
            }
        }
    }

    internal class ShakerFrame
    {
        public int Idx { get; set; }
        public Double LatG { get; set; }
        public Double LonG { get; set; }
        public Double VertG { get; set; }

        public Double[] SuspensionDelta { get; set; }

        public ShakerFrame()
        {
            SuspensionDelta = new Double[] { 0.0F, 0.0F, 0.0F, 0.0F };
        }
    }
}
