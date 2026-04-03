USE blog;
GO

-- ======================
-- USERS
-- ======================
INSERT INTO [user] (firstName, lastName, email, passwordHash, registeredAt, lastLogin)
VALUES
(N'Ravi',   N'Kumar',   N'ravi@example.com',   N'a1b2c3d4e5f6g7h8i9j0k1l2m3n4o5p6', '2024-01-10 08:30:00', '2025-06-01 09:00:00'),
(N'Priya',  N'Sharma',  N'priya@example.com',  N'b2c3d4e5f6g7h8i9j0k1l2m3n4o5p6q7', '2024-02-15 10:00:00', '2025-05-28 14:20:00'),
(N'Arjun',  N'Reddy',   N'arjun@example.com',  N'c3d4e5f6g7h8i9j0k1l2m3n4o5p6q7r8', '2024-03-20 12:45:00', '2025-06-02 11:10:00');

-- ======================
-- TAGS
-- ======================
INSERT INTO tag (title) VALUES
(N'C#'),
(N'ASP.NET'),
(N'SQL Server'),
(N'JavaScript'),
(N'Cloud'),
(N'AWS'),
(N'DevOps'),
(N'REST API'),
(N'Performance'),
(N'Security');

-- ======================
-- POSTS (10)
-- ======================
INSERT INTO post (authorId, title, slug, published, createdAt, updatedAt, publishedAt, content)
VALUES
(1, N'Getting Started with C# 12',           N'getting-started-with-c-12',           1, '2024-06-01 09:00:00', '2024-06-02 10:00:00', '2024-06-02 10:00:00',
 N'C# 12 introduces primary constructors for classes, collection expressions, and more. This post walks through the key features with practical examples.'),

(1, N'Building REST APIs with ASP.NET Core', N'building-rest-apis-with-aspnet-core', 1, '2024-06-10 11:00:00', '2024-06-11 08:30:00', '2024-06-11 08:30:00',
 N'Learn how to build clean, minimal REST APIs using ASP.NET Core. We cover routing, dependency injection, and middleware configuration.'),

(2, N'SQL Server Query Optimization Tips',   N'sql-server-query-optimization-tips',   1, '2024-07-05 14:00:00', '2024-07-06 09:00:00', '2024-07-06 09:00:00',
 N'Slow queries can cripple your application. This post covers indexing strategies, execution plans, and common anti-patterns to avoid.'),

(2, N'Introduction to AWS Lambda',           N'introduction-to-aws-lambda',           1, '2024-07-20 10:30:00', NULL,                  '2024-07-20 10:30:00',
 N'AWS Lambda lets you run code without provisioning servers. We explore triggers, cold starts, and best practices for .NET on Lambda.'),

(3, N'JavaScript Async Patterns Explained',  N'javascript-async-patterns-explained',  1, '2024-08-01 08:00:00', '2024-08-02 12:00:00', '2024-08-02 12:00:00',
 N'Callbacks, promises, and async/await — understanding asynchronous JavaScript is essential. This guide breaks down each pattern with examples.'),

(1, N'Deploying to AWS with CI/CD Pipelines', N'deploying-to-aws-with-cicd-pipelines', 1, '2024-08-15 13:00:00', NULL,                 '2024-08-15 13:00:00',
 N'Automate your deployments using AWS CodePipeline and CodeBuild. This post covers setting up a full CI/CD pipeline for an ASP.NET Core app.'),

(3, N'Securing Your Web Application',        N'securing-your-web-application',        1, '2024-09-01 07:30:00', '2024-09-03 16:00:00', '2024-09-03 16:00:00',
 N'From HTTPS to CORS to input validation — security is not optional. Learn the essential practices to protect your web application from common attacks.'),

(2, N'Understanding SQL Server Indexes',     N'understanding-sql-server-indexes',     1, '2024-09-20 09:00:00', NULL,                  '2024-09-20 09:00:00',
 N'Clustered vs non-clustered, covering indexes, and filtered indexes. A deep dive into how SQL Server indexes work under the hood.'),

