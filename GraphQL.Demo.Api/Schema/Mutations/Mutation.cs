using GraphQL.Demo.Api.DTOs;
using GraphQL.Demo.Api.Schema.Queries;
using GraphQL.Demo.Api.Schema.Subscriptions;
using GraphQL.Demo.Api.Services.Courses;
using HotChocolate.Subscriptions;
using System.Threading.Tasks;

namespace GraphQL.Demo.Api.Schema.Mutations
{
    public class Mutation
    {
        private readonly CoursesRepository _coursesRepository;

        public Mutation(CoursesRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
        }


        public async Task<CourseResult> CreateCourse(CourseInputType courseInput, [Service] ITopicEventSender topicEventSender)
        {

            CourseDTO courseDTO = new CourseDTO()
            {
                Name = courseInput.Name,
                Subjects = courseInput.Subject,
                InstructorId = courseInput.InstructorId,
            };

            courseDTO = await _coursesRepository.Create(courseDTO);


            CourseResult course = new CourseResult()
            {
                Id = courseDTO.Id,
                Name = courseDTO.Name,
                Subject = courseDTO.Subjects,
                InstructorId = courseDTO.InstructorId,

            };

            await topicEventSender.SendAsync(nameof(Subscription.CourseCreated), course);
            return course;
        }


        public async Task<CourseResult> UpdateCourse(Guid id, CourseInputType courseInput, [Service] ITopicEventSender topicEventSender)
        {

            CourseDTO courseDTO = new CourseDTO()
            {
                Id = id,
                Name = courseInput.Name,
                Subjects = courseInput.Subject,
                InstructorId = courseInput.InstructorId,
            };

            
            courseDTO = await _coursesRepository.Update(courseDTO);

            CourseResult course = new CourseResult()
            {
                Id = courseDTO.Id,
                Name = courseDTO.Name,
                Subject = courseDTO.Subjects,
                InstructorId = courseDTO.InstructorId,

            };

            //Custome Topic 

            string updateCourseTopic = $"{course.Id}_{nameof(Subscription.CourseUpdated)}";
            await topicEventSender.SendAsync(updateCourseTopic, course);

            return course;
        }

        public async Task<bool> DeleteCourse(Guid id)
        {
            try
            {
                return await _coursesRepository.Dalete(id);
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
