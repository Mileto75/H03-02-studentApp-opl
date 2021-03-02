using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wba.Oefening.Students.Web.ViewModels
{
    public class HomeShowTeacherInfoViewModel
    {
        public int? Id { get; set; }
        public string TeacherName { get; set; }
        public IEnumerable<string> CourseNames { get; set; }
    }
}
