namespace BootstrapBlazor.Components;

/// <summary>
/// 浮动层 触发事件约定
/// </summary>
/// <param name="e"></param>
internal delegate void FloatingTriggerEventHandler(FloatingEventArgs e);

/// <summary>
/// 浮动层 事件参数
/// </summary>
internal sealed class FloatingEventArgs : EventArgs
{
    public bool Show { get; }

    public FloatingEventArgs(bool show) => Show = show;
}
