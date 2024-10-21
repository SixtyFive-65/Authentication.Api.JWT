namespace Polls.Api.Data.DomainModels
{
    public class Option
    {
        public int Id { get; set; }
        public string OptionText { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public ICollection<Vote> Votes { get; set; } = new List<Vote>();
    }
}
