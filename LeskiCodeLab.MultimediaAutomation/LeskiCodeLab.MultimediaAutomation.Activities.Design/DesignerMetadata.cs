using System.Activities.Presentation.Metadata;
using System.ComponentModel;
using System.ComponentModel.Design;
using LeskiCodeLab.MultimediaAutomation.Activities.Design.Designers;
using LeskiCodeLab.MultimediaAutomation.Activities.Design.Properties;

namespace LeskiCodeLab.MultimediaAutomation.Activities.Design
{
    public class DesignerMetadata : IRegisterMetadata
    {
        public void Register()
        {
            var builder = new AttributeTableBuilder();
            builder.ValidateTable();

            var categoryAttribute = new CategoryAttribute($"{Resources.Category}");

            builder.AddCustomAttributes(typeof(AudioVideoCombine), categoryAttribute);
            builder.AddCustomAttributes(typeof(AudioVideoCombine), new DesignerAttribute(typeof(AudioVideoCombineDesigner)));
            builder.AddCustomAttributes(typeof(AudioVideoCombine), new HelpKeywordAttribute(""));

            builder.AddCustomAttributes(typeof(MultimediaProcess), categoryAttribute);
            builder.AddCustomAttributes(typeof(MultimediaProcess), new DesignerAttribute(typeof(MultimediaProcessDesigner)));
            builder.AddCustomAttributes(typeof(MultimediaProcess), new HelpKeywordAttribute(""));

            builder.AddCustomAttributes(typeof(VideoDeliveryCodec), categoryAttribute);
            builder.AddCustomAttributes(typeof(VideoDeliveryCodec), new DesignerAttribute(typeof(VideoDeliveryCodecDesigner)));
            builder.AddCustomAttributes(typeof(VideoDeliveryCodec), new HelpKeywordAttribute(""));

            builder.AddCustomAttributes(typeof(VideoDenoise), categoryAttribute);
            builder.AddCustomAttributes(typeof(VideoDenoise), new DesignerAttribute(typeof(VideoDenoiseDesigner)));
            builder.AddCustomAttributes(typeof(VideoDenoise), new HelpKeywordAttribute(""));

            builder.AddCustomAttributes(typeof(VideoFolderCombine), categoryAttribute);
            builder.AddCustomAttributes(typeof(VideoFolderCombine), new DesignerAttribute(typeof(VideoFolderCombineDesigner)));
            builder.AddCustomAttributes(typeof(VideoFolderCombine), new HelpKeywordAttribute(""));

            builder.AddCustomAttributes(typeof(VideoIntermediateCodec), categoryAttribute);
            builder.AddCustomAttributes(typeof(VideoIntermediateCodec), new DesignerAttribute(typeof(VideoIntermediateCodecDesigner)));
            builder.AddCustomAttributes(typeof(VideoIntermediateCodec), new HelpKeywordAttribute(""));

            builder.AddCustomAttributes(typeof(VideoLUT), categoryAttribute);
            builder.AddCustomAttributes(typeof(VideoLUT), new DesignerAttribute(typeof(VideoLUTDesigner)));
            builder.AddCustomAttributes(typeof(VideoLUT), new HelpKeywordAttribute(""));

            builder.AddCustomAttributes(typeof(VideoStabilize), categoryAttribute);
            builder.AddCustomAttributes(typeof(VideoStabilize), new DesignerAttribute(typeof(VideoStabilizeDesigner)));
            builder.AddCustomAttributes(typeof(VideoStabilize), new HelpKeywordAttribute(""));

            builder.AddCustomAttributes(typeof(VideoText), categoryAttribute);
            builder.AddCustomAttributes(typeof(VideoText), new DesignerAttribute(typeof(VideoTextDesigner)));
            builder.AddCustomAttributes(typeof(VideoText), new HelpKeywordAttribute(""));


            MetadataStore.AddAttributeTable(builder.CreateTable());
        }
    }
}
