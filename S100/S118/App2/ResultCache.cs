using System.Text;
using System.Text.Json;
using Dapr.Client;
using Shared;

namespace App2;
public class ResultCache : IResultCache
{
    private readonly DaprClient _daprClient;
    private readonly string _keyOfKeys = "ResultKeys";
    private readonly string _storeName = "statestore";
    private readonly Func<string, Input, string> _keyGenerator;

    public ResultCache(DaprClient daprClient)
    {
        _daprClient = daprClient;
        //存储键由四则运算符号、计算数1，计算数2组成
        _keyGenerator = (method, input) => $"Result_{method}_{input.X}_{input.Y}";
    }

    //获取全部运算结果缓存键
    private async Task<HashSet<string>?> GetKeysAsync()
    {
        return await _daprClient.GetStateAsync<HashSet<string>>
            (storeName: _storeName, key: _keyOfKeys) ?? new HashSet<string>();
    }

    public async Task ClearAsync(params string[] methods)
    {
        var keys = await GetKeysAsync();
        if (keys != null)
        {
            //筛选出使用了methods中包含的四则运算的运算结果缓存键
            var selectedKeys = keys.Where(it => methods.Any(m => it.StartsWith($"Result_{m}"))).ToArray();
            if (selectedKeys.Length > 0)
            {
                //建立多个删除缓存键对应缓存的状态事务请求
                var operations = selectedKeys
                    .Select(it => new StateTransactionRequest(key: it, value: null,
                    operationType: StateOperationType.Delete))
                    .ToList();
                
                //将准备删除的缓存键从列表中删除
                operations.ForEach(it => keys.Remove(it.Key));
                
                var value = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(keys));
                
                //更新缓存健列表缓存
                operations.Add(new StateTransactionRequest(key: _keyOfKeys, value: value, operationType: StateOperationType.Upsert));
                
                await _daprClient.ExecuteStateTransactionAsync(storeName: _storeName, operations: operations);
            }
        }
    }

    //将
    public Task<Output> GetAsync(string method, Input input)
    {
        var key = _keyGenerator(method, input);
        return _daprClient.GetStateAsync<Output>(storeName: _storeName, key: key);
    }

    public async Task SaveAsync(string method, Input input, Output output)
    {
        var key = _keyGenerator(method, input);

        //HashSet 类型实现了一个无序的、唯一值的集合。
        var keys = await GetKeysAsync() ?? new HashSet<string>();
        keys.Add(key);

        //包含两个存储状态事务请求的数组
        var operations = new StateTransactionRequest[2];

        var value = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(output));
        
        //新建运算结果缓存
        operations[0] = new StateTransactionRequest(key: key, value: value, operationType: StateOperationType.Upsert);

        value = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(keys));
        //更新缓存健列表缓存
        operations[1] = new StateTransactionRequest(key: _keyOfKeys, value: value, operationType: StateOperationType.Upsert);

        await _daprClient.ExecuteStateTransactionAsync(storeName: _storeName, operations: operations);
    }
}