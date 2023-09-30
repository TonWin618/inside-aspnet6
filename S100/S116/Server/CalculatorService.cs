using Grpc.Core;

namespace App;
//在proto文件中为Calculator服务定义的4个操作会转换成CalculatorBase类型中的4个虚方法
public class CalculatorService : Calculator.CalculatorBase
{
    private readonly ILogger _logger;
    //可以使用依赖注入
    public CalculatorService(ILogger<CalculatorService> logger)
    {
        _logger = logger;
    }

        public override Task<OutputMessage> Add(InputMessage request, ServerCallContext context)
            => InvokeAsync((op1, op2) => op1 + op2, request);
        public override Task<OutputMessage> Substract(InputMessage request, ServerCallContext context)
            => InvokeAsync((op1, op2) => op1 - op2, request);
        public override Task<OutputMessage> Multiply(InputMessage request, ServerCallContext context)
            => InvokeAsync((op1, op2) => op1 * op2, request);
        public override Task<OutputMessage> Divide(InputMessage request, ServerCallContext context)
            => InvokeAsync((op1, op2) => op1 / op2, request);

    private Task<OutputMessage> InvokeAsync(Func<int, int, int> calculate, InputMessage input)
    {
        OutputMessage output;
        try
        {
            output = new OutputMessage
            {
                Status = 0,
                Result = calculate(input.X, input.Y)
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Calculation error");
            output = new OutputMessage { Status = 1, Error = ex.ToString() };
        }
        return Task.FromResult(output);
    }
}