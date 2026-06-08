using Blog.Solution.Domain.Entities.Comment;
using Blog.Solution.Domain.Entities.Common;
using Blog.Solution.Domain.Entities.Post;

namespace Blog.Solution.Domain.Entities.Author;

public sealed class AuthorEntity : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Surname { get; private set;} = string.Empty;
    public string Email { get; private set; } = string.Empty;
    private readonly List<PostEntity> _posts = new List<PostEntity>();
    private readonly List<CommentEntity> _comments = new List<CommentEntity>();

    public IReadOnlyList<PostEntity> Posts => _posts.AsReadOnly();
    public IReadOnlyList<CommentEntity> Comments => _comments.AsReadOnly();

    public void AddPost(PostEntity post) => _posts.Add(post);
    public void AddComment(CommentEntity comment) => _comments.Add(comment);
    private AuthorEntity()
    {
        
    }

    public AuthorEntity(Guid id, string name, string surname, string email)
    {
        Id = id;
        Name = name;
        Surname = surname;
        Email = email;
    }

}
