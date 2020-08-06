using AvalieMe.APP.Models;
using MonkeyCache.SQLite;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AvalieMe.APP.API
{
    public static class Service
    {
        public const string apiUrl = "https://avalieme-api.conveyor.cloud/";

        public static async Task<List<Usuario>> ObterUsuarios()
        {
            try
            {
                var client = new HttpClient();
                var url = apiUrl + "api/usuario";
                
                var response = await client.GetStringAsync(url);

                var usuarios = JsonConvert.DeserializeObject<List<Usuario>>(response);

                return usuarios;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static async Task<Usuario> AutenticarUsuario(string login, string senha)
        {
            try
            {
                var token = await ObterToken(login, senha);

                var client = new HttpClient();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var url = apiUrl + $"api/usuario?login={login}&senha={senha}";

                var response = await client.GetStringAsync(url);

                var usuario = JsonConvert.DeserializeObject<Usuario>(response);

                return usuario;
            }
            catch (Exception ex)
            {
                return new Usuario() { Message = ex.Message };
            }
        }


        public static async Task<Usuario> SalvarUsuario(Usuario usuario)
        {
            try
            {
                var client = new HttpClient();
                var url = apiUrl + $"api/usuario";

                var content = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, content);

                var json = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<Usuario>(json);
            }
            catch (Exception ex)
            {
                return new Usuario() { Message = ex.Message };
            }
        }


        private static async Task<string> ObterToken(string login, string senha)
        {
            var token = Barrel.Current.Get<string>("access_token");

            if (string.IsNullOrEmpty(token))
            {
                if (string.IsNullOrWhiteSpace(login))
                {
                    login = Barrel.Current.Get<string>("access_token");
                    senha = Barrel.Current.Get<string>("access_token");
                }

                var client = new HttpClient();
                var url = apiUrl + $"token";

                var dicionario = new Dictionary<string, string>
                {
                    { "username", login },
                    { "password", senha },
                    { "grant_type", "password" }
                };

                var req = new HttpRequestMessage(HttpMethod.Get, url) { Content = new FormUrlEncodedContent(dicionario) };

                var response = await client.SendAsync(req);

                var json = await response.Content.ReadAsStringAsync();

                var objeto = JsonConvert.DeserializeObject<TokenAcesso>(json);

                Barrel.Current.Add("access_token", objeto.access_token, TimeSpan.FromSeconds(objeto.expires_in));

                return objeto.access_token;
            }

            return token;
        }
    }
}
