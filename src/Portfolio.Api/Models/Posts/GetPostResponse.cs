using System.Collections.Generic;

namespace Portfolio.Api.Models.Posts
{
    public class GetPostResponse
    {
        /// <summary>
        /// Posts list response
        /// </summary>
        public IEnumerable<PostResponse> Posts { get; set; }
    }
}
