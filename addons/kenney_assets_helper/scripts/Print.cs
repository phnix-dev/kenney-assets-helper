// ╔═════════════════════════════════════════════════════════════════════╗
// ║ This file is licensed under the Mozilla Public License Version 2.0. ║
// ╚═════════════════════════════════════════════════════════════════════╝

#if TOOLS

namespace KenneyAssetsHelper;

[Tool]
public static class Print
{
	public static void Info(string what)
	{
		GD.PrintRich($"[color=8b8b8b][KennyAssetViewer] {what}[/color]");
	}

	public static void Warn(string what)
	{
		GD.PrintRich($"[color=FF8C00][KennyAssetViewer] {what}[/color]");
	}

	public static void Error(string what)
	{
		GD.PrintErr($"[KennyAssetViewer] {what}");
	}
}
#endif
