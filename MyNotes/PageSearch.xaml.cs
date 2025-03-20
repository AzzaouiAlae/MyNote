namespace MyNotes;

public partial class PageSearch : ContentPage
{
	public PageSearch()
	{
		InitializeComponent();
	}
    async void Search1()
    {
        vLayoutLeft.Children.Clear();
        vLayoutRight.Children.Clear();
        bool isLeft = true;

        List<clsNote> notes = await clsNote.Search(txtSearch.Text);
        foreach (clsNote note in notes)
        {
            note.Updated += Search1;
            if (isLeft)
            {
                ViewNote vn = new ViewNote(note);
                vLayoutLeft.Children.Add(vn);
                isLeft = false;
            }
            else
            {
                ViewNote vn = new ViewNote(note);
                vLayoutRight.Children.Add(vn);
                isLeft = true;
            }
        }
    }
    private void BtnSeach_Clicked(object sender, EventArgs e)
    {
        Search1();
    }
}