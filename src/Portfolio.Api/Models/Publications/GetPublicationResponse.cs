using System.Collections.Generic;

namespace Portfolio.Api.Models.Publications
{
    public class GetPublicationResponse
    {
        /// <summary>
        /// Messa unique identifier
        /// </summary>
        public IEnumerable<PublicationResponse> Publications { get; set; }
    }
}
