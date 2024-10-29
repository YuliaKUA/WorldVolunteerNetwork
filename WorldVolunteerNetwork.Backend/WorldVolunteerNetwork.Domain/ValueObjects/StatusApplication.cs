using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldVolunteerNetwork.Domain.Common;

namespace WorldVolunteerNetwork.Domain.ValueObjects
{
    public class StatusApplication : Common.ValueObject
    {
        public static readonly StatusApplication Approved = new(nameof(Approved));
        public static readonly StatusApplication Denied = new (nameof(Denied));
        public static readonly StatusApplication Reviewed = new (nameof(Reviewed));
               
        private static readonly StatusApplication[] _all = [Approved, Denied, Reviewed];
        public string Value { get; }

        public StatusApplication() { }
        private StatusApplication(string value)
        {
            Value = value;
        }
        public static Result<StatusApplication, Error> Create(string input)
        {
            if (input.IsEmpty())
            {
                return Errors.General.ValueIsRequired("application status");
            }

            var status = input.Trim().ToUpper();

            if (_all.Any(p => p.Value.ToUpper() == status) == false)
            {
                return Errors.General.ValueIsInvalid("application status");
            }

            return new StatusApplication(status);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
