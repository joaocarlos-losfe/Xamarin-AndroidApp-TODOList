using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tasks.Models;

namespace Tasks.Data
{
    public class LocalDatabase
    {
        readonly SQLiteAsyncConnection database;

        public LocalDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Tarefa>().Wait();
        }

        public Task<List<Tarefa>> GetNotesAsync()
        {
            //obtem todas as notas
            return database.Table<Tarefa>().ToListAsync();
        }

        public Task<Tarefa> GetNoteAsync(int id)
        {
            // obtem uma nota esfecifica pelo id repassado.
            return database.Table<Tarefa>().Where(i => i.id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveNoteAsync(Tarefa tarefa)
        {
            //verifica se ja tem uma nota adicionada

            if (tarefa.id != 0)
            {
                //se ja existe, atualiza a nota.
                return database.UpdateAsync(tarefa);
            }
            else
            {
                //se não existe, adiciona a nova nota.
                return database.InsertAsync(tarefa);
            }
        }

        public Task<int> DeleteNoteAsync(Tarefa tarefa)
        {
            //deleta a nota.
            return database.DeleteAsync(tarefa);
        }
    }
}
