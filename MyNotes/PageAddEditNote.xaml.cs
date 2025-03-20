using static MyNotes.Global;

namespace MyNotes;

public partial class PageAddEditNote : ContentPage
{
    public PageAddEditNote()
	{        
        InitializeComponent();
        Note = new clsNote();
        Task.Run(() => Load());
    }
    public PageAddEditNote(clsNote note)
    {
        InitializeComponent();
        Note = note;
        Task.Run(() => Load());
    }
    void LoadNote()
    {
        Header.title = "Update Note";
        SetColors();
        txtTitle.Text = Note.Title;
        txtNote.Text = Note.Note;
        lblCreateDate.Text = "Create Date : " + Note.CreateDate.ToString("dd MMM yyyy HH:mm");
        lblUpdateDate.Text = "Update Date : " + Note.UpdateDate.ToString("dd MMM yyyy HH:mm");
    }
    private void Button_Clicked(object? sender, EventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            if (sender == null) return;

            foreach (var item in hLayout.Children)
            {
                ((Button)item).BorderWidth = 5;
            }
            ((Button)sender).BorderWidth = 0;

            if(int.TryParse(((Button)sender).AutomationId, out int Result))
            {
                Note.color = colors[Result];
                SetColors();
            }
        });
    }
    void SetColors()
    {
        if (Note.color == "#000000" || Note.color == "")
            Note.color = "#FFF";

        if (Note.color == "#6c0bd2" || Note.color == "#636363")
        {
            txtPass.TextColor = Colors.White;
            txtTitle.TextColor = Colors.White;
            txtNote.TextColor = Colors.White;
        }
        else
        {
            txtPass.TextColor = Colors.Black;
            txtTitle.TextColor = Colors.Black;
            txtNote.TextColor = Colors.Black;
        }
        txtPass.Background = new SolidColorBrush(Color.FromArgb(Note.color));
        txtTitle.Background = new SolidColorBrush(Color.FromArgb(Note.color));
        txtNote.Background = new SolidColorBrush(Color.FromArgb(Note.color));
    }
    private async void btnSave_Clicked(object? sender, EventArgs e)
    {
        Note.Title = txtTitle.Text;
        Note.Note = txtNote.Text;
        Note.UpdateDate = DateTime.Now;

        if(Note.ID == 0)
            Note.CreateDate = DateTime.Now;

        if (swPass.IsToggled && !string.IsNullOrEmpty(txtPass.Text))       
            Note.Password = ComputeHash(txtPass.Text);        
        else if(!swPass.IsToggled)
            Note.Password = "";
        Note.isHide = swIsHide.IsToggled;

        bool Result = await Note.Save();
        if (Result)
        {
            LoadNote();
            //await DisplayAlert("Saved", "The Note Saved Successfully", "OK");
            Result = await DisplayAlert("Saved", "The Note Saved Successfully", "Back", "Stay");
            if (Result)
            {
                txtTitle.IsEnabled = false;
                txtNote.IsEnabled = false;
                txtPass.IsEnabled = false;
                await Navigation.PopAsync();
            }
        }
        else
            await DisplayAlert("Failed", "Failed to Save Note", "OK");
    }
    private void swPass_Toggled(object? sender, ToggledEventArgs e)
    {
        txtPass.IsVisible = swPass.IsToggled;
    }

}