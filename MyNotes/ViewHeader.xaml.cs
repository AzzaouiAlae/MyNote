namespace MyNotes;

public partial class ViewHeader : ContentView
{
	public ViewHeader()
	{
		InitializeComponent();
	}
    public Action? RefreshPreviousPage;
    public string title
	{
		get { return lblTitle.Text; }
		set { lblTitle.Text = value; }
	}
	public bool isBackButtonVisible
	{
		get { return btnPop.IsVisible; }
		set { btnPop.IsVisible = value; }
	}
    private void btnPop_Clicked(object sender, EventArgs e)
    {
		Navigation.PopAsync();
		MainThread.BeginInvokeOnMainThread(() => RefreshPreviousPage?.Invoke());
    }
}