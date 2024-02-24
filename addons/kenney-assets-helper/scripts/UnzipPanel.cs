#if TOOLS

using System.IO.Compression;
using System.Threading.Tasks;

namespace KennyAssetViewer;

[Tool]
internal partial class UnzipPanel : Node
{
	// Constants

	// Enums

	// Signal
	[Signal] public delegate void ZipExtractedEventHandler();

	// Export variables
	[Export] private Label labelWait;

	// Member variables
	private int labelWaitLength;
	private Timer timer = new();



	public override void _Ready()
	{
		labelWaitLength = labelWait.Text.Length;
	}

	public async void StartUnzip()
	{
		await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
		CallDeferred(nameof(StartUnzipAsync));
		AddChild(timer);

		timer.WaitTime = .3;
		timer.Timeout += OnTimer_Timeout;
		timer.Start();
	}

	private void StartUnzipAsync()
	{
		_ = Task.Run(() =>
		{
			ZipFile.ExtractToDirectory(Main.PathToZip, Main.UserFolderAssets);
			CallDeferred(nameof(EmitSignal_ZipExtracted));
			QueueFree();
		});
	}

	private void OnTimer_Timeout()
	{
		if (labelWait.Text.Length == labelWaitLength)
		{
			labelWait.Text = labelWait.Text[..^3];
		}
		else
		{
			labelWait.Text += '.';
		}
	}

	private void EmitSignal_ZipExtracted()
	{
		EmitSignal(SignalName.ZipExtracted);
	}
}
#endif
