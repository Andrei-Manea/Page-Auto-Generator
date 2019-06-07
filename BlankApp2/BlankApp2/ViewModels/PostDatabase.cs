using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankApp2.ViewModels
{
    public partial class PostDatabase
    {
        // Vom implementa principiul Singleton
        private static PostDatabase _dataBase;
        private static SQLiteAsyncConnection _connection;

        public static PostDatabase Instance
        {
            get
            {
                if (_dataBase == null)
                {
                    _dataBase = new PostDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), $"PostItem.db3"));
                }

                return _dataBase;
            }
        }

        internal Task UpdateItemData(object text)
        {
            throw new NotImplementedException();
        }

        internal async Task UpdateItemData(string message)
        {
            uint MaxId = 0;
            try
            {
                MaxId = (await _connection.Table<PostItem>().ToListAsync()).Max(x => x.ID);
            }
            catch
            {

            }

            PostItem newPost = new PostItem { ID = MaxId + 1, Content = message };
            await _connection.InsertAsync(newPost);
        }

        private PostDatabase(string dbPath)
        {
            _connection = new SQLiteAsyncConnection(dbPath); 
            _connection.CreateTableAsync<PostItem>().Wait();
        }

        public Task<List<PostItem>> GetItemsAsync()
        {
            return _connection.Table<PostItem>().OrderBy(x => x.ID).ToListAsync();
        }

        public Task<PostItem> GetItemAsync(uint id)
        {
            return _connection.Table<PostItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> UpdateItemData(PostItem item)
        {
            return _connection.InsertOrReplaceAsync(item);
        }

        public Task<int> DeleteItemAsync(PostItem item)
        {
            return _connection.DeleteAsync(item);
        }
    }

    public class PostItem
    {
        [PrimaryKey]
        public uint ID { get; set; }

        [NotNull]
        public string Content { get; set; }


    }
}

