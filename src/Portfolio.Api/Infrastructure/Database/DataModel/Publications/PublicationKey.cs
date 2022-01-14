namespace Portfolio.Api.Infrastructure.Database.DataModel.Publications
{
    public class PublicationKey
    {
        public PublicationKey(string id)
        {
            PK = $"Publication";
            SK = $"Id#{id}";
        }

        public string PK { get; }

        public string SK { get; }

        public void AssignTo(Publication publication)
        {
            publication.PK = PK;
            publication.SK = SK;
        }
    }
}
