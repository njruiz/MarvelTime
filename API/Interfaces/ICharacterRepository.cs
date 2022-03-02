using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface ICharacterRepository
    {
        Task<IEnumerable<CharacterDto>> GetCharactersAsync();
        Task<CharacterDto> GetCharacterByCharacterIdAsync(string characterId);
        Task<Character> GetCharacterAsync(string characterId);
        void Update(Character character);
    }
}