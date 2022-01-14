using System.Collections.Generic;

namespace Portfolio.Api.Models.Subscribers
{
    public class GetSubscriberResponse
    {
        /// <summary>
        /// Messa unique identifier
        /// </summary>
        public IEnumerable<SubscriberResponse> Subscribers {get;set;}
    }
}
