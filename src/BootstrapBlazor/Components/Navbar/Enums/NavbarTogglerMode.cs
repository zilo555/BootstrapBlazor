namespace BootstrapBlazor.Components;

/// <summary>
/// 定义 <see cref="NavbarToggler"/> 的外观和位置。
/// </summary>
public enum NavbarTogglerMode
{
    /// <summary>
    /// 标准模式，使用内联切换，支持水平、垂直模式。
    /// </summary>
    Normal,

    /// <summary>
    /// 弹出式，仅在垂直模式下受支持。
    /// </summary>
    Popout
}
