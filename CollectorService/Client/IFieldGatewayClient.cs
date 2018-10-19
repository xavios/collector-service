using System.Threading.Tasks;

namespace CollectorService.Client
{
    /// <summary>
    /// The contract for the field gateway client.
    /// </summary>
    public interface IFieldGatewayClient
    {
        Task<int?> GetProcessTime();
    }
}
