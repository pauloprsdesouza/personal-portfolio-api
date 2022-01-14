using NUlid;

namespace Portfolio.Api.Infrastructure.Database.DataModel.Categories
{
    public class CategoryKey
    {
        public CategoryKey(string categoryName)
        {
            PK = $"Category";
            SK = $"Category#{categoryName}";
        }

        public string PK { get; }

        public string SK { get; }

        public void AssignTo(Category category)
        {
            category.PK = PK;
            category.SK = SK;
        }
    }
}
