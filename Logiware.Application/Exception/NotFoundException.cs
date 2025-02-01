using System.Runtime.Serialization;

namespace Logiware.Application.Exception;

public class NotFoundException :  System.Exception
{
    public NotFoundException()
    {
    }

    protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public NotFoundException(string? message) : base(message)
    {
    }
}