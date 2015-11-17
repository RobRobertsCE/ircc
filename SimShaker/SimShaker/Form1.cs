using SimShaker.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimShaker
{
    public partial class Form1 : Form
    {
        ChassisDefinition c;

        public Form1()
        {
            InitializeComponent();

            UpdateCGDisplay();
            UpdateDeltaWeight();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            float x = (float).84 * trackX.Value - 42F;
            float y = (float)1.05 * trackY.Value - 52.5F;
            float z = (float)trackZ.Value;
            var CG = new Point3D() { X = x, Y = y, Z = z };
            c = new Modified(2800, CG);
            c.EvaluateChange(trackDelta.Value);
            var r = c.Report();
            txtReport.Text = r;
            Console.WriteLine(r);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            EvaluateModified();
        }
        void EvaluateModified()
        {
            float x = (float).84 * trackX.Value - 42F;
            float y = (float)1.05 * trackY.Value - 52.5F;
            float z = (float)trackZ.Value;
            var CG = new Point3D() { X = x, Y = y, Z = z };
            c = new Modified(2800, CG);
            var r = c.Report();
            txtReport.Text = r;
            Console.WriteLine(r);
        }
        void Evaluate(ChassisDefinition c)
        {
            c.Evaluate();
            Console.WriteLine(c.Report());
        }

        private void track_Scroll(object sender, EventArgs e)
        {
            UpdateCGDisplay();
        }

        void UpdateCGDisplay()
        {
            lblCG.Text = String.Format("X:{0} Y:{1} Z:{2}", trackX.Value, trackY.Value, trackZ.Value);
        }

        private void trackDelta_Scroll(object sender, EventArgs e)
        {
            UpdateDeltaWeight();
        }

        void UpdateDeltaWeight()
        {
            lblDelta.Text = String.Format("Delta Weight:{0}", trackDelta.Value);
        }

    }
}
