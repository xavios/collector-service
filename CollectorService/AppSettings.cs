namespace CollectorService
{
    using Microsoft.Extensions.Configuration;
    using System;
    using System.IO;

    /// <summary>
    /// Singleton class for the application settings.
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// The only instance
        /// </summary>
        private static AppSettings _instance;

        /// <summary>
        /// Gets or sets the threshold of parallel requests.
        /// </summary>
        /// <value>
        /// The threshold of parallel requests.
        /// </value>
        public int ThresholdOfParallelRequests { get; set; }

        /// <summary>
        /// Gets or sets the provider service address.
        /// </summary>
        /// <value>
        /// The provider service address.
        /// </value>
        public Uri ProviderServiceAddress { get; set; }

        /// <summary>
        /// Gets or sets the timespan between calls.
        /// </summary>
        /// <value>
        /// The timespan between calls.
        /// </value>
        public TimeSpan TimespanBetweenCalls { get; set; }

        /// <summary>
        /// Prevents a default instance of the <see cref="AppSettings"/> class from being created.
        /// </summary>
        private AppSettings()
        {

        }

        /// <summary>
        /// Initializes the <see cref="AppSettings"/> class.
        /// </summary>
        static AppSettings()
        {

        }

        /// <summary>
        /// Gets the only instance.
        /// </summary>
        /// <value>
        /// The only instance.
        /// </value>
        public static AppSettings Settings
        {
            get
            {
                if (_instance == null)
                {
                    IConfigurationBuilder builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appSettings.json");

                    IConfiguration configuration = builder.Build();
                    _instance= new AppSettings();
                    configuration.Bind(_instance);
                }
                return _instance;
            }
        }
    }
}
