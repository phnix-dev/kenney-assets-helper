#if TOOLS
global using Godot;
global using System;
global using System.Linq;
global using System.Collections.Generic;

namespace KennyAssetViewer;

[Tool]
internal partial class KenneyAssetsPlugin : EditorPlugin
{
	private const string PATH_TO_DOCK = "res://addons/kenney_assets_viewer/dock.tscn";
	
	private Control dock;

	public override void _EnterTree()
	{
		dock = (Control)GD.Load<PackedScene>(PATH_TO_DOCK).Instantiate();
		dock.Name = "Kenney Assets";
				
		AddControlToDock(DockSlot.LeftBr, dock);
		Print.Info("Loading plugin!");
	}

	public override void _ExitTree()
	{
		Print.Info("Unloading plugin!");
		RemoveControlFromDocks(dock);
		dock.Free();
	}
}
#endif
