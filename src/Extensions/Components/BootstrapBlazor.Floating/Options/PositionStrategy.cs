using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// 浮动层 定位模式
/// </summary>
public enum PositionStrategy
{
    /// <summary>
    /// 绝对定位
    /// </summary>
    [Description("absolute")]
    Absolute,

    /// <summary>
    /// 固定定位
    /// </summary>
    [Description("fixed")]
    Fixed,
}
