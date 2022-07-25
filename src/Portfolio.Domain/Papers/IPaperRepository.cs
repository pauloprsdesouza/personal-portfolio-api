namespace Portfolio.Domain.Papers
{
    public interface IPaperRepository
    {
        Task<Paper> FindById(int id);

        Task<Paper> Create(Paper paper);

        Task<Paper> Update(Paper paper);

        Task<Paper> Delete(Paper paper);
    }
}
