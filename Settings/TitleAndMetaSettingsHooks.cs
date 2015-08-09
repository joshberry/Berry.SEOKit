using Orchard.ContentManagement;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.ContentManagement.MetaData.Models;
using Orchard.ContentManagement.ViewModels;
using System.Collections.Generic;

namespace Berry.SEOKit.Settings {
    public class TitleAndMetaSettingsHooks : ContentDefinitionEditorEventsBase {

        public override IEnumerable<TemplateViewModel> TypePartEditor(ContentTypePartDefinition definition) {
            if (definition.PartDefinition.Name != "TitleAndMetaPart")
                yield break;

            var model = definition.Settings.GetModel<TitleAndMetaPartSettings>();

            yield return DefinitionTemplate(model);
        }

        public override IEnumerable<TemplateViewModel> TypePartEditorUpdate(ContentTypePartDefinitionBuilder builder, IUpdateModel updateModel) {
            if (builder.Name != "TitleAndMetaPart")
                yield break;

            var model = new TitleAndMetaPartSettings();
            updateModel.TryUpdateModel(model, "TitleAndMetaPartSettings", null, null);
            builder.WithSetting("TitleAndMetaPartSettings.AllowTitle", model.AllowTitle.ToString());
            builder.WithSetting("TitleAndMetaPartSettings.AllowDescription", model.AllowDescription.ToString());
            builder.WithSetting("TitleAndMetaPartSettings.DefaultTitle", model.DefaultTitle);
            builder.WithSetting("TitleAndMetaPartSettings.DefaultDescription", model.DefaultDescription);

            yield return DefinitionTemplate(model);
        }
    }
}