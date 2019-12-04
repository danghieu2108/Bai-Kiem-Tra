using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppG4.Model
{
    public class Student
    {
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public GENDER Gender { get; set; }
        public string PlaceOfBirth { get; set; }
        public virtual ICollection<HistoryLearning> ListHistoryLearning { get; set; }

        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", LastName, FirstName);
            }
        }
    }
    public enum GENDER
    {
        Male, Female, Other
    }
}
