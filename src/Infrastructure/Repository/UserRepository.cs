using tmp.src.Domain.Entities.Request;
using tmp.src.Domain.IRepository;
using tmp.src.Domain.Models;
using tmp.src.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using webApiTemplate.src.App.Provider;

namespace tmp.src.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserModel?> AddAsync(SignUpBody body, string role)
        {
            var oldUser = await GetAsync(body.Email);
            if (oldUser != null)
                return null;

            var newUser = new UserModel
            {
                Email = body.Email,
                PasswordHash = Hmac512Provider.Compute(body.Password),
                RoleName = role,
            };

            var result = await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return result?.Entity;
        }

        public async Task<UserModel?> GetAsync(Guid id)
            => await _context.Users
                .FirstOrDefaultAsync(e => e.Id == id);

        public async Task<UserModel?> GetAsync(string email)
            => await _context.Users
                .FirstOrDefaultAsync(e => e.Email == email);

        public async Task<UserModel?> GetByTokenAsync(string refreshTokenHash)
            => await _context.Users
            .FirstOrDefaultAsync(e => e.Token == refreshTokenHash);


        public async Task<UserModel?> UpdateProfileIconAsync(Guid userId, string filename)
        {
            var user = await GetAsync(userId);
            if (user == null)
                return null;

            user.Image = filename;
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<string?> UpdateTokenAsync(string refreshToken, Guid userId, TimeSpan? duration = null)
        {
            var user = await GetAsync(userId);
            if (user == null)
                return null;

            if (duration == null)
                duration = TimeSpan.FromDays(15);

            if (user.TokenValidBefore <= DateTime.UtcNow || user.TokenValidBefore == null)
            {
                user.TokenValidBefore = DateTime.UtcNow.Add((TimeSpan)duration);
                user.Token = refreshToken;
                await _context.SaveChangesAsync();
            }

            return user.Token;
        }
    }
}