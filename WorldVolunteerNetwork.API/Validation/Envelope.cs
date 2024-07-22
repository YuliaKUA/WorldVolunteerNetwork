using WorldVolunteerNetwork.Domain.Common;

namespace WorldVolunteerNetwork.API.Validation;
//For vflidation: wrapper so that the response from the api is always in the same style
public class Envelope
{
    public object? Result { get; }
    public List<ErrorInfo>? errorInfo { get; }
    private Envelope(object? result, List<ErrorInfo>? errors)
    {
        Result = result;
        errorInfo = errors;
        TimeGenerated = DateTime.Now;
    }


    public DateTime TimeGenerated { get; }

    public static Envelope Ok(object? result = null)
    {
        return new(result, null);
    }
    public static Envelope Error(List<ErrorInfo>? errors)
    {
        return new(null, errors);
    }

    //public static Envelope Error(List<Error>? errors)
    //{
    //    var errorInfo = errors?.Select(e => new ErrorInfo(e));
    //    return new(null, errorInfo);
    //}

    public class ErrorInfo
    {
        public string? ErrorCode { get; }
        public string? ErrorMessage { get; }
        public string? InvalidField { get; }

        public ErrorInfo(Error? error, string? invalidField = null)
        {
            ErrorCode = error?.Code;
            ErrorMessage = error?.Message;
            InvalidField = invalidField;
        }
    }
}

