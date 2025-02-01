namespace Logiware.Application.Exception;
public class ApiException
{
   

    // Standard structure fields to match validation error format
    public string Type { get; set; }
    public string Title { get; set; }
    public int Status { get; set; }
    public Dictionary<string, string[]> Errors { get; set; }
    public string TraceId { get; set; }

    // Optional: Include details for development environment
    public string Details { get; set; }

     public ApiException(int statusCode, string message, string details, string traceId)
    {
        Status = statusCode;
        Title = message;
        Details = details;
        TraceId = traceId;
        Errors = new Dictionary<string, string[]>();
        Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1";
    }

    // Method to add errors
    public void AddError(string key, string[] messages)
    {
        Errors[key] = messages;
    }
}
