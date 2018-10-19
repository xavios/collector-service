namespace CollectorService
{
    using CollectorService.Client;
    using CollectorService.Worker;
    using System;

    /// <summary>
    /// The main entry class.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            Console.WriteLine("András Ács's solution to the Collector Service problem.");
            Console.WriteLine($"Address: {AppSettings.Settings.ProviderServiceAddress}");
            Console.WriteLine($"Max parallel requests: {AppSettings.Settings.ThresholdOfParallelRequests}{Environment.NewLine}");

            FieldGatewayClient fieldGatewayClient = new FieldGatewayClient();
            var serviceWorker = new ServiceWorker(fieldGatewayClient);
            serviceWorker.Process();
        }
    }
}
