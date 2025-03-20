namespace MyNotes;

public partial class ViewNoteInfo : ContentView
{
    clsNote _Note;
    public ViewNoteInfo(clsNote note)
	{
		InitializeComponent();
        _Note = note;
        LoadNote();
    }
    void LoadNote()
    {
        SetColor();
        lblID.Text = _Note.ID.ToString();
        if (_Note.isHide)
        {
            frm.Opacity = .3;
            lblNote.Opacity = 0;
        }

        if (_Note.Password == "")
            lblNote.Text = _Note.Note;
        else
            lblNote.Text = "**********";
        lblNoteDate.Text = _Note.UpdateDate.ToString("dd MMM yyyy HH:mm");
    }
    void SetColor()
    {
        if (_Note.color != "")
            frm.Background = new SolidColorBrush(Color.FromArgb(_Note.color));
        if (_Note.color == "#6c0bd2" || _Note.color == "#636363")
        {            
            lblNote.TextColor = Colors.White;
            lblNoteDate.TextColor = Colors.White;
        }
    }
}