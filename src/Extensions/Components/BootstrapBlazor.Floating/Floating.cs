using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.JSInterop;

namespace BootstrapBlazor.Components;

/// <summary>
/// 浮动层 组件
/// </summary>
/// <typeparam name="TRef">参照元素类型</typeparam>
public class Floating<TRef> : IdComponentBase, IAsyncDisposable
{
    /// <summary>
    /// client计算的位置参数
    /// </summary>
    private FloatingState State { get; set; } = new();

    /// <summary>
    /// 是否需要重新计算位置
    /// </summary>
    private bool ShouldCompute { get; set; }

    /// <summary>
    /// 是否需要清除自动更新
    /// </summary>
    private bool ShouldCleanup { get; set; }

    /// <summary>
    /// 是否客户端更新了状态
    /// </summary>
    private bool ClientUpdated { get; set; }

    /// <summary>
    /// 浮动层显示状态控制
    /// </summary>
    private bool Show { get; set; }

    private ElementReference _floating;

    private FloatingModule<Floating<TRef>>? _jsModuleRef;

    private DotNetObjectReference<Floating<TRef>>? _dotNetObjectRef;

    /// <summary>
    /// 参照元素 或选择器，从Target解析而来
    /// </summary>
    private ElementReference? _target;
    private string? _selecter;

    private string? GetOverlayStyleString() => CssBuilder.Default()
        .AddStyleFromAttributes(AdditionalAttributes)
        .AddClass($"display:none;", !ClientUpdated && !DefaultShow)
        .AddClass($"position:{Strategy.ToDescriptionString()};", ClientUpdated)
        .AddClass($"left:{State.FloatingLeft}px;", ClientUpdated && State.FloatingLeft.HasValue)
        .AddClass($"top:{State.FloatingTop}px;", ClientUpdated && State.FloatingTop.HasValue)
        .Build();

    private string? GetArrowStyleString() => CssBuilder.Default()
        .AddClass($"position:{Strategy.ToDescriptionString()};", ClientUpdated)
        .AddClass($"left:{State.ArrowLeft}px;", ClientUpdated && State.ArrowLeft.HasValue)
        .AddClass($"top:{ State.ArrowTop}px;", ClientUpdated && State.ArrowTop.HasValue)
        .AddClass($"{ State.ArrowOffset}:{ArrowOffset}px;", ClientUpdated && !string.IsNullOrEmpty(State.ArrowOffset))
        .Build();

    /// <summary>
    /// 获得/设置 浮动层 TagName 属性 默认为 div
    /// </summary>
    [Parameter]
    [NotNull]
    public string TagName { get; init; } = "div";

    /// <summary>
    /// 获得/设置 控制浮动层 的初始可见性
    /// </summary>
    [Parameter]
    public bool DefaultShow { get; init; }

    /// <summary>
    /// 获得/设置 是否自动更新位置 默认 true
    /// </summary>
    [Parameter]
    public bool AutoUpdate { get; init; } = true;

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

    private PositionStrategy _strategy;
    /// <summary>
    /// 获得/设置 浮动层显示时 使用的定位模式 默认为 绝对定位
    /// </summary>
    [Parameter]
    public PositionStrategy Strategy
    {
        get => _strategy;
        set
        {
            ShouldCompute = _strategy != value;
            _strategy = value;
        }
    }

    private Placement _placement;
    /// <summary>
    /// 获得/设置 浮动层相对于它的参考元素位置。
    /// </summary>
    [Parameter]
    public Placement Placement
    {
        get => _placement;
        set
        {
            ShouldCompute = _placement != value;
            _placement = value;
        }
    }

    /// <summary>
    /// 获得/设置 主轴：浮动元素和参考元素之间的距离。
    /// </summary>
    [Parameter]
    public int? MainAxis { get; set; }

    /// <summary>
    /// 获得/设置 配置需要填充的宽度，沿指定轴移动浮动元素以使其保持在视图中。
    /// </summary>
    [Parameter]
    public int? ShiftPadding { get; set; }

    /// <summary>
    /// 获得/设置 是否使用 提示箭头 浮动层必须预先设置为 绝对定位
    /// </summary>
    [Parameter]
    public bool UseArrow { get; set; }

    /// <summary>
    /// 获得/设置 箭头的偏移量
    /// </summary>
    [Parameter]
    public int? ArrowOffset { get; set; }

    /// <summary>
    /// 获得/设置 提示箭头的 样式
    /// </summary>
    [Parameter]
    public string? ArrowStyleClass { get; set; }

    private void OnFloatingChanged(FloatingEventArgs e)
    {
        if (e.Show != Show)
        {
            if (Delay > 0)
            {
                Task.Delay(Delay);
            }

            Show = e.Show;

            //发生变化时，重置中间变量
            ClientUpdated = false;
            State.Reset();

            //叠加复原时 需要清除自动更新
            ShouldCleanup = !e.Show && AutoUpdate;

            //叠加显示时 需要重新计算位置
            ShouldCompute = e.Show == true;
        }
    }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        Target.FloatingChanged += OnFloatingChanged;

        if (Target.Reference is ElementReference element)
        {
            _target = element;
        }
        if (Target.Reference is IdComponentBase component)
        {
            _selecter = $"#{component.Id}";
        }
        else if (Target.Reference is string val)
        {
            _selecter = val;
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
        }

        if (ShouldCompute)
        {
            if (_jsModuleRef is not null)
            {
                await _jsModuleRef.CreateFloating(_dotNetObjectRef, _floating, _target, new
                {
                    Hidden = !DefaultShow && !ClientUpdated,
                    TargetSelecter = _selecter,
                    Placement = Placement.ToDescriptionString(),
                    MainAxis,
                    ShiftPadding,
                    UseArrow,
                    AutoUpdate
                });
            }

            ClientUpdated = false;
            ShouldCompute = false;
        }

        if (ShouldCleanup)
        {
            if (_jsModuleRef?.Available == true)
            {
                await _jsModuleRef.CleanupFloating(_floating);
            }
            ShouldCleanup = false;
        }
    }

    /// <summary>
    /// 客户端更新样式
    /// </summary>
    [JSInvokable]
    public async Task ApplyStyles(decimal? x, decimal? y, decimal? arrowX, decimal? arrowY, string? arrowOffset)
    {
        //只有处于显示状态，且位置发生变化时重新渲染
        if (Show && (x != State.FloatingLeft || y != State.FloatingTop))
        {
            ClientUpdated = true;
            State.UpdateFloating(x, y);
            State.UpdateArrow(arrowX, arrowY, arrowOffset);
            await InvokeAsync(StateHasChanged);
        }
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
                await _jsModuleRef.CleanupFloating(_floating);
                await _jsModuleRef.DisposeAsync();
                _jsModuleRef = null;
            }

            if (_dotNetObjectRef != null)
            {
                _dotNetObjectRef.Dispose();
                _dotNetObjectRef = null;
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
