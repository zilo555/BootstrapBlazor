// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;


/// <summary>
/// <see cref="NavbarIcon"/> 组件
/// 在<see cref="Navbar"/> 组件中使用的 图标组件。
/// </summary>
public partial class NavbarIcon
{
    /// <summary>
    /// 获得 样式类集合 
    /// </summary>
    /// <returns></returns>
    private string? ClassString => CssBuilder.Default("b-bar-icon")
        .AddClass("fa", !IsCustom && !IconName.Contains("fa "))
        .AddClass("fa-fw", !IsCustom && !IconName.Contains("fa-fw"))
        .AddClass(IconName, !IsCustom)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得/设置 是否自定义图标内容
    /// </summary>
    [Parameter]
    public bool IsCustom { get; set; }

    /// <summary>
    /// 获得/设置 图标名称 类似于 fa-database
    /// </summary>
    [Parameter]
    public string IconName { get; set; } = string.Empty;

    /// <summary>
    /// 获得/设置 子组件内容
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
}
