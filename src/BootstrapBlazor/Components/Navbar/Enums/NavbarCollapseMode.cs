using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// 折叠模式
/// </summary>
public enum NavbarCollapseMode
{
    /// <summary>
    /// 导航栏在折叠时将完全隐藏。
    /// </summary>
    [Description("hide")]
    Hide,

    /// <summary>
    /// 导航栏将被折叠成带有图标的较小版本。
    /// </summary>
    [Description("small")]
    Small
}
