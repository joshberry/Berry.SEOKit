using Orchard.ContentManagement;
using Orchard.Environment.Extensions;

namespace Berry.SEOKit.Models {

    [OrchardFeature("Berry.OpenGraph")]
    public class OpenGraphPart : ContentPart {
        public string Title {
            get { return this.Retrieve(p => p.Title); }
            set { this.Store(p => p.Title, value); }
        }

        public string Type {
            get { return this.Retrieve(p => p.Type); }
            set { this.Store(p => p.Type, value); }
        }

        public string Image {
            get { return this.Retrieve(p => p.Image); }
            set { this.Store(p => p.Image, value); }
        }

        public string Url {
            get { return this.Retrieve(p => p.Url); }
            set { this.Store(p => p.Url, value); }
        }

        public string Description {
            get { return this.Retrieve(p => p.Description); }
            set { this.Store(p => p.Description, value); }
        }

        public string SiteName {
            get { return this.Retrieve(p => p.SiteName); }
            set { this.Store(p => p.SiteName, value); }
        }
    }
}