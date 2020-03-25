using AutoMapper;
using CoronaVirusApi.Models;
using CoronaVirusApi.Models.DTO;
using Jose;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoronaVirusApi.Services
{
    public interface IUserService
    {
        CurrentUserDTO CurrentUser { get; set; }
        bool ValidateToken(string token);
        Task<string> Login(UserDTO model);
        Task<bool> Register(UserDTO model);
        Task<IEnumerable<Guid>> GetNotificationUserIds();
    }

    public class UserService : IUserService
    {
        public CurrentUserDTO CurrentUser { get; set; }
        private const string _jwtKey = "msCcNCTw8RCZVSF3Sn";

        private readonly CoronaVirusContext _context;
        private readonly IMapper _mapper;

        public UserService(CoronaVirusContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Guid>> GetNotificationUserIds() =>
            await _context.Users.Where(x => x.SendNofitication).Select(x => x.Id).ToListAsync();

        public async Task<string> Login(UserDTO model)
        {
            var user = await _context
                .Users
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Username == model.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
                return null;

            return JWT.Encode(_mapper.Map<CurrentUserDTO>(user),
                Encoding.UTF8.GetBytes(_jwtKey),
                JwsAlgorithm.HS256);
        }

        public async Task<bool> Register(UserDTO model)
        {
            if (await _context.Users.AnyAsync(x => x.Username == model.Username))
                return false;

            model.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);

            await _context.Users.AddAsync(_mapper.Map<User>(model));
            return await _context.SaveChangesAsync() > 0;
        }

        public bool ValidateToken(string token)
        {
            try
            {
                CurrentUser = JWT.Decode<CurrentUserDTO>(token, Encoding.UTF8.GetBytes(_jwtKey));
            }
            catch
            {
                return false;
            }

            return CurrentUser != null;
        }
    }
}
