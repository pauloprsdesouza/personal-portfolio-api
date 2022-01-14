using System.Collections.Generic;

namespace Portfolio.Api.Models.Papers
{
    public class GetPaperResponse
    {
        /// <summary>
        /// Papers list response.
        /// </summary>
        public IEnumerable<PaperResponse> Papers {get;set;}
    }
}
