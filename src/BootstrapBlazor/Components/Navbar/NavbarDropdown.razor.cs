// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// <see cref="NavbarDropdown"/> 组件
/// 导航栏组件的一部分，用于显示其中的下拉菜单，其中可以包括导航项和分隔符
/// </summary>
public partial class NavbarDropdown : IDisposable
{
    private NavbarItemState _parentBarItemState = new();

    /// <summary>
    /// 父级下拉菜单的状态
    /// </summary>
    private NavbarDropdownState _parentBarDropdownState = new();

    /// <summary>
    /// 当前下拉菜单的状态
    /// </summary>
    private NavbarDropdownState _state = new() { NestedIndex = 1 };

    /// <summary>
    /// 当前下拉菜单的下拉菜单子项。
    /// </summary>
    private NavbarDropdown? _childBarDropdown;

    /// <summary>
    /// 获得 样式集合
    /// </summary>
    /// <returns></returns>
    public string? ClassString => CssBuilder.Default()
        .AddClass("dropdown", State.IsHorizontal)
        .AddClass("b-bar-dropdown", !State.IsHorizontal)
        .AddClass("show", State.Visible)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <inheritdoc/>
    protected override Task OnInitializedAsync()
    {
        ParentBarItem?.NotifyBarDropdownInitialized(this);
        ParentBarDropdown?.NotifyChildDropdownInitialized(this);

        return base.OnInitializedAsync();
    }

    /// <inheritdoc/>
    protected override void OnAfterRender(bool firstRender)
    {
        WasJustToggled = false;

        base.OnAfterRender(firstRender);
    }

    /// <inheritdoc/>
    public override Task SetParametersAsync(ParameterView parameters)
    {
        // 这是双向绑定正常工作所必需的。
        // 否则，内部值的设置顺序将不正确。
        if (parameters.TryGetValue<bool>(nameof(Visible), out var newVisible))
        {
            _state = _state with { Visible = newVisible };
        }

        return base.SetParametersAsync(parameters);
    }

