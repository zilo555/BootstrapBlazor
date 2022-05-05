// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// <see cref="NavbarDropdownMenu"/> 组件
/// <see cref="NavbarDropdown"/> 菜单的主容器，可包含一个或多个 <see cref="NavbarDropdownItem"/>。
/// </summary>
public partial class NavbarDropdownMenu
{
    /// <summary>
    /// 获得 样式类集合 
    /// </summary>
    /// <returns></returns>
    private string? ClassString => CssBuilder.Default()
        .AddClass("dropdown-menu", ParentDropdownState.IsHorizontal)
        .AddClass("b-bar-dropdown-menu", !ParentDropdownState.IsHorizontal)
        .AddClass("show", ParentDropdownState.Visible)
        .AddClass("b-bar-right", !ParentDropdownState.IsHorizontal && ParentDropdownState.RightAligned)
        .AddClass("dropdown-menu-end", ParentDropdownState.IsHorizontal && ParentDropdownState.RightAligned)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得 样式类集合 
    /// </summary>
    /// <returns></returns>
    private string? ContainerClassString => CssBuilder.Default()
        .AddClass("b-bar-dropdown-menu-container")
        .AddClass("b-bar-right", !ParentDropdownState.IsHorizontal && ParentDropdownState.RightAligned)
        .AddClass("dropdown-menu-end", ParentDropdownState.IsHorizontal && ParentDropdownState.RightAligned)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得 样式集合
    /// </summary>
    /// <returns></returns>
    private string? ContainerStyleClassString => CssBuilder.Default()
        .AddClass("margin-top:0;", ParentDropdownState.IsHover)
        .AddStyleFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得/设置 当前显示状态
    /// </summary>
    protected string? VisibleString => ParentDropdownState.Visible.ToString().ToLower();

    /// <summary>
    /// 获得/设置 子组件内容
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 父级 <see cref="NavbarDropdown"/> 组件的状态
    /// </summary>
    [CascadingParameter]
    protected NavbarDropdownState ParentDropdownState { get; set; } = new();

    /// <summary>
    /// 获得/设置 父级 <see cref="NavbarDropdown"/> 组件的引用
    /// </summary>
    [CascadingParameter]
    protected NavbarDropdown? ParentBarDropdown { get; set; }

    /// <summary>
    /// 放置此 <see cref="NavbarDropdown"/> 的 <see cref="NavbarItem"/> 组件。
    /// </summary>
    [CascadingParameter]
    protected NavbarItem? ParentBarItem { get; set; }

    /// <summary>
    /// 获得/设置 父级 <see cref="NavbarItem"/> 组件的状态
    /// </summary>
    [CascadingParameter]
    protected NavbarItemState ParentBarItemState { get; set; } = new();

    /// <summary>
    /// 获得/设置 父级 <see cref="Navbar"/> 组件对象
    /// </summary>
    [CascadingParameter]
    private Navbar? ParentNavbar { get; set; }

    /// <summary>
    /// 鼠标移入处理事件
    /// </summary>
    /// <returns></returns>
    public Task OnMouseEnterHandler()
    {
        if (ParentBarDropdown is not null)
        {
            ParentDropdownState.HoverMenu = true;
            return ParentBarDropdown.OnMouseEnterHandler();
        }

        return Task.CompletedTask;
    }

    /// <summary>
    /// 鼠标移出处理事件
    /// </summary>
    /// <returns></returns>
    public async Task OnMouseLeaveHandler()
    {
        if (ParentBarDropdown is not null)
        {
            ParentDropdownState.HoverMenu = false;
            await ParentBarDropdown.OnMouseLeaveHandler();
        }
    }
}
