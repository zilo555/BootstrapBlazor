// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// <see cref="NavbarBrand"/> 组件
/// 导航栏组件的一部分，始终可见，通常包含logo和可选的一些链接或图标
/// 垂直模式下，显示 <see cref="NavbarToggler"/> 控制组件
/// </summary>
public sealed partial class NavbarBrand
{
    /// <summary>
    /// 获得 样式集合
    /// </summary>
    /// <returns></returns>
    private string? ClassString => CssBuilder.Default()
        .AddClass(IsHorizontal ? "navbar-brand" : "b-bar-brand")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得 是否水平显示
    /// </summary>
    private bool IsHorizontal => ParentBarState is null || ParentBarState.Mode == NavbarMode.Horizontal;

    /// <summary>
    /// 获得/设置 父级 <see cref="Navbar"/> 组件的状态
    /// </summary>
    [CascadingParameter]
    private NavbarState? ParentBarState { get; set; }

    /// <summary>
    /// 获得/设置 子组件内容
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
}
