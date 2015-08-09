using Orchard.DisplayManagement.Descriptors;
using Orchard.Environment.Extensions;

namespace Berry.SEOKit.ShapeProviders {
    [OrchardFeature("Berry.SEOKit")]
    public class LayoutShapeProvider : IShapeTableProvider {
        public void Discover(ShapeTableBuilder builder) {
            builder.Describe("Layout").OnDisplaying(displaying => {

                // Remove default layout wrapper
                displaying.ShapeMetadata.Wrappers.Clear();
                // Add SEOKit layout wrapper with custom title handling
                displaying.ShapeMetadata.Wrappers.Add("SEODocument");
            });
        }
    }
}