namespace API.Entities
{
    public class CharacterLike
    {
        public AppUser SourceUser { get; set; }
        public int SourceUserId { get; set; }

        public Character LikedCharacter { get; set; }
        public int LikedCharacterId { get; set; }
    }
}