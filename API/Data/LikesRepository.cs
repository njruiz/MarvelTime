using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class LikesRepository : ILikesRepository
    {
        private readonly DataContext _context;
        public LikesRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<UserLike> GetUserLike(int sourceUserId, int likedUserId)
        {
            return await _context.Likes.FindAsync(sourceUserId, likedUserId);
        }

        public async Task<PagedList<LikeDto>> GetUserLikes(LikesParams likesParams)
        {
            var users = _context.Users.OrderBy(u => u.UserName).AsQueryable();
            var likes = _context.Likes.AsQueryable();

            if (likesParams.Predicate == "liked")
            {
                likes = likes.Where(like => like.SourceUserId == likesParams.UserId);
                users = likes.Select(like => like.LikedUser);
            }

            if (likesParams.Predicate == "likedBy")
            {
                likes = likes.Where(like => like.LikedUserId == likesParams.UserId);
                users = likes.Select(like => like.SourceUser);
            }

            var likedUsers = users.Select(user => new LikeDto
            {
                Username = user.UserName,
                KnownAs = user.KnownAs,
                Age = user.DateOfBirth.CalculateAge(),
                PhotoUrl = user.Photos.FirstOrDefault(p => p.IsMain).Url,
                City = user.City,
                UserId = user.Id
            });

            return await PagedList<LikeDto>.CreateAsync(likedUsers, likesParams.PageNumber, likesParams.PageSize);
        }

        public async Task<AppUser> GetUserWithLikes(int userId)
        {
            return await _context.Users
                .Include(x => x.LikedUsers)
                .FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<AppUser> GetUserWithCharacterLikes(int userId)
        {
            return await _context.Users
                .Include(x => x.LikedCharacters)
                .FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<CharacterLike> GetCharacterLike(int sourceUserId, int likedCharacterId)
        {
            return await _context.CharacterLikes.FindAsync(sourceUserId, likedCharacterId);
        }

        public async Task<Character> GetCharacterWithLikes(string characterId)
        {
            return await _context.Characters
                .Include(x => x.LikedCharacters)
                .FirstOrDefaultAsync(x => x.CharacterId == characterId);
        }

        public async Task<PagedList<LikeCharacterDto>> GetCharacterLikes(LikesCharacterParams likesParams)
        {
            var characters = _context.Characters.OrderBy(c => c.CharacterId).AsQueryable();
            var likes = _context.CharacterLikes.AsQueryable();

            if (likesParams.Predicate == "liked")
            {
                likes = likes.Where(like => like.SourceUserId == likesParams.UserId);
                characters = likes.Select(like => like.LikedCharacter);
            }

            var likedCharacters = characters.Select(character => new LikeCharacterDto
            {
                CharacterId = character.CharacterId,
                CharacterName = character.CharacterName,
                RealName = character.RealName,
                Role = character.Role,
                Gender = character.Gender,
                PlaceOfOrigin = character.PlaceOfOrigin,
                PlayedBy = character.PlayedBy,
                PhotoUrl = character.Photos.FirstOrDefault(p => p.IsMain).Url
            });

            return await PagedList<LikeCharacterDto>.CreateAsync(likedCharacters, likesParams.PageNumber, likesParams.PageSize);
        }
    }
}