using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using Orchard.Environment.Extensions;

namespace Berry.SEOKit.Migrations {
    [OrchardFeature("Berry.SEOKit")]
    public class MetaMigrations : DataMigrationImpl {

        public int Create() {
            ContentDefinitionManager.AlterPartDefinition("TitleAndMetaPart", part => part
                .Attachable());

            return 1;
        }

        public int UpdateFrom1() {
            ContentDefinitionManager.AlterPartDefinition("TitleAndMetaPart", part => part
                .WithDescription("Provides ability to define page title and meta data.")
            );

            return 2;
        }

    }
}
