namespace Polls.Api.Data.DomainModels
{
    public class Question
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }

        public int PollId { get; set; }
        public Poll Poll { get; set; }

        public ICollection<Option> Options { get; set; } = [];
    }
}
