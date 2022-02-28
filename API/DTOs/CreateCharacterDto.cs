using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class CreateCharacterDto
    {
        [Required] public string CharacterName { get; set; }
        [Required] public string RealName { get; set; }
        [Required] public string Gender { get; set; }
        [Required] public string Role { get; set; }
        [Required] public DateTime DateOfBirth { get; set; }
        [Required] public string PlaceOfOrigin { get; set; }
        [Required] public string PlayedBy { get; set; }
    }
}