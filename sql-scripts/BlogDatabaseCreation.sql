-- Create Database
IF DB_ID('blog') IS NULL
BEGIN
    CREATE DATABASE blog;
END
GO

USE blog;
GO

-- ======================
-- USER
-- ======================
IF OBJECT_ID('[user]', 'U') IS NOT NULL DROP TABLE [user];
GO

CREATE TABLE [user] (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,
    firstName NVARCHAR(50),
    lastName NVARCHAR(50),
    email NVARCHAR(50),
    passwordHash NVARCHAR(32) NOT NULL,
    registeredAt DATETIME NOT NULL,
    lastLogin DATETIME,
    CONSTRAINT uq_email UNIQUE (email)
);
GO

-- ======================
-- POST
-- ======================
IF OBJECT_ID('post', 'U') IS NOT NULL DROP TABLE post;
GO

CREATE TABLE post (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,
    authorId BIGINT NOT NULL,
    title NVARCHAR(75) NOT NULL,
    slug NVARCHAR(100) NOT NULL,
    published BIT NOT NULL DEFAULT 0,
    createdAt DATETIME NOT NULL,
    updatedAt DATETIME,
    publishedAt DATETIME,
    content NVARCHAR(MAX)
);

CREATE INDEX idx_post_user ON post(authorId);

ALTER TABLE post
ADD CONSTRAINT fk_post_user FOREIGN KEY (authorId) REFERENCES [user](id);
GO

-- ======================
-- TAG
-- ======================
IF OBJECT_ID('tag', 'U') IS NOT NULL DROP TABLE tag;
GO

CREATE TABLE tag (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,
    title NVARCHAR(75) NOT NULL
);
GO

-- ======================
-- POST_TAG
-- ======================
IF OBJECT_ID('post_tag', 'U') IS NOT NULL DROP TABLE post_tag;
GO

CREATE TABLE post_tag (
    postId BIGINT NOT NULL,
    tagId BIGINT NOT NULL,
    CONSTRAINT PK_post_tag PRIMARY KEY (postId, tagId)
);

CREATE INDEX idx_pt_tag ON post_tag(tagId);
CREATE INDEX idx_pt_post ON post_tag(postId);

ALTER TABLE post_tag
ADD CONSTRAINT fk_pt_post FOREIGN KEY (postId) REFERENCES post(id);

ALTER TABLE post_tag
ADD CONSTRAINT fk_pt_tag FOREIGN KEY (tagId) REFERENCES tag(id);
GO

-- ======================
-- POST_COMMENT
-- ======================
IF OBJECT_ID('post_comment', 'U') IS NOT NULL DROP TABLE post_comment;
GO

CREATE TABLE post_comment (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,
    postId BIGINT NOT NULL,
    authorId BIGINT NOT NULL,
    createdAt DATETIME NOT NULL,
    content NVARCHAR(MAX)
);

CREATE INDEX idx_comment_post ON post_comment(postId);

ALTER TABLE post_comment
ADD CONSTRAINT fk_comment_post FOREIGN KEY (postId) REFERENCES post(id);

ALTER TABLE post_comment
ADD CONSTRAINT fk_comment_user FOREIGN KEY (authorId) REFERENCES [user](id);
GO

