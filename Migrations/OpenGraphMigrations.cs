using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using Orchard.Environment.Extensions;

namespace Berry.SEOKit.Migrations {
    [OrchardFeature("Berry.OpenGraph")]
    public class OpenGraphMigrations : DataMigrationImpl {

        public int Create() {
            ContentDefinitionManager.AlterPartDefinition("OpenGraphPart", part => part
                .Attachable());
            ContentDefinitionManager.AlterPartDefinition("OpenGraphPart", part => part
                .WithDescription("Provides ability to define Open Graph social meta data."));

            return 1;
        }
    }
}
