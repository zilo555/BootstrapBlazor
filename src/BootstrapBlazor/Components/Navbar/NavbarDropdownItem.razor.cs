// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace BootstrapBlazor.Components;

/// <summary>
/// <see cref="NavbarDropdownItem"/> 组件
/// <see cref="NavbarDropdownMenu"/> 组件的一个菜单项
/// </summary>
public partial class NavbarDropdownItem
{
    /// <summary>
    /// 获得 样式类集合 
    /// </summary>
    /// <returns></returns>
    private string? ClassString => CssBuilder.Default()
        .AddClass("b-bar-dropdown-item", !ParentDropdownState.IsHorizontal)
        .AddClass("dropdown-item", ParentDropdownState.IsHorizontal)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得 样式集合
    /// </summary>
    /// <returns></returns>
    private string? StyleClassString => CssBuilder.Default()
        .AddClass(FormattableString.Invariant($"padding-left: {Indentation * (ParentDropdownState.NestedIndex + 1d)}rem"), ParentDropdownState.IsInlineDisplay)
        .AddStyleFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 下拉项点击事件
    /// </summary>
    /// <returns></returns>
    protected async Task ClickHandler()
    {
        if (ParentBarDropdown is not null && ParentDropdownState.IsHorizontal)
        {
            if (!ParentBarDropdown.WasJustToggled)
            {
                await ParentBarDropdown.Hide(true);
            }
        }
        await Clicked.InvokeAsync();
    }

    /// <summary>
    /// 获得/设置 点击事件
    /// </summary>
    [Parameter] public EventCallback Clicked { get; set; }

    /// <summary>
    /// 获得/设置 指定链接指向的页面URL
    /// </summary>
    [Parameter] public string? To { get; set; }

    /// <summary>
    /// 获得/设置 链接指定打开的目标
    /// </summary>
    [Parameter]
    public NavLinkTarget Target { get; set; } = NavLinkTarget.None;

    /// <summary>
    /// 获得/设置 额外提示信息
    /// </summary>
    [Parameter]
    public NavLinkMatch Match { get; set; } = NavLinkMatch.All;

    /// <summary>
    /// 获得/设置 额外提示信息
    /// </summary>
    [Parameter]
    public string? Title { get; set; }

    /// <summary>
    /// 获得/设置 确定将应用于 下拉项 的 left 填充量。（使用rem）
    /// </summary>
    [Parameter]
    public double Indentation { get; set; } = 1.5d;

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
}
