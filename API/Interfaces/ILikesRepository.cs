using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface ILikesRepository
    {
        Task<UserLike> GetUserLike(int sourceUserId, int likedUserId);
        Task<AppUser> GetUserWithLikes(int userId);
        Task<PagedList<LikeDto>> GetUserLikes(LikesParams likesParams);
        Task<AppUser> GetUserWithCharacterLikes(int userId);
        Task<CharacterLike> GetCharacterLike(int sourceUserId, int likedCharacterId);
        Task<Character> GetCharacterWithLikes(string characterId);
        Task<PagedList<LikeCharacterDto>> GetCharacterLikes(LikesCharacterParams likesParams);
    }
}