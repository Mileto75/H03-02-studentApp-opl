using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wba.Oefening.Students.Web.Models;
using Wba.Oefening.Students.Domain;
using Wba.Oefening.Students.Web.ViewModels;

namespace Wba.Oefening.Students.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly StudentRepository studentRepository;
        private readonly CourseRepository courseRepository;
        private readonly TeacherRepository teacherRepository;

        public HomeController()
        {
            studentRepository = new StudentRepository();
            courseRepository = new CourseRepository();
            teacherRepository = new TeacherRepository();
        }

        [Route("/")]
        public IActionResult Index()
        {
            ViewBag.Title = "Student App";
            ViewBag.Message = "Welcome on our Student App";
            return View();
        }

        [Route("Courses")]
        public IActionResult ShowCourses()
        {
            //viewmodel
            List<HomeShowCoursesViewModel> homeShowCoursesViewModel = new List<HomeShowCoursesViewModel>();
            foreach(var course in courseRepository.Courses)
            {
                homeShowCoursesViewModel.Add(
                    new HomeShowCoursesViewModel {Id=course?.Id??0,Name=course?.Name??"noName" }
                    );
            }
            return View(homeShowCoursesViewModel);
        }
        [Route("courses/{courseId}/students")]
        public IActionResult ShowCourseStudents(int courseId)
        {
            //viewModel
            HomeShowCourseStudentsViewModel homeShowCourseStudentsViewModel = new HomeShowCourseStudentsViewModel();
            //courseName
            homeShowCourseStudentsViewModel.CourseName = courseRepository.GetCourseById(courseId).Name;
            //students
            homeShowCourseStudentsViewModel.StudentNames = studentRepository
                .GetStudentsInCourseId(courseId)
                .Select(s => $"{s.FirstName} {s.LastName}");
            return View(homeShowCourseStudentsViewModel);
        }
        [Route("Students")]
        public IActionResult ShowStudents()
        {
            HomeShowStudentsViewModel homeShowStudentsViewModel = new HomeShowStudentsViewModel();
            foreach(var student in studentRepository.Students)
            {
                homeShowStudentsViewModel.Students.Add(
                    new HomeShowStudentInfoViewModel
                    {
                        StudentId = student.Id,
                        StudentName = $"{student.FirstName} {student.LastName}",
                    }
                    );
            }
            return View(homeShowStudentsViewModel);
        }
        [Route("Students/{studentId:int}")]
        public IActionResult StudentInfo(int studentId)
        {
            var student = studentRepository.GetStudentById(studentId);
            var courses = studentRepository.GetCoursesForStudentById(studentId);
            HomeShowStudentInfoViewModel homeShowStudentInfoViewModel = new HomeShowStudentInfoViewModel();
            homeShowStudentInfoViewModel.StudentId = student.Id;
            homeShowStudentInfoViewModel.StudentName = $"{student?.FirstName ?? "nofirstName"} {student?.LastName ?? "noLastName"}";
            homeShowStudentInfoViewModel.StudentCourses = studentRepository
                .GetCoursesForStudentById(studentId).Select(c => c.Name);
            return View(homeShowStudentInfoViewModel);
        }

        [Route("Teachers")]
        public IActionResult Teachers()
        {
            HomeTeachersViewModel homeTeachersViewModel = new HomeTeachersViewModel();
            homeTeachersViewModel.Teachers = new List<HomeShowTeacherInfoViewModel>();
            foreach(var teacher in teacherRepository.Teachers)
            {
                homeTeachersViewModel.Teachers.Add(
                    new HomeShowTeacherInfoViewModel
                    {
                        Id = teacher?.Id ?? 0,
                        TeacherName = $"{teacher?.FirstName} {teacher?.LastName}"
                    }
                    );
            }
            return View(homeTeachersViewModel);
        }

        [Route("Teachers/{teacherId:int}")]
        public IActionResult ShowTeacherInfo(int teacherId)
        {
            HomeShowTeacherInfoViewModel homeShowTeacherInfoViewModel = new HomeShowTeacherInfoViewModel();
            var teacher = teacherRepository.GetTeacherById(teacherId);
            homeShowTeacherInfoViewModel.TeacherName = $"{teacher.FirstName} {teacher.LastName}";
            homeShowTeacherInfoViewModel.CourseNames = teacher.Courses.Select(c => c.Name);
                
            return View(homeShowTeacherInfoViewModel);
        }

        [Route("Privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
