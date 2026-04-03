using System;
using System.Collections.Generic;
using System.Data;
using NbtBlogEngine.Models;
using NbtBlogEngine.Repositories;

namespace NbtBlogEngine.Tests.Mocks
{
    public class MockPostRepository : IPostRepository
    {
        public List<PostDTO> Posts { get; set; } = new List<PostDTO>();
        public long LastCreatedId { get; set; } = 100;
        public bool DeleteCalled { get; set; }
        public string LastSlugPassed { get; set; }

        public DataTable GetByAuthor(int authorId)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("id", typeof(long));
            dataTable.Columns.Add("title", typeof(string));
            foreach (var post in Posts)
            {
                if (post.AuthorId == authorId)
                {
                    dataTable.Rows.Add(post.Id, post.Title);
                }
            }

            return dataTable;
        }

        public long Create(int authorId, string title, string slug, string content, bool published)
        {
            LastSlugPassed = slug;
            LastCreatedId++;
            Posts.Add(new PostDTO
            {
                Id = LastCreatedId,
                AuthorId = authorId,
                Title = title,
                Slug = slug,
                Content = content,
                Published = published,
                CreatedAt = DateTime.Now
            });

            return LastCreatedId;
        }

        public bool Update(long postId, int authorId, string title, string slug, string content, bool published)
        {
            LastSlugPassed = slug;
            var post = Posts.Find(p => p.Id == postId);
            if (post == null)
            {
                return false;
            }

            post.Title = title;
            post.Slug = slug;
            post.Content = content;
            post.Published = published;
            return true;
        }

        public PostDTO FindById(long postId)
        {
            return Posts.Find(p => p.Id == postId);
        }

        public PostDTO FindBySlug(string slug)
        {
            return Posts.Find(p => p.Slug == slug);
        }

        public bool Delete(long postId)
        {
            DeleteCalled = true;
            return Posts.RemoveAll(p => p.Id == postId) > 0;
        }

        public DataTable GetPublished()
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("id", typeof(long));
            dataTable.Columns.Add("title", typeof(string));
            foreach (var post in Posts)
            {
                if (post.Published)
                {
                    dataTable.Rows.Add(post.Id, post.Title);
                }
            }

            return dataTable;
        }

        public DataTable GetPublishedByTag(string tagTitle)
        {
            return GetPublished();
        }

        public DataTable Search(string searchTerm)
        {
            return GetPublished();
        }
    }
}
