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
        private readonly IPhotoService _photoService;
        public CharactersController(DataContext context, IMapper mapper, IUnitOfWork unitOfWork, IPhotoService photoService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
            _photoService = photoService;
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

        [HttpPut("{characterId}")]
        public async Task<ActionResult> UpdateCharacter(CharacterUpdateDto characterUpdateDto, string characterId)
        {
            var character = await _unitOfWork.CharacterRepository.GetCharacterAsync(characterId);

            _mapper.Map(characterUpdateDto, character);

            _unitOfWork.CharacterRepository.Update(character);

            if (await _unitOfWork.Complete()) return NoContent();

            return BadRequest("Failed to update user");
        }

        private async Task<bool> CharacterExists(string characterName)
        {
            return await _context.Characters.AnyAsync(x => x.CharacterName == characterName);
        }

        [HttpPost("{characterId}/add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file, string characterId)
        {
            var character = await _unitOfWork.CharacterRepository.GetCharacterAsync(characterId);

            var result = await _photoService.AddPhotoAsync(file);

            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new PhotoCharacter
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            if (character.Photos.Count == 0)
            {
                photo.IsMain = true;
            }

            character.Photos.Add(photo);

            if (await _unitOfWork.Complete())
            {
                return CreatedAtRoute("GetCharacter", new { characterId = character.CharacterId }, _mapper.Map<PhotoCharacterDto>(photo));
            }

            return BadRequest("Problem adding photo");
        }

        [HttpPut("{characterId}/set-main-photo/{photoId}")]
        public async Task<ActionResult> SetMainPhoto(int photoId, string characterId)
        {
            var character = await _unitOfWork.CharacterRepository.GetCharacterAsync(characterId);

            var photo = character.Photos.FirstOrDefault(x => x.Id == photoId);

            if (photo.IsMain) return BadRequest("This is already the main photo");

            var currentMain = character.Photos.FirstOrDefault(x => x.IsMain);
            if (currentMain != null) currentMain.IsMain = false;
            photo.IsMain = true;

            if (await _unitOfWork.Complete()) return NoContent();

            return BadRequest("Failed to set main photo");
        }

        [HttpDelete("{characterId}/delete-photo/{photoId}")]
        public async Task<ActionResult> DeletePhoto(int photoId, string characterId)
        {
            var character = await _unitOfWork.CharacterRepository.GetCharacterAsync(characterId);

            var photo = character.Photos.FirstOrDefault(x => x.Id == photoId);

            if (photo == null) return NotFound();

            if (photo.IsMain) return BadRequest("You cannot delete your main photo");

            if (photo.PublicId != null)
            {
                var result = await _photoService.DeletePhotoAsync(photo.PublicId);
                if (result.Error != null) return BadRequest(result.Error.Message);
            }

            character.Photos.Remove(photo);

            if (await _unitOfWork.Complete()) return Ok();

            return BadRequest("Failed to delete photo");
        }
    }
}