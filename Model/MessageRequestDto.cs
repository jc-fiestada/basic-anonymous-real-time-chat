namespace MiniChat.Model
{
    class MessageRequestDto
    {
        public string? Message { get; set; }
        public string? User { get; set; }
        
        public DateTime? date { get; set; }
    }
}