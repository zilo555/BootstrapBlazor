// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// <see cref="NavbarSection"/> 组件
/// 导航栏内容分段
/// </summary>
public partial class NavbarSection
{
    /// <summary>
    /// 获得 样式类集合 
    /// </summary>
    /// <returns></returns>
    private string? ClassString => CssBuilder.Default()
        .AddClass("navbar-nav me-auto", ParentBarState.IsHorizontal)
        .AddClass("b-bar-start", !ParentBarState.IsHorizontal && !IsFarPart)
        .AddClass("b-bar-end", !ParentBarState.IsHorizontal && !IsFarPart)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得/设置 是否导航栏的远端部分 默认 false 表示近端
    /// </summary>
    [Parameter]
    public bool IsFarPart { get; set; }

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
