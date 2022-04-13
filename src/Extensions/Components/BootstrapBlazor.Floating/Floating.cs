using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.JSInterop;

namespace BootstrapBlazor.Components;

/// <summary>
/// 浮动层 组件
/// </summary>
/// <typeparam name="TRef">参照元素类型</typeparam>
public class Floating<TRef> : IdComponentBase, IAsyncDisposable
{
    private FloatingConfig Config { get; set; } = new();

    private ElementReference _floating;

    private FloatingModule<Floating<TRef>>? _jsModuleRef;

    private DotNetObjectReference<Floating<TRef>>? _dotNetObjectRef;

    private Queue<Func<ValueTask>>? _executeAfterRenderQueue;

    private string? GetOverlayStyleString() => CssBuilder.Default()
        .AddStyleFromAttributes(AdditionalAttributes)
        .AddClass($"display:none;", InitialState == FloatingState.Hidden && !Config.Show)
        .AddClass($"position:{Strategy.ToDescriptionString()};", Config.Show)
        .AddClass($"left:{Config.Left}px;", Config.Show)
        .AddClass($"top:{Config.Top}px;", Config.Show)
        .Build();

    private string? GetArrowStyleString() => CssBuilder.Default()
        .AddClass($"position:{Strategy.ToDescriptionString()};", Config.Show)
        .AddClass($"left:{Config.ArrowLeft}px;", Config.Show)
        .AddClass($"top:{ Config.ArrowTop}px;", Config.Show)
        .AddClass($"{ Config.ArrowSide}:{ArrowOffset}px;", Config.Show)
        .Build();

    /// <summary>
    /// 获得/设置 浮动层 TagName 属性 默认为 div
    /// </summary>
    [Parameter]
    public string TagName { get; init; } = "div";


    /// <summary>
    /// 获得/设置 是否使用虚拟元素
    /// </summary>
    [Parameter]
    public bool UseVirtualElement
    {
        get => Config.UseVirtualElement;
        init => Config.UseVirtualElement = value;
    }

    /// <summary>
    /// 获得/设置 浮动层的初始可见性
    /// </summary>
    [Parameter]
    public FloatingState InitialState
    {
        get => Config.InitialState;
        init => Config.InitialState = value;
    }

    /// <summary>
    /// 获得/设置 是否自动更新位置 默认 true
    /// </summary>
    [Parameter]
    public bool AutoUpdate
    {
        get => Config.AutoUpdate;
        init => Config.AutoUpdate = value;
    }

    /// <summary>
    /// 获得/设置 触发后显示和隐藏浮动层的 毫秒延迟量
    /// </summary>
    [Parameter]
    public int Delay { get; set; }

    /// <summary>
    /// 获得/设置 浮动层的参考元素
    /// </summary>
    [Parameter]
    public FloatingContext<TRef> Target { get; set; } = new();

    /// <summary>
    /// 获得/设置 子组件内容 
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 浮动层显示时 使用的定位模式 默认为 绝对定位
    /// </summary>
    [Parameter]
    public PositionStrategy Strategy
    {
        get => Config.Strategy;
        init => Config.Strategy = value;
    }

    /// <summary>
    /// 获得/设置 浮动层相对于它的参考元素位置。
    /// </summary>
    [Parameter]
    public Placement Placement
    {
        get => Config.Placement;
        set
        {
            Config.Placement = value;
            if (Config.Show)
            {
                //组件内部的内容可能会发生变化，必须延迟到组件的内容渲染完成后，再计算位置
                ExecuteAfterRender(ComputeFloating);
            }
        }
    }

    /// <summary>
    /// 获得/设置 主轴：浮动元素和参考元素之间的距离。
    /// </summary>
    [Parameter]
    public int MainAxis
    {
        get => Config.MainAxis;
        init => Config.MainAxis = value;
    }

    /// <summary>
    /// 获得/设置 配置需要填充的宽度，沿指定轴移动浮动元素以使其保持在视图中。
    /// </summary>
    [Parameter]
    public int ShiftPadding
    {
        get => Config.ShiftPadding;
        init => Config.ShiftPadding = value;
    }

    /// <summary>
    /// 获得/设置 是否使用 提示箭头 浮动层必须预先设置为 绝对定位
    /// </summary>
    [Parameter]
    public bool UseArrow
    {
        get => Config.UseArrow;
        init => Config.UseArrow = value;
    }

    /// <summary>
    /// 获得/设置 箭头的偏移量
    /// </summary>
    [Parameter]
    public int ArrowOffset
    {
        get => Config.ArrowOffset;
        init => Config.ArrowOffset = value;
    }

