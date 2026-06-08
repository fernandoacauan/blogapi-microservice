using System;

namespace Blog.Identity.Application.Exceptions;

public sealed class ForbiddenException : Exception
{
    public ForbiddenException(string message) : base(message)
    {
    }
}
