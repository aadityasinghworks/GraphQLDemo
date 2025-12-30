using GraphQL.Demo.Api.Schema.Queries;

namespace GraphQL.Demo.Api.Schema.Mutations
{
    public class Mutation
    {
        private readonly List<CourseResult> _courcses;
        public Mutation()
        {
            _courcses = new List<CourseResult>();
        }
        public CourseResult CreateCourse(CourseInputType courseInput)
        {
            CourseResult courseType = new CourseResult()
            {
                Id = Guid.NewGuid(),
                Name = courseInput.Name,
                Subject = courseInput.Subject,
                InstructorId = courseInput.InstructorId,

            };
            _courcses.Add(courseType);
            return courseType;
        }


        public CourseResult UpdateCourse(Guid id, CourseInputType courseInput)
        {
            CourseResult courseResult = _courcses.FirstOrDefault(c => c.Id == id);
            if (courseResult == null)
            {
                throw new GraphQLException(new Error("404 Course Not Found", "404"));
            }
            courseResult.Name = courseInput.Name;
            courseResult.Subject = courseInput.Subject;
            courseResult.Id = id;
            courseResult.InstructorId = courseInput.InstructorId;
            return courseResult;
        }

        public bool DeleteCourse(Guid id)
        {
            return _courcses.RemoveAll(c => c.Id == id) >= 1;
        }
    }
}
