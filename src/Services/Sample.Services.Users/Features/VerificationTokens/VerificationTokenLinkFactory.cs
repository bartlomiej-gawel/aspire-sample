namespace Sample.Services.Users.Features.VerificationTokens;

public sealed class VerificationTokenLinkFactory
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly LinkGenerator _linkGenerator;

    public VerificationTokenLinkFactory(
        IHttpContextAccessor httpContextAccessor,
        LinkGenerator linkGenerator)
    {
        _httpContextAccessor = httpContextAccessor;
        _linkGenerator = linkGenerator;
    }
    
    public string CreateLink(VerificationToken verificationToken)
    {
        
        
        return "";
    }
}