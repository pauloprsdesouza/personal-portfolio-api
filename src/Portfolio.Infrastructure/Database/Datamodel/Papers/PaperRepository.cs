using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Papers;

namespace Portfolio.Infrastructure.Database.Datamodel.Papers
{
    public class PaperRepository : IPaperRepository
    {
        private readonly ApiDbContext _dbContext;

        private readonly DbSet<Paper> _papers;

        public PaperRepository(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
            _papers = dbContext.Set<Paper>();
        }

        public async Task<Paper> Create(Paper paper)
        {
            await _papers.AddAsync(paper);
            await _dbContext.SaveChangesAsync();

            return paper;
        }

        public async Task<Paper> Delete(Paper paper)
        {
            _papers.Remove (paper);
            await _dbContext.SaveChangesAsync();

            return paper;
        }

        public async Task<Paper> FindById(int id)
        {
            return await _papers.Where(p => p.Id == id).SingleOrDefaultAsync();
        }

        public async Task<Paper> Update(Paper paper)
        {
            _papers.Update (paper);
            await _dbContext.SaveChangesAsync();

            return paper;
        }
    }
}
