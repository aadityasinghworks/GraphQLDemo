using GraphQL.Demo.Api.DTOs;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Demo.Api.Services.Instructors
{
    public class InstructorsRepository
    {
        private readonly IDbContextFactory<SchoolDbContext> _contextFactory;

        public InstructorsRepository(IDbContextFactory<SchoolDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }


        public async Task<InstructorDTO> GetInstructorById(Guid instuctorId)
        {
            using (SchoolDbContext contex = _contextFactory.CreateDbContext())
            {
                return await contex.Instructors.FirstOrDefaultAsync(c => c.Id == instuctorId);
            }
        }

        public async Task<IEnumerable<InstructorDTO>> GetManyByIds(IReadOnlyList<Guid> instructorIds)
        {

            using (SchoolDbContext contex = _contextFactory.CreateDbContext())
            {
                return await contex.Instructors.Where(i=> instructorIds.Contains(i.Id)).ToListAsync();
            }
        }


    }
}