    /// <summary>
    /// 显示下拉菜单。
    /// </summary>
    /// <returns></returns>
    public async Task Show()
    {
        if (!Visible)
        {
            Visible = true;

            await InvokeAsync(StateHasChanged);

            //只需要浮动第一级菜单
            if (_state.NestedIndex == 1)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Visible)));
            }
        }
    }

    /// <summary>
    /// 隐藏下拉菜单。
    /// </summary>
    /// <param name="hideAll">指示是否需要隐藏当前下拉菜单及其所有父下拉菜单。</param>
    /// <returns></returns>
    public async Task Hide(bool hideAll = false)
    {
        if (!Visible)
        {
            return;
        }

        if (ParentBarDropdown is not null && (ParentBarDropdown.ShouldClose || hideAll))
        {
            await ParentBarDropdown.Hide(hideAll);
        }

        Visible = false;

        //只需要浮动第一级菜单
        if (_state.NestedIndex == 1)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Visible)));
        }

        await InvokeAsync(StateHasChanged);
    }

    /// <summary>
    /// 切换下拉菜单的可见性。
    /// </summary>
    /// <returns></returns>
    public Task Toggle(string dropdownToggleElementId)
    {
        // 当菜单处于垂直“弹出”样式模式时，不允许切换。
        // 这将由下面的鼠标操作来处理。
        if (ParentBarItemState.IsHorizontal && !State.IsInlineDisplay)
        {
            return Task.CompletedTask;
        }

        SetWasJustToggled(true);
        SetSelectedDropdownElementId(dropdownToggleElementId);

        Visible = !Visible;

        return InvokeAsync(StateHasChanged);
    }

    /// <summary>
    /// 在当前下拉列表和每个现有父下拉列表上设置WastToggled标志。
    /// </summary>
    /// <param name="wasToggled"></param>
    internal void SetWasJustToggled(bool wasToggled)
    {
        WasJustToggled = wasToggled;
        ParentBarDropdown?.SetWasJustToggled(wasToggled);
    }

    /// <summary>
    /// 设置选定的下拉切换ElementId
    /// </summary>
    /// <param name="dropdownToggleElementId"></param>
    internal void SetSelectedDropdownElementId(string dropdownToggleElementId)
    {
        SelectedBarDropdownElementId = dropdownToggleElementId;
        if (ParentBarDropdown is not null)
        {
            ParentBarDropdown.SetSelectedDropdownElementId(dropdownToggleElementId);
        }
    }

    /// <summary>
    /// 鼠标移入处理事件
    /// </summary>
    /// <returns></returns>
    public Task OnMouseEnterHandler()
    {
        ShouldClose = false;

        if (ParentBarItemState.IsHorizontal || State.IsInlineDisplay)
        {
            return Task.CompletedTask;
        }

        return Show();
    }

    /// <summary>
    /// 鼠标移出处理事件
    /// </summary>
    /// <returns></returns>
    public Task OnMouseLeaveHandler()
    {
        ShouldClose = true;

        if (ParentBarItemState.IsHorizontal || State.IsInlineDisplay)
        {
            return Task.CompletedTask;
        }

        return Hide();
    }

    /// <summary>
    /// 更新子下拉菜单组件引用
    /// </summary>
    /// <param name="barDropdown"></param>
    internal void NotifyChildDropdownInitialized(NavbarDropdown barDropdown)
    {
        if (_childBarDropdown == null)
        {
            _childBarDropdown = barDropdown;
        }
    }

    /// <summary>
    /// 移除子下拉菜单组件
    /// </summary>
    internal void NotifyChildDropdownRemoved() => _childBarDropdown = null;

    /// <summary>
    /// 跟踪下拉列表是否处于应该关闭的状态。
    /// </summary>
    internal bool ShouldClose { get; set; } = false;

    /// <summary>
    /// 跟踪下拉列表是否刚刚切换，忽略可能会关闭下拉列表的下拉项单击。
    /// </summary>
    internal bool WasJustToggled { get; set; } = false;

    /// <summary>
    /// 获得 当前 <see cref="NavbarDropdown"/> 组件状态。
    /// </summary>
    protected NavbarDropdownState State => _state;

    /// <summary>
    /// 获得 指示当前下拉菜单是否被包含在另外一个下拉菜单中
    /// </summary>
    protected internal bool IsBarDropdownSubmenu => ParentBarDropdown is not null;

    /// <summary>
    /// 获得 指示当前下拉菜单是否包含有下拉菜单
    /// </summary>
    protected internal bool HasSubmenu => _childBarDropdown is not null;

    /// <summary>
    /// 获得 下拉菜单及其所有子控件可见状态。
    /// </summary>
    protected string VisibleString => State.Visible.ToString().ToLower();

    /// <summary>
    /// 获得/设置 可见性切换事件
    /// </summary>

    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// 获得/设置 跟踪最后一个动作的BarDropdownToggle元素的Id。
    /// </summary>
    public string? SelectedBarDropdownElementId { get; set; }

    /// <summary>
    /// 获得/设置 控制下拉菜单及其所有子控件是否可见。
    /// </summary>
    [Parameter]
    public bool Visible
    {
        get => _state.Visible;
        set
        {
            if (value != _state.Visible)
            {
                _state = _state with { Visible = value };
                VisibleChanged.InvokeAsync(value);
            }
        }
    }

    /// <summary>
    /// 获得/设置 可见性变化事件
    /// </summary>
    [Parameter]
    public EventCallback<bool> VisibleChanged { get; set; }

    /// <summary>
    /// 获得/设置 下拉菜单将右对齐
    /// </summary>
    [Parameter]
    public bool RightAligned
    {
        get => _state.RightAligned;
        set => _state = _state with { RightAligned = value };
    }

    /// <summary>
    /// 放置此 <see cref="NavbarDropdown"/> 的 <see cref="NavbarItem"/> 组件。
    /// </summary>
    [CascadingParameter]
    protected NavbarItem? ParentBarItem { get; set; }

    /// <summary>
    /// 获得/设置 父级 <see cref="NavbarItem"/> 组件的状态
    /// </summary>
    [CascadingParameter]
    protected NavbarItemState ParentBarItemState
    {
        get => _parentBarItemState;
        set
        {
            if (_parentBarItemState != value)
            {
                _parentBarItemState = value;
                _state = _state with
                {
                    Mode = _parentBarItemState.Mode,
                    NavbarVisible = _parentBarItemState.NavbarVisible
                };
                if (!_state.NavbarVisible)
                {
                    Visible = false;
                }
            }
        }
    }

    /// <summary>
    /// 获得/设置 父级 <see cref="NavbarDropdown"/> 组件的状态
    /// </summary>
    [CascadingParameter]
    protected NavbarDropdownState ParentBarDropdownState
    {
        get => _parentBarDropdownState;
        set
        {
            if (_parentBarDropdownState != value)
            {
                _parentBarDropdownState = value;
                _state = _state with { NestedIndex = _parentBarDropdownState.NestedIndex + 1 };
            }
        }
    }

    /// <summary>
    /// 获得/设置 父级 <see cref="NavbarDropdown"/> 组件
    /// </summary>
    [CascadingParameter]
    protected NavbarDropdown? ParentBarDropdown { get; set; }

    /// <summary>
    /// 获得/设置 子组件内容
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <inheritdoc/>
    protected void Dispose(bool disposing)
    {
        if (disposing && ParentBarDropdown is not null)
        {
            ParentBarDropdown.NotifyChildDropdownRemoved();
        }
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
