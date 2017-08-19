using System.IO;


namespace Adre.SEA.Reports
{
    using System;
    using System.Drawing;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for ReportChart.
    /// </summary>
    public partial class ReportChart : Telerik.Reporting.Report
    {
        private Image _pictureBox1Value;

        public ReportChart(string path)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //

            if (!String.IsNullOrEmpty(path))
            {
                _pictureBox1Value = Image.FromStream(new MemoryStream(File.ReadAllBytes(path)));
                pictureBox1.Value = _pictureBox1Value;
            }
            else
            {
                pictureBox1.Visible = false;
            }

            ResizeReport();
        }

        public void ResizeReport()
        {
            ResizeReport(Report.PageSettings);
        }

        public void ResizeReport(PageSettings newPageSettings)
        {
            if (pictureBox1.Visible == false)
                return;


            Unit pageWidth = newPageSettings.PaperSize.Width;
            Unit pageHeight = newPageSettings.PaperSize.Height;

            
            if (newPageSettings.Landscape)
            {
                pageWidth = newPageSettings.PaperSize.Height;
                pageHeight = newPageSettings.PaperSize.Width;
            }

            textBox1.Left = Unit.Cm(0.4d);
            textBox2.Left = textBox1.Left;
            textBox3.Left = textBox1.Left;

            textBox1.Width = pageWidth - (Unit.Cm(0.4d * 2));
            textBox2.Width = textBox1.Width;
            textBox3.Width = textBox1.Width;

            pictureBox1.Height = pageWidth.Multiply((double) _pictureBox1Value.Height / _pictureBox1Value.Width);

            var f = pageHeight.Value * (pageHeight.Type == UnitType.Cm ? 10 : 1);
            var g = pageFooterSection1.Height.Value * (pageFooterSection1.Height.Type == UnitType.Cm ? 10 : 1);
            var h = pageHeaderSection1.Height.Value * (pageHeaderSection1.Height.Type == UnitType.Cm ? 10 : 1);

            var max = Math.Min(pictureBox1.Height.Value, f - h - g);

            pictureBox1.Height = Unit.Mm(max);
            
        }

        private void Report1_Disposed(object sender, EventArgs e)
        {
            _pictureBox1Value.Dispose();
            _pictureBox1Value = null;
        }
    }
}