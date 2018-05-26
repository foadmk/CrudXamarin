using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace XamarinFinal
{
    public class TasksVM : BaseVM
    {
        public ICommand DeleteTask { get; private set; }
        public ICommand SaveTask { get; private set; }
        public ICommand RefreshTasks { get; private set; }

        public bool ListaAtualizando { get; set; }

        private App app;
        private RestAPI api;

        public ObservableCollection<TaskModel> Tasks { get; private set; }
        public List<string> ListaStatus { get { return new List<string>() { "pending", "ongoing", "completed" }; } }

        public TaskModel TaskSelecionado { get; set; }
        public string ButtonText { get { return TaskSelecionado._id == null ? "Cancelar" : "Excluir"; } }

        public TasksVM(App _app)
        {
            ListaAtualizando = false;
            app = _app;
            api = app.getAPI();
            TaskSelecionado = new TaskModel();
            Tasks = new ObservableCollection<TaskModel>();
            DeleteTask = new Command(async () => await _deleteTask());
            SaveTask = new Command(async () => await _saveTask());
            RefreshTasks = new Command(async () => await _refreshTasks());
        }

        private async Task<bool> _refreshTasks()
        {
            await _atualizaLista();
            ListaAtualizando = false;
            OnPropertyChanged("ListaAtualizando");
            return true;
        }
        private async Task<bool> _saveTask()
        {
            if (TaskSelecionado._id == null)
            {
                await api.Create<TaskModel>("/tasks", TaskSelecionado);
                TaskSelecionado = new TaskModel();
            }
            else
            {
                await api.Update<TaskModel>("/tasks", TaskSelecionado._id, TaskSelecionado);
            }
            OnPropertyChanged("TaskSelecionado");
            await _atualizaLista();
            app.MasterDetailPage.IsPresented = true;
            return true;
        }

        private async Task<bool> _deleteTask()
        {            
            if (TaskSelecionado._id != null)
            {
                var confirma = await app.MasterDetailPage.DisplayAlert("Excluir","Confirma a Exclusão?", "Sim", "Não");
                if (confirma)
                {
                    await api.Delete("/tasks", TaskSelecionado._id);
                    _reset();
                }
            } else
            {
                _reset();
            }
            return true;
        }

        private async void _reset()
        {
            TaskSelecionado = new TaskModel();
            OnPropertyChanged("TaskSelecionado");
            await _atualizaLista();
            app.MasterDetailPage.IsPresented = true;
        }

        public void ShowDetail(TaskModel item)
        {
            try
            {
                var page = (Page)Activator.CreateInstance(typeof(ListaDeTasksDetail));
                if (item._id == null) item = new TaskModel();
                TaskSelecionado = item;
                page.BindingContext = this;

                app.MasterDetailPage.Detail = new NavigationPage(page);
                app.MasterDetailPage.IsPresented = false;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        public async Task<bool> _atualizaLista()
        {
            try
            {
                List<TaskModel> lt =
                    await api.GetAll<TaskModel>("/tasks");
                Tasks.Clear();
                foreach (TaskModel t in lt)
                {
                    Tasks.Add(t);
                }
                TaskModel t2 = new TaskModel();
                t2.name = "Novo";
                t2.status = null;
                Tasks.Add(t2);
                OnPropertyChanged("Tasks");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
