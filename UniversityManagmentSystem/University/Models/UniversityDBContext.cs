using System.Data.Entity;

namespace University.Models
{
    public class UniversityDBContext:DbContext
    {
        public UniversityDBContext() : base("UniversitySystemDBConnectionString")
        {

        }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Course> Courses { get; set; }

        public DbSet<Designation> Designations { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<AssignCourse> AssignCourses { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<EnrollCourse> EnrollCourses { get; set; }

        public DbSet<Grade> Grades { get; set; }
        public DbSet<ResultEntry> ResultEntries { get; set; }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<RoomAllocation> RoomAllocations { get; set; }
    }
}