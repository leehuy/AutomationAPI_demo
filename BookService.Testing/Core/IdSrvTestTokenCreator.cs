//using IdentityModel.Client;

namespace BookService.Testing.Core
{
    public static class IdSrvTestTokenCreator
    {

        //public static TokenResponse GetToken()
        //{
        //    return GetToken(
        //        Properties.Settings.Default.IdentityServerUsername,
        //        Properties.Settings.Default.IdentityServerPassword,
        //        Properties.Settings.Default.IdentityServerScopes);
        //}

        //public static TokenResponse GetToken(string username, string password, string scopes)
        //{
        //    var tokenClient = GetTokenClient(
        //        Properties.Settings.Default.IdentityServerUri,
        //        Properties.Settings.Default.IdentityServerClientId,
        //        Properties.Settings.Default.IdentityServerSecret);

        //    var tokenResponse = tokenClient.RequestResourceOwnerPasswordAsync(username, password, scopes).Result;

        //    return tokenResponse;
        //}

        //private static TokenClient GetTokenClient(string identityUrl, string clientId, string clientPassword)
        //{
        //    return new TokenClient(identityUrl + "connect/token", clientId, clientPassword);
        //}
    }
}
