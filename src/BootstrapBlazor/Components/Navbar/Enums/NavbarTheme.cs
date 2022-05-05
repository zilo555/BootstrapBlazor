using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// 定义 <see cref="Navbar"/> 的主题颜色。
/// </summary>
public enum NavbarTheme
{
    /// <summary>
    /// 
    /// </summary>
    None,

    /// <summary>
    /// 将主题调整为浅色。
    /// </summary>
    [Description("light")]
    Light,

    /// <summary>
    /// 将主题调整为深色。
    /// </summary>
    [Description("dark")]
    Dark,
}
