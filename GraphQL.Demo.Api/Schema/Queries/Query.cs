using Bogus;
using GraphQL.Demo.Api.DTOs;
using GraphQL.Demo.Api.Models;
using GraphQL.Demo.Api.Services.Courses;
using System.Reflection.Metadata.Ecma335;

namespace GraphQL.Demo.Api.Schema.Queries
{
    public class Query
    {
        private readonly CoursesRepository _coursesRepository;

        public Query(CoursesRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
        }

        public async Task<IEnumerable<CourseType>> GetCourses()
        {
            try
            {
                IEnumerable<CourseDTO> courseDTOs = await _coursesRepository.GetAll();

                return courseDTOs.Select(c => new CourseType()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Subjects = c.Subjects,
                    InstructorId = c.InstructorId,
                    //Instructor = new InstructorType()
                    //{
                    //    Id = c.Instructor.Id,
                    //    FirstName = c.Instructor.FirstName,
                    //    LastName = c.Instructor.LastName,
                    //    Salary = c.Instructor.Salary
                    //}
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<CourseType> GetCourseByIdAsync(Guid id)
        {
            CourseDTO courseDTO = await _coursesRepository.GetCourseById(id);
            return new CourseType()
            {
                Id = courseDTO.Id,
                Name = courseDTO.Name,
                Subjects = courseDTO.Subjects,
                InstructorId = courseDTO.InstructorId,
                //Instructor = new InstructorType()
                //{
                //    Id = courseDTO.Instructor.Id,
                //    FirstName = courseDTO.Instructor.FirstName,
                //    LastName = courseDTO.Instructor.LastName,
                //    Salary = courseDTO.Instructor.Salary
                //}
            };
        }

        [GraphQLDeprecated("This query is deprecated")]
        public string Insructions => "Welcome to GraphQL ";
    }
}
