namespace MyNotes
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();            
        }

        private void btnAdd_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageAddEditNote());
            
        }

        private void btnNotes_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageShowNotes());
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageSearch());
        }

        private void btnNotesInfo_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageNotesInfo());
        }

        private void btnDeleteNote_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageDeleteNotes());
        }

        private void btnShowHiddenNotes_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync( new PageShowHiddenNotes() );
        }

        private void btnShowAllNotes_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageShowAllNotes() );
        }
    }
}