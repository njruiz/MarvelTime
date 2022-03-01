using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public CharacterRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<CharacterDto>> GetCharactersAsync()
        {
            return await _context.Characters
                .ProjectTo<CharacterDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<CharacterDto> GetCharacterByCharacterIdAsync(string characterId)
        {
            return await _context.Characters
                .Where(x => x.CharacterId == characterId)
                .ProjectTo<CharacterDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<Character> GetCharacterAsync(string characterId)
        {
            return await _context.Characters
            .Include(p => p.Photos)
            .SingleOrDefaultAsync(x => x.CharacterId == characterId);
        }

        public void Update(Character character)
        {
            _context.Entry(character).State = EntityState.Modified;
        }
    }
}