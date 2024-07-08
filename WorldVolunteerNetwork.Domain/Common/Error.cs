using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public string Code { get; set; }
        public string Message { get; set; }
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
        public static Error ValueIsRequired()
        {
            return new("value.is.required", "Value is required");
        }

        public static Error ValueIsInvalid(string name)
        {
            return new("value.is.invalid", $"{name} is invalid");
        }
        public static Error InvalidLength(string? name = null)
        {
            var label = name == null ? " " : $" {name} ";
            return new("invalid.string.length", $"Invalid{label}length");
        }
    }
}