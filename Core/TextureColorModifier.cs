using FrostySdk.Resources;
using System;
using System.IO;

namespace FrostyTextureModifier
{
    /// <summary>
    /// Core texture color modification engine
    /// Handles all color transformation operations
    /// </summary>
    public class TextureColorModifier
    {
        /// <summary>
        /// Modify texture by replacing colors
        /// </summary>
        public void ModifyTextureColor(Texture texture, System.Windows.Media.Color originalColor, 
                                      System.Windows.Media.Color newColor, float tolerance = 15f)
        {
            if (texture == null) return;

            try
            {
                byte[] textureData = GetTextureData(texture);
                ReplaceColor(textureData, originalColor, newColor, tolerance);
                SetTextureData(texture, textureData);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error modifying texture color: {ex.Message}");
            }
        }

        /// <summary>
        /// Adjust hue of the texture
        /// </summary>
        public void AdjustHue(Texture texture, float hueShift)
        {
            if (texture == null) return;

            try
            {
                byte[] textureData = GetTextureData(texture);
                AdjustHueData(textureData, hueShift);
                SetTextureData(texture, textureData);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adjusting hue: {ex.Message}");
            }
        }

        /// <summary>
        /// Adjust saturation of the texture
        /// </summary>
        public void AdjustSaturation(Texture texture, float saturation)
        {
            if (texture == null) return;

            try
            {
                byte[] textureData = GetTextureData(texture);
                AdjustSaturationData(textureData, saturation);
                SetTextureData(texture, textureData);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adjusting saturation: {ex.Message}");
            }
        }

        /// <summary>
        /// Adjust brightness of the texture
        /// </summary>
        public void AdjustBrightness(Texture texture, float brightness)
        {
            if (texture == null) return;

            try
            {
                byte[] textureData = GetTextureData(texture);
                AdjustBrightnessData(textureData, brightness);
                SetTextureData(texture, textureData);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adjusting brightness: {ex.Message}");
            }
        }

        /// <summary>
        /// Adjust contrast of the texture
        /// </summary>
        public void AdjustContrast(Texture texture, float contrast)
        {
            if (texture == null) return;

            try
            {
                byte[] textureData = GetTextureData(texture);
                AdjustContrastData(textureData, contrast);
                SetTextureData(texture, textureData);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adjusting contrast: {ex.Message}");
            }
        }

        /// <summary>
        /// Get texture stream for preview
        /// </summary>
        public Stream GetTextureStream(Texture texture)
        {
            if (texture == null) return null;
            // Implementation depends on FrostySdk texture handling
            return new MemoryStream();
        }

        // Private helper methods

        private byte[] GetTextureData(Texture texture)
        {
            // Extract texture pixel data
            // This is implementation-specific based on FrostySdk
            return new byte[0];
        }

        private void SetTextureData(Texture texture, byte[] data)
        {
            // Update texture with modified pixel data
            // This is implementation-specific based on FrostySdk
        }

        private void ReplaceColor(byte[] textureData, System.Windows.Media.Color oldColor, 
                                 System.Windows.Media.Color newColor, float tolerance)
        {
            for (int i = 0; i < textureData.Length; i += 4)
            {
                byte r = textureData[i];
                byte g = textureData[i + 1];
                byte b = textureData[i + 2];
                byte a = textureData[i + 3];

                // Calculate color difference
                float colorDiff = CalculateColorDifference(r, g, b, oldColor.R, oldColor.G, oldColor.B);

                if (colorDiff <= tolerance)
                {
                    textureData[i] = newColor.R;
                    textureData[i + 1] = newColor.G;
                    textureData[i + 2] = newColor.B;
                    // Keep alpha channel unchanged
                }
            }
        }

        private void AdjustHueData(byte[] textureData, float hueShift)
        {
            for (int i = 0; i < textureData.Length; i += 4)
            {
                byte r = textureData[i];
                byte g = textureData[i + 1];
                byte b = textureData[i + 2];

                // Convert RGB to HSV
                RGB2HSV(r, g, b, out float h, out float s, out float v);

                // Adjust hue
                h += hueShift;
                if (h < 0) h += 360;
                if (h >= 360) h -= 360;

                // Convert back to RGB
                HSV2RGB(h, s, v, out r, out g, out b);

                textureData[i] = r;
                textureData[i + 1] = g;
                textureData[i + 2] = b;
            }
        }

