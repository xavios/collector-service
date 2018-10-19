using CollectorService.Client;
using System;
namespace CollectorService.Worker
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// The worker of the collector service.
    /// </summary>
    class ServiceWorker
    {
        /// <summary>
        /// The parallel processes count
        /// </summary>
        private int _parallelProcessesCount;

        /// <summary>
        /// The field gateway client
        /// </summary>
        private FieldGatewayClient _fieldGatewayClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceWorker"/> class.
        /// </summary>
        /// <param name="fieldGatewayClient">The field gateway client.</param>
        public ServiceWorker(FieldGatewayClient fieldGatewayClient)
        {
            // One should allways run based on the FSD
            _parallelProcessesCount = 1;
            _fieldGatewayClient = fieldGatewayClient;
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        public async void Process()
        {
            while (true)
            {
                var processTime = _fieldGatewayClient.GetProcessTime().Result;

                if(processTime != null)
                {
                    if(_parallelProcessesCount < AppSettings.Settings.ThresholdOfParallelRequests)
                    {
                        Interlocked.Increment(ref _parallelProcessesCount);
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                        Task.Factory.StartNew(ProcessOnOtherThread).ConfigureAwait(false);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                    }
                    System.Threading.Thread.Sleep(processTime.Value);
                }
                else
                {
                    await Task.Delay(AppSettings.Settings.TimespanBetweenCalls);
                }
            }
        }

        /// <summary>
        /// Processes the sub task.
        /// </summary>
        public void ProcessOnOtherThread()
        {
            IFieldGatewayClient fieldGatewayClient = new FieldGatewayClient();
            int? processTime = fieldGatewayClient.GetProcessTime().Result;

            while (processTime.HasValue)
            {

                Console.WriteLine($"Process count: {_parallelProcessesCount} --> Processing time: {processTime}");
                System.Threading.Thread.Sleep(processTime.Value);
                processTime = fieldGatewayClient.GetProcessTime().Result;
            }
            Interlocked.Decrement(ref _parallelProcessesCount);
        }
    }
}
