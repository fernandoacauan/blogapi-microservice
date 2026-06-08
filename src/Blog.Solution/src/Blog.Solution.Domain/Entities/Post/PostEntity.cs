using Blog.Solution.Domain.Entities.Author;
using Blog.Solution.Domain.Entities.Comment;
using Blog.Solution.Domain.Entities.Common;

namespace Blog.Solution.Domain.Entities.Post;

public sealed class PostEntity : BaseEntity
{
    public string Title { get; private set; } = string.Empty;
    public Guid AuthorId { get; private set; }
    public string Body { get; private set; } = string.Empty;
    public AuthorEntity Author { get; private set; } = null!;
    private readonly List<AuthorEntity> _likes = new();
    private readonly List<CommentEntity> _comments = new();

    public IReadOnlyList<AuthorEntity> Likes => _likes.AsReadOnly();
    public IReadOnlyList<CommentEntity> Comments => _comments.AsReadOnly();

    public void AddLike(AuthorEntity author)
    {
        if (Likes.Any(a => a.Id == author.Id))
        {
            return;
        }
        _likes.Add(author);
    }

    public void AddComment(CommentEntity comment) => _comments.Add(comment);

    private PostEntity()
    {
        
    }

    public PostEntity(string title, Guid authorId, string body)
    {
        Title = title;
        AuthorId = authorId;
        Body = body;
    }
}
