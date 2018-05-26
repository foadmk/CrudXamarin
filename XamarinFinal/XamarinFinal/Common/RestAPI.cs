using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XamarinFinal
{
    public class RestAPI
    {
        const string _apiBaseUrl = "http://159.65.180.92:3000";
        struct AuthResult
        {
            public string token { get; set; }
            public bool auth { get; set; }
        }

        struct AuthParams
        {
            public string username { get; set; }
            public string password { get; set; }
        }

        AuthResult authResult;
        MyClient client;

        public RestAPI()
        {
            authResult = new AuthResult();
            authResult.auth = false;
            client = new MyClient();
        }

        public bool isLogedIn()
        {
            return authResult.auth;
        }

        public async Task<Boolean> Auth(string restUrl, string username, string password)
        {
            if (client == null) return false;
            AuthParams authParams = new AuthParams();
            authParams.username = username;
            authParams.password = password;

            string myContent = JsonConvert.SerializeObject(authParams);
            try
            { 
                string str = await client.Post(_apiBaseUrl + restUrl, myContent);
                AuthResult objs = JsonConvert.DeserializeObject<AuthResult>(str);
                client.SetToken(objs.token);
                authResult = objs;
                return objs.auth;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<List<T>> GetAll<T>(string restUrl)
        {
            List<T> objs;

            try
            {
                string str = await client.Get(_apiBaseUrl + restUrl);
                objs = JsonConvert.DeserializeObject<List<T>>(str);
                return objs;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<T> Get<T>(string restUrl, string _id)
        {
            T obj;

            try
            {
                string str = await client.Get($"{_apiBaseUrl}{restUrl}/{_id}");
                obj = JsonConvert.DeserializeObject<T>(str);
                return obj;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Boolean> Delete(string restUrl, string _id)
        {
            try
            {
                await client.Delete($"{_apiBaseUrl}{restUrl}/{_id}");
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Boolean> Create<T>(string restUrl, T obj)
        {
            var myContent = JsonConvert.SerializeObject(obj);

            try
            {
                await client.Post(_apiBaseUrl + restUrl, myContent);
                return true;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<Boolean> Update<T>(string restUrl, string _id, T obj)
        {
            string myContent = JsonConvert.SerializeObject(obj);
         
            try
            {
                await client.Put($"{_apiBaseUrl}{restUrl}/{_id}", myContent);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
