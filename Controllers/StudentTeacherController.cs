using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SchoolManagementClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SchoolManagementClient.Controllers
{
    public class StudentTeacherController : Controller
    {
        private static List<MyStudentTeacher> studentTeachers = new List<MyStudentTeacher>();
        private static MyStudentTeacher studentTeacher = new MyStudentTeacher();
        private string url = "";
        private HttpClient client = new HttpClient();

        public StudentTeacherController()
        {
            url = @"http://localhost:41530/api/StudentTeacher";
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: StudentTeacherController
        public async Task<ActionResult> Index()
        {
            var msg = await client.GetAsync(url);
            var response = msg.Content.ReadAsStringAsync();

            studentTeachers = JsonConvert.DeserializeObject<List<MyStudentTeacher>>(response.Result);

            return View(studentTeachers);
        }

        // GET: StudentTeacherController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StudentTeacherController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentTeacherController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentTeacherController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StudentTeacherController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentTeacherController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StudentTeacherController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
