using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCFirstProject.Models
{
    public class Student
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }

        #region MyRegion
        /// <summary>
        /// Virtual - потому что, даёт возможность воспользоваться преимуществами EF, например, отложенная загрузка
        /// ICollection - коллекция для многих элементов, которая дает возмжжность добавлять, удалять, обновлять элементы
        /// </summary>
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        #endregion

    }
}