    /// <summary>
    /// 获得/设置 提示箭头的 样式
    /// </summary>
    [Parameter]
    public string? ArrowStyleClass { get; init; }

    private async void OnFloatingChanged(FloatingEventArgs e)
    {
        Config.ClientX = e.ClientX;
        Config.ClientY = e.ClientY;

        if (e.Show != Config.Show)
        {
            if (Delay > 0)
            {
                await Task.Delay(Delay);
            }

            if (e.Show)
            {
                //重新计算位置
                ExecuteAfterRender(ComputeFloating);
            }
            else
            {
                Config.Show = false;
                //可能需要优化，需要保证先解除浮动后，再更新组件
                await CleanupFloating();
                StateHasChanged();
            }
        }
        else if (e.Show && UseVirtualElement)
        {
            //重新计算位置
            ExecuteAfterRender(ComputeFloating);
        }
    }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        Target.FloatingChanged += OnFloatingChanged;

        if (Target.Reference is ElementReference)
        {
            Config.ReferenceType = FloatingRefType.ElementReference;
        }
        else if (Target.Reference is string)
        {
            Config.ReferenceType = FloatingRefType.CssSelecter;
        }
        else if (Target.Reference is IdComponentBase component)
        {
            Config.ReferenceType = FloatingRefType.IdComponentBase;
            Config.RefElementId = component.Id;
        }
    }

    /// <summary>
    /// BuildRenderTree 方法
    /// </summary>
    /// <param name="builder"></param>
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(1, TagName);
        if (AdditionalAttributes != null)
        {
            builder.AddMultipleAttributes(2, AdditionalAttributes);
        }
        builder.AddAttribute(3, "style", GetOverlayStyleString());
        builder.AddAttribute(4, "data-id", Id);
        if (ChildContent != null)
        {
            builder.AddContent(5, ChildContent);
        }
        if (UseArrow)
        {
            builder.OpenElement(6, "div");
            if (!string.IsNullOrEmpty(ArrowStyleClass))
            {
                builder.AddAttribute(7, "class", ArrowStyleClass);
            }
            builder.AddAttribute(8, "style", GetArrowStyleString());
            builder.AddAttribute(9, "data-element", "arrow");
            builder.CloseElement();
        }
        builder.AddElementReferenceCapture(10, floating => { _floating = floating; });
        builder.CloseElement();
    }

    /// <summary>
    /// OnAfterRenderAsync 方法
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            _dotNetObjectRef = DotNetObjectReference.Create(this);
            _jsModuleRef = new FloatingModule<Floating<TRef>>(JSRuntime);

            if (InitialState == FloatingState.Overlay)
            {
                await ComputeFloating();
            }
        }

        if (_executeAfterRenderQueue?.Count > 0)
        {
            while (_executeAfterRenderQueue.Count > 0)
            {
                await _executeAfterRenderQueue.Dequeue()();
            }
        }
    }

    /// <summary>
    /// 客户端更新样式
    /// </summary>
    [JSInvokable]
    public async Task ApplyStyles(decimal? x, decimal? y, decimal? arrowX, decimal? arrowY, string? arrowSide)
    {
        //只有位置发生变化时重新渲染
        //if (x != Config.Left || y != Config.Top || !Config.Show)
        //{
        Config = Config with { Left = x, Top = y };
        Config = Config with { ArrowLeft = arrowX, ArrowTop = arrowY, ArrowSide = arrowSide };
        Config.Show = true;
        await InvokeAsync(StateHasChanged);
        //}
    }

    private async ValueTask ComputeFloating()
    {
        if (_jsModuleRef is not null)
        {
            await _jsModuleRef.ComputeFloating(_dotNetObjectRef, Target.Reference, _floating, Config);
        }
    }

    private async ValueTask CleanupFloating()
    {
        if (_jsModuleRef is not null && AutoUpdate)
        {
            await _jsModuleRef.CleanupFloating(_floating);
        }
    }

    private void ExecuteAfterRender(Func<ValueTask> action)
    {
        _executeAfterRenderQueue ??= new();
        _executeAfterRenderQueue.Enqueue(action);
    }

    /// <summary>
    /// DisposeAsync 方法
    /// </summary>
    /// <param name="disposing"></param>
    private async ValueTask DisposeAsync(bool disposing)
    {
        if (disposing)
        {
            Target.FloatingChanged -= OnFloatingChanged;
            if (_jsModuleRef is not null)
            {
                await CleanupFloating();
                await _jsModuleRef.DisposeAsync();
            }

            if (_dotNetObjectRef != null)
            {
                _dotNetObjectRef.Dispose();
            }
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
