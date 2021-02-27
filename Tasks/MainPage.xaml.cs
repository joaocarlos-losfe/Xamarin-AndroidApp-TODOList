using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Tasks.Models;
using Xamarin.Essentials;
using System.Diagnostics;


namespace Tasks
{
    public partial class MainPage : ContentPage
    {
        ObservableCollection<Tarefa> tarefas = new ObservableCollection<Tarefa>();
        public ObservableCollection<Tarefa> Tarefas { get { return tarefas; } }

        private int IdxI = -1;

        public MainPage()
        {
            InitializeComponent();
            listView.ItemsSource = tarefas;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var db_load_file =  await App.Local_database.GetNotesAsync();

            if (db_load_file.Count == 0)
                return;
            else
            {
                foreach (var db_tarefa in db_load_file)
                {
                    tarefas.Add(db_tarefa);
                }
            }
        }

        private async void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            IdxI = e.SelectedItemIndex;

            await menuButtons.ScaleTo(0, 50);

            menuButtons.IsVisible = true;

            await menuButtons.ScaleTo(1.1, 200);
            await menuButtons.ScaleTo(1.0, 200);
        }

        
        private async void entryText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(string.IsNullOrEmpty(entryText.Text) || string.IsNullOrWhiteSpace(entryText.Text))
            {
                await btnSaveTask.RotateTo(-90, 250);
                await btnSaveTask.FadeTo(0.50);
                btnSaveTask.IsEnabled = false;
            }
            else
            {
                

                await btnSaveTask.FadeTo(1.0);
                btnSaveTask.IsEnabled = true;
                await btnSaveTask.RotateTo(90, 250);

            }
        }

        private async void btnSaveTask_Clicked(object sender, EventArgs e)
        {
            Tarefa _tarefa = new Tarefa();

            if (string.IsNullOrEmpty(entryText.Text) || string.IsNullOrWhiteSpace(entryText.Text))
                return;
            else
            {
                indicatorLoading.IsVisible = indicatorLoading.IsEnabled = true;

                await btnSaveTask.ScaleTo(0.80);
                await btnSaveTask.ScaleTo(1.0);

                _tarefa.tarefa = entryText.Text;
                tarefas.Add(_tarefa);

                await App.Local_database.SaveNoteAsync(_tarefa);

                indicatorLoading.IsVisible = indicatorLoading.IsEnabled = false;

                listView.ScrollTo(_tarefa, ScrollToPosition.End, true);
            }
        }

        private async void btnRemove_Clicked(object sender, EventArgs e)
        {
            if (IdxI == -1)
                return;
            else
            {
                indicatorLoading.IsVisible = indicatorLoading.IsEnabled = true;

                await btnRemove.ScaleTo(0.80);
                await btnRemove.ScaleTo(1.0);

                Tarefa _tarefa = new Tarefa();

                _tarefa = tarefas[IdxI];
                tarefas.RemoveAt(IdxI);

                await App.Local_database.DeleteNoteAsync(_tarefa);

                IdxI = -1;

                indicatorLoading.IsVisible = indicatorLoading.IsEnabled = false;

                menuButtons.IsVisible = false;

            }
        }

        private async void bntCopyClipboard_Clicked(object sender, EventArgs e)
        {

            if (IdxI == -1)
                return;
            else
            {
                indicatorLoading.IsVisible = indicatorLoading.IsEnabled = true;

                await bntCopyClipboard.ScaleTo(0.80);
                await bntCopyClipboard.ScaleTo(1.0);

                await Clipboard.SetTextAsync(tarefas[IdxI].tarefa);

                //await DisplayAlert("Copiado para area de transferencia", $"\"{tarefas[IdxI].tarefa}\"", "OK");

                indicatorLoading.IsVisible = indicatorLoading.IsEnabled = false;
            }
        }

        private async void btnCheck_Clicked(object sender, EventArgs e)
        {
            if (IdxI == -1)
                return;
            else
            {
                if (tarefas[IdxI].concluida)
                    return;

                indicatorLoading.IsVisible = indicatorLoading.IsEnabled = true;

                await btnCheck.ScaleTo(0.80);
                await btnCheck.ScaleTo(1.0);

                Tarefa _tarefa = new Tarefa();
                _tarefa = tarefas[IdxI];

                _tarefa.imagem_concluida = "check.png";
                _tarefa.concluida = true;

                tarefas.RemoveAt(IdxI);
                tarefas.Insert(IdxI, _tarefa);

                await App.Local_database.SaveNoteAsync(_tarefa);

                indicatorLoading.IsVisible = indicatorLoading.IsEnabled = false;
            } 
        }

       
    }
}
