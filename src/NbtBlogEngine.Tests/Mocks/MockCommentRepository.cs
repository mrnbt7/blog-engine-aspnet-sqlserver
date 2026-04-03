using System;
using System.Collections.Generic;
using System.Linq;
using NbtBlogEngine.Models;
using NbtBlogEngine.Repositories;

namespace NbtBlogEngine.Tests.Mocks
{
    public class MockCommentRepository : ICommentRepository
    {
        public List<CommentDTO> Comments { get; set; } = new List<CommentDTO>();
        public long NextId { get; set; } = 100;

        public List<CommentDTO> GetByPostId(long postId)
        {
            return Comments.Where(c => c.PostId == postId).OrderBy(c => c.CreatedAt).ToList();
        }

        public long Create(long postId, long authorId, string content)
        {
            NextId++;
            Comments.Add(new CommentDTO
            {
                Id = NextId,
                PostId = postId,
                AuthorId = authorId,
                AuthorName = "User " + authorId,
                Content = content,
                CreatedAt = DateTime.Now
            });

            return NextId;
        }

        public int GetCountByPostId(long postId)
        {
            return Comments.Count(c => c.PostId == postId);
        }
    }
}
