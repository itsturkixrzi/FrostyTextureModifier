using Frosty.Core;
using Frosty.Core.Controls;
using FrostySdk.Interfaces;
using System.Windows.Media;

namespace FrostyTextureModifier
{
    /// <summary>
    /// Asset definition for Texture Modifier plugin
    /// Extends texture editing capabilities with real-time color modification
    /// </summary>
    public class TextureModifierAssetDefinition : AssetDefinition
    {
        private static ImageSource iconSource = new ImageSourceConverter().ConvertFromString(
            "pack://application:,,,/FrostyCore;component/Images/Assets/ImageFileType.png") as ImageSource;

        public override FrostyAssetEditor GetEditor(ILogger logger)
        {
            return new TextureModifierEditor(logger);
        }

        public override ImageSource GetIcon()
        {
            return iconSource;
        }
    }
}
