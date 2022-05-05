namespace BootstrapBlazor.Components;

/// <summary>
/// 保存 <see cref="NavbarDropdown"/> 组件当前状态的信息
/// </summary>
public record NavbarDropdownState
{
    /// <summary>
    /// 获得/设置 下拉菜单的可见性。
    /// </summary>
    public bool Visible { get; init; }

    /// <summary>
    /// 获得/设置 如果为true，则下拉菜单将右对齐。
    /// </summary>
    public bool RightAligned { get; init; }

    /// <summary>
    /// 获得/设置 放置下拉列表的导航栏模式。
    /// </summary>
    public NavbarMode Mode { get; init; }

    /// <summary>
    /// 获得/设置 放置项的导航栏可见性。
    /// </summary>
    public bool NavbarVisible { get; init; }

    /// <summary>
    /// 获得/设置 <see cref="Navbar"/> 组件中下拉列表的层次索引。
    /// </summary>
    public int NestedIndex { get; init; }

    /// <summary>
    /// 获得 当前下拉菜单处于垂直内联模式且该条可见
    /// </summary>
    public bool IsInlineDisplay => Mode == NavbarMode.VerticalInline && NavbarVisible;

    /// <summary>
    /// 获得 当前下拉菜单是否水平显示
    /// </summary>
    public bool IsHorizontal => Mode == NavbarMode.Horizontal;

    /// <summary>
    /// 获得/设置
    /// </summary>
    public bool HoverToggle { get; set; }

    /// <summary>
    /// 获得/设置
    /// </summary>
    public bool HoverMenu { get; set; }

    /// <summary>
    /// 获得 当前下拉菜单是否水平显示
    /// </summary>
    public bool IsHover => HoverToggle || HoverMenu;
}
