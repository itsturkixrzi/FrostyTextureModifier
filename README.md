# Frosty Texture Modifier Plugin 🎨

A powerful real-time texture color modifier plugin for Frosty Editor. Modify TextureAsset colors instantly without export/import cycles.

## Features ✨

- 🎨 **Real-time Color Editing** - Modify texture colors instantly
- 🎯 **Advanced Color Picker** - Pick colors from textures directly
- ⚙️ **Precision Controls** - Tolerance, Hue, Saturation, Brightness adjustments
- 👁️ **Live Preview** - See changes as you make them
- ↩️ **Undo/Redo Support** - Full history management
- 🔄 **Multiple Modification Modes** - Replace, Shift, HSB adjustment
- 💾 **Non-Destructive Editing** - Original textures preserved

## Installation

1. Download the latest release
2. Extract DLL files to your Frosty Editor `Plugins` folder
3. Restart Frosty Editor
4. Open any TextureAsset and look for the **"Modify TextureAsset"** button

## Usage

### Basic Workflow
1. Open a TextureAsset in Frosty Editor
2. Click the **"Modify TextureAsset"** button above the texture preview
3. Select your modification mode
4. Use Color Picker to choose colors
5. Adjust tolerance and parameters
6. Click **Apply** to modify the texture

### Modification Modes

#### Direct Replace
Replace colors with exact matching or tolerance-based matching.

#### Hue Shift
Rotate colors in the color wheel.

#### Saturation
Increase or decrease color saturation.

#### Brightness
Adjust overall brightness.

#### Contrast
Adjust contrast levels.

## UI Layout

```
┌─────────────────────────────────────────────────┐
│  Frosty Texture Modifier Plugin                 │
├─────────────────────────────────────────────────┤
│  ┌───────────────────┬─────────────────────────┐│
│  │   Preview         │   Control Panel         ││
│  │   Viewport        │   ┌───────────────────┐ ││
│  │                   │   │Color Picker       │ ││
│  │  [Modify]         │   └───────────────────┘ ││
│  │  [Reset]          │   Original: #...       ││
│  │  [Save]           │   New: #...            ││
│  │                   │   ┌───────────────────┐ ││
│  │                   │   │ Tolerance: 15%   │ ││
│  │                   │   └───────────────────┘ ││
│  │                   │   ┌───────────────────┐ ││
│  │                   │   │ [Apply]           │ ││
│  │                   │   │ [Preview]         │ ││
│  │                   │   │ [Save]            │ ││
│  │                   │   └───────────────────┘ ││
│  └───────────────────┴─────────────────────────┘│
└─────────────────────────────────────────────────┘
```

## System Requirements

- Frosty Editor 1.0.6.1 or higher
- .NET Framework 4.6+
- Direct3D 11

## Building from Source

1. Clone this repository
```bash
git clone https://github.com/itsturkixrzi/FrostyTextureModifier.git
cd FrostyTextureModifier
```

2. Open the `.sln` file in Visual Studio 2015 or higher

3. Add missing assemblies from your Frosty Editor installation:
   - FrostyControls.dll
   - FrostyCore.dll
   - FrostySdk.dll
   
   Right-click Project → Properties → References → Add Reference

4. Build the solution:
   ```
   Visual Studio → Build → Build Solution
   ```

5. Find the compiled DLL in `bin/Release/FrostyTextureModifier.dll`

6. Copy to your Frosty Editor Plugins folder:
   ```
   C:\Program Files\Frosty Editor\Plugins\FrostyTextureModifier.dll
   ```

## Project Structure

```
FrostyTextureModifier/
├── README.md                          # Documentation
├── LICENSE                            # MIT License
├── FrostyTextureModifier.csproj       # Project file
├── TextureModifierAssetDefinition.cs  # Asset definition
├── TextureModifierEditor.xaml         # UI Layout
├── TextureModifierEditor.xaml.cs      # UI Logic
├── Core/
│   ├── ColorModificationMode.cs       # Modification modes enum
│   ├── TextureColorModifier.cs        # Core modification engine
│   └── ModificationHistory.cs         # Undo/Redo system
├── Properties/
│   └── AssemblyInfo.cs                # Assembly information
└── .gitignore                         # Git ignore file
```

