using Frosty.Core;
using Frosty.Core.Controls;
using FrostySdk.Interfaces;
using FrostySdk.Managers;
using FrostySdk.Resources;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FrostyTextureModifier
{
    /// <summary>
    /// Interaction logic for TextureModifierEditor.xaml
    /// Real-time texture color modification interface
    /// </summary>
    public partial class TextureModifierEditor : FrostyAssetEditor
    {
        private Texture currentTexture;
        private TextureColorModifier colorModifier;
        private ModificationHistory history;
        private Color originalColor = Colors.Red;
        private Color newColor = Colors.Lime;
        private ColorModificationMode currentMode = ColorModificationMode.DirectReplace;

        public TextureModifierEditor(ILogger logger) : base(logger)
        {
            InitializeComponent();
            colorModifier = new TextureColorModifier();
            history = new ModificationHistory();
            InitializeModes();
        }

        public override void SetAsset(EbxAsset asset)
        {
            base.SetAsset(asset);
            
            // Get the texture resource
            dynamic textureAsset = asset.RootObject;
            ResAssetEntry resEntry = App.AssetManager.GetResEntry(textureAsset.Resource);
            currentTexture = App.AssetManager.GetResAs<Texture>(resEntry);

            // Display texture in preview
            DisplayTexture();
        }

        public override FrostyAsset GetAsset()
        {
            return base.GetAsset();
        }

        private void InitializeModes()
        {
            ModeModeComboBox.Items.Add("Direct Replace");
            ModeModeComboBox.Items.Add("Hue Shift");
            ModeModeComboBox.Items.Add("Saturation");
            ModeModeComboBox.Items.Add("Brightness");
            ModeModeComboBox.Items.Add("Contrast");
            ModeModeComboBox.SelectedIndex = 0;

            // Initialize color displays
            OriginalColorDisplay.Fill = new SolidColorBrush(originalColor);
            NewColorDisplay.Fill = new SolidColorBrush(newColor);
            OriginalColorText.Text = originalColor.ToString();
            NewColorText.Text = newColor.ToString();
        }

        private void DisplayTexture()
        {
            if (currentTexture == null) return;

            try
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = colorModifier.GetTextureStream(currentTexture);
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                TexturePreview.Source = bitmapImage;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error displaying texture: {ex.Message}");
            }
        }

        private void OnModifyTextureAssetClick(object sender, RoutedEventArgs e)
        {
            Logger.Log("Modify TextureAsset button clicked");
            MessageBox.Show("✓ Color Modification Mode Activated!\n\nUse the controls on the right panel to modify texture colors in real-time.", 
                          "Texture Modifier", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void OnPickOriginalColorClick(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.ColorDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                originalColor = Color.FromArgb(dialog.Color.A, dialog.Color.R, dialog.Color.G, dialog.Color.B);
                OriginalColorDisplay.Fill = new SolidColorBrush(originalColor);
                OriginalColorText.Text = originalColor.ToString();
            }
        }

        private void OnPickNewColorClick(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.ColorDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                newColor = Color.FromArgb(dialog.Color.A, dialog.Color.R, dialog.Color.G, dialog.Color.B);
                NewColorDisplay.Fill = new SolidColorBrush(newColor);
                NewColorText.Text = newColor.ToString();
            }
        }

        private void OnModeChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ModeModeComboBox.SelectedIndex >= 0)
            {
                currentMode = (ColorModificationMode)ModeModeComboBox.SelectedIndex;
                
                // Toggle HSV controls visibility
                bool showHSV = currentMode != ColorModificationMode.DirectReplace;
                HSVLabel.Visibility = showHSV ? Visibility.Visible : Visibility.Collapsed;
                HueSlider.Visibility = showHSV ? Visibility.Visible : Visibility.Collapsed;
                SaturationSlider.Visibility = showHSV ? Visibility.Visible : Visibility.Collapsed;
                BrightnessSlider.Visibility = showHSV ? Visibility.Visible : Visibility.Collapsed;
                ContrastSlider.Visibility = showHSV ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private void OnPreviewClick(object sender, RoutedEventArgs e)
        {
            try
            {
                float tolerance = (float)ToleranceSlider.Value;
                
                switch (currentMode)
                {
                    case ColorModificationMode.DirectReplace:
                        colorModifier.ModifyTextureColor(currentTexture, originalColor, newColor, tolerance);
                        break;
                    case ColorModificationMode.HueShift:
                        colorModifier.AdjustHue(currentTexture, (float)HueSlider.Value);
                        break;
                    case ColorModificationMode.Saturation:
                        colorModifier.AdjustSaturation(currentTexture, (float)SaturationSlider.Value);
                        break;
                    case ColorModificationMode.Brightness:
                        colorModifier.AdjustBrightness(currentTexture, (float)BrightnessSlider.Value);
                        break;
                    case ColorModificationMode.Contrast:
                        colorModifier.AdjustContrast(currentTexture, (float)ContrastSlider.Value);
                        break;
                }

                DisplayTexture();
                Logger.Log("Preview updated");
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error in preview: {ex.Message}");
            }
        }

        private void OnApplyClick(object sender, RoutedEventArgs e)
        {
            try
            {
                float tolerance = (float)ToleranceSlider.Value;
                
                // Save to history
                history.AddSnapshot($"{currentMode} - Tolerance: {tolerance}");
                HistoryListBox.Items.Add($"✓ {currentMode} - Tolerance: {tolerance}");

                switch (currentMode)
                {
                    case ColorModificationMode.DirectReplace:
                        colorModifier.ModifyTextureColor(currentTexture, originalColor, newColor, tolerance);
                        break;
                    case ColorModificationMode.HueShift:
                        colorModifier.AdjustHue(currentTexture, (float)HueSlider.Value);
                        break;
                    case ColorModificationMode.Saturation:
                        colorModifier.AdjustSaturation(currentTexture, (float)SaturationSlider.Value);
                        break;
                    case ColorModificationMode.Brightness:
                        colorModifier.AdjustBrightness(currentTexture, (float)BrightnessSlider.Value);
                        break;
                    case ColorModificationMode.Contrast:
                        colorModifier.AdjustContrast(currentTexture, (float)ContrastSlider.Value);
                        break;
                }

                DisplayTexture();
                OnModified();
                Logger.Log($"Applied {currentMode} modification");
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error applying changes: {ex.Message}");
            }
        }

        private void OnResetClick(object sender, RoutedEventArgs e)
        {
            try
            {
                SetAsset(Asset);
                history.Clear();
                HistoryListBox.Items.Clear();
                HueSlider.Value = 0;
                SaturationSlider.Value = 0;
                BrightnessSlider.Value = 0;
                ContrastSlider.Value = 0;
                Logger.Log("Texture reset to original");
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error resetting texture: {ex.Message}");
            }
        }

        private void OnSaveClick(object sender, RoutedEventArgs e)
        {
            try
            {
                // Update the asset
                OnModified();
                Logger.Log("Texture modifications saved");
                MessageBox.Show("✓ Texture modifications have been saved!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error saving texture: {ex.Message}");
            }
        }

        private void OnUndoClick(object sender, RoutedEventArgs e)
        {
            history.Undo();
            if (HistoryListBox.Items.Count > 0)
                HistoryListBox.Items.RemoveAt(HistoryListBox.Items.Count - 1);
            Logger.Log("Undo operation");
        }

        private void OnRedoClick(object sender, RoutedEventArgs e)
        {
            history.Redo();
            Logger.Log("Redo operation");
        }
    }
}
