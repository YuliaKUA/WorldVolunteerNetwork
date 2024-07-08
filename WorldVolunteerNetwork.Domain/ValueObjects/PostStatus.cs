using CSharpFunctionalExtensions;
//using static System.Runtime.InteropServices.JavaScript.JSType;
using WorldVolunteerNetwork.Domain.Common;

namespace WorldVolunteerNetwork.Domain.ValueObjects
{
    public record PostStatus
    {
        public static readonly PostStatus Active = new(nameof(Active));
        public static readonly PostStatus Done = new(nameof(Done));

        private static readonly PostStatus[] _all = [Active, Done];
        public string Value { get; }

        public PostStatus() { }
        private PostStatus(string value)
        {
            Value = value;
        }
        public static Result<PostStatus, Error> Create(string input)
        {
            if (input.IsEmpty())
            {
                return Errors.General.ValueIsRequired();
            }
            var status = input.Trim().ToUpper();
            if (_all.Any(p => p.Value.ToUpper() == status) == false)
            {
                return Errors.General.ValueIsInvalid("post status");
            }

            return new PostStatus(status);
        }
    }
}
