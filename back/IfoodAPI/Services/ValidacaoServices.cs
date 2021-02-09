using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IfoodAPI.Services
{
    public class ValidacaoServices
    {

        /// <summary>
        /// Método para validar o login contra API externa 
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public async Task<int> ValidarLogin(string Username, string Password)
        {
            try
            {

                var client = new RestClient("https://dev.sitemercado.com.br/api/login");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);

                ////Monta o token base64 da requisição
                string strToken = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(Username + ":" + Password));
                request.AddHeader("Authorization", "Basic " + strToken);
                request.AddHeader("Content-Type", "application/json");

                IRestResponse response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    var returnAPI = JObject.Parse(response.Content);
                    string a = returnAPI.GetValue("success").ToString();

                    if (returnAPI.GetValue("success").ToString() == "True")
                    {

                        return 200;
                    }
                    else
                    {
                        return 401;
                    }
                }
                else
                {
                    return 400;
                }
            }            
            catch (Exception ex)
            {
                throw;
            }
}
    }
}
