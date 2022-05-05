// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// <see cref="NavbarToggler"/> 组件
/// 导航栏可见性切换控制
/// </summary>
public sealed partial class NavbarToggler
{
    /// <summary>
    /// 获得 样式类集合 
    /// </summary>
    /// <returns></returns>
    private string? ClassString => CssBuilder.Default()
        .AddClass("navbar-toggler", ParentNavbarState.IsHorizontal)
        .AddClass("b-bar-toggler-popout", !ParentNavbarState.IsHorizontal && IsPopout)
        .AddClass("b-bar-toggler-inline", !ParentNavbarState.IsHorizontal && !IsPopout)
        .AddClass("collapsed", !IsShow && ParentNavbarState.IsHorizontal)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得 样式集合
    /// </summary>
    /// <returns></returns>
    private string? StyleClassString => CssBuilder.Default()
        .AddStyleFromAttributes(AdditionalAttributes)
        .AddClass("display: inline-flex;", Navbar != null)
        .Build();

    /// <summary>
    /// 获得 是否飘窗模式
    /// </summary>
    private bool IsPopout => Mode == NavbarTogglerMode.Popout;

    /// <summary>
    /// 获得 是否可见
    /// </summary>
    private bool IsShow => Navbar != null ? Navbar.Visible : ParentNavbarState.Visible;

    /// <summary>
    /// 切换事件
    /// </summary>
    /// <returns></returns>
    private async Task ClickHandler()
    {
        if (Clicked.HasDelegate)
        {
            await Clicked.InvokeAsync();
        }
        if (Navbar != null)
        {
            await Navbar.Toggle();
        }
        else if (ParentNavbar != null)
        {
            await ParentNavbar.Toggle();
        }
    }

    /// <summary>
    /// 按钮单击回调
    /// </summary>
    [Parameter]
    public EventCallback Clicked { get; set; }

    /// <summary>
    /// 提供内联或弹出样式的选项。仅由垂直杆支撑。默认情况下使用内联。
    /// </summary>
    [Parameter]
    public NavbarTogglerMode Mode { get; set; } = NavbarTogglerMode.Normal;

    /// <summary>
    /// 获得/设置 要控制 的导航栏。默认情况下使用父导航栏。
    /// </summary>
    [Parameter]
    public Navbar? Navbar { get; set; }

    /// <summary>
    /// 获得/设置 子组件内容
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 父级 <see cref="Navbar"/> 组件的状态
    /// </summary>
    [CascadingParameter]
    private NavbarState ParentNavbarState { get; set; } = new();

    /// <summary>
    /// 获得/设置 父级 <see cref="Navbar"/> 组件对象
    /// </summary>
    [CascadingParameter]
    private Navbar? ParentNavbar { get; set; }
}
