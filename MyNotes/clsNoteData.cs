using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes
{
    
    public class clsNoteData
    {
        static SQLiteAsyncConnection DBConnection;
        static async Task Init()
        {
            if (DBConnection == null)
                DBConnection = new SQLiteAsyncConnection(DataSettings.DatabasePath, DataSettings.flags);            

            try
            {
                await DBConnection.CreateTableAsync<clsNote>();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }
        static async Task CloseDB()
        {
            await DBConnection.CloseAsync();
        }

        static bool isWorking = false;
        static public async Task<int> Add(clsNote note)
        {
            while(isWorking)
                await Task.Delay(1000);

            isWorking = true;
            await Init();

            int Result = await DBConnection.InsertAsync(note);

            await CloseDB();
            isWorking = false;
            return Result;
        }
        static public async Task<int> Update(clsNote note)
        {
            while (isWorking)
                await Task.Delay(1000);

            isWorking = true;
            await Init();

            int Result = await DBConnection.UpdateAsync(note);

            await CloseDB();
            isWorking = false;
            return Result;
        }
        static public async Task<List<clsNote>> GetNormalNote()
        {
            while (isWorking)
                await Task.Delay(1000);
            isWorking = true;
            await Init();

            List<clsNote> notes = await DBConnection.QueryAsync<clsNote>($"Select * from [clsNote] where isHide = {false}");

            await CloseDB();
            isWorking = false;
            return notes;
        }
        static public async Task<List<clsNote>> Search(string Text)
        {
            while (isWorking)
                await Task.Delay(1000);
            isWorking = true;
            await Init();

            List<clsNote> notes = await DBConnection.QueryAsync<clsNote>($"Select * from [clsNote] where Note like '%{Text}%' or Title like '%{Text}%'");            

            await CloseDB();
            isWorking = false;
            return notes;
        }
        static public async Task<List<clsNote>> GetAll()
        {
            while (isWorking)
                await Task.Delay(1000);
            isWorking = true;
            await Init();

            List<clsNote> notes = await DBConnection.QueryAsync<clsNote>($"Select * from [clsNote]");

            await CloseDB();
            isWorking = false;
            return notes;
        }
        static public async Task<bool> Delete(clsNote note)
        {
            while (isWorking)
                await Task.Delay(1000);
            isWorking = true;
            await Init();

            int Result = await DBConnection.DeleteAsync(note);

            await CloseDB();
            isWorking = false;
            return Result > 0;
        }
        static public async Task<List<clsNote>>  GetHiddenNotes()
        {
            while (isWorking)
                await Task.Delay(1000);
            isWorking = true;
            await Init();

            List<clsNote> notes = await DBConnection.QueryAsync<clsNote>($"Select * from [clsNote] where isHide = {true}");

            await CloseDB();
            isWorking = false;
            return notes;
        }
    }
}
