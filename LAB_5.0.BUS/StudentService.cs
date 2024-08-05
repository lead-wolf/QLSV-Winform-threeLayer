using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace LAB_5._0.BUS
{
    public class StudentService
    {
        private static StudentService instance;
        private static StudentDb context = new StudentDb();

        private StudentService() { }

        public static StudentService Instance 
        { 
             get
             {
                if (instance == null)
                {
                    instance = new StudentService();
                }
                return instance; 
             }
        }

        public List<Student> GetAll()
        {
            //StudentDb context = new StudentDb();
            //StudentDb context = new StudentDb();
            return context.Students.ToList();
        }

        public List<Student> GetAllHasNoMajor()
        {
           // StudentDb context = new StudentDb();
            return context.Students.Where(p => p.MajorID == null).ToList();
        }

        public List<Student> GetAllHasNoMajor(int facultyID)
        {
            //StudentDb context = new StudentDb();
            return context.Students.Where(p => p.MajorID == null && p.FacultyID == facultyID).ToList();
        }

        public Student FindById(string studentId)
        {
            //StudentDb context = new StudentDb();
            //Student s = context.Students.FirstOrDefault(p => p.StudentID == studentId);
            //context.Entry(s).State = EntityState.Detached;
            //return s;
            return context.Students.FirstOrDefault(p => p.StudentID == studentId);

        }

        public void InsertUpdate(Student s)
        {
            //StudentDb context = new StudentDb();
            context.Students.AddOrUpdate(s);
            context.SaveChanges();
        }

        public bool RemoveByID(String ID)
        {
            //StudentDb context = new StudentDb();
            Student stuDele = FindById(ID);
            if (stuDele != null)
            {
                //context.Students.Attach(stuDele);
                context.Students.Remove(stuDele);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public void Remove(Student s)
        {
            //StudentDb context = new StudentDb();
            context.Students.Attach(s);
            context.Students.Remove(s);
            context.SaveChanges();
        }
    }
}
