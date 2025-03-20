namespace MyNotes;

public partial class PageShowAllNotes : ContentPage
{
	public PageShowAllNotes()
	{
		InitializeComponent();
        LoadNotes();
    }
    async void LoadNotes()
    {
        List<clsNote> list = await clsNote.GetAll();
        vLayout.Clear();
        foreach (clsNote note in list)
        {
            note.Updated += LoadNotes;
            ViewNote vn = new ViewNote(note);
            vLayout.Children.Add(vn);
        }
    }
}