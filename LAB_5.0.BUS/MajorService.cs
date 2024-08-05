using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB_5._0.BUS
{
    public class MajorService
    {
        private static StudentDb context = new StudentDb();
        public List<Major> GetAllByFaculty(int facultyID)
        {
            
            return context.Majors.Where(p => p.FacultyID == facultyID).ToList();
        }

        public Major GetByFaculty(int facultyID)
        { 
            return context.Majors.FirstOrDefault(p => p.FacultyID == facultyID);
        }
    }
}
