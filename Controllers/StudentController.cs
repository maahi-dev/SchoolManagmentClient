using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SchoolManagementClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementClient.Controllers
{
    public class StudentController : Controller
    {

        private static List<MyStudent> students = new List<MyStudent>();
        private static MyStudent student = new MyStudent();
        private HttpClient client = new HttpClient();
        private string url = "";

        public StudentController()
        {
            url = @"http://localhost:41530/api/Student";
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: StudentController
        public async Task<ActionResult> Index()
        {
                var msg = await client.GetAsync(url);
                var studentResponse = msg.Content.ReadAsStringAsync();

                students = JsonConvert.DeserializeObject<List<MyStudent>>(studentResponse.Result);
            
            return View(students);
        }

        // GET: StudentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StudentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentController/Create
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

        // GET: StudentController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var msg = await client.GetAsync(url+"/"+id);
            var singleStudent = msg.Content.ReadAsStringAsync();

            student = JsonConvert.DeserializeObject<MyStudent>(singleStudent.Result);
            return View(student);
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MyStudent updateStudent)
        {
            try
            {
                student.StudentId = updateStudent.StudentId;
                student.StudentName = updateStudent.StudentName;
                student.Contact = updateStudent.Contact;
                student.Age = updateStudent.Age;
                student.Gender = updateStudent.Gender;

                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json");

                var msg = client.PutAsync(url + "/" + id, stringContent);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StudentController/Delete/5
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

        // GET: StudentController/Search
        public ActionResult Search()
        {
            return View();
        }

        // POST: StudentController/Search
        [HttpPost]
        public async Task<ActionResult> Search(MyStudent student)
        {
            try
            {
                StringContent studentContent = new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json");

                var msg = await client.PostAsync("http://localhost:41530/api/Search", studentContent);
                var response = msg.Content.ReadAsStringAsync();

                students = JsonConvert.DeserializeObject<List<MyStudent>>(response.Result);
                return RedirectToAction(nameof(SearchList), students);
                //return CreatedAtAction("Index", new { id = 1 }, students);
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController
        public async Task<ActionResult> SearchList()
        {
            return View(students);
        }

    }
}
