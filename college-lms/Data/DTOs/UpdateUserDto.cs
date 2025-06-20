namespace college_lms.Data.DTOs.Users
{
    public class UpdateUserDto
    {
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public bool? IsTeacher { get; set; }
        public bool? IsAdmin { get; set; }
    }
}