namespace Pactify
{
    public interface IHttpPactRequestBuilder
    {
        IHttpPactRequestBuilder WithMethod(string method);
        IHttpPactRequestBuilder WithPath(string path);
    }
}
