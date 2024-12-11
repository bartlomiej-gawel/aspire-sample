using ErrorOr;

namespace Sample.Services.Users.Features.VerificationTokens;

public abstract class VerificationTokenLinkFactory
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly LinkGenerator _linkGenerator;

    protected VerificationTokenLinkFactory(
        IHttpContextAccessor httpContextAccessor,
        LinkGenerator linkGenerator)
    {
        _httpContextAccessor = httpContextAccessor;
        _linkGenerator = linkGenerator;
    }
    
    public ErrorOr<string> CreateLink(VerificationToken verificationToken)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext is null)
            return VerificationTokenErrors.HttpContextNotAvailable;
        
        var verificationLink = _linkGenerator.GetUriByRouteValues(
            _httpContextAccessor.HttpContext!,
            routeName: null,
            values: new { VerificationToken = verificationToken.Id });

        if (verificationLink is null)
            return VerificationTokenErrors.FailedToGenerate;
        
        return verificationLink;
    }
}