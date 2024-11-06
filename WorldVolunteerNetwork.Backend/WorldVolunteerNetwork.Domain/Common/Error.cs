using WorldVolunteerNetwork.Domain.Common;

namespace WorldVolunteerNetwork.Domain.Common
{
    public class Error
    {
        public const string Separator = "||";
        public Error(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public string Code { get; }
        public string Message { get; }

        public string Serialize()
        {
            return $"{Code}{Separator}{Message}";
        }

        public static Error? Deserialize(string serialized)
        {
            var data = serialized.Split([Separator], StringSplitOptions.RemoveEmptyEntries);

            if (data.Length < 2)
                throw new($"Invalid error serialization: '{serialized}'");

            return new(data[0], data[1]);
        }
    }
}

public static class Errors
{
    public static class General
    {
        public static Error Iternal(string message)
            => new("iternal", message);
        public static Error Unexpected()
            => new("unexpecret", "unexpecret");
        public static Error NotFound(Guid? id = null)
        {
            var forId = id == null ? "" : $" for Id {id}";
            return new("record.not.found", $"Record not found{forId}");
        }
        public static Error NotFound(string? str = null)
        {
            var forEmail = str == null ? "" : $" for Email {str}";
            return new("record.not.found", $"Record not found{forEmail}");
        }
        public static Error ValueIsRequired(string? name = null)
        {
            var label = name ?? "Value";
            return new("value.is.required", $"{label} is required");
        }

        public static Error ValueIsInvalid(string? name = null)
        {
            var label = name ?? "Value";
            return new("value.is.invalid", $"{label} is invalid");
        }
        public static Error InvalidLength(string? name = null)
        {
            var label = name == null ? " " : $" {name} ";
            return new("invalid.string.length", $"Invalid{label}length");
        }

        public static Error SaveFailure(string? name = null)
        {
            var label = name ?? "Value";
            return new("record.save.failure", $"{label} failed to save");
        }
        public static Error GetFailure(string? name = null)
        {
            var label = name ?? "Value";
            return new("record.get.failure", $"{label} failed to get");
        }

        public static Error ItsNotNumber(string? name = null)
        {
            var label = name ?? "Value";
            return new("its.not.number", $"{label} is not number");
        }
    }

    public static class Organizers
    {
        public static Error PhotoCountLimit(int limit)
        {
            return new("organizer.photo.limit", $"Max photo count limit is {limit}");
        }

        public static Error PhotoNotFound()
        {
            return new("organizer.photo.not.found", "Photo for removing not found");
        }
        public static Error NotFound(Guid? id = null)
        {
            var forId = id == null ? "" : $" for Id {id}";
            return new("record.not.found", $"Record not found{forId}");
        }
        public static Error ValueIsRequired(string? name = null)
        {
            var label = name ?? "Value";
            return new("value.is.required", $"{label} is required");
        }

        public static Error SaveFailure(string? name = null)
        {
            var label = name ?? "Value";
            return new("record.save.failure", $"{label} failed to save");
        }
    }

    public static class Users
    {
        public static Error InvalidData()
        {
            return new("users.invalid.data", "User's data is invalid");
        }
    }

    public static class VolunteersApplications
    {
        public static Error AlredyApproved()
        {
            return new("volunteers.applications", "Volunteer application has been already approved");
        }
    }
}