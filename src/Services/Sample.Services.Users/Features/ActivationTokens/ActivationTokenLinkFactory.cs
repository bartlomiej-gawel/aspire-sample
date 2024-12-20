namespace Sample.Services.Users.Features.ActivationTokens;

public sealed class ActivationTokenLinkFactory
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly LinkGenerator _linkGenerator;

    public ActivationTokenLinkFactory(
        IHttpContextAccessor httpContextAccessor,
        LinkGenerator linkGenerator)
    {
        _httpContextAccessor = httpContextAccessor;
        _linkGenerator = linkGenerator;
    }

    public string CreateLink(ActivationToken activationToken)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext is null)
            throw new InvalidOperationException("HttpContext is not available.");
        
        var activationLink = _linkGenerator.GetUriByName(
            httpContext,
            "activate-user",
            values: new { ActivationToken = activationToken.Id });

        if (activationLink is null)
            throw new InvalidOperationException("Failed to generate activation link.");

        return activationLink;
    }
}