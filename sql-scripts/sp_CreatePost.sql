  
-- Create procedure  
CREATE PROCEDURE dbo.sp_CreatePost  
    @AuthorId BIGINT,  
    @Title NVARCHAR(75),  
    @Slug NVARCHAR(100),  
    @Published BIT = 0,  
    @CreatedAt DATETIME = NULL,  
    @UpdatedAt DATETIME = NULL,  
    @PublishedAt DATETIME = NULL,  
    @Content NVARCHAR(MAX) = NULL  
AS  
BEGIN  
    SET NOCOUNT ON;  
  
    -- Default handling (hint: avoids forcing caller to pass values)  
    SET @CreatedAt = ISNULL(@CreatedAt, GETDATE());  
  
    INSERT INTO dbo.post  
    (  
        authorId,  
        title,  
        slug,  
        published,  
        createdAt,  
        updatedAt,  
        publishedAt,  
        content  
    )  
    VALUES  
    (  
        @AuthorId,  
        @Title,  
        @Slug,  
        @Published,  
        @CreatedAt,  
        @UpdatedAt,  
        @PublishedAt,  
        @Content  
    );  
  
    -- Return inserted ID (best practice)  
    SELECT SCOPE_IDENTITY() AS NewPostId;  
END  