using System.Text.Json;

using Godot;

public partial class Main : Node2D
{
	public UserConfig config;
	private readonly string configPath = "user://config.json";

	public override void _Ready()
	{
		// TODO: Move all this config business out to a separate class

		if (FileAccess.FileExists(configPath))
		{
			// get and parse config file
			using var cfgFile = FileAccess.Open(configPath, FileAccess.ModeFlags.Read);
			config = JsonSerializer.Deserialize<UserConfig>(cfgFile.GetAsText());
		}
		else
		{
			// create a default config file
			config = new UserConfig();
			string cfgStr = JsonSerializer.Serialize(config);
			using var cfgFile = FileAccess.Open(configPath, FileAccess.ModeFlags.Write);
			cfgFile.StoreString(cfgStr);
		}

		// get and parse selected color theme
		using var colourFile = FileAccess.Open($"res://themes/{config.ColourTheme}.json", FileAccess.ModeFlags.Read);
		var colourTheme = JsonSerializer.Deserialize<ColourTheme>(colourFile.GetAsText());
		RenderingServer.SetDefaultClearColor(Color.FromHtml(colourTheme.BackgroundColour));

		// get and parse selected icon theme
	}
}
