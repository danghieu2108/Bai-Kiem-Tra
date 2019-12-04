using AppG4.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppG4.Service
{
    public class StudentService
    {
        /// <summary>
        /// Lấy 1 sinh viên có mã tương ứng idStudent
        /// </summary>
        /// <param name="idStudent">Mã sinh viên</param>
        /// <returns>Sinh viên tương ứng hoặc null</returns>
        public static Student GetStudent(string idStudent)
        {
            Student student = new Student
            {
                ID = idStudent,
                FirstName = "Zũng",
                LastName = "Micheal",
                DateOfBirth = new DateTime(2002, 2, 22),
                Gender = GENDER.Male,
                PlaceOfBirth = "Bệnh Viện"
            };
            return student;
        }

        /// <summary>
        /// Lấy 1 sinh viên có mã tương ứng từ File dữ liệu
        /// </summary>
        /// <param name="path">Đường dẫn tới file</param>
        /// <param name="idStudent">Mã sinh viên</param>
        /// <returns>Sinh viên có mã tương ứng hoặc NULL nếu không tồn tại</returns>
        public static Student GetStudent(string path, string idStudent)
        {
            var lines = File.ReadAllLines(path);
            foreach (var line in lines)
            {
                var items = line.Split(new char[] { '#' });
                if (items.Length == 6)
                {
                    var student = new Student
                    {
                        ID = items[0],
                        LastName = items[1],
                        FirstName = items[2],
                        DateOfBirth = DateTime.ParseExact(items[3],"yyyy-MM-dd", CultureInfo.InvariantCulture),
                        Gender =
                        (items[4] == "Male" ? GENDER.Male : (items[4] == "Female" ?
                        GENDER.Female : GENDER.Other)),
                        PlaceOfBirth = items[5]
                    };
                    if (student.ID == idStudent)
                        return student;
                }
            }
            return null;
        }
    }
}
