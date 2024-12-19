using ErrorOr;

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

    public ErrorOr<string> CreateLink(ActivationToken activationToken)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext is null)
            return ActivationTokenErrors.HttpContextNotAvailable;

        var activationLink = _linkGenerator.GetUriByName(
            httpContext,
            "ActivateUser",
            values: new { ActivationToken = activationToken.Id });

        if (activationLink is null)
            return ActivationTokenErrors.FailedToGenerate;

        return activationLink;
    }
}