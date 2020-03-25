using AutoMapper;
using CoronaVirusApi.Jobs;
using CoronaVirusApi.Models;
using CoronaVirusApi.Models.DTO;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaVirusApi.Services
{
    public interface IInfectedService
    {
        Task<InfectedDTO> Get(Guid id);
        Task<IEnumerable<InfectedDTO>> GetAll();
        Task<bool> Add(InfectedDTO model);
        Task<bool> Update(Guid id, InfectedDTO model);
        Task<bool> Delete(Guid id);
    }
    public class InfectedService : IInfectedService
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly CoronaVirusContext _context;
        public InfectedService(IMapper mapper, CoronaVirusContext context, IUserService userService)
        {
            _mapper = mapper;
            _context = context;
            _userService = userService;
        }

        public async Task<InfectedDTO> Get(Guid id) =>
            _mapper.Map<InfectedDTO>(
                await _context.Infecteds.SingleOrDefaultAsync(x => x.Id == id && !x.IsDeleted));
        public async Task<IEnumerable<InfectedDTO>> GetAll() =>
            _mapper.Map<IEnumerable<InfectedDTO>>(
                await _context.Infecteds.Where(x => !x.IsDeleted).ToListAsync());

        public async Task<bool> Add(InfectedDTO model)
        {
            var infected = _mapper.Map<Infected>(model);
            infected.UserId = _userService.CurrentUser.Id;

            await _context.Infecteds.AddAsync(infected);

            BackgroundJob.Enqueue<NotificationJob>(x => x.Run($"Infected {model.Name} {model.Surname} added!"));

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(Guid id, InfectedDTO model)
        {
            var infected = await _context.Infecteds.SingleOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            if (infected == null)
                return false;

            _mapper.Map(model, infected);

            infected.UserId = _userService.CurrentUser.Id;

            BackgroundJob.Enqueue<NotificationJob>(x => x.Run($"Infected {model.Name} {model.Surname} updated!"));

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(Guid id)
        {
            var infected = await _context.Infecteds.SingleOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            if (infected == null)
                return false;

            infected.IsDeleted = true;

            BackgroundJob.Enqueue<NotificationJob>(x => x.Run($"Infected {infected.Name} {infected.Surname} deleted!"));

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
