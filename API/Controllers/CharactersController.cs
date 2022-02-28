using System.Text.RegularExpressions;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class CharactersController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public CharactersController(DataContext context, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }

        [HttpPost("create-character")]
        public async Task<ActionResult<CharacterDto>> CreateCharacter(CreateCharacterDto characterDto)
        {
            if (await CharacterExists(characterDto.CharacterName)) return BadRequest("Character is already registered");

            var character = _mapper.Map<Character>(characterDto);
            character.CharacterId = Regex.Replace(character.CharacterName, @"\s+", "").ToLower();

            _context.Characters.Add(character);
            await _context.SaveChangesAsync();

            return new CharacterDto
            {
                RealName = character.RealName,
                CharacterName = character.CharacterName,
                Gender = character.Gender,
                Role = character.Role
            };
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CharacterDto>>> GetCharacters()
        {
            var characters = await _unitOfWork.CharacterRepository.GetCharactersAsync();
            return Ok(characters);
        }

        [HttpGet("{characterId}", Name = "GetCharacter")]
        public async Task<ActionResult<CharacterDto>> GetCharacter(string characterId)
        {
            return await _unitOfWork.CharacterRepository.GetCharacterByCharacterIdAsync(characterId);

        }

        private async Task<bool> CharacterExists(string characterName)
        {
            return await _context.Characters.AnyAsync(x => x.CharacterName == characterName);
        }
    }
}