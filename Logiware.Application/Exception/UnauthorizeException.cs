using System.Runtime.Serialization;

namespace Logiware.Application.Exception;

public class UnauthorizeException : System.Exception
{
    public UnauthorizeException()
    {
    }

    protected UnauthorizeException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public UnauthorizeException(string? message) : base(message)
    {
    }
}