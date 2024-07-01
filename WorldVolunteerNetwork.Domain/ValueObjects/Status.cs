namespace WorldVolunteerNetwork.Domain.ValueObjects
{
    public record Status
    {
        public static readonly Status Active = new(nameof(Active));
        public static readonly Status Done = new(nameof(Done));

        private static readonly Status[] _all = [Active, Done];
        public string Value { get; }

        public Status() { }
        private Status(string value)
        {
            Value = value;
        }
        public static Status Create(string input)
        {
            if (input.IsEmpty())
            {
                throw new ArgumentException(nameof(input));
            }
            var status = input.Trim().ToUpper();
            if (_all.Any(p => p.Value.ToUpper() == input) == false)
            {
                throw new ArgumentException();
            }

            return new(status);
        }
    }
}
