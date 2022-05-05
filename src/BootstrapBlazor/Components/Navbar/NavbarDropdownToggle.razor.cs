// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// <see cref="NavbarDropdownToggle"/> 组件
/// 控制下拉菜单的显示或隐藏切换
/// </summary>
public partial class NavbarDropdownToggle
{
    /// <summary>
    /// 获得 样式集合
    /// </summary>
    /// <returns></returns>
    private string? ClassString => CssBuilder.Default()
        .AddClass("b-bar-link b-bar-dropdown-toggle", !ParentDropdownState.IsHorizontal)
        .AddClass("dropdown-item", ParentDropdownState.IsHorizontal && ParentBarDropdown?.IsBarDropdownSubmenu == true)
        .AddClass("nav-link dropdown-toggle", ParentDropdownState.IsHorizontal && ParentBarDropdown?.IsBarDropdownSubmenu != true)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得 样式集合
    /// </summary>
    /// <returns></returns>
    private string? StyleClassString => CssBuilder.Default()
        .AddClass($"padding-left: { Indentation * ParentDropdownState.NestedIndex}rem", ParentDropdownState.IsInlineDisplay)
        .AddStyleFromAttributes(AdditionalAttributes)
        .Build();

    private ElementReference ElementRef { get; set; }

    /// <summary>
    /// 切换按钮点击事件
    /// </summary>
    /// <returns></returns>
    protected Task ClickHandler()
    {
        if (ParentBarDropdown != null)
        {
            return ParentBarDropdown.Toggle(Id!);
        }
        return Clicked.InvokeAsync();
    }

    /// <summary>
    /// 获得/设置 确定将应用于 下拉切换 的剩余填充量。（使用rem）
    /// </summary>
    [Parameter]
    public double Indentation { get; set; } = 1.5d;

    /// <summary>
    /// 获得/设置 切换按钮点击事件
    /// </summary>
    [Parameter]
    public EventCallback Clicked { get; set; }

    /// <summary>
    /// 获得/设置 子组件内容
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 父级 <see cref="NavbarDropdown"/> 组件的状态
    /// </summary>
    [CascadingParameter]
    public NavbarDropdownState ParentDropdownState { get; set; } = new();

    /// <summary>
    /// 获得/设置 子组件内容
    /// </summary>
    [CascadingParameter]
    protected NavbarDropdown? ParentBarDropdown { get; set; }

    /// <summary>
    /// 鼠标移入处理事件
    /// </summary>
    /// <returns></returns>
    public Task OnMouseEnterHandler()
    {
        if (ParentBarDropdown is not null)
        {
            ParentDropdownState.HoverToggle = true;
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
            ParentDropdownState.HoverToggle = false;
            await ParentBarDropdown.OnMouseLeaveHandler();
        }
    }
}
