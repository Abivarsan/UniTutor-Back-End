using System.ComponentModel.DataAnnotations;

namespace UniTutor.DTO
{
    public class UpdateStudent
    {
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? grade { get; set; }
        public String? address { get; set; }
        public string? profileImage { get; set; }

       
    }
}
