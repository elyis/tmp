using tmp.src.Domain.Enums;

namespace tmp.src.Domain.Entities.Response
{
    public class ProfileBody
    {
        public string Email { get; set; }
        public UserRole Role { get; set; }
        public string? UrlIcon { get; set; }
    }
}