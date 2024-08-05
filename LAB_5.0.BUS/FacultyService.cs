using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace LAB_5._0.BUS
{
    public class FacultyService
    {
        private static FacultyService instance;

        private static StudentDb context = new StudentDb();

        private FacultyService() { }

        public static FacultyService Instance { 
            get
            {
                if (instance == null)
                {
                    instance = new FacultyService();
                }  
                return instance;
            }
        }

        public List<Faculty> GetAll()
        {
            //StudentDb context = new StudentDb();
            return context.Faculties.ToList();
        }

        public Faculty FindByID(int id)
        {
            return context.Faculties.FirstOrDefault(p => p.FacultyID == id); 
        }
    }
}
