using college_lms.Data;
using college_lms.Data.DTOs.Users;
using college_lms.Data.Entities;
using college_lms.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace college_lms.Services.Implementations
{
    public class UserService(ApplicationDbContext context) : IUserService
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users.Select(MapToDto);
        }

        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            var user = await _context.Users
                .Include(u => u.Groups)
                .Include(u => u.AttendanceMarks)
                .Include(u => u.Lessons)
                .FirstOrDefaultAsync(u => u.Id == id);

            return user is null ? null : MapToDto(user);
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto dto)
        {
            var user = new User
            {
                LastName = dto.LastName,
                FirstName = dto.FirstName,
                MiddleName = dto.MiddleName,
                Email = dto.Email,
                UserName = dto.UserName,
                IsTeacher = dto.IsTeacher,
                IsAdmin = dto.IsAdmin,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return MapToDto(user);
        }

        public async Task<UserDto> UpdateUserAsync(int id, UpdateUserDto dto)
        {
            var user = await _context.Users
                .Include(u => u.Groups)
                .Include(u => u.AttendanceMarks)
                .Include(u => u.Lessons)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                throw new KeyNotFoundException("User not found");

            if (!string.IsNullOrEmpty(dto.LastName)) user.LastName = dto.LastName;
            if (!string.IsNullOrEmpty(dto.FirstName)) user.FirstName = dto.FirstName;
            if (dto.MiddleName != null) user.MiddleName = dto.MiddleName;
            if (!string.IsNullOrEmpty(dto.Email)) user.Email = dto.Email;
            if (!string.IsNullOrEmpty(dto.UserName)) user.UserName = dto.UserName;
            if (dto.IsTeacher.HasValue) user.IsTeacher = dto.IsTeacher.Value;
            if (dto.IsAdmin.HasValue) user.IsAdmin = dto.IsAdmin.Value;

            user.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return MapToDto(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        private static UserDto MapToDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                LastName = user.LastName,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                Email = user.Email,
                UserName = user.UserName,
                IsTeacher = user.IsTeacher,
                IsAdmin = user.IsAdmin,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                GroupIds = user.Groups?.Select(g => g.Id).ToList() ?? [],
                AttendanceMarkIds = user.AttendanceMarks?.Select(am => am.Id).ToList() ?? [],
                LessonIds = user.Lessons?.Select(l => l.Id).ToList() ?? []
            };
        }
    }
}