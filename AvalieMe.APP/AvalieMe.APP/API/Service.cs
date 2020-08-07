using AvalieMe.APP.Models;
using MonkeyCache.SQLite;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AvalieMe.APP.API
{
    public static class Service
    {
        public const string apiUrl = "https://avalieme-api.conveyor.cloud/";

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

        public static async Task<string> Teste()
        {
            try
            {
                var token = await ObterToken(null, null);

                var client = new HttpClient();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var url = apiUrl + $"api/teste";

                var usuario = Barrel.Current.Get<Usuario>("usuario");

                var base64 = "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";

                var novoTeste = new NovoTeste()
                {
                    UserId = usuario.Id,
                    Categoria = "Encontro",
                    MultiplasPessoas = false,
                    Imagem = Convert.FromBase64String(base64)
                };

                var content = new StringContent(JsonConvert.SerializeObject(novoTeste), Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, content);

                var json = await response.Content.ReadAsStringAsync();

                var retorno = JsonConvert.DeserializeObject<RetornoAPI>(json);

                return null;
            }
            catch (Exception ex)
            {
                return new RetornoAPI().Message;
            }
        }

        private static async Task<string> ObterToken(string login, string senha)
        {
            var token = Barrel.Current.Get<string>("access_token");

            if (string.IsNullOrEmpty(token))
            {
                if (string.IsNullOrWhiteSpace(login))
                {
                    var usuario = Barrel.Current.Get<Usuario>("login");

                    login = usuario.Email;
                    senha = usuario.Senha;
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

                Barrel.Current.Add("access_token", objeto.access_token, TimeSpan.FromHours(1));

                return objeto.access_token;
            }

            return token;
        }
    }
}
