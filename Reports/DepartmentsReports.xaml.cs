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
    public partial class DepartmentsReport
    {
        public DepartmentsReport()
        {
            InitializeComponent();
        }

        private void GenerateReport()
        {
            try
            {
                var reportDocument = new ReportDocument();
                var reader = new StreamReader(new FileStream(@"Reports\Templates\Departments.xaml", FileMode.Open, FileAccess.Read));
                reportDocument.XamlData = reader.ReadToEnd();
                reportDocument.XamlImagePath = Path.Combine(Environment.CurrentDirectory, @"Reports\Templates\");
                reader.Close();

                var data = new ReportData();

                data.ReportDocumentValues.Add("PrintDate", DateTime.Now); // print date is now

                var table = new DataTable("Departments");
                table.Columns.Add("Department", typeof(string));
                table.Columns.Add("Visitors", typeof(string));

                var randomNames = new[] { "Department A", "Department B", "Department C", "Department D" };
                var rnd = new Random((int)DateTime.Now.TimeOfDay.TotalSeconds);

                for (var i = 0; i < 4; i++)
                {
                    table.Rows.Add(new object[] { randomNames[i], rnd.Next(255) });
                }
                data.DataTables.Add(table);

                var xps = reportDocument.CreateXpsDocument(data);
                documentViewer.Document = xps.GetFixedDocumentSequence();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n\r\n\n" + ex.StackTrace, ex.GetType().ToString(), MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        private void VisitationReport_OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (IsVisible)
                GenerateReport();
        }
    }
}
