using API.Entities;

namespace API.DTOs
{
    public class CharacterDto
    {
        public int Id { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string CharacterId { get; set; }
        public string CharacterName { get; set; }
        public string RealName { get; set; }
        public string PhotoUrl { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Role { get; set; }
        public string SourceOfPower { get; set; }
        public string McuStory { get; set; }
        public string PlaceOfOrigin { get; set; }
        public string PlayedBy { get; set; }
        public ICollection<PhotoCharacter> Photos { get; set; }
    }
}