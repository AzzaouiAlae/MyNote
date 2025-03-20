namespace MyNotes;

public partial class PageShowNotes : ContentPage
{
	public PageShowNotes()
	{
		InitializeComponent();
        Load();
    }
    async void Load()
	{
        LeftLayout.Children.Clear();
        RightLayout.Children.Clear();
        List<clsNote> list = await clsNote.GetNormalNote();
        bool isLeft = true;
        foreach (clsNote note in list)
        {
            note.Updated += Load;
            if (isLeft)
            {
                ViewNote vn = new ViewNote(note);
                LeftLayout.Children.Add(vn);
                isLeft = false;
            }
            else
            {
                ViewNote vn = new ViewNote(note);
                RightLayout.Children.Add(vn);
                isLeft = true;
            }
        }
    }
}