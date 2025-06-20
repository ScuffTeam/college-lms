namespace college_lms.Data.DTOs.Users
{
    public class CreateUserDto
    {
        [Required]
        public required string LastName { get; set; }

        [Required]
        public required string FirstName { get; set; }

        public string? MiddleName { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }

        [Required]
        public required string UserName { get; set; }

        public bool IsTeacher { get; set; }
        public bool IsAdmin { get; set; }
    }
}