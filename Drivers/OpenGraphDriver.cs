using Berry.SEOKit.Models;
using Berry.SEOKit.Settings;
using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.Handlers;
using Orchard.Environment.Extensions;
using Orchard.Tokens;
using Orchard.UI.Resources;

namespace Berry.SEOKit.Drivers {
    [OrchardFeature("Berry.OpenGraph")]
    public class OpenGraphDriver : ContentPartDriver<OpenGraphPart> {
        private readonly IWorkContextAccessor _wca;
        private readonly ITokenizer _tokenizer;
        public OpenGraphDriver(IWorkContextAccessor workContextAccessor, ITokenizer tokenizer) {
            _wca = workContextAccessor;
            _tokenizer = tokenizer;
        }

        protected override string Prefix {
            get {
                return "OpenGraphPart";
            }
        }

        protected override DriverResult Display(OpenGraphPart part, string displayType, dynamic shapeHelper) {
            if (displayType != "Detail") return null;

            var settings = part.TypePartDefinition.Settings.GetModel<OpenGraphPartSettings>();
            
            // Set open graph meta data
            var resourceManager = _wca.GetContext().Resolve<IResourceManager>();

            SetMetaEntry("og:title", part.Title, settings.DefaultTitle, settings.AllowTitle, part.ContentItem, resourceManager);
            SetMetaEntry("og:url", part.Url, settings.DefaultUrl, settings.AllowUrl, part.ContentItem, resourceManager);
            SetMetaEntry("og:image", part.Image, settings.DefaultImage, settings.AllowImage, part.ContentItem, resourceManager);
            SetMetaEntry("og:type", part.Type, settings.DefaultType, settings.AllowType, part.ContentItem, resourceManager);
            SetMetaEntry("og:site_name", part.SiteName, settings.DefaultSiteName, settings.AllowSiteName, part.ContentItem, resourceManager);
            SetMetaEntry("og:description", part.Description, settings.DefaultDescription, settings.AllowDescription, part.ContentItem, resourceManager);

            // Nothing to display
            return null;
        }
              
        // GET
        protected override DriverResult Editor(OpenGraphPart part, dynamic shapeHelper) {

            return ContentShape("Parts_OpenGraph_Edit",
                () => shapeHelper.EditorTemplate(
                    TemplateName: "Parts/OpenGraph",
                    Model: part,
                    Prefix: Prefix));
        }

        // POST
        protected override DriverResult Editor(OpenGraphPart part, IUpdateModel updater, dynamic shapeHelper) {
            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }

        protected override void Exporting(OpenGraphPart part, ExportContentContext context) {
            context.Element(part.PartDefinition.Name).SetAttributeValue("Url", part.Url);
            context.Element(part.PartDefinition.Name).SetAttributeValue("Title", part.Title);
            context.Element(part.PartDefinition.Name).SetAttributeValue("Type", part.Type);
            context.Element(part.PartDefinition.Name).SetAttributeValue("Description", part.Description);
            context.Element(part.PartDefinition.Name).SetAttributeValue("Image", part.Image);
            context.Element(part.PartDefinition.Name).SetAttributeValue("SiteName", part.SiteName);
        }

        protected override void Importing(OpenGraphPart part, ImportContentContext context) {
            part.Url = context.Attribute(part.PartDefinition.Name, "Url");
            part.Title = context.Attribute(part.PartDefinition.Name, "Title");
            part.Type = context.Attribute(part.PartDefinition.Name, "Type");
            part.Description = context.Attribute(part.PartDefinition.Name, "Description");
            part.Image = context.Attribute(part.PartDefinition.Name, "Image");
            part.SiteName = context.Attribute(part.PartDefinition.Name, "SiteName");
        }

        private void SetMetaEntry(string name, string value, string defaultValue, bool allowOverride, ContentItem contentItem, IResourceManager resourceManager) {
            string metaValue = null;

            if (allowOverride && !string.IsNullOrWhiteSpace(value)) {
                metaValue = value;
            } else if (!string.IsNullOrWhiteSpace(defaultValue)) {
                metaValue = _tokenizer.Replace(defaultValue, new { Content = contentItem });
            }

            if (!string.IsNullOrWhiteSpace(metaValue)) {
                resourceManager.SetMeta(new MetaEntry {
                    Name = name,
                    Content = metaValue
                });
            }
        }
    }
}