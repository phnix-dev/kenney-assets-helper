#if TOOLS

namespace KennyAssetViewer;

[Tool]
internal partial class ViewerPanel : Control
{
	// Constants

	// Enums

	// Signal

	// Export variables
	[Export] private OptionButton optionAssetType;
	[Export] private OptionButton optionAssetName;
	[Export] private Button buttonFolder;
	[Export] private Button buttonPreview;
	[Export] private Button buttonAssets;
	[Export] private Button buttonImport;
	[Export] private FileDialog popupImportFrom;
	[Export] private FileDialog popupImportTo;

	// Member variables
	private string assetsTypeName;
	private string assetsNameName;
	private string importFromPath;
	private string importToPath;



	public void StartWork()
	{
		optionAssetType.ItemSelected += OnOptionAssetType_ItemSelected;
		optionAssetName.ItemSelected += OnOptionAssetName_ItemSelected;
		buttonFolder.Pressed += OnButtonFolder_Pressed;
		buttonPreview.Pressed += OnButtonPreview_Pressed;
		buttonAssets.Pressed += OnButtonAssets_Pressed;
		buttonImport.Pressed += OnButtonImport_Pressed;
		popupImportFrom.GetOkButton().Pressed += OnPopupImportFromOkButton_Pressed;
		popupImportTo.GetOkButton().Pressed += OnPopupImportToOkButton_Pressed;

		var popupSize = optionAssetName.GetPopup().MaxSize;
		optionAssetName.GetPopup().MaxSize = popupSize;

		popupImportFrom.FileMode = FileDialog.FileModeEnum.OpenDir;
		popupImportTo.FileMode = FileDialog.FileModeEnum.OpenDir;

		optionAssetType.Select(0);
		OnOptionAssetType_ItemSelected(0);

		optionAssetName.Select(0);
		OnOptionAssetName_ItemSelected(0);
	}



	private void OnOptionAssetType_ItemSelected(long index)
	{
		optionAssetName.Clear();

		assetsTypeName = optionAssetType.GetItemText((int)index);
		var path = Main.UserFolderAssets + "/" + optionAssetType.GetItemText((int)index);

		foreach (var subfolder in DirAccess.GetDirectoriesAt(path))
		{
			optionAssetName.AddItem(subfolder);
		}

		optionAssetName.Select(0);
		OnOptionAssetName_ItemSelected(0);
	}

	private void OnOptionAssetName_ItemSelected(long index)
	{
		assetsNameName = optionAssetName.GetItemText((int)index);
	}



	private void OnButtonFolder_Pressed()
	{
		OS.ShellOpen($"{Main.UserFolderAssets}/{assetsTypeName}/{assetsNameName}");
	}

	private void OnButtonPreview_Pressed()
	{
		var previews = new List<string>();
		var path = $"{Main.UserFolderAssets}/{assetsTypeName}/{assetsNameName}";
		var items = DirAccess.GetFilesAt(path).ToList();
		items.AddRange(DirAccess.GetDirectoriesAt(path));

		foreach (var item in items)
		{
			if (item.Find("Preview") != -1)
			{
				previews.Add(item);
			}
		}

		foreach (var item in previews)
		{
			OS.ShellOpen($"{path}/{item}");
		}
	}

	private void OnButtonAssets_Pressed()
	{
		var assetFolder = string.Empty;
		var path = $"{Main.UserFolderAssets}/{assetsTypeName}/{assetsNameName}";
		var folders = new string[] { "Model", "PNG", "Tiles", "Audio" };
		var dirs = DirAccess.GetDirectoriesAt(path);

		foreach (var dir in dirs)
		{
			foreach (var folder in folders)
			{
				if (dir.Find(folder) != -1)
				{
					assetFolder = dir;
					break;
				}
			}
		}

		OS.ShellOpen($"{path}/{assetFolder}");
	}



	private void OnButtonImport_Pressed()
	{
		var assetPath = $"{Main.UserFolderAssets}/{assetsTypeName}/{assetsNameName}/";
		popupImportFrom.CurrentDir = assetPath;
		// popupImportFrom.RootSubfolder = assetPath;

		popupImportFrom.PopupCentered(new(700, 500));
	}

	private void OnPopupImportFromOkButton_Pressed()
	{
		importFromPath = popupImportFrom.CurrentPath;
		var resPath = ProjectSettings.GlobalizePath("res://");
		popupImportTo.CurrentDir = resPath;
		// popupImportTo.RootSubfolder = resPath;

		popupImportTo.PopupCentered(new(700, 500));
	}

	private void OnPopupImportToOkButton_Pressed()
	{
		importToPath = popupImportTo.CurrentPath;

		// Add windows drive letter
		if (Main.UserFolderAssets[1..3] == ":/")
		{
			importFromPath = Main.UserFolderAssets[0] + ":" + importFromPath;
		}

		var userPath = ProjectSettings.GlobalizePath("res://");

		// Add windows drive letter
		if (userPath[1..3] == ":/")
		{
			importToPath = userPath[0] + ":" + importToPath;
		}

		var selectedAssetFolder = $"{Main.UserFolderAssets}/{assetsTypeName}/{assetsNameName}/";

		if (importFromPath.Find(selectedAssetFolder) == -1)
		{
			Print.Error("Cannot select a folder outside the selected assets folder");
			ClearLastPath();
			
			return;
		}

		if (importToPath.Find(userPath) == -1)
		{
			Print.Error("Cannot select a folder outside the project folder");
			ClearLastPath();

			return;
		}

		importToPath += $"{assetsNameName}/";

		DirAccess.MakeDirRecursiveAbsolute(importToPath);

		foreach (var file in DirAccess.GetFilesAt(importFromPath))
		{
			DirAccess.CopyAbsolute(importFromPath + file, importToPath + file);
		}
	}

	private void ClearLastPath()
	{
		importFromPath = string.Empty;
		importToPath = string.Empty;
	}
}
#endif
