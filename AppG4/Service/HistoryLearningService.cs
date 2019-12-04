using AppG4.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppG4.Service
{
    public class HistoryLearningService
    {
        /// <summary>
        /// Lấy danh sách quá trình học tập của một sinh viên dựa vào idStudent
        /// </summary>
        /// <param name="idStudent">Mã sinh viên</param>
        /// <returns>Danh sách quá trình học tập của sinh viên</returns>
        public static List<HistoryLearning> GetListHistoryLearning(string idStudent)
        {
            List<HistoryLearning> histories = new List<HistoryLearning>();
            for (int i = 1; i <= 12; i++)
            {
                HistoryLearning history = new HistoryLearning
                {
                    ID = i.ToString(),
                    YearFrom = 2000 + i,
                    YearEnd = 2001 + i,
                    SchoolName = "Phan Bội Châu",
                    IDStudent = idStudent
                };
                histories.Add(history);
            }
            return histories;
        }

        /// <summary>
        /// Lấy danh sách quá trình học tập của một sinh viên dựa vào file
        /// </summary>
        /// <param name="path">Đường dẫn chứa file dữ liệu</param>
        /// <param name="idStudent">Mã sinh viên</param>
        /// <returns>Danh sách quá trình học tập của 1 sinh viên</returns>
        public static List<HistoryLearning> GetListHistoryLearning(
            string path, string idStudent)
        {
            List<HistoryLearning> rs = new List<HistoryLearning>();
            if (File.Exists(path))
            {
                var lines = File.ReadAllLines(path);
                foreach (var line in lines)
                {
                    //Line: ma#tuNam#denNam#noiHoc#masv
                    var history = HistoryLearning.Parse(line);
                    if (history.IDStudent == idStudent)
                        rs.Add(history);
                }
                return rs;
            }
            else
                return null;
        }

        public static void Remove(string path, HistoryLearning history)
        {
            if (File.Exists(path))
            {
                List<string> rs = new List<string>();
                var lines = File.ReadAllLines(path);
                foreach(var line in lines)
                {
                    var data = HistoryLearning.Parse(line);
                    if (data.ID != history.ID)
                        rs.Add(line);
                }
                File.WriteAllLines(path, rs);
            }
            else
                throw new Exception("File dữ liệu không có tồn tại");
        }

        public static void Add(string path, HistoryLearning history)
        {
            
        }
        public static void Update(string path, HistoryLearning history)
        {

        }
    }
}
