using System.ComponentModel.DataAnnotations;
using NUlid;

namespace Portfolio.Api.Models.Subscribers
{
    public class GetSubscribersQuery
    {
        /// <summary>
        /// Message's length
        /// </summary>
        [MaxLength(100)]
        public int? Length { get; set; }
    }
}
