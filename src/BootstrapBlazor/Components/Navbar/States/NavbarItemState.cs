namespace BootstrapBlazor.Components;

/// <summary>
/// 保存 <see cref="NavbarItem"/> 组件当前状态的信息
/// </summary>
public record NavbarItemState
{
    /// <summary>
    /// 获得/设置 禁用状态标志，指示 <see cref="NavbarItem"/> 是否处于非活动状态。
    /// </summary>
    public bool Disabled { get; init; }

    /// <summary>
    /// 获得/设置 放置项的导航栏模式。
    /// </summary>
    public NavbarMode Mode { get; init; }

    /// <summary>
    /// 获得/设置 放置项的导航栏可见性。
    /// </summary>
    public bool NavbarVisible { get; init; }

    /// <summary>
    /// 获得 当前下拉菜单是否水平显示
    /// </summary>
    public bool IsHorizontal => Mode == NavbarMode.Horizontal;
}
