// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// <see cref="NavbarItem"/> 组件
/// <see cref="NavbarLink"/> 或 <see cref="NavbarDropdown"/> 的外层容器组件
/// </summary>
public partial class NavbarItem
{
    private NavbarState _parentBarState = new();

    private NavbarItemState _state = new() { Mode = NavbarMode.Horizontal, };

    private NavbarDropdown? _barDropdown;

    /// <summary>
    /// 获得 样式类集合 
    /// </summary>
    /// <returns></returns>
    private string? ClassString => CssBuilder.Default()
        .AddClass("b-bar-item", !ParentBarState.IsHorizontal)
        .AddClass("nav-item", ParentBarState.IsHorizontal)
        .AddClass("dropdown", ParentBarState.IsHorizontal && HasDropdown)
        .AddClass("active", Active)
        .AddClass("disabled", State.Disabled)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <inheritdoc/>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (firstRender)
        {
            if (HasDropdown)
            {
                await InvokeAsync(StateHasChanged);
            }
        }
    }

    /// <summary>
    /// 传递子组件 <see cref="NavbarDropdown"/> 的引用给当前 <see cref="NavbarItem"/> 组件
    /// </summary>
    /// <param name="barDropdown"></param>
    internal void NotifyBarDropdownInitialized(NavbarDropdown barDropdown) => _barDropdown = barDropdown;

    /// <summary>
    /// 获得/设置 当前 <see cref="NavbarItem"/> 组件的状态
    /// </summary>
    protected NavbarItemState State => _state;

    /// <summary>
    /// 获得 当前 <see cref="NavbarItem"/> 组件是否包含 <see cref="NavbarDropdown"/> 组件
    /// </summary>
    protected bool HasDropdown => _barDropdown is not null;

    /// <summary>
    /// 获取/设置 激活状态标志，指示 <see cref="NavbarItem"/> 是处于活动状态还是处于焦点状态
    /// </summary>
    [Parameter]
    public bool Active { get; set; }

    /// <summary>
    /// 获得/设置 禁用状态标志，使 <see cref="NavbarItem"/> 处于非活动状态
    /// </summary>
    [Parameter]
    public bool Disabled
    {
        get => _state.Disabled;
        set => _state = _state with { Disabled = value };
    }

    /// <summary>
    /// 获得/设置 子组件内容
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 父级 <see cref="Navbar"/> 组件的状态
    /// </summary>
    [CascadingParameter]
    protected NavbarState ParentBarState
    {
        get => _parentBarState;
        set
        {
            if (_parentBarState != value)
            {
                _parentBarState = value;
                _state = _state with
                {
                    Mode = _parentBarState.Mode,
                    NavbarVisible = _parentBarState.Visible
                };
            }
        }
    }
}
