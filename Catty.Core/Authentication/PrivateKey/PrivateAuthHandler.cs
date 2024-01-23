
namespace Catty.Core.Authentication.PrivateKey
{
    public class PrivateAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly PrivateAuthConfig _privateAuthConfig;
        public PrivateAuthHandler(IOptions<PrivateAuthConfig> privateAuthConfig, IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory loggerFactory, UrlEncoder encoder, ISystemClock clock) : base(options, loggerFactory, encoder, clock) => _privateAuthConfig = privateAuthConfig.Value;
        protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            try
            {
                await Task.Delay(0);

                if (!Request.Headers.ContainsKey("Authorization"))
                    return AuthenticateResult.Fail("Authorization header not provided");

                bool succeeded;

                var auth = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);

                if (!CoreConstants.AuthenticationScheme.PrivateKey.Equals(auth.Scheme, StringComparison.OrdinalIgnoreCase))
                    return AuthenticateResult.NoResult();

                succeeded = _privateAuthConfig!.Value!.Equals(auth.Parameter);

                if (!succeeded)
                    return AuthenticateResult.Fail("Invalid auth parameter provided");

                // All good

                Claim[] claims = new Claim[2]
                {
                    new(ClaimTypes.NameIdentifier,"SchemeIdentifierUri"),
                    new(ClaimTypes.Name,"SchemeName")
                };

                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var passedTicket = new AuthenticationTicket(principal, Scheme.Name);

                return AuthenticateResult.Success(passedTicket);
            }
            catch (Exception)
            {

                return AuthenticateResult.Fail("Invalid Auth Parameter");
            }
        }
    }
}
