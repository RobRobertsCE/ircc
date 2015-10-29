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
