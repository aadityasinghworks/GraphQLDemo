using GraphQL.Demo.Api.DTOs;
using GraphQL.Demo.Api.Services.Instructors;

namespace GraphQL.Demo.Api.DataLoaders
{
    public class InstructorDataLoader : BatchDataLoader<Guid, InstructorDTO>
    {
        private readonly InstructorsRepository _instructorsRepository;
        public InstructorDataLoader(
            InstructorsRepository instructorsRepository,
            IBatchScheduler batchScheduler,
            DataLoaderOptions options) : base(batchScheduler, options)
        {
            _instructorsRepository = instructorsRepository;

        }

        protected override async Task<IReadOnlyDictionary<Guid, InstructorDTO>> LoadBatchAsync(
            IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
        {
            IEnumerable<InstructorDTO> instructors = await _instructorsRepository.GetManyByIds(keys);

            return instructors.ToDictionary(i => i.Id);
        }
    }
}
