namespace MyNotes;

public partial class PageDeleteNotes : ContentPage
{

	public PageDeleteNotes()
	{
		InitializeComponent();
		LoadNotes();

    }
	async void LoadNotes()
	{
		List<clsNote> notes = await clsNote.GetAll();
        vLayout.Children.Clear();

        foreach (clsNote note in notes)
		{
			note.Deleted += LoadNotes;
            ViewDelete vd = new ViewDelete(note);
            vLayout.Children.Add(vd);
        }
	}
}