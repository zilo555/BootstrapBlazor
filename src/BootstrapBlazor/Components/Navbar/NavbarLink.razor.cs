// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace BootstrapBlazor.Components;

/// <summary>
///  <see cref="NavbarLink"/> 组件
///  可点击的链接，<see cref="NavbarItem"/> 或 <see cref="NavbarDropdown"/> 的兄弟组件
/// </summary>
public partial class NavbarLink
{
    /// <summary>
    /// 获得 样式类集合 
    /// </summary>
    /// <returns></returns>
    private string? ClassString => CssBuilder.Default()
        .AddClass("nav-link", ParentBarItemState.IsHorizontal)
        .AddClass("b-bar-link", !ParentBarItemState.IsHorizontal)
        .AddClass("disabled", ParentBarItemState.Disabled)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得/设置 点击事件
    /// </summary>
    [Parameter]
    public EventCallback Clicked { get; set; }

    /// <summary>
    /// 获得/设置 指定链接指向的页面URL
    /// </summary>
    [Parameter]
    public string? To { get; set; }

    /// <summary>
    /// 获得/设置 链接指定打开的目标
    /// </summary>
    [Parameter]
    public NavLinkTarget Target { get; set; } = NavLinkTarget.None;

    /// <summary>
    /// 获得/设置 URL匹配方式。
    /// </summary>
    [Parameter]
    public NavLinkMatch Match { get; set; } = NavLinkMatch.All;

    /// <summary>
    /// 获得/设置 额外提示信息
    /// </summary>
    [Parameter]
    public string? Title { get; set; }

    /// <summary>
    /// 获得/设置 子组件内容
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 父级 <see cref="NavbarItem"/> 组件的状态
    /// </summary>
    [CascadingParameter]
    protected NavbarItemState ParentBarItemState { get; set; } = new();
}
