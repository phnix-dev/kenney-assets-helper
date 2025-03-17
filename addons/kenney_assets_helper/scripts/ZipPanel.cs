// ╔═════════════════════════════════════════════════════════════════════╗
// ║ This file is licensed under the Mozilla Public License Version 2.0. ║
// ╚═════════════════════════════════════════════════════════════════════╝

#if TOOLS

namespace KenneyAssetsHelper;

[Tool]
internal partial class ZipPanel : Control
{
	// Constants

	// Enums

	// Signal

	// Export variables
	[Export] private Button buttonOpenUserFolder;

	// Member variables
	
	
	public override void _Ready()
	{
		buttonOpenUserFolder.Pressed += OnButtonOpenUserFolder_Pressed;
	}

	private void OnButtonOpenUserFolder_Pressed()
	{
		OS.ShellOpen(Main.UserFolder);
	}
}
#endif
