using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AssistantScrapMechanic.Domain.Result;
using Newtonsoft.Json;

namespace AssistantScrapMechanic.Integration.Repository
{
    public class BaseExternalApiRepository
    {
        public async Task<ResultWithValue<T>> Get<T>(string url, Action<HttpRequestHeaders> manipulateHeaders = null, bool useJsonApiSerializerSettings = true)
        {
            ResultWithValue<string> webGetResult = await Get(url, manipulateHeaders);
            if (webGetResult.HasFailed) return new ResultWithValue<T>(false, default, webGetResult.ExceptionMessage);

            try
            {
                T result = JsonConvert.DeserializeObject<T>(webGetResult.Value);
                return new ResultWithValue<T>(true, result, string.Empty);
            }
            catch (Exception ex)
            {
                return new ResultWithValue<T>(false, default, ex.Message);
            }
        }

        public async Task<ResultWithValue<string>> Get(string url, Action<HttpRequestHeaders> manipulateHeaders = null)
        {
            HttpClient client = new HttpClient();
            try
            {
                manipulateHeaders?.Invoke(client.DefaultRequestHeaders);
                HttpResponseMessage httpResponse = await client.GetAsync(url);
                httpResponse.EnsureSuccessStatusCode();
                string content = await httpResponse.Content.ReadAsStringAsync();
                return new ResultWithValue<string>(true, content, string.Empty);
            }
            catch (Exception ex)
            {
                return new ResultWithValue<string>(false, default, ex.Message);
            }
            finally
            {
                client.Dispose();
            }
        }

        public async Task<ResultWithValue<string>> Post(string url, string postContentString, Action<HttpRequestHeaders> manipulateHeaders = null)
        {
            HttpClient client = new HttpClient();
            try
            {
                manipulateHeaders?.Invoke(client.DefaultRequestHeaders);
                StringContent postContent = new StringContent(postContentString, Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponse = await client.PostAsync(url, postContent);
                httpResponse.EnsureSuccessStatusCode();
                string content = await httpResponse.Content.ReadAsStringAsync();
                return new ResultWithValue<string>(true, content, string.Empty);
            }
            catch (Exception ex)
            {
                return new ResultWithValue<string>(false, default, ex.Message);
            }
            finally
            {
                client.Dispose();
            }
        }

        public async Task<ResultWithValue<string>> Put(string url, string putContentString, Action<HttpRequestHeaders> manipulateHeaders = null)
        {
            HttpClient client = new HttpClient();
            try
            {
                manipulateHeaders?.Invoke(client.DefaultRequestHeaders);
                StringContent putContent = new StringContent(putContentString, Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponse = await client.PutAsync(url, putContent);
                httpResponse.EnsureSuccessStatusCode();
                string content = await httpResponse.Content.ReadAsStringAsync();
                return new ResultWithValue<string>(true, content, string.Empty);
            }
            catch (Exception ex)
            {
                return new ResultWithValue<string>(false, default, ex.Message);
            }
            finally
            {
                client.Dispose();
            }
        }
    }
}
