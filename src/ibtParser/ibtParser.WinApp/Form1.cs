using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ibtParserLibrary;

namespace ibtParser.WinApp
{
    public partial class Form1 : Form
    {
        //C:\Users\rroberts\Source\Repos\ircc\src\ibtParser\ibtParserLibrary\Samples\skmodified tour_martinsville 2015-05-30 20-32-44.ibt
        private const string SampleFile =
            @"C:\Users\rroberts\Source\Repos\ircc\src\ibtParser\ibtParserLibrary\Samples\skmodified tour_martinsville 2015-05-30 20-32-44.ibt";

        private const string SampleFile2 =
            @"C:\Users\rroberts\Source\Repos\ircc\src\ibtParser\ibtParserLibrary\Samples\skmodified_langley 2015-05-28 14-45-12.ibt";
        private const string SampleFilesk =
           @"C:\Users\rroberts\Source\Repos\ircc\src\ibtParser\ibtParserLibrary\Samples\sk.ibt";
        private const string SampleFilesk2 =
           @"C:\Users\rroberts\Source\Repos\ircc\src\ibtParser\ibtParserLibrary\Samples\sk2.ibt";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ParserEngine.ParseFile(SampleFilesk2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ParserEngine.runtest();
        }

        private void btnPattern_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.DefaultExt = ".ibt";
            if ( dialog.ShowDialog() == DialogResult.OK)
            {
                ParserEngine.FindFieldPattern(dialog.FileName);
            }            
        }
    }
}
