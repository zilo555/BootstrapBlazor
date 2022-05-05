using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// 导航栏显示方向
/// </summary>
public enum NavbarMode
{
    /// <summary>
    /// 带有下拉菜单的水平导航栏。
    /// </summary>
    [Description("horizontal")]
    Horizontal,

    /// <summary>
    /// 带有弹出菜单的垂直导航栏。
    /// </summary>
    [Description("vertical-popout")]
    VerticalPopout,

    /// <summary>
    /// 带有内联下拉菜单的垂直导航栏。
    /// </summary>
    [Description("vertical-inline")]
    VerticalInline,

    /// <summary>
    /// 带有飘窗菜单的图标垂直导航栏。
    /// </summary>
    [Description("vertical-small")]
    VerticalSmall,
}
