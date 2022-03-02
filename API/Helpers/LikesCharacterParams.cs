namespace API.Helpers
{
    public class LikesCharacterParams : PaginationParams
    {
        public int UserId { get; set; }
        public int CharacterId { get; set; }
        public string Predicate { get; set; }
    }
}