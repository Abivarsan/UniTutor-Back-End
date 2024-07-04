namespace UniTutor.DTO
{
    public class SubjectRequest
    {
        public string title { get; set; }
        public string description { get; set; }
        public string coverImage { get; set; }
        public string[] medium { get; set; }
        public string mode { get; set; }
        public string[] availability { get; set; }
        
    }
}
