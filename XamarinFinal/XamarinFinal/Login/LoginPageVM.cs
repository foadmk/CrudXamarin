using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace XamarinFinal
{
    class LoginPageVM : BaseVM
    {
        private string login;
        private string senha;
        private string erro;
        private bool ocupado;
        private bool botaoVisivel;
        private RestAPI api;
        private App app;
        public ICommand Autenticar { get; private set; }
        public ICommand Registrar { get; private set; }

        public LoginPageVM(App _app)
        {
            app = _app;
            api = app.getAPI();
            BotaoVisivel = true;
            Autenticar = new Command(async () => await _autenticar());
            Registrar = new Command(async () => await _registrar());
        }

        public string Login {
            get
            {
                return login;
            }
            set
            {
                if(value != login)
                {
                    login = value;
                    OnPropertyChanged("Login");
                }
            }
        }
        public string Senha
        {
            get
            {
                return senha;
            }
            set
            {
                if (value != senha)
                {
                    senha = value;
                    OnPropertyChanged("Senha");
                }
            }
        }

        public string Erro
        {
            get
            {
                return erro;
            }
            set
            {
                if (value != erro)
                {
                    erro = value;
                    OnPropertyChanged("Erro");
                }
            }
        }

        public bool Ocupado
        {
            get
            {
                return ocupado;
            }
            set
            {
                if (value != ocupado)
                {
                    ocupado = value;
                    OnPropertyChanged("Ocupado");
                }
            }
        }


        public bool BotaoVisivel
        {
            get
            {
                return botaoVisivel;
            }
            set
            {
                if (value != botaoVisivel)
                {
                    botaoVisivel = value;
                    OnPropertyChanged("BotaoVisivel");
                }
            }
        }

        private async Task<bool> _callApi(string url)
        {
            bool logged_in = false;
            BotaoVisivel = false;
            Ocupado = true;
            Erro = null;
            try
            {
                logged_in = await api.Auth(url, login, senha);
            }
            catch (Exception e)
            {
                Erro = $"Erro: '{e.Message}'";
            }

            BotaoVisivel = true;
            Ocupado = false;

            if (logged_in) app.GoToMain();

            return logged_in;
        }
        private async Task<bool> _autenticar()
        {
            return await _callApi("/login");
        }

        private async Task<bool> _registrar()
        {
            return await _callApi("/register");
        }



    }
}
