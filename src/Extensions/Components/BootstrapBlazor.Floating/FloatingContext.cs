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
    /// <param name="forceTrigger"></param>
    public FloatingContext(bool forceTrigger) => ForceTrigger = forceTrigger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="reference"></param>
    public FloatingContext(TRef reference) => Reference = reference;

    /// <summary>
    /// 获得/设置 无关显示状态，强制触发事件
    /// </summary>
    public bool ForceTrigger { get; init; }

    /// <summary>
    /// 获得/设置 参照控件或元素对象
    /// </summary>
    public TRef? Reference { get; set; }

    private bool _show;
    /// <summary>
    /// 获得 是否显示浮动层
    /// </summary>
    public bool Show => _show;

    /// <summary>
    /// 切换显示状态
    /// </summary>
    public void Toggle<TEventArgs>(TEventArgs args) where TEventArgs : EventArgs
    {
        if (_show)
        {
            Reset(args);
        }
        else
        {
            Overlay(args);
        }
    }

    /// <summary>
    /// 叠加显示，虚拟元素需要读取参数中的坐标值
    /// </summary>
    public void Overlay<TEventArgs>(TEventArgs args) where TEventArgs : EventArgs
    {
        if (_show != true || ForceTrigger)
        {
            _show = true;
            FloatingChanged?.Invoke(FloatingEventArgs.Create(args, true));
        }
    }

    /// <summary>
    /// 还原默认状态
    /// </summary>
    public void Reset<TEventArgs>(TEventArgs args) where TEventArgs : EventArgs
    {
        if (_show != false || ForceTrigger)
        {
            _show = false;
            FloatingChanged?.Invoke(FloatingEventArgs.Create(args, false));
        }
    }
}
