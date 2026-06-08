using Blog.Solution.Domain.Entities.Author;
using Blog.Solution.Domain.Entities.Common;
using Blog.Solution.Domain.Entities.Post;

namespace Blog.Solution.Domain.Entities.Comment;

public sealed class CommentEntity : BaseEntity
{
    public string Body { get; private set; } = string.Empty;
    public Guid PostId { get; private set; }
    public Guid AuthorId { get; private set; }
    public Guid? ParentCommentId { get; private set; }
    public PostEntity Post { get; private set; } = null!;
    public CommentEntity ParentComment { get; private set; } = null!;
    public AuthorEntity Author { get; private set; } = null!;
    private readonly List<AuthorEntity> _likes = new List<AuthorEntity>();

    public IReadOnlyList<AuthorEntity> Likes => _likes.AsReadOnly();

    public void AddLike(AuthorEntity author)
    {
        if (_likes.Any(a => a.Id == author.Id))
        {
            return;
        }
        _likes.Add(author);
    }

    private CommentEntity()
    {
        
    }

    public CommentEntity(string body, Guid postId, Guid authorId, Guid? parentCommentId)
    {
        Body = body;
        PostId = postId;
        AuthorId = authorId;
        ParentCommentId = parentCommentId;
    }
}
