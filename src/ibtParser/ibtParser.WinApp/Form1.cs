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
        //System.IO.File.WriteAllText(@"C:\Users\rroberts\Source\Repos\ircc\src\ibtParser\ibtParserLibrary\Samples\dumpsk2.5.txt", sb.ToString());

        //C:\Users\rroberts\Source\Repos\ircc\src\ibtParser\ibtParserLibrary\Samples\skmodified tour_martinsville 2015-05-30 20-32-44.ibt
        private const string SampleFile =
            @"C:\Users\rroberts\Source\Repos\ircc\src\ibtParser\ibtParserLibrary\Samples\skmodified tour_martinsville 2015-05-30 20-32-44.ibt";

        private const string SampleFile2 =
            @"C:\Users\rroberts\Source\Repos\ircc\src\ibtParser\ibtParserLibrary\Samples\skmodified_langley 2015-05-28 14-45-12.ibt";
        private const string SampleFilesk =
           @"C:\Users\rroberts\Source\Repos\ircc\src\ibtParser\ibtParserLibrary\Samples\sk.ibt";
        private const string SampleFilesk2 =
           @"C:\Users\rroberts\Source\Repos\ircc\src\ibtParser\ibtParserLibrary\Samples\sk2.ibt";

        ParserEngine engine;

        public Form1()
        {
            InitializeComponent();
            engine = new ParserEngine();
        }
        
        private void btnPattern_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.DefaultExt = ".ibt";
                dialog.InitialDirectory = @"C:\Users\rroberts\Source\Repos\ircc\src\ibtParser\ibtParserLibrary\Samples\";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var session = engine.ParseTelemetryFile(dialog.FileName);
                    Console.WriteLine(session.Yaml.Substring(0,50));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
                
        }
    }
}
