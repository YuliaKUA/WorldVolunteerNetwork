using WorldVolunteerNetwork.Domain.Common;

namespace WorldVolunteerNetwork.API.Validation;
//For vflidation: wrapper so that the response from the api is always in the same style
public class Envelope
{
    private Envelope(object? result, Error? error)
    {
        Result = result;
        ErrorCode = error?.Code;
        ErrorMessage = error?.Message;
        TimeGenerated = DateTime.Now;
    }

    public object? Result { get; }
    public string? ErrorCode { get; }
    public string? ErrorMessage { get; }
    public DateTime TimeGenerated { get; }

    public static Envelope Ok(object? result = null)
    {
        return new(result, null);
    }
    public static Envelope Error(Error? error)
    {
        return new(null, error);
    }

}
