namespace MyNotes;

public partial class ViewLogo : ContentView
{
	public ViewLogo()
	{
		InitializeComponent();
	}

    private void Image_Loaded(object sender, EventArgs e)
    {
		if(DeviceInfo.Platform != DevicePlatform.WinUI) 
			((Image)sender).Margin = new Thickness(0);
    }
}