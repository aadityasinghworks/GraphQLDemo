using GraphQL.Demo.Api.Schema.Queries;
using HotChocolate.Data.Sorting;

namespace GraphQL.Demo.Api.Schema.Shorters
{
    public class CourseSortType : SortInputType<CourseType>
    {
        protected override void Configure(ISortInputTypeDescriptor<CourseType> descriptor)
        {
            descriptor.Name("CourseTypeSorting");

            descriptor.Ignore(i => i.Id);
            descriptor.Ignore(i => i.InstructorId);
            base.Configure(descriptor);
        }
    }
}
