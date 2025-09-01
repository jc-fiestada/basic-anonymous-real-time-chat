namespace MiniChat.Model
{
    class MessageRequestDto
    {
        public string? Message { get; set; }
        public string? User { get; set; }

        public DateTime? Date { get; set; }

        public MessageRequestDto() { }

        public MessageRequestDto(string? message, string? user, DateTime? date)
        {
            Message = message;
            User = user;
            Date = date;
        }
    }
}