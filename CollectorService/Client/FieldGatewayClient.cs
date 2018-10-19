namespace CollectorService.Client
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// The Filed Gateway client.
    /// </summary>
    /// <seealso cref="CollectorService.Client.IFieldGatewayClient" />
    class FieldGatewayClient : IFieldGatewayClient
    {
        /// <summary>
        /// The HTTP client
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldGatewayClient"/> class.
        /// </summary>
        public FieldGatewayClient()
        {
            _httpClient = new HttpClient();
        }

        /// <summary>
        /// Gets the process time.
        /// </summary>
        /// <returns>The process time required to process the response data.</returns>
        public async Task<int?> GetProcessTime()
        {
            var response = await _httpClient.GetAsync(AppSettings.Settings.ProviderServiceAddress)
                .ConfigureAwait(true);

            if (response.StatusCode == HttpStatusCode.NoContent)
                return null;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                int processTime = int.MinValue;
                string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (!int.TryParse(content, out processTime))
                    return null;

                return processTime;
            }

            return null;
        }
    }
}
