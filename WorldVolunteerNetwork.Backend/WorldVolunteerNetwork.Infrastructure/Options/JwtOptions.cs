namespace WorldVolunteerNetwork.Infrastructure.Options
{
    public class JwtOptions
    {
        public const string Jwt = nameof(Jwt);
        public string SecretKey { get; init; } = string.Empty;
        public double Expires { get; init; }
    }
}
