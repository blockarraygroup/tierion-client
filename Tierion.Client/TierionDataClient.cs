using System;
using System.Collections;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Tierion.Client
{
    public class TierionDataClient
    {
        public string EndPoint { get; set; }
        public string Username { get; set; }
        public string ApiKey { get; set; }

        public TierionDataClient(string username, string apiKey, string endPoint = "https://api.tierion.com/v1/")
        {
            EndPoint = endPoint;
            Username = username;
            ApiKey = apiKey;
        }

        protected virtual string GetUrl(string path)
        {
            return $"{this.EndPoint}{path}";
        }
        protected virtual async Task<T> Post<T>(string path, object request, bool includeAuth = true)
        {

            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (includeAuth)
            {
                httpClient.DefaultRequestHeaders.Add("X-Username", Username);
                httpClient.DefaultRequestHeaders.Add("X-Api-Key", ApiKey);
            }

            var body = JsonConvert.SerializeObject(request);
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync((string)GetUrl(path), content);
            T obj = JsonConvert.DeserializeObject<T>(
                response.Content.ReadAsStringAsync().Result);
            return obj;
        }
        protected virtual async Task<T> Put<T>(string path, object request, bool includeAuth = true)
        {

            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (includeAuth)
            {
                httpClient.DefaultRequestHeaders.Add("X-Username", Username);
                httpClient.DefaultRequestHeaders.Add("X-Api-Key", ApiKey);
            }

            var body = JsonConvert.SerializeObject(request);
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync((string)GetUrl(path), content);
            T obj = JsonConvert.DeserializeObject<T>(
                response.Content.ReadAsStringAsync().Result);
            return obj;
        }
        protected virtual async Task<T> Get<T>(string path, bool includeAuth = true)
        {

            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (includeAuth)
            {
                httpClient.DefaultRequestHeaders.Add("X-Username", Username);
                httpClient.DefaultRequestHeaders.Add("X-Api-Key", ApiKey);
            }

            var response = await httpClient.GetAsync((string)GetUrl(path));
            T obj = JsonConvert.DeserializeObject<T>(
                response.Content.ReadAsStringAsync().Result);
            return obj;
        }
        protected virtual async Task<T> Delete<T>(string path, bool includeAuth = true)
        {

            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (includeAuth)
            {
                httpClient.DefaultRequestHeaders.Add("X-Username", Username);
                httpClient.DefaultRequestHeaders.Add("X-Api-Key", ApiKey);
            }

            var response = await httpClient.DeleteAsync((string)GetUrl(path));
            T obj = JsonConvert.DeserializeObject<T>(
                response.Content.ReadAsStringAsync().Result);
            return obj;
        }

        /// <summary>
        /// This method returns a list of all datastores in your Tierion account.
        /// </summary>
        /// <returns></returns>
        public async Task<DataStore[]> GetAllDatastores()
        {
            return await Get<DataStore[]>("datastores");
        }
        /// <summary>
        /// This method returns the details of a specific datastore.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DataStore> GetDatastore(string id)
        {
            return await Get<DataStore>($"datastores/{id}");
        }
        /// <summary>
        /// This method creates a new Datastore in your Tierion account.
        /// </summary>
        /// <param name="dataStore"></param>
        /// <returns></returns>
        public async Task<DataStore> CreateDatastore(DataStore dataStore)
        {
            return await Post<DataStore>($"datastores", dataStore);
        }

        /// <summary>
        /// This method will update parameters of an existing Datastore.
        /// </summary>
        /// <param name="dataStore"></param>
        /// <returns></returns>
        public async Task<DataStore> UpdateDatastore(DataStore dataStore)
        {
            return await Put<DataStore>($"datastores/{dataStore.id}", dataStore);
        }
        /// <summary>
        /// This method will delete the Datastore. All associated data including the Datastore's records will be deleted as well.
        /// </summary>
        /// <param name="dataStore"></param>
        /// <returns></returns>
        public async Task<DataStore> DeleteDatastore(DataStore dataStore)
        {
            return await Delete<DataStore>($"datastores/{dataStore.id}");
        }
        /// <summary>
        /// This method will delete the Datastore. All associated data including the Datastore's records will be deleted as well.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DataStore> DeleteDatastore(string id)
        {
            return await Delete<DataStore>($"datastores/{id}");
        }


        /// <summary>
        /// This method will return a filtered summary of Records from a specific Datastore.
        /// </summary>
        /// <param name="datastoreId">A unique numeric identifier for the Datastore from which Records are being requested. [Required]</param>
        /// <param name="page">The page number of the Record results to view. If not specified, page will default to 1.</param>
        /// <param name="pageSize">The number of Records to include in the Record results, between 1 and 10000. If not specified, pageSize will default to 100.</param>
        /// <param name="startDate">A timestamp representing the start of the requested date range for the Record results. If not specified, startDate will default to creation date and time of the Datastore.</param>
        /// <param name="endDate">A timestamp representing the end of the requested date range for the Record results. If not specified, endDate will default to the current date and time.</param>
        /// <returns></returns>
        public async Task<RecordSet> GetAllRecords(string datastoreId, int page = 1, int pageSize = 100, DateTime? startDate = null, DateTime? endDate = null)
        {
            var sb = new StringBuilder();
            sb.Append($"datastoreId={datastoreId}");
            sb.Append($"&page={page}");
            sb.Append($"&pageSize={pageSize}");
            if (startDate != null)
            {
                sb.Append($"&startDate={startDate}");
            }
            if (endDate != null)
            {
                sb.Append($"&endDate={startDate}");
            }
            return await Get<RecordSet>($"records?{sb.ToString()}");
        }
        /// <summary>
        /// This method will return the details of a specific Record.
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<RecordDetail<TData>> GetRecord<TData>(string id)
        {
            return await Get<RecordDetail<TData>>($"records/{id}");
        }
        /// <summary>
        /// This method will queue a new Record for creation in a Datastore.
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<RecordDetail<TData>> CreateRecord<TData>(TData data)
        {
            return await Post<RecordDetail<TData>>($"records", data);
        }
        public async Task<RecordDetail<TData>> DeleteRecord<TData>(RecordDetail<TData> dataStore)
        {
            return await Delete<RecordDetail<TData>>($"records/{dataStore.id}");
        }
        public async Task<DataStore> DeleteRecord(string id)
        {
            return await Delete<DataStore>($"records/{id}");
        }
        /// <summary>
        /// This method will validate the format and contents of a blockchain receipt, and will confirm that the merkle root is stored in the blockchain.
        /// </summary>
        /// <param name="receipt">A Blockchain Receipt object to be verified. [Required]</param>
        /// <returns></returns>
        public async Task<ValidateReceiptResponse> ValidateReceipt(BlockChainReceipt receipt)
        {
            return await Post<ValidateReceiptResponse>($"validatereceipt", new ValidateReceiptRequest() { blockchain_receipt = receipt });
        }
        /// <summary>
        /// This method will validate the format and contents of a blockchain receipt, and will confirm that the merkle root is stored in the blockchain.
        /// </summary>
        /// <param name="receiptRequest">A Blockchain Receipt object to be verified. [Required]</param>
        /// <returns></returns>
        public async Task<ValidateReceiptResponse> ValidateReceipt(ValidateReceiptRequest receiptRequest)
        {
            return await Post<ValidateReceiptResponse>($"validatereceipt", receiptRequest);
        }


    }
}