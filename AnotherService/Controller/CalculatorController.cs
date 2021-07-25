using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SomeService.Protos;

namespace AnotherService.Controller
{
    [Route("api/")]
    public class CalculatorController : ControllerBase
    {
        private readonly SomeServiceApi.SomeServiceApiClient _someServiceApiClient;


        public CalculatorController(SomeServiceApi.SomeServiceApiClient someServiceApiClient) =>
            _someServiceApiClient = someServiceApiClient;


        [HttpGet("calc/add")]
        public async Task<int> AddNumbersAsync(int a, int b, CancellationToken ct) =>
            (await _someServiceApiClient.AddNumbersAsync(
                new AddNumbersRequest {
                    NumA = a,
                    NumB = b
                },
                cancellationToken: ct
            )).Sum;

        [HttpPost("calc/add")]
        public async Task<int> AddNumbersAsync([FromBody] int[] numbers, CancellationToken ct) =>
            (await _someServiceApiClient.AddNumbersFromArrayAsync(
                new AddNumbersFromArrayRequest {
                    Numbers = { numbers }
                },
                cancellationToken: ct
            )).Sum;
    }
}
