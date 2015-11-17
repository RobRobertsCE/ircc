using SimShaker.Logic;
using System;
using System.Windows.Forms;
using System.Windows.Media.Media3D;

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
            Double x = (Double).84 * trackX.Value - 42F;
            Double y = (Double)1.05 * trackY.Value - 52.5F;
            Double z = (Double)trackZ.Value;
            var CG = new Point3D() { X = x, Y = y, Z = z };
            c = new Modified(2800, CG);
            c.EvaluateChange(trackDelta.Value);
            var r = c.ToString();
            txtReport.Text = r;
            Console.WriteLine(r);
        }


        private void button3_Click(object sender, EventArgs e)
        {
            //ShakerEngine engine = new ShakerEngine();

            Double x = (Double).84 * trackX.Value - 42F;
            Double y = (Double)1.05 * trackY.Value - 52.5F;
            Double z = (Double)trackZ.Value;
            var CG = new Point3D() { X = x, Y = y, Z = z };

            c = new Modified(2800, CG);
            var r = c.ToString();
            txtReport.Text = r;

            c.EvaluateChange(new Vector3D(.8, 0, 0));
            var r1 = c.ToString();
            txtReport.Text += "\r\n" + r1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EvaluateModified();
        }
        void EvaluateModified()
        {
            Double x = (Double).84 * trackX.Value - 42F;
            Double y = (Double)1.05 * trackY.Value - 52.5F;
            Double z = (Double)trackZ.Value;
            var CG = new Point3D() { X = x, Y = y, Z = z };
            c = new Modified(2800, CG);
            var r = c.ToString();
            txtReport.Text = r;
            Console.WriteLine(r);
        }
        void Evaluate(ChassisDefinition c)
        {
            c.Evaluate();
            Console.WriteLine(c.ToString());
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
