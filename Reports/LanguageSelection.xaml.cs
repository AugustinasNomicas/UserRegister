using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows;
using CodeReason.Reports;
using UserRegister.Model;

namespace UserRegister.Reports
{
    /// <summary>
    /// Interaction logic for WaitTimeReport.xaml
    /// </summary>
    public partial class LanguageSelectionReport
    {
        public LanguageSelectionReport()
        {
            InitializeComponent();
        }

        private void GenerateReport()
        {
            try
            {
                var reportDocument = new ReportDocument();
                var reader = new StreamReader(new FileStream(@"Reports\Templates\LanguageSelection.xaml", FileMode.Open, FileAccess.Read));
                reportDocument.XamlData = reader.ReadToEnd();
                reportDocument.XamlImagePath = Path.Combine(Environment.CurrentDirectory, @"Reports\Templates\");
                reader.Close();

                var data = new ReportData();

                data.ReportDocumentValues.Add("PrintDate", DateTime.Now); // print date is now

                var table = new DataTable("LanguageSelection");
                table.Columns.Add("VisitorName", typeof(string));
                table.Columns.Add("Language", typeof(string));

                var randomNames = Visitor.GetRandomNames();
                var languages = new List<string> { "English", "Spanish" };
                var rnd = new Random((int)DateTime.Now.TimeOfDay.TotalSeconds);

                for (var i = 1; i <= 100; i++)
                {
                    table.Rows.Add(new object[] { randomNames[rnd.Next(randomNames.Count)], languages[rnd.Next(0, 2)] });
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
