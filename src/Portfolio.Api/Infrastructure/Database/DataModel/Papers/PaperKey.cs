
namespace Portfolio.Api.Infrastructure.Database.DataModel.Papers
{
    public class PaperKey
    {
         public PaperKey(string id)
        {
            PK = $"Paper";
            SK = $"Id#{id}";
        }

        public string PK { get; }

        public string SK { get; }

        public void AssignTo(Paper paper)
        {
            paper.PK = PK;
            paper.SK = SK;
        }
    }
}
