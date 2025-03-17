// ╔═════════════════════════════════════════════════════════════════════╗
// ║ This file is licensed under the Mozilla Public License Version 2.0. ║
// ╚═════════════════════════════════════════════════════════════════════╝

#if TOOLS

namespace KenneyAssetsHelper;

[Tool]
internal partial class Tabs : TabContainer
{
	// Constants

	// Enums

	// Signal

	// Export variables

	// Member variables
	private Dictionary<Type, int> childs;



	public override void _Ready()
	{
		childs = new();
		int i = 0;

		foreach (var child in GetChildren())
		{
			childs.Add(child.GetType(), i);

			i += 1;
		}
	}

	public T ShowChild<T>()
	where
		T : Node
	{
		CurrentTab = childs.GetValueOrDefault(typeof(T), 0);
		return GetChild(CurrentTab) as T;
	}
}
#endif
