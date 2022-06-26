using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SchoolManagementClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementClient.Controllers
{
    public class TeacherController : Controller
    {

        private static List<MyTeacher> teachers = new List<MyTeacher>();
        private static MyTeacher teacher = new MyTeacher();

        private HttpClient client = new HttpClient();

        private string url;

        public TeacherController()
        {
            url = @"http://localhost:41530/api/Teacher";
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: TeacherController
        public async Task<ActionResult> Index()
        {
            var msg = await client.GetAsync(url);
            var TeacherResponse = msg.Content.ReadAsStringAsync();

            teachers = JsonConvert.DeserializeObject<List<MyTeacher>>(TeacherResponse.Result);
            return View(teachers);
        }

        // GET: TeacherController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var msg = await client.GetAsync(url + "/" + id);
            var SingleTeacher = msg.Content.ReadAsStringAsync();

            teacher = JsonConvert.DeserializeObject<MyTeacher>(SingleTeacher.Result);
            return View(teacher);
        }

        // GET: TeacherController/Create
        public async Task<ActionResult> Create()
        {
            return View();
        }

        // POST: TeacherController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MyTeacher newTeacher)
        {
            try
            {
                StringContent teacherContent = new StringContent(JsonConvert.SerializeObject(newTeacher), Encoding.UTF8, "application/json");

                var msg = await client.PostAsync(url, teacherContent);
                var response = msg.Content.ReadAsStringAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TeacherController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var msg = await client.GetAsync(url + "/" + id);
            var SingleTeacher = msg.Content.ReadAsStringAsync();

            teacher = JsonConvert.DeserializeObject<MyTeacher>(SingleTeacher.Result);
            return View(teacher);
        }

        // POST: TeacherController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, MyTeacher updateTeacher)
        {
            try
            {
                teacher.TeacherId = updateTeacher.TeacherId;
                teacher.TeacherName = updateTeacher.TeacherName;
                teacher.Departement = updateTeacher.Departement;
                teacher.Contact = updateTeacher.Contact;

                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(teacher), Encoding.UTF8, "application/json");

                var msg = await client.PutAsync(url + "/" + id, stringContent);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TeacherController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var msg = await client.GetAsync(url + "/" + id);
            var SingleTeacher = msg.Content.ReadAsStringAsync();

            teacher = JsonConvert.DeserializeObject<MyTeacher>(SingleTeacher.Result);
            return View(teacher);
        }

        // POST: TeacherController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var msg = await client.DeleteAsync(url + "/" + id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
