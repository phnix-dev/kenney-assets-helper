#if TOOLS

namespace KennyAssetViewer;

[Tool]
internal partial class Main : Control
{
	// Constants
	public const string FILE_ZIP = "Kenney Game Assets All-in-1 1.8.0.zip";
	public const string FOLDER_ZIP = "assets";
	public const string FOLDER_ASSETS = "kenney_assets_viewer";

	public static string UserFolder { get; private set; }
	public static string UserFolderAssets { get; private set; }
	public static string PathToZip { get; private set; }

	// Enums

	// Signal

	// Export variables
	[Export] private Tabs tabs;

	// Member variables



	public override void _Ready()
	{
		var dir = DirAccess.Open(ProjectSettings.GlobalizePath("user://"));

		dir.ChangeDir("..");

		if (!dir.DirExists(FOLDER_ASSETS))
		{
			dir.MakeDir(FOLDER_ASSETS);
		}

		dir.ChangeDir(FOLDER_ASSETS);

		UserFolder = dir.GetCurrentDir();
		UserFolderAssets = $"{UserFolder}/{FOLDER_ZIP}";
		PathToZip = $"{UserFolder}/{FILE_ZIP}";

		CheckAssetFolder();
	}

	private void CheckAssetFolder()
	{
		// If the archive has been unzipped
		if (DirAccess.DirExistsAbsolute(UserFolderAssets))
		{
			var viewerPanel = tabs.ShowChild<ViewerPanel>();
			viewerPanel.StartWork();

			return;
		}
		
		// If the archive exists
		if (FileAccess.FileExists(PathToZip))
		{
			var unzipPanel = tabs.ShowChild<UnzipPanel>();
			unzipPanel.ZipExtracted += CheckAssetFolder;
			unzipPanel.StartUnzip();

			return;
		}
		
		// If the archive does not exists
		tabs.ShowChild<ZipPanel>();
	}

#pragma warning disable CA1822
	private void OnButtonUser_Pressed()
#pragma warning restore CA1822
	{
		OS.ShellOpen(UserFolder);
	}
}
#endif
