using tmp.src.Domain.Enums;

namespace tmp.src.Domain.Entities.Response
{
    public class ProfileBody
    {
        public string Email { get; set; }
        public UserRole Role { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string? UrlIcon { get; set; }
    }
}