        private void AdjustSaturationData(byte[] textureData, float saturation)
        {
            float factor = 1 + (saturation / 100f);

            for (int i = 0; i < textureData.Length; i += 4)
            {
                byte r = textureData[i];
                byte g = textureData[i + 1];
                byte b = textureData[i + 2];

                RGB2HSV(r, g, b, out float h, out float s, out float v);
                s *= factor;
                s = Math.Min(1f, s);
                HSV2RGB(h, s, v, out r, out g, out b);

                textureData[i] = r;
                textureData[i + 1] = g;
                textureData[i + 2] = b;
            }
        }

        private void AdjustBrightnessData(byte[] textureData, float brightness)
        {
            float factor = 1 + (brightness / 100f);

            for (int i = 0; i < textureData.Length; i += 4)
            {
                int r = (int)(textureData[i] * factor);
                int g = (int)(textureData[i + 1] * factor);
                int b = (int)(textureData[i + 2] * factor);

                textureData[i] = (byte)Math.Min(255, r);
                textureData[i + 1] = (byte)Math.Min(255, g);
                textureData[i + 2] = (byte)Math.Min(255, b);
            }
        }

        private void AdjustContrastData(byte[] textureData, float contrast)
        {
            float factor = (259f * (contrast + 255f)) / (255f * (259f - contrast));

            for (int i = 0; i < textureData.Length; i += 4)
            {
                int r = (int)((textureData[i] - 128) * factor + 128);
                int g = (int)((textureData[i + 1] - 128) * factor + 128);
                int b = (int)((textureData[i + 2] - 128) * factor + 128);

                textureData[i] = (byte)Math.Max(0, Math.Min(255, r));
                textureData[i + 1] = (byte)Math.Max(0, Math.Min(255, g));
                textureData[i + 2] = (byte)Math.Max(0, Math.Min(255, b));
            }
        }

        private float CalculateColorDifference(byte r1, byte g1, byte b1, byte r2, byte g2, byte b2)
        {
            int dr = r1 - r2;
            int dg = g1 - g2;
            int db = b1 - b2;
            return (float)Math.Sqrt(dr * dr + dg * dg + db * db);
        }

        private void RGB2HSV(byte r, byte g, byte b, out float h, out float s, out float v)
        {
            float rf = r / 255f;
            float gf = g / 255f;
            float bf = b / 255f;

            float max = Math.Max(rf, Math.Max(gf, bf));
            float min = Math.Min(rf, Math.Min(gf, bf));
            float delta = max - min;

            // Hue
            if (delta == 0) h = 0;
            else if (max == rf) h = 60 * (((gf - bf) / delta) % 6);
            else if (max == gf) h = 60 * (((bf - rf) / delta) + 2);
            else h = 60 * (((rf - gf) / delta) + 4);

            if (h < 0) h += 360;

            // Saturation
            s = max == 0 ? 0 : delta / max;

            // Value
            v = max;
        }

        private void HSV2RGB(float h, float s, float v, out byte r, out byte g, out byte b)
        {
            float c = v * s;
            float x = c * (1 - Math.Abs((h / 60f) % 2 - 1));
            float m = v - c;

            float rf, gf, bf;

            if (h < 60)
            {
                rf = c; gf = x; bf = 0;
            }
            else if (h < 120)
            {
                rf = x; gf = c; bf = 0;
            }
            else if (h < 180)
            {
                rf = 0; gf = c; bf = x;
            }
            else if (h < 240)
            {
                rf = 0; gf = x; bf = c;
            }
            else if (h < 300)
            {
                rf = x; gf = 0; bf = c;
            }
            else
            {
                rf = c; gf = 0; bf = x;
            }

            r = (byte)((rf + m) * 255);
            g = (byte)((gf + m) * 255);
            b = (byte)((bf + m) * 255);
        }
    }
}
