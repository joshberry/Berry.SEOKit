using Berry.SEOKit.Models;
using Berry.SEOKit.Settings;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using Orchard.Tokens;

namespace Berry.SEOKit.Tokens {
    [OrchardFeature("Berry.SEOKit")]
    public class TitleAndMetaTokens : ITokenProvider {

        private readonly ITokenizer _tokenizer;

        public TitleAndMetaTokens(ITokenizer tokenizer) {
            T = NullLocalizer.Instance;
            _tokenizer = tokenizer;
        }

        public Localizer T { get; set; }

        public void Describe(DescribeContext context) {
            context.For("Content", T("TitleAndMetadata"), T("Title and metadata"))
                .Token("PageTitle", T("Page Title"), T("The page title."))
                .Token("MetaDescription", T("Meta description"), T("The page meta description."));
        }

        public void Evaluate(EvaluateContext context) {
            context.For<IContent>("Content")
                .Token("PageTitle", content => {
                    var part = content.As<TitleAndMetaPart>();
                    var settings = part.TypePartDefinition.Settings.GetModel<TitleAndMetaPartSettings>();
                    string title = null;
                    if (settings.AllowTitle && !string.IsNullOrWhiteSpace(part.Title)) {
                        title = part.Title;
                    } else if (!string.IsNullOrWhiteSpace(settings.DefaultTitle)) {
                        title = _tokenizer.Replace(settings.DefaultTitle, new { Content = part.ContentItem });
                    }
                    return title;
                })
                .Token("MetaDescription", content => {
                    var part = content.As<TitleAndMetaPart>();
                    var settings = part.TypePartDefinition.Settings.GetModel<TitleAndMetaPartSettings>();
                    string description = null;

                    if (settings.AllowDescription && !string.IsNullOrWhiteSpace(part.Description)) {
                        description = part.Description;
                    } else if (!string.IsNullOrWhiteSpace(settings.DefaultDescription)) {
                        description = _tokenizer.Replace(settings.DefaultDescription, new { Content = part.ContentItem });
                    }
                    return description;
                });
        }
    }
}