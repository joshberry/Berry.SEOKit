using Berry.SEOKit.Models;
using Berry.SEOKit.Settings;
using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.Handlers;
using Orchard.Environment.Extensions;
using Orchard.Tokens;
using Orchard.UI.Resources;
using System.Web;

namespace Berry.SEOKit.Drivers {
    [OrchardFeature("Berry.SEOKit")]
    public class TitleAndMetaDriver : ContentPartDriver<TitleAndMetaPart> {
        private readonly IWorkContextAccessor _wca;
        private readonly ITokenizer _tokenizer;
        public TitleAndMetaDriver(IWorkContextAccessor workContextAccessor, ITokenizer tokenizer) {
            _wca = workContextAccessor;
            _tokenizer = tokenizer;
        }

        protected override string Prefix {
            get {
                return "TitleAndMetaPart";
            }
        }

        protected override DriverResult Display(TitleAndMetaPart part, string displayType, dynamic shapeHelper) {
            if (displayType != "Detail") return null;

            var settings = part.TypePartDefinition.Settings.GetModel<TitleAndMetaPartSettings>();

            // Add the page title to items dictionary for use in layout wrapper
            string title = null;
            if (settings.AllowTitle && !string.IsNullOrWhiteSpace(part.Title)) {
                title = part.Title;
            } else if (!string.IsNullOrWhiteSpace(settings.DefaultTitle)) {
                title = _tokenizer.Replace(settings.DefaultTitle, new { Content = part.ContentItem });
            }
            HttpContext.Current.Items.Add("Berry.SEOKit.Title", title);

            // Set meta data
            var resourceManager = _wca.GetContext().Resolve<IResourceManager>();

            string description = null;

            if (settings.AllowDescription && !string.IsNullOrWhiteSpace(part.Description)) {
                description = part.Description;
            } else if (!string.IsNullOrWhiteSpace(settings.DefaultDescription)) {
                description = _tokenizer.Replace(settings.DefaultDescription, new { Content = part.ContentItem });
            }

            if (!string.IsNullOrWhiteSpace(description)) {
                resourceManager.SetMeta(new MetaEntry {
                    Name = "description",
                    Content = description
                });
            }

            // Nothing to display
            return null;
        }

        // GET
        protected override DriverResult Editor(TitleAndMetaPart part, dynamic shapeHelper) {

            return ContentShape("Parts_TitleAndMeta_Edit",
                () => shapeHelper.EditorTemplate(
                    TemplateName: "Parts/TitleAndMeta",
                    Model: part,
                    Prefix: Prefix));
        }

        // POST
        protected override DriverResult Editor(TitleAndMetaPart part, IUpdateModel updater, dynamic shapeHelper) {
            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }

        protected override void Exporting(TitleAndMetaPart part, ExportContentContext context) {
            context.Element(part.PartDefinition.Name).SetAttributeValue("Title", part.Title);
            context.Element(part.PartDefinition.Name).SetAttributeValue("Description", part.Description);
        }

        protected override void Importing(TitleAndMetaPart part, ImportContentContext context) {
            part.Title = context.Attribute(part.PartDefinition.Name, "Title");
            part.Description = context.Attribute(part.PartDefinition.Name, "Description");
        }
    }
}