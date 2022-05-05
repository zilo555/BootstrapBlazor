// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace BootstrapBlazor.Components;

/// <summary>
/// <see cref="Navbar"/> 组件
/// Navbar组件是一个包装器，它将logo、导航和其他元素封装到简洁的菜单或侧栏中。
/// </summary>
public sealed partial class Navbar : IDisposable
{
    /// <summary>
    /// 获得 样式集合
    /// </summary>
    /// <returns></returns>
    public string? ClassString => CssBuilder.Default("navbar")
        .AddClass($"navbar-{ThemeContrast.ToDescriptionString()}", ThemeContrast != NavbarTheme.None)
        .AddClass($"b-bar-{ThemeContrast.ToDescriptionString()}", ThemeContrast != NavbarTheme.None)
        .AddClass($"navbar-expand-{BreakpointStyle}", Breakpoint != BreakPoint.None)
        .AddClass($"justify-content-{Alignment.ToDescriptionString()}", Alignment != Alignment.None)
        .AddClass($"b-bar-{Mode.ToDescriptionString()}")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 用于跟踪此组件的断点状态
    /// </summary>
    private bool IsBroken { get; set; } = true;

    /// <summary>
    /// 组件的状态
    /// </summary>
    private NavbarState state = new()
    {
        Visible = true,
        Mode = NavbarMode.Horizontal,
        Breakpoint = BreakPoint.None
    };

    /// <inheritdoc/>
    protected override Task OnInitializedAsync()
    {
        if (NavigationBreakpoint != BreakPoint.None && NavigationManager != null)
        {
            NavigationManager.LocationChanged += OnLocationChanged;
        }

        return base.OnInitializedAsync();
    }

    /// <inheritdoc/>
    protected override async void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);

        if (firstRender)
        {
            if (Mode != NavbarMode.Horizontal && BrokenBreakpoint > Breakpoint)
            {
                await Toggle();
            }
        }
    }

    /// <summary>
    /// 切换组件状态
    /// </summary>
    internal Task Toggle()
    {
        Visible = !Visible;
        return InvokeAsync(StateHasChanged);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (NavigationBreakpoint != BreakPoint.None && NavigationManager != null)
        {
            NavigationManager.LocationChanged -= OnLocationChanged;
        }
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// 导航路径更改时
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    private async void OnLocationChanged(object? sender, LocationChangedEventArgs args)
    {
        // 自动折叠
        if (Visible && (NavigationBreakpoint > Breakpoint))
        {
            await Toggle();
        }
    }

    /// <summary>
    /// 获得 组件的状态对象的引用。
    /// </summary>
    private NavbarState State => state;

    /// <summary>
    /// 获得 当前断点对应的显示样式
    /// </summary>
    private string? BreakpointStyle => Breakpoint switch
    {
        BreakPoint.ExtraExtraLarge => "xl",
        BreakPoint.ExtraLarge => "xl",
        BreakPoint.Large => "lg",
        BreakPoint.Medium => "md",
        BreakPoint.Small => "sm",
        BreakPoint.ExtraSmall => "xs",
        _ => null,
    };

    /// <summary>
    /// 获得 当前突变状态
    /// </summary>
    public string BrokenStateString => IsBroken.ToString().ToLower();

    /// <summary>
    /// 获得 当前折叠模式
    /// </summary>
    public string? CollapseModeString => Visible ? null : CollapseMode.ToDescriptionString();

    /// <summary>
    /// NavigationManager
    /// </summary>
    [Inject]
    private NavigationManager? NavigationManager { get; set; }

    /// <summary>
    /// 控制当前的状态。
    /// </summary>
    [Parameter]
    public bool Visible
    {
        get => state.Visible;
        set
        {
            if (state.Visible != value)
            {
                state = state with { Visible = value };
                VisibleChanged.InvokeAsync(value);
            }
        }
    }

    /// <summary>
    /// 获得/设置 导航栏可见性变化时回调。
    /// </summary>
    [Parameter]
    public EventCallback<bool> VisibleChanged { get; set; }

    /// <summary>
    /// 获得/设置 接收当前布局断点
    /// </summary>
    [Parameter]
    public BreakPoint Breakpoint
    {
        get => state.Breakpoint;
        set
        {
            if (state.Breakpoint != value)
            {
                IsBroken = BrokenBreakpoint > value;
                state = state with { Breakpoint = value };
            }
        }
    }

    /// <summary>
    /// 获得/设置 显示方向。当用作侧边栏时，需要设置垂直模式。
    /// </summary>
    [Parameter]
    public NavbarMode Mode
    {
        get => state.Mode;
        set
        {
            if (state.Mode != value)
            {
                state = state with { Mode = value };
            }
        }
    }

    /// <summary>
    /// 获得/设置 布局变化界限断点
    /// </summary>
    [Parameter]
    public BreakPoint BrokenBreakpoint { get; init; }

    /// <summary>
    /// 获得/设置 导航变化界限断点
    /// </summary>
    [Parameter]
    public BreakPoint NavigationBreakpoint { get; init; } = BreakPoint.None;

    /// <summary>
    /// 获得/设置 当前 <see cref="Navbar"/> 组件的主题
    /// </summary>
    [Parameter]
    public NavbarTheme ThemeContrast { get; init; } = NavbarTheme.Light;

    /// <summary>
    /// 获得/设置 对齐方式。
    /// </summary>
    [Parameter]
    public Alignment Alignment { get; init; } = Alignment.None;

    /// <summary>
    /// 获得/设置 折叠模式
    /// </summary>
    [Parameter]
    public NavbarCollapseMode CollapseMode { get; init; } = NavbarCollapseMode.Hide;

    /// <summary>
    /// 获得/设置 子组件内容
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
}
