using static MyNotes.Global;

namespace MyNotes;

public partial class ViewNote : ContentView
{
	clsNote _Note;

	public ViewNote(clsNote note)
	{
		InitializeComponent();
		_Note = note;
        LoadNote();
        test_p_Click();        
    }
	void LoadNote()
	{
		SetColor();		
        lblTitle.Text = _Note.Title;

		if (_Note.Password == "")           
                lblNote.Text = _Note.Note;            
        else
                lblNote.Text = "**********";
        lblNoteDate.Text = _Note.UpdateDate.ToString("dd MMM yyyy HH:mm");
    }
    void test_p_Click()
    {
        TapGestureRecognizer p_Click = new TapGestureRecognizer();

        p_Click.Tapped += async (s, e) =>
        {
            if(_Note.Password == "")
                await Navigation.PushAsync(new PageAddEditNote(_Note));
            else
            {
                string str = await Shell.Current.DisplayPromptAsync("Password", "Enter you password: ");
                if (!string.IsNullOrEmpty(str) && ComputeHash(str) == _Note.Password)
                    await Navigation.PushAsync(new PageAddEditNote(_Note));
                else if(!string.IsNullOrEmpty(str))
                    await Shell.Current.DisplayAlert("Password", "Invalid Password", "OK");
            }
        };
        frm.GestureRecognizers.Add(p_Click); 
    }
    void SetColor()
	{
		if(_Note.color != "")
            frm.Background = new SolidColorBrush(Color.FromArgb(_Note.color));
        if (_Note.color == "#6c0bd2" || _Note.color == "#636363")
        {
            lblTitle.TextColor = Colors.White;
            lblNote.TextColor = Colors.White;
            bLine.Stroke = Colors.White;
        }
    }

    private async void btnCopyNote_Clicked(object sender, EventArgs e)
    {
        if (_Note.Password == "")
            await Clipboard.SetTextAsync(_Note.ToString());
        else
        {
            string str = await Shell.Current.DisplayPromptAsync("Password", "Enter you password: ");
            if (!string.IsNullOrEmpty(str) && ComputeHash(str) == _Note.Password)
                await Clipboard.SetTextAsync(_Note.ToString());
            else if (!string.IsNullOrEmpty(str))
                await Shell.Current.DisplayAlert("Password", "Invalid Password", "OK");
        }
    }
}