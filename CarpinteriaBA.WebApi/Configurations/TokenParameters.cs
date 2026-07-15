using CarpinteriaBA.Abstactions;

namespace CarpinteriaBA.WebApi.Configurations
{
    public class TokenParameters : ITokensParameters
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PaswordHash { get; set; }
        public string Id { get; set; }
        public IList<string>? Roles { get; set; }
    }
}
