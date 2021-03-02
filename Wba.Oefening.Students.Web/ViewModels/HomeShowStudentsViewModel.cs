using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wba.Oefening.Students.Web.ViewModels
{
    public class HomeShowStudentsViewModel
    {
        public List<HomeShowStudentInfoViewModel> Students { get; set; } = new List<HomeShowStudentInfoViewModel>();
    }
}
