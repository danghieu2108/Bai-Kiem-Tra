using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppG4.Model
{
    public class HistoryLearning
    {
        public string ID { get; set; }
        public int YearFrom { get; set; }
        public int YearEnd { get; set; }
        public string SchoolName { get; set; }
        public string IDStudent { get; set; }
        public virtual Student Student { get; set; }

        /// <summary>
        /// Chuyển đổi 1 chuỗi dạng ma#tuNam#denNam#noiHoc#masv thành đối tượng
        /// </summary>
        /// <param name="dataString">Chuỗi theo định dạng</param>
        /// <returns>Quá trình học tập</returns>
        public static HistoryLearning Parse(string dataString)
        {
            var items = dataString.Split(new char[] { '#' });
            HistoryLearning history = new HistoryLearning
            {
                ID = items[0],
                YearFrom = int.Parse(items[1]),
                YearEnd = int.Parse(items[2]),
                SchoolName = items[3],
                IDStudent = items[4]
            };
            return history;
        }

        /// <summary>
        /// Chuyển đổi một đối tượng thành chuỗi
        /// </summary>
        /// <param name="history"></param>
        /// <returns></returns>
        public static string Parse(HistoryLearning history)
        {
            return string.Format("{0}#{1}#{2}#{3}#{4}", 
                history.ID, 
                history.YearFrom, 
                history.YearEnd, 
                history.SchoolName, 
                history.IDStudent);
        }
        public string Parse()
        {
            return string.Format("{0}#{1}#{2}#{3}#{4}",
                this.ID,
                this.YearFrom,
                this.YearEnd,
                this.SchoolName,
                this.IDStudent);
        }
    }
}
