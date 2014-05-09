using System;
using System.Collections.Generic;

namespace UserRegister.Model
{
    public class Visitor
    {
        public string VisitorName { get; set; }
        public DateTime SignInDate { get; set; }
        public string MeetingWith { get; set; }
        public string Language { get; set; }
        public DateTime ScheduleTime { get; set; }
        public string Department { get; set; }
        public bool IsMetingWithVisitor { get; set; }
        public bool MeetingEnded { get; set; }


        public static List<string> GetRandomNames()
        {
            return new List<string>()
                {
                    "Kathrin Haverly",
                    "Narcisa Notter",
                    "Gonzalo Brickman",
                    "Jenniffer Birmingham",
                    "Leonard Dumais",
                    "Harlan Timlin",
                    "Deborah Quintal",
                    "Caitlyn Frederic",
                    "Caprice Vannatta",
                    "Alessandra Bellamy",
                    "Tamera Nantz",
                    "Grayce Spiller",
                    "Javier Dimitri",
                    "Milagros Schilling",
                    "Deedra Neu",
                    "Tiffiny Liriano",
                    "Riley Whittenberg",
                    "Benjamin Madeiros", 
                    "Erich Whyte",
                    "Shanda Bott", 
                    "Louisa Beaver",
                    "Larhonda Bowser",
                    "Ricki Hardy",
                    "Margie Ogg",
                    "Grace Silsby",
                    "Merna Hemphill",
                    "Ofelia Bronk",
                    "Trudie Greenway",
                    "Toshiko Guzzi", 
                    "Sheila Corr"
                };
        }
    }
}
