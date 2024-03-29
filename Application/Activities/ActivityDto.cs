using Application.Comments;

namespace Application.Activities
{
    public class ActivityDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }
		public string Category { get; set; }

        public string City { get; set; } 

        public string Venue { get; set; } 

        public string HostUsername { get; set; }

        public bool IsCancelled { get; set; }
		
        public ICollection<AttendeeDto> Attendees { get; set; }

        public string[] Options { get; set; }

        public string WinningOption { get; set; }

        public int AmountIfWon { get; set; }

        public int AmountIfLose { get; set; }

        public bool IsMandatoryActivity { get; set; }
		
		public ICollection<CommentDto> Comments { get; set; }
    }
}