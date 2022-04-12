using System.Reflection;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TValue"></typeparam>
public sealed class FloatingModule<TValue> : IAsyncDisposable where TValue : class
{
    /// <summary>
    /// 版本号
    /// </summary>
    public static Lazy<Version?> Version => new(() => Assembly.GetExecutingAssembly()?.GetName()?.Version);

    /// <summary>
    /// 模块名称
    /// </summary>
    public static string ModuleFileName => $"./_content/BootstrapBlazor.Floating/floating.bundle.js?v={Version.Value}";

    /// <summary>
    /// 指示是否可用。
    /// </summary>
    public bool Available => _module is not null;

    private IJSObjectReference? _module;

    private readonly Lazy<ValueTask<IJSObjectReference>> _moduleTask;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="jsRuntime"></param>
    public FloatingModule(IJSRuntime jsRuntime)
        => _moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>("import", ModuleFileName));

    private async Task<bool> CreateModule()
    {
        if (!Available)
        {
            _module = await _moduleTask.Value;
        }

        return Available;
    }

    /// <summary>
    /// 创建浮动层对象
    /// </summary>
    /// <param name="interop"></param>
    /// <param name="floating"></param>
    /// <param name="target"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    public async ValueTask CreateFloating(DotNetObjectReference<TValue>? interop, ElementReference floating, ElementReference? target, object config)
    {
        if (interop == null)
        {
            return;
        }

        if (await CreateModule())
        {
            await _module!.InvokeVoidAsync("createFloating", interop, floating, target, config);
        }
    }

    /// <summary>
    /// 清除浮动
    /// </summary>
    /// <param name="floating"></param>
    /// <returns></returns>
    public async ValueTask CleanupFloating(ElementReference floating)
    {
        if (await CreateModule())
        {
            await _module!.InvokeVoidAsync("cleanupFloating", floating);
        }
    }

    /// <summary>
    /// DisposeAsync 方法
    /// </summary>
    /// <param name="disposing"></param>
    private async ValueTask DisposeAsync(bool disposing)
    {
        if (disposing && Available)
        {
            await _module!.DisposeAsync();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }
}
