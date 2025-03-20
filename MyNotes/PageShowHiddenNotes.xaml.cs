namespace MyNotes;

public partial class PageShowHiddenNotes : ContentPage
{
	public PageShowHiddenNotes()
	{
		InitializeComponent();
		LoadNotes();
    }
	async void LoadNotes()
	{
        List<clsNote> list = await clsNote.GetHiddenNotes();
        vLayout.Clear();
        foreach (clsNote note in list)
        {
            note.Updated += LoadNotes;
            ViewNote vn = new ViewNote(note);
            vLayout.Children.Add(vn);            
        }
    }
}