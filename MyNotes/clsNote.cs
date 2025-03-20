using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes
{
    public class clsNote
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int ID { get; set; } = 0;
        public string Title { get; set; } = "";
        public string Note { get; set; } = "";
        public string color { get; set; } = "";
        public bool isHide { get; set; } = false;
        public string Password { get; set; } = "";
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        async Task<bool> Add()
        {
            UpdateDate = DateTime.Now;
            int result = await clsNoteData.Add(this);

            return result > 0;
        }
        async Task<bool> Update()
        {
            UpdateDate = DateTime.Now;
            int result = await clsNoteData.Update(this);
            if (result > 0)
                Updated?.Invoke();
            return result > 0;
        }
        public async Task<bool> Save()
        {
            if (ID == 0)
                return await Add();
            else
                return await Update();
        }
        public static async Task<List<clsNote>> GetNormalNote()
        {
            List<clsNote> notes = await clsNoteData.GetNormalNote();

            return notes;
        }
        static public async Task<List<clsNote>> Search(string Text)
        {
            return await clsNoteData.Search(Text);
        }
        static public async Task<List<clsNote>> GetAll()
        {
            return await clsNoteData.GetAll();
        }
        void DeleteInfo()
        {
            ID = 0;
            Title = "";
            Note = "";
            color = "";
            isHide = false;
            Password = "";
        }
        public async Task<bool> Delete()
        {
            bool Result = await clsNoteData.Delete(this);

            if (Result)
            {
                DeleteInfo();
                Deleted?.Invoke();
            }

            return Result;
        }
        static public async Task<List<clsNote>> GetHiddenNotes()
        {
            return await clsNoteData.GetHiddenNotes();
        }
        public override string ToString()
        {
            return Title + "\n" + Note + "\n" + UpdateDate.ToString("dd MMM yyyy HH:mm");
        }

        public event Action? Updated;

        public event Action? Deleted;
    }
}
