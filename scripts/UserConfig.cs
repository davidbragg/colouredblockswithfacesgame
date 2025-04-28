public class UserConfig
{
	public string ColourTheme { get; set; }
	public string IconTheme { get; set; }

	public UserConfig ()
	{
		ColourTheme = "default";
		IconTheme = "default";
	}
}