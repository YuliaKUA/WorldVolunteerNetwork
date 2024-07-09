using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WorldVolunteerNetwork.Domain.Common;

namespace WorldVolunteerNetwork.Domain.Common
{
    public class Error
    {
        public Error(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public string Code { get; }
        public string Message { get; }

        public string Serialize()
        {
            return JsonSerializer.Serialize(this);
        }

        public static Error? Deserialize(string serialized)
        {
            return JsonSerializer.Deserialize<Error>(serialized);
        }
    }
}

public static class Errors
{
    public static class General
    {
        public static Error NotFound(Guid? id = null)
        {
            var forId = id == null ? "" : $" for Id {id}";
            return new("record.not.found", $"Record not found{forId}");
        }
        public static Error ValueIsRequired(string? name)
        {
            var label = name ?? "Value";
            return new("value.is.required", $"{label} is required");
        }

        public static Error ValueIsInvalid(string? name)
        {
            var label = name ?? "Value";
            return new("value.is.invalid", $"{label} is invalid");
        }
        public static Error InvalidLength(string? name = null)
        {
            var label = name == null ? " " : $" {name} ";
            return new("invalid.string.length", $"Invalid{label}length");
        }
    }
}