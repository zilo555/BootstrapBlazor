// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace BootstrapBlazor.Components;

/// <summary>
/// <see cref="NavbarAnchor"/> 组件
/// 用于在 <see cref="NavbarItem"/> 或 <see cref="NavbarDropdown"/> 中显示一个可点击的链接或锚点
/// </summary>
public sealed partial class NavbarAnchor
{
    private string? anchorTarget;

    /// <summary>
    /// 获得/设置 样式类集合 
    /// </summary>
    /// <returns></returns>
    [Parameter]
    public string? ClassString { get; set; }

    /// <summary>
    /// 获得/设置 样式集合
    /// </summary>
    /// <returns></returns>
    [Parameter]
    public string? StyleClassString { get; set; }

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

    /// <inheritdoc/>
    protected override void OnParametersSet()
    {
        if (AdditionalAttributes != null && AdditionalAttributes.TryGetValue("href", out var href))
        {
            To = $"{href}";
        }

        if (!string.IsNullOrWhiteSpace(To) && To.StartsWith("#"))
        {
            anchorTarget = To;
        }

        base.OnParametersSet();
    }

    /// <summary>
    /// 处理链接onclick事件。
    /// </summary>
    /// <returns></returns>
    public Task ClickHandler() => Clicked.InvokeAsync();
}
