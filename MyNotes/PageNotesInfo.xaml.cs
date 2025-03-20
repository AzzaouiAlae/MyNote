namespace MyNotes;

public partial class PageNotesInfo : ContentPage
{
	public PageNotesInfo()
	{
		InitializeComponent();
	}

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
		List<clsNote> notes = await clsNote.GetAll();
		vLayout.Clear();

        foreach (clsNote note in notes)
		{
			ViewNoteInfo vni = new ViewNoteInfo(note);
			vLayout.Add(vni);
		}
    }
}