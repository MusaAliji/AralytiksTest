using System;
using AralytiksTest2.Contracts;
using AralytiksTest2.DTO;
using AralytiksTest2.Models;
using AralytiksTest2.Services.Common;

namespace AralytiksTest2.Services
{
    public class PostService : IPostService
    {
        private readonly ILogger<PostService> _logger;
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PostService(ILogger<PostService> logger, IPostRepository postRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _postRepository = postRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<(IEnumerable<Post>? posts, int totalCount)> GetAllPostsAsync(int page, int pageSize)
        {
            try
            {
                (IEnumerable<Post>? Entities, int TotalCount) allPosts = await _postRepository.GetAllWithPagingationAsync(page, pageSize);
                return (allPosts.Entities, allPosts.TotalCount);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Something went wrong!");
                throw;
            }
        }

        public async Task<Post?> GetPostByIdAsync(int id)
        {
            try
            {
                Post? post = await _postRepository.GetWithUserAsync(id);
                return post;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Something went wrong!");
                throw;
            }
        }

        public async Task<Post?> CreatePostAsync(PostDto post)
        {
            try
            {
                User? user = await _userRepository.GetAsync(post.UserId);
                if (user == null)
                    return null;

                Post newPost = new Post()
                {
                    Title = post.Title,
                    Description = post.Description,
                    Content = post.Content,
                    Slug = SlugGenerator.GenerateSlug(post.Title),
                    UserId = post.UserId,
                    CreatedAt = DateTime.UtcNow
                };

                Post createdPost = await _postRepository.AddAsync(newPost);
                await _unitOfWork.SaveChangesAsync();

                return createdPost;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Something went wrong!");
                throw;
            }
        }

        public async Task<Post?> UpdatePostAsync(int id, PostDto post)
        {
            try
            {
                User? user = await _userRepository.GetAsync(post.UserId);
                if (user == null)
                    return null;

                Post? findPost = await _postRepository.GetAsync(id);
                if (findPost == null)
                    return null;

                findPost.Title = post.Title;
                findPost.Description = post.Description;
                findPost.Content = post.Content;
                findPost.Slug = SlugGenerator.GenerateSlug(post.Title);
                findPost.UserId = post.UserId;
                findPost.UpdatedAt = DateTime.UtcNow;

                _postRepository.Update(findPost);
                await _unitOfWork.SaveChangesAsync();

                return findPost;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Something went wrong!");
                throw;
            }
        }

        public async Task<bool> DeletePostAsync(int id)
        {
            try
            {
                Post? findPost = await _postRepository.GetAsync(id);
                if (findPost == null)
                    return false;

                _postRepository.Remove(findPost);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Something went wrong!");
                throw;
            }
        }
    }
}
