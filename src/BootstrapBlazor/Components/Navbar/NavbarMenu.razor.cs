// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// <see cref="NavbarMenu"/> 组件
/// 导航栏的主要部分，在触摸设备上隐藏，在桌面上可见。
/// </summary>
public partial class NavbarMenu
{
    /// <summary>
    /// 获得 样式类集合 
    /// </summary>
    /// <returns></returns>
    private string? ClassString => CssBuilder.Default()
        .AddClass("collapse navbar-collapse", ParentBarState.IsHorizontal)
        .AddClass("b-bar-menu", !ParentBarState.IsHorizontal)
        .AddClass("show", ParentBarState.Visible)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得/设置 子组件内容
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 父级 <see cref="Navbar"/> 组件的状态
    /// </summary>
    [CascadingParameter]
    private NavbarState ParentBarState { get; set; } = new();
}