## Features in Detail

### Color Modification Modes

**Direct Replace Mode:**
- Select original color (or pick from texture)
- Select new color
- Adjust tolerance for similar colors
- Apply to replace colors in real-time

**Hue Shift Mode:**
- Rotate all colors in the hue spectrum
- Range: -180° to +180°
- Useful for team color variations

**Saturation Mode:**
- Increase or decrease color intensity
- Range: -100 to +100
- Make colors more vivid or muted

**Brightness Mode:**
- Adjust overall brightness
- Range: -100 to +100
- Lighten or darken textures uniformly

**Contrast Mode:**
- Enhance or reduce color contrast
- Range: -100 to +100
- Make details pop or blend colors

### Real-time Preview
- See changes as you adjust sliders
- Preview button for quick visual feedback
- No need to apply to see changes

### History System
- Full undo/redo support
- View modification history
- Roll back to any previous state
- Non-destructive editing

## Workflow Example

1. **Open a weapon texture:**
   - Open Frosty Editor
   - Find a weapon TextureAsset (e.g., `Foot Soldier` weapon texture)
   - Double-click to open editor

2. **Click "Modify TextureAsset" button**
   - Button appears in the toolbar above texture preview
   - Color modification panel opens on the right

3. **Select colors to change:**
   - Pick original color (e.g., yellow for Foot Soldier)
   - Click "Pick Color" button
   - Click on yellow area in texture

4. **Choose new color:**
   - Click "New Color" button
   - Select desired color (e.g., red for different team)

5. **Adjust tolerance:**
   - Slide tolerance to match color variations
   - Adjust until all yellow shades are selected

6. **Apply changes:**
   - Click "Apply Changes" button
   - Texture updates in real-time
   - All yellow becomes red

7. **Save modifications:**
   - Click "Save" button
   - Changes are saved to TextureAsset
   - No export/import needed!

## Advanced Tips

- **Use HSV modes for team colors:** Use Hue Shift to quickly create team variations
- **Fine-tune with saturation:** Adjust saturation to match game style
- **Batch processing:** Modify multiple textures in sequence using history
- **Combine modes:** Use multiple modifications together (Hue + Brightness)

## Known Limitations

- High-value vectors not yet supported
- HDR calculations tested on limited game titles
- Some compressed texture formats may have limitations

## Troubleshooting

### Plugin doesn't appear in Frosty Editor
- Ensure DLL is in correct Plugins folder
- Check Frosty Editor version (1.0.6.1 or higher required)
- Restart Frosty Editor after installing

### Color picker not working
- Ensure texture is loaded
- Try clicking on texture preview area
- Check that color mode is set to Direct Replace

### Changes not saving
- Click "Save" button explicitly
- Check that you have write permissions
- Verify texture asset isn't read-only

## Performance

- CPU-based modifications for compatibility
- Real-time preview performance depends on texture resolution
- Typical 1024x1024 texture: <50ms per operation

## Future Enhancements

- [ ] GPU-accelerated processing with HLSL shaders
- [ ] Batch texture processing
- [ ] Custom gradient mapping
- [ ] Pattern-based modifications
- [ ] Layer support
- [ ] Custom presets/profiles
- [ ] Color harmony suggestions
- [ ] Texture comparison tools

## Credits

- **Frosty Editor:** [CadeEvs/FrostyToolsuite](https://github.com/CadeEvs/FrostyToolsuite)
- **Community:** Frosty Modding Community

## License

MIT License - See LICENSE file for details

## Support

For issues, questions, or suggestions:
- Open an issue on GitHub
- Check existing issues first
- Provide detailed reproduction steps

## Contributing

Contributions are welcome! Please:
1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Open a Pull Request

---

**Made with ❤️ for the Frosty Editor community**
