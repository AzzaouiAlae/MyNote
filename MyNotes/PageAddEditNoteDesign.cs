using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes
{
    public partial class PageAddEditNote : ContentPage
    {
        clsNote Note;
        ViewHeader Header;
        string[] colors;
        Button btnSave;
        Switch swIsHide;
        Switch swPass;
        Entry txtPass;
        Entry txtTitle;
        Label lblCharCount;
        Editor txtNote;

        VerticalStackLayout vLayout2 = new VerticalStackLayout();
        Label lblCreateDate;
        Label lblUpdateDate;
        HorizontalStackLayout hLayout;

        Grid myGrid;
        Grid myGrid2;
        Grid myGrid3;
        ScrollView sv;

        void InitilComponent()
        {
            Header = new ViewHeader();
            colors = new string[] { "#000000", "#db553e", "#0eacf9", "#ecca60", "#319a2d", "#6c0bd2", "#636363" };

            btnSave = new Button()
            {
                Text = "Save Note",
                Padding = new Thickness(15)
            };

            swIsHide = new Switch();
            swPass = new Switch();

            txtPass = new Entry()
            {
                IsPassword = true,
                IsVisible = false,
                Placeholder = "Password"
            };

            txtTitle = new Entry()
            {
                Placeholder = "Title",
                FontAttributes = FontAttributes.Bold
            };

            lblCharCount = new Label();
            lblCharCount.Text = "You use 0 characters";
            lblCharCount.Margin = new Thickness(12, 0, 0, 0);

            txtNote = new Editor()
            {
                Placeholder = "Text Note",
                FontAttributes = FontAttributes.Bold                
            };
            txtNote.Margin = new Thickness(0,23,0,0);

            lblCreateDate = new Label();
            lblUpdateDate = new Label();

            hLayout = new HorizontalStackLayout();

            myGrid = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                }
            };

            myGrid2 = new Grid
            {
                Padding = new Thickness(15, 10, 15, 15),
                RowSpacing = 10,
                MinimumHeightRequest = 400,
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = GridLength.Auto }
                }
            };

            myGrid3 = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(50, GridUnitType.Absolute) },
                    new ColumnDefinition { Width = GridLength.Star },
                    new ColumnDefinition { Width = new GridLength(50, GridUnitType.Absolute) },
                    new ColumnDefinition { Width = GridLength.Star }
                }
            };

            sv = new ScrollView();
        }
        async Task Load()
        {
            InitilComponent();

            if(Note.ID == 0)
            {
                Header.title = "Add New Note";
                lblCreateDate.Text = "Create Date : " + DateTime.Now.ToString("dd MMM yyyy HH:mm");
                lblUpdateDate.Text = "Update Date : " + DateTime.Now.ToString("dd MMM yyyy HH:mm");
            }                
            else
            {
                Header.title = "Update Note";
                SetColors();
                swIsHide.IsToggled = Note.isHide;
                swPass.IsToggled = Note.Password != "" ;
                txtPass.IsVisible = swPass.IsToggled;
                //txtPass.Text = Note.Password;
                txtTitle.Text = Note.Title;
                txtNote.Text = Note.Note;
                lblCreateDate.Text = "Create Date : " + Note.CreateDate.ToString("dd MMM yyyy HH:mm");
                lblUpdateDate.Text = "Update Date : " + Note.UpdateDate.ToString("dd MMM yyyy HH:mm");
                lblCharCount.Text = $"You use {txtNote.Text.Length} characters";                
            }
            txtNote.TextChanged += (s, e) => lblCharCount.Text = $"You use {txtNote.Text.Length} characters";
            btnSave.Clicked += btnSave_Clicked;
            swPass.Toggled += swPass_Toggled;
            
            int Bw = 0;

            Label lbl1 = new Label()
            {
                Text = "Hide",
                VerticalOptions = LayoutOptions.Center,
            };

            Label lbl2 = new Label()
            {
                Text = "Password",
                VerticalOptions = LayoutOptions.Center,
            };

            hLayout.VerticalOptions = LayoutOptions.Center;
            for (int i = 0; i < colors.Length; i++)
            {
                Button btn = new Button()
                {
                    Background = Color.FromArgb(colors[i]),
                    AutomationId = i.ToString(),
                    BorderWidth = Bw,
                    BorderColor = Colors.White,
                    CornerRadius = 25
                };
                btn.Clicked += Button_Clicked;
                hLayout.Children.Add(btn);
                Bw = 5;
            }
            
            vLayout2.Add(lblCreateDate);
            
            vLayout2.Add(lblUpdateDate);

            myGrid2.Add(myGrid3, 0, 2);
            sv.Content = myGrid2;
            myGrid.Add(sv, 0, 2);
            myGrid.Add(Header);
            myGrid2.Add(btnSave);
            myGrid2.Add(hLayout, 0, 1);
            myGrid3.Add(swIsHide);
            myGrid3.Add(lbl1, 1, 0);
            myGrid3.Add(swPass, 2, 0);
            myGrid3.Add(lbl2, 3, 0);
            myGrid3.AddWithSpan(txtPass, 1, 0, 1, 4);
            myGrid2.Add(txtTitle, 0, 3);
            myGrid2.Add(lblCharCount, 0, 4);
            myGrid2.Add(txtNote, 0, 4);
            myGrid2.Add(vLayout2, 0, 5);

            await Task.Delay(150);
            MainThread.BeginInvokeOnMainThread(() => {
                this.Content = myGrid;
            });

        }
    }
}
