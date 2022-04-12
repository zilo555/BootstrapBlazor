namespace BootstrapBlazor.Components;

/// <summary>
/// 浮动层 引用控制上下文，不能被多个组件同时使用
/// </summary>
/// <typeparam name="TRef"></typeparam>
public sealed class FloatingContext<TRef>
{
    /// <summary>
    /// 浮动层 触发变化事件
    /// </summary>
    internal event FloatingTriggerEventHandler? FloatingChanged;

    /// <summary>
    /// 构造函数
    /// </summary>
    public FloatingContext() { }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="reference"></param>
    public FloatingContext(TRef reference) => Reference = reference;

    /// <summary>
    /// 是否初始化组件引用，接收到第一个不为空的对象时设置为 true
    /// </summary>
    public bool Initialized { get; private set; }

    private TRef? _reference;
    /// <summary>
    /// 获得/设置 参照控件或元素对象
    /// </summary>
    public TRef? Reference
    {
        get => _reference;
        set
        {
            _reference = value;
            if (!Initialized && value != null)
            {
                Initialized = true;
            }
        }
    }

    private bool _show;
    /// <summary>
    /// 获得/设置 是否显示浮动层
    /// </summary>
    public bool Show
    {
        get => _show;
        set
        {
            _show = value;
            FloatingChanged?.Invoke(new(value));
        }
    }

    /// <summary>
    /// 切换显示状态
    /// </summary>
    public void Toggle() => Show = !Show;

    /// <summary>
    /// 叠加显示
    /// </summary>
    public void Overlay() => Show = true;

    /// <summary>
    /// 还原默认状态
    /// </summary>
    public void Reset() => Show = false;
}
