namespace BootstrapBlazor.Components;

/// <summary>
/// 保存 <see cref="Navbar"/> 组件当前状态的信息
/// </summary>
public record NavbarState
{
    /// <summary>
    /// 获得/设置 切换器、菜单的显示状态
    /// </summary>
    public bool Visible { get; init; }

    /// <summary>
    /// 定义条显示模式，当用作侧边栏时，需要垂直显示。
    /// </summary>
    public NavbarMode Mode { get; init; }

    /// <summary>
    /// 获得/设置 当前布局断点
    /// </summary>
    public BreakPoint Breakpoint { get; init; }

    /// <summary>
    /// 获得 当前下拉菜单是否水平显示
    /// </summary>
    public bool IsHorizontal => Mode == NavbarMode.Horizontal;
}
