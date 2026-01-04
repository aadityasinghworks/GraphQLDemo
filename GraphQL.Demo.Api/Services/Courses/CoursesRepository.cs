using GraphQL.Demo.Api.DTOs;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Demo.Api.Services.Courses
{
    public class CoursesRepository
    {
        private readonly IDbContextFactory<SchoolDbContext> _contextFactory;

        public CoursesRepository(IDbContextFactory<SchoolDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }


        public async Task<IEnumerable<CourseDTO>> GetAll()
        {
            using (SchoolDbContext contex = _contextFactory.CreateDbContext())
            {
                return await contex.Courses
                    .Include(c => c.Instructor).Include(s => s.Students)
                    .ToListAsync();
            }
        }


        public async Task<CourseDTO> GetCourseById(Guid courseId
            )
        {
            using (SchoolDbContext contex = _contextFactory.CreateDbContext())
            {
                return await contex.Courses
                    .Include(c => c.Instructor).Include(s => s.Students).
                    FirstOrDefaultAsync(c => c.Id == courseId);
            }
        }


        public async Task<CourseDTO> Create(CourseDTO course)
        {
            using (SchoolDbContext contex = _contextFactory.CreateDbContext())
            {

                contex.Courses.Add(course);
                await contex.SaveChangesAsync();
                return course;
            }
        }

        public async Task<CourseDTO> Update(CourseDTO course)
        {
            using (SchoolDbContext contex = _contextFactory.CreateDbContext())
            {

                contex.Courses.Update(course);
                await contex.SaveChangesAsync();
                return course;
            }
        }

        public async Task<bool> Dalete(Guid id)
        {
            using (SchoolDbContext contex = _contextFactory.CreateDbContext())
            {
                CourseDTO course = new CourseDTO
                {
                    Id = id
                };
                contex.Courses.Remove(course);
                return await contex.SaveChangesAsync() > 0;

            }
        }
    }
}
