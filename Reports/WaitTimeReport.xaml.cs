using System;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Xps.Packaging;
using CodeReason.Reports;
using UserRegister.Model;

namespace UserRegister.Reports
{
    /// <summary>
    /// Interaction logic for WaitTimeReport.xaml
    /// </summary>
    public partial class WaitTimeReport
    {
        public WaitTimeReport()
        {
            InitializeComponent();
        }

        private void GenerateReport()
        {
            try
            {
                var reportDocument = new ReportDocument();
                var reader = new StreamReader(new FileStream(@"Reports\Templates\WaitTime.xaml", FileMode.Open, FileAccess.Read));
                reportDocument.XamlData = reader.ReadToEnd();
                reportDocument.XamlImagePath = Path.Combine(Environment.CurrentDirectory, @"Reports\Templates\");
                reader.Close();

                var data = new ReportData();

                data.ReportDocumentValues.Add("PrintDate", DateTime.Now); // print date is now

                var table = new DataTable("Visitors");
                table.Columns.Add("VisitorName", typeof(string));
                table.Columns.Add("WaitingTime", typeof(string));

                var randomNames = Visitor.GetRandomNames();
                var rnd = new Random((int)DateTime.Now.TimeOfDay.TotalSeconds);

                for (var i = 1; i <= 100; i++)
                {
                    table.Rows.Add(new object[] { randomNames[rnd.Next(randomNames.Count)], DateTime.Now.AddMinutes(rnd.Next(255)).ToString("HH:mm")});
                }
                data.DataTables.Add(table);

                var xps = reportDocument.CreateXpsDocument(data);
                documentViewer.Document = xps.GetFixedDocumentSequence();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n\r\n" + ex.GetType() + "\r\n" + ex.StackTrace, ex.GetType().ToString(), MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        private void WaitTimeReport_OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (IsVisible)
                GenerateReport();
        }
    }
}
