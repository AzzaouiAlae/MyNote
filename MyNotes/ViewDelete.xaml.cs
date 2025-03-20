using static MyNotes.Global;

namespace MyNotes;
public partial class ViewDelete : ContentView
{
    clsNote _Note;

    public ViewDelete(clsNote note)
	{
		InitializeComponent();
        _Note = note;
        LoadNote();
        frm_Click();
    }
    void LoadNote()
    {
        SetColor();
        lblID.Text = _Note.ID.ToString();
        lblTitle.Text = _Note.Title;
        if (_Note.isHide)
        {
            frm.Opacity = .3;
            lblNote.Opacity = 0.3;
        }

        if (_Note.Password == "")
            lblNote.Text = _Note.Note;
        else
            lblNote.Text = "**********";
        lblNoteDate.Text = _Note.UpdateDate.ToString("dd MMM yyyy HH:mm");
    }
    async void DeleteNote()
    {
        bool Result = await Shell.Current.DisplayAlert("Delete note", "Are you sure you want to delete this note?", "Yes", "No");
        if (Result)
        {
            Result = await _Note.Delete();
            if (Result)            
                await Shell.Current.DisplayAlert("Deleted", "The note deleted Successefully", "OK");            
            else
                await Shell.Current.DisplayAlert("Deleted", "Faild to deleted note", "OK");
        }
    }
    void frm_Click()
    {
        TapGestureRecognizer p_Click = new TapGestureRecognizer();

        p_Click.Tapped += async (s, e) =>
        {
            if (_Note.Password == "")
            {
                DeleteNote();                
            }
            else
            {
                string str = await Shell.Current.DisplayPromptAsync("Password", "Enter you password: ");
                if (!string.IsNullOrEmpty(str) && ComputeHash(str) == _Note.Password)
                    DeleteNote();
                else if (!string.IsNullOrEmpty(str))
                    await Shell.Current.DisplayAlert("Password", "Invalid Password", "OK");
            }
        };
        frm.GestureRecognizers.Add(p_Click);
    }
    void SetColor()
    {
        if (_Note.color != "")
            frm.Background = new SolidColorBrush(Color.FromArgb(_Note.color));

        if (_Note.color == "#6c0bd2" || _Note.color == "#636363")
        {
            lblTitle.TextColor = Colors.White;
            lblNote.TextColor = Colors.White;
            bLine.Stroke = Colors.White;
        }
    }
}