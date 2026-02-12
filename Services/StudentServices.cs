using AspNetCoreGeneratedDocument;
using FirstControllerProject.Controllers;
using FirstControllerProject.Models;
using FirstControllerProject.Services;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System;
using System.Linq;
using FirstControllerProject.Services.CommonMethods;
using FirstControllerProject.Services.BaseBo;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
namespace FirstControllerProject.Services
{
    public class StudentServices : CommonBaseBO<StudentDetails>
    {
        public static List<StudentDetails> _students;
        private int nextId;
        private readonly ILogger<StudentServices> _Studentlogger;
        private readonly object _lock = new ();
        public StudentServices( ILogger<StudentServices> logger) : base (logger, _students)
        {
            _Studentlogger = logger;
            _students = new List<StudentDetails>
            {
                new StudentDetails { Id = 1, Name = "Arun", Age = 20, Email = "Computer Science" },
                new StudentDetails { Id = 2, Name = "Divya", Age = 21, Email = "Electronics" },
                new StudentDetails { Id = 3, Name = "Kiran", Age = 19, Email = "Mechanical" }
            };
            _allData = _students;
            _Studentlogger.LogInformation("Student Controller Called Student Count {1}", _students.Count());
        }
       
        public override StudentDetails Update(StudentDetails Studentupdate)
        {
            lock(_lock)
            {
                StudentDetails updatedStudent = new StudentDetails();
                if (Studentupdate != null)
                {
                    updatedStudent = _students.Where(x => x.Id == Studentupdate.Id).First();
                    updatedStudent.Age = Studentupdate.Age;
                    updatedStudent.Name = Studentupdate.Name;
                    updatedStudent.Email = Studentupdate.Email;
                }
                return updatedStudent;
            }
           
        }
        public override IEnumerable<StudentDetails> Add(IEnumerable<StudentDetails> Student)
        {
            try
            {
                lock(_lock)
                {
                    Task.Run(() =>
                    {
                        EmailHelper.EmailSender(_Studentlogger);
                    });
                    if (Student != null && Student.Any())
                    {
                        foreach (var stude in Student)
                        {
                            if (!string.IsNullOrWhiteSpace(stude.Name)) // skip empty rows
                            {
                                stude.Id = nextId;
                            }
                            nextId++;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error in Add: {ex.Message}");
                _Studentlogger.LogError("Error in Add: {msg}", ex.Message);
                throw ;
            }
            
            _students.AddRange(Student);
            _Studentlogger.LogInformation("Data Saved Student {msg}","Success");

            return _students;
        }
        public List<object> SearchEmployee(string term)
        {
            var result = _students
                .Where(e => e.Name.ToLower().Contains(term.ToLower()))
                .Select(e => new
                {
                    label = e.Name,  // shown in dropdown
                    value = e.Name,  // filled in textbox
                    Email = e.Email  // extra info
                })
                .ToList<object>();

            return result; 
        }

    }
    public static class EmailHelper
    {
        public static void EmailSender(ILogger _logger)
        {
            Thread.Sleep(3000);
            for(var i =0;i<=100;i++ )
            {
                string strCnt = "Extension Method Count Example";
                _logger.LogInformation("Method Count {cnt}", strCnt.StringCnt());
                Console.WriteLine($"Thread Taks for test {i}");
                _logger.LogInformation("Thread is running in Background. Current iteration: {Iteration} , Next iteration: {Iteration} ",i,i+1);
            }
        }
    }
   
}
