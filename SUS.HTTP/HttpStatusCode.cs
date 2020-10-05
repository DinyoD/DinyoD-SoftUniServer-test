namespace SUS.HTTP
{
    public enum HttpStatusCode
    {
        Ok = 200,
        MovedPermanently = 301,
        Found = 302,
        TemporeryRedirect = 307,
        NotFound = 404,
        ServerError = 500,
    }
}