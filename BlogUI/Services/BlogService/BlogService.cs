using BlogUI.Dtos;
using BlogUI.Models;
using BlogUI.Repository.BlogRepo;
using System.Reflection.Metadata;

namespace BlogUI.Services.BlogService
{
    public class BlogService
    {
        private readonly BlogRepo _repo;

        public BlogService(BlogRepo repo)
        {
            _repo = repo;
        }

        public async Task<bool> Save(AddBlogDto request)
        {
            if (request is not null)
            {
                var blog = new Blog()
                {
                    Title = request.Title,
                    Content = request.Content,
                    UserId = Guid.NewGuid(),
                    Tags = request.Tags.ToArray()
                };
                await _repo.Save(blog);
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<List<Blog>> Search(string text)
        {
            return await _repo.Search(text);
        }
    }
}
