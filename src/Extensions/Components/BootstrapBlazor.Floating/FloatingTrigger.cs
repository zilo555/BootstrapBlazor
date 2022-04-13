using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace BootstrapBlazor.Components;

/// <summary>
/// 浮动层 触发组件
/// </summary>
public sealed class FloatingTrigger : IdComponentBase
{
    /// <summary>
    /// 获得/设置 触发组件内容
    /// </summary>
    [Parameter]
    public RenderFragment? TriggerTemplate { get; set; }

    /// <summary>
    /// 获得/设置 浮动层
    /// </summary>
    [Parameter]
    public RenderFragment? FloatingTemplate { get; set; }

    /// <summary>
    /// 获得/设置 触发浮动层 可见性的事件 默认为悬停 和 焦点 事件
    /// </summary>
    [Parameter]
    public TriggerEvent EventTrigger { get; set; } = TriggerEvent.Hover | TriggerEvent.Focus;

    /// <summary>
    /// 获得/设置 是否无关显示状态，强制触发事件 默认不强制，通常用于组件封装
    /// </summary>
    [Parameter]
    public bool ForceTrigger { get; init; }

    /// <summary>
    /// 获得/设置 触发组件 TagName 属性 默认为 div
    /// 如果不想外包装一层容器，建议使用 Floating 自行包装
    /// </summary>
    [Parameter]
    [NotNull]
    public string TagName { get; set; } = "div";

    /// <summary>
    /// 获得/设置 浮动层隐藏时 触发回调
    /// </summary>
    [Parameter]
    public Func<Task>? OnReset { get; set; }

    /// <summary>
    /// 获得/设置 浮动层显示时 触发回调
    /// </summary>
    [Parameter]
    public Func<Task>? OnOverlay { get; set; }

    /// <summary>
    /// 获得/设置 浮动层 TagName 属性 默认为 div
    /// </summary>
    [Parameter]
    [NotNull]
    public string FloatingTagName { get; set; } = "div";

    /// <summary>
    /// 获得/设置 浮动层的 样式
    /// </summary>
    [Parameter]
    public string? FloatingStyleClass { get; set; }

    /// <summary>
    /// 获得/设置 是否跟随触发
    /// </summary>
    [Parameter]
    public bool UseFollowTrigger { get; set; }

    /// <summary>
    /// 获得/设置 是否使用虚拟元素
    /// </summary>
    [Parameter]
    public bool UseVirtualElement { get; init; }

    /// <summary>
    /// 获得/设置 控制浮动层 的初始可见性
    /// </summary>
    [Parameter]
    public FloatingState InitialState { get; init; }

    /// <summary>
    /// 获得/设置 是否自动更新位置 默认 true
    /// </summary>
    [Parameter]
    public bool AutoUpdate { get; set; } = true;

    /// <summary>
    /// 获得/设置 触发后显示和隐藏浮动层的 毫秒延迟量
    /// </summary>
    [Parameter]
    public int Delay { get; set; }

    /// <summary>
    /// 获得/设置 浮动层显示时 使用的定位模式 默认为 绝对定位
    /// </summary>
    [Parameter]
    public PositionStrategy Strategy { get; set; }

    /// <summary>
    /// 获得/设置 浮动层相对于它的参考元素位置。
    /// </summary>
    [Parameter]
    public Placement Placement { get; set; } = Placement.Right;

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

    private FloatingContext<ElementReference>? _floatingContext;

    /// <summary>
    /// OnInitialized
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        _floatingContext = new(ForceTrigger || UseVirtualElement);
    }

    /// <summary>
    /// BuildRenderTree
    /// </summary>
    /// <param name="builder"></param>
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, TagName);
        if (AdditionalAttributes != null)
        {
            builder.AddMultipleAttributes(1, AdditionalAttributes);
        }

        if (EventTrigger.HasFlag(TriggerEvent.Hover))
        {
            builder.AddAttribute(2, "onmouseenter", EventCallback.Factory.Create<MouseEventArgs>(this, Overlay));
            builder.AddAttribute(3, "onmouseleave", EventCallback.Factory.Create<MouseEventArgs>(this, Reset));
        }

        if (EventTrigger.HasFlag(TriggerEvent.Click))
        {
            builder.AddAttribute(4, "onclick", EventCallback.Factory.Create<MouseEventArgs>(this, Toggle));
        }

        if (EventTrigger.HasFlag(TriggerEvent.Focus))
        {
            builder.AddAttribute(5, "onfocus", EventCallback.Factory.Create<FocusEventArgs>(this, Overlay));
            builder.AddAttribute(6, "onblur", EventCallback.Factory.Create<FocusEventArgs>(this, Reset));
        }

        builder.AddContent(7, TriggerTemplate);

        builder.AddElementReferenceCapture(9, reference => { _floatingContext!.Reference = reference; });
        builder.CloseElement();

        //浮动层 必须是单独的一块元素，不然floating-ui无法计算
        builder.OpenComponent<Floating<ElementReference>>(10);
        builder.AddAttribute(11, nameof(Floating<ElementReference>.Target), _floatingContext);
        builder.AddAttribute(12, nameof(Floating<ElementReference>.TagName), FloatingTagName);
        builder.AddAttribute(13, nameof(Floating<ElementReference>.InitialState), InitialState);
        builder.AddAttribute(17, nameof(Floating<ElementReference>.AutoUpdate), AutoUpdate);
        builder.AddAttribute(14, nameof(Floating<ElementReference>.Delay), Delay);
        builder.AddAttribute(15, nameof(Floating<ElementReference>.ChildContent), FloatingTemplate);
        builder.AddAttribute(16, nameof(Floating<ElementReference>.Strategy), Strategy);
        builder.AddAttribute(17, nameof(Floating<ElementReference>.Placement), Placement);
        builder.AddAttribute(18, nameof(Floating<ElementReference>.MainAxis), MainAxis);
        builder.AddAttribute(19, nameof(Floating<ElementReference>.ShiftPadding), ShiftPadding);
        builder.AddAttribute(20, nameof(Floating<ElementReference>.UseArrow), UseArrow);
        builder.AddAttribute(21, nameof(Floating<ElementReference>.ArrowOffset), ArrowOffset);
        builder.AddAttribute(22, nameof(Floating<ElementReference>.ArrowStyleClass), ArrowStyleClass);
        builder.AddAttribute(13, nameof(Floating<ElementReference>.UseVirtualElement), UseVirtualElement);
        builder.AddAttribute(24, "class", FloatingStyleClass);

        builder.CloseComponent();
    }

    private async void Overlay<TEventArgs>(TEventArgs args) where TEventArgs : EventArgs
    {
        _floatingContext!.Overlay(args);
        if (OnOverlay != null)
        {
            await OnOverlay.Invoke();
        }
    }

    private async void Reset<TEventArgs>(TEventArgs args) where TEventArgs : EventArgs
    {
        _floatingContext!.Reset(args);
        if (OnReset != null)
        {
            await OnReset.Invoke();
        }
    }

    private void Toggle<TEventArgs>(TEventArgs args) where TEventArgs : EventArgs
    {
        if (_floatingContext!.Show)
        {
            Reset(args);
            if (UseFollowTrigger)
            {
                Overlay(args);
            }
        }
        else
        {
            Overlay(args);
        }
    }
}
