namespace Polls.Api.Data.DomainModels
{
    public class Poll
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Question { get; set; }
        public ICollection<Question> Questions { get; set; } = [];
    }
}
