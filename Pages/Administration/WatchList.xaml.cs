using System;
using System.Collections.Generic;
using System.Windows;
using UserRegister.Model;

namespace UserRegister.Pages.Administration
{
    /// <summary>
    /// Interaction logic for WatchList.xaml
    /// </summary>
    public partial class WatchList
    {
        public List<Visitor> Visitors { get; set; }

        public WatchList()
        {
            InitializeComponent();

            var randomNames = Visitor.GetRandomNames();
            var randomDepartments = new List<string> { "Department A", "Department B", "Department C", "Department D" };
            var rnd = new Random((int)DateTime.Now.TimeOfDay.TotalSeconds);

            Visitors = new List<Visitor>();

            for (var i = 0; i < 100; i++)
            {
                Visitors.Add(new Visitor
                {
                    VisitorName = randomNames[rnd.Next(randomNames.Count)],
                    Department = randomDepartments[rnd.Next(randomDepartments.Count)],
                    IsMetingWithVisitor = rnd.NextDouble() > 0.5,
                    MeetingEnded = rnd.NextDouble() > 0.5,
                    Language = rnd.NextDouble() > 0.5 ? "English" : "Spanish",
                    MeetingWith = randomNames[rnd.Next(randomNames.Count)],
                    ScheduleTime = DateTime.Now.AddMinutes(rnd.Next(255) * -1),
                    SignInDate = DateTime.Now.AddMinutes(rnd.Next(255) * -2)
                });
            }

            DataContext = this;
        }

        private void BtnTransfer_OnClick(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBox.Show("Do You Really Wish To Transfer to another employee?", "Transfer", MessageBoxButton.YesNo,
                            MessageBoxImage.Question);
        }
    }
}
