using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// 浮动层 显示状态
/// </summary>
public enum FloatingState
{
    /// <summary>
    /// 隐藏状态
    /// </summary>
    [Description("hidden")]
    Hidden,

    /// <summary>
    /// 不对内容做任何调整，原样显示
    /// </summary>
    [Description("raw")]
    Raw,

    /// <summary>
    /// 浮动叠加
    /// </summary>
    [Description("overlay")]
    Overlay,
}
