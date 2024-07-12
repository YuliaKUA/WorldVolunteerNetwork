﻿using CSharpFunctionalExtensions;
using WorldVolunteerNetwork.Domain.Common;

namespace WorldVolunteerNetwork.Domain.ValueObjects
{
    public record Social
    {
        public static readonly Social Telegram = new(nameof(Telegram));
        public static readonly Social Instargam = new(nameof(Instargam));
        public static readonly Social WhatsApp = new(nameof(WhatsApp));
        public static readonly Social VK = new(nameof(VK));
        public static readonly Social FaceBook = new(nameof(FaceBook));

        private static readonly Social[] _all = [Telegram, Instargam, WhatsApp, VK, FaceBook];
        public string Value { get; }

        public Social() { }
        private Social(string value)
        {
            Value = value;
        }
        public static Result<Social, Error> Create(string input)
        {
            if (input.IsEmpty())
            {
                return Errors.General.ValueIsRequired("social");
            }

            var social = input.Trim().ToUpper();

            if (_all.Any(p => p.Value.ToUpper() == social) == false)
            {
                return Errors.General.ValueIsInvalid("social");
            }

            return new Social(social);
        }
    }
}
