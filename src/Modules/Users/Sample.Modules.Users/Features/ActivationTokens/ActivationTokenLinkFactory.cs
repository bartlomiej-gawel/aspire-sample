using ErrorOr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Sample.Modules.Users.Features.ActivationTokens;

internal sealed class ActivationTokenLinkFactory
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
            "activate-user",
            values: new { ActivationToken = activationToken.Id });

        if (string.IsNullOrEmpty(activationLink))
            return ActivationTokenErrors.LinkGenerationFailed;

        return activationLink;
    }
}