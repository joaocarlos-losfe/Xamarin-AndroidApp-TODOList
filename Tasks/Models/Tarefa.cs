using Sequential_tasks.Services;
using System;
using SQLite;
using System.Collections.Generic;
using System.Text;

namespace Tasks.Models
{
    public class Tarefa
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public bool concluida { get; set; }
        public string tarefa { get; set; }
        public string data_criacao { get; set; }
        public string imagem_concluida { get; set; }

        public Tarefa()
        {
            this.concluida = false;
            this.data_criacao = DateTime.Now.ToString("g");
            this.imagem_concluida = "";
        }
    }
}
