# Setup Instructions for Frosty Texture Modifier Plugin

## Prerequisites
- Visual Studio 2015 or higher
- .NET Framework 4.6+
- Frosty Editor installed on your system

## Setup Steps

### 1. Open the Solution
```
Open FrostyTextureModifier.sln in Visual Studio
```

### 2. Add References from Frosty Editor
You need to add three DLL references from your Frosty Editor installation:

#### Method: Manual Reference Addition
1. Right-click on **References** in Solution Explorer
2. Click **Add Reference...**
3. Browse to your Frosty Editor installation folder (usually `C:\Program Files\Frosty Editor\` or similar)
4. Add these three DLL files:
   - `FrostySdk.dll`
   - `FrostyCore.dll`
   - `FrostyControls.dll`

### 3. Build the Project
```
Build → Build Solution (Ctrl+Shift+B)
```

### 4. Find the Compiled DLL
The compiled DLL will be located at:
```
bin\Release\FrostyTextureModifier.dll
```

### 5. Install the Plugin
1. Copy `FrostyTextureModifier.dll` to your Frosty Editor Plugins folder:
   ```
   C:\Program Files\Frosty Editor\Plugins\
   ```
2. Restart Frosty Editor
3. The plugin should now be available when editing Texture Assets

## Troubleshooting

### References Not Found
- Make sure Frosty Editor is installed
- Check the path to Frosty Editor installation
- Try using the full path when adding references

### Build Failed
- Check Error List for specific errors
- Ensure all three Frosty references are added
- Make sure you're targeting .NET Framework 4.6+

### Plugin Not Appearing
- Verify the DLL was copied to the Plugins folder
- Check that the filename is `FrostyTextureModifier.dll`
- Restart Frosty Editor completely
- Check Frosty Editor logs for error messages

## Additional Notes
- This plugin modifies texture colors in real-time
- No export/import cycles needed
- Supports 5 different modification modes
- Full Undo/Redo support included
