﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class Switch
{
    private string? ClassName => CssBuilder.Default("switch")
        .AddClass("is-checked", Value)
        .AddClass("disabled", IsDisabled)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? CoreClassName => CssBuilder.Default("switch-core")
        .AddClass($"border-{OnColor.ToDescriptionString()}", OnColor != Color.None && Value)
        .AddClass($"bg-{OnColor.ToDescriptionString()}", OnColor != Color.None && Value)
        .AddClass($"border-{OffColor.ToDescriptionString()}", OffColor != Color.None && !Value)
        .AddClass($"bg-{OffColor.ToDescriptionString()}", OffColor != Color.None && !Value)
        .Build();

    private string? GetInnerText()
    {
        string? ret = null;
        if (ShowInnerText)
        {
            ret = Value ? OnInnerText : OffInnerText;
        }

        return ret;
    }

    /// <summary>
    /// 获得 显示文字
    /// </summary>
    private string? Text => Value ? OnText : OffText;

    /// <summary>
    /// 获得 组件最小宽度
    /// </summary>
    private string? SwitchStyleName => CssBuilder.Default()
        .AddClass($"min-width: {Width}px;", Width > 0)
        .AddStyleFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得 Style 集合
    /// </summary>
    protected override string? StyleName => CssBuilder.Default()
        .AddClass($"width: {Width}px;", Width > 0)
        .AddClass($"height: {Height}px;", Height >= 20)
        .Build();

    /// <summary>
    /// 获得/设置 开颜色
    /// </summary>
    [Parameter]
    public Color OnColor { get; set; } = Color.Success;

    /// <summary>
    /// 获得/设置 关颜色
    /// </summary>
    [Parameter]
    public Color OffColor { get; set; }

    /// <summary>
    /// 获得/设置 组件宽度 默认 40
    /// </summary>
    [Parameter]
    public override int Width { get; set; } = 40;

    /// <summary>
    /// 获得/设置 控件高度默认 20px
    /// </summary>
    [Parameter]
    public int Height { get; set; } = 20;

    /// <summary>
    /// 获得/设置 组件 On 时内置显示文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? OnInnerText { get; set; }

    /// <summary>
    /// 获得/设置 组件 Off 时内置显示文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? OffInnerText { get; set; }

    /// <summary>
    /// 获得/设置 是否显示内置文字 默认 false 显示
    /// </summary>
    [Parameter]
    public bool ShowInnerText { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Switch>? Localizer { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        OnInnerText ??= Localizer[nameof(OnInnerText)];
        OffInnerText ??= Localizer[nameof(OffInnerText)];
    }

    /// <summary>
    /// 点击控件时触发此方法
    /// </summary>
    private async Task OnClick()
    {
        if (!IsDisabled)
        {
            Value = !Value;

            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(Value);
            }

            // 回调 OnValueChanged 再调用 EventCallback
            if (OnValueChanged != null)
            {
                await OnValueChanged(Value);
            }
        }
    }
}