(3, N'Cloud Architecture Patterns',          N'cloud-architecture-patterns',          0, '2024-10-05 11:00:00', NULL,                  NULL,
 N'Explore common cloud architecture patterns like microservices, event-driven design, and CQRS. Draft post — still adding diagrams.'),

(1, N'Performance Tuning ASP.NET Core Apps', N'performance-tuning-aspnet-core-apps', 0, '2024-10-15 15:00:00', NULL,                  NULL,
 N'Response caching, output caching, and async everywhere. A collection of tips to squeeze maximum performance out of your ASP.NET Core application.');

-- ======================
-- POST_TAG
-- ======================
-- Post 1: Getting Started with C# 12
INSERT INTO post_tag (postId, tagId) VALUES (1, 1);  -- C#

-- Post 2: Building REST APIs with ASP.NET Core
INSERT INTO post_tag (postId, tagId) VALUES (2, 1);  -- C#
INSERT INTO post_tag (postId, tagId) VALUES (2, 2);  -- ASP.NET
INSERT INTO post_tag (postId, tagId) VALUES (2, 8);  -- REST API

-- Post 3: SQL Server Query Optimization Tips
INSERT INTO post_tag (postId, tagId) VALUES (3, 3);  -- SQL Server
INSERT INTO post_tag (postId, tagId) VALUES (3, 9);  -- Performance

-- Post 4: Introduction to AWS Lambda
INSERT INTO post_tag (postId, tagId) VALUES (4, 5);  -- Cloud
INSERT INTO post_tag (postId, tagId) VALUES (4, 6);  -- AWS

-- Post 5: JavaScript Async Patterns Explained
INSERT INTO post_tag (postId, tagId) VALUES (5, 4);  -- JavaScript

-- Post 6: Deploying to AWS with CI/CD Pipelines
INSERT INTO post_tag (postId, tagId) VALUES (6, 5);  -- Cloud
INSERT INTO post_tag (postId, tagId) VALUES (6, 6);  -- AWS
INSERT INTO post_tag (postId, tagId) VALUES (6, 7);  -- DevOps

-- Post 7: Securing Your Web Application
INSERT INTO post_tag (postId, tagId) VALUES (7, 2);  -- ASP.NET
INSERT INTO post_tag (postId, tagId) VALUES (7, 10); -- Security

-- Post 8: Understanding SQL Server Indexes
INSERT INTO post_tag (postId, tagId) VALUES (8, 3);  -- SQL Server
INSERT INTO post_tag (postId, tagId) VALUES (8, 9);  -- Performance

-- Post 9: Cloud Architecture Patterns
INSERT INTO post_tag (postId, tagId) VALUES (9, 5);  -- Cloud
INSERT INTO post_tag (postId, tagId) VALUES (9, 6);  -- AWS

-- Post 10: Performance Tuning ASP.NET Core Apps
INSERT INTO post_tag (postId, tagId) VALUES (10, 2); -- ASP.NET
INSERT INTO post_tag (postId, tagId) VALUES (10, 9); -- Performance

-- ======================
-- POST_COMMENTS (sample)
-- ======================
INSERT INTO post_comment (postId, authorId, createdAt, content) VALUES
(1, 2, '2024-06-03 08:00:00', N'Great intro to C# 12! The primary constructors feature is a game changer.'),
(2, 3, '2024-06-12 09:30:00', N'This helped me set up my first minimal API. Thanks!'),
(3, 1, '2024-07-07 11:00:00', N'The execution plan tips saved me hours of debugging.'),
(5, 2, '2024-08-03 14:00:00', N'Finally understood the difference between promises and async/await.'),
(7, 1, '2024-09-04 10:15:00', N'Every developer should read this. Security is so often overlooked.');
