using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// 触发浮动层 可见性的事件
/// </summary>
public enum TriggerEvent
{
    /// <summary>
    /// 鼠标单击事件
    /// </summary>
    [Description("click")]
    Click,

    /// <summary>
    /// 鼠标悬停事件
    /// </summary>
    [Description("hover")]
    Hover,

    /// <summary>
    /// 焦点事件
    /// </summary>
    [Description("focus")]
    Focus,
}
