using System.ComponentModel.DataAnnotations;

namespace Polls.Api.Data.DomainModels
{
    public class Vote
    {
        public int Id { get; set; }
        [Required]
        public int OptionId { get; set; }
        public Option Option { get; set; } 
    }
}
