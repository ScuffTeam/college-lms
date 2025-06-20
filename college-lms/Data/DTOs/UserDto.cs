namespace college_lms.Data.DTOs.Users
{
    public class UserDto
    {
        public int Id { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public bool IsTeacher { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<int> GroupIds { get; set; } = [];
        public ICollection<int> AttendanceMarkIds { get; set; } = [];
        public ICollection<int> LessonIds { get; set; } = [];
    }
}