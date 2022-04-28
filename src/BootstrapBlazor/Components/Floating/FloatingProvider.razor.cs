// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// <see cref="Floating"/> 服务渲染容器组件
/// </summary>
public partial class FloatingProvider : IDisposable
{
    private RenderFragment? Content { get; set; }

    private FloatingOption? Options { get; set; }

    /// <summary>
    /// 获得/设置 Floating 服务实例   
    /// </summary>
    [Inject]
    [NotNull]
    private FloatingService? FloatingService { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        FloatingService.Register(this, Show);
    }

    private async Task Show(FloatingOption option)
    {
        if (option.ChildContent == null)
        {
            Options = null;
            Content = null;
        }
        else
        {
            Options = option;
            Content = builder =>
            {
                var index = 0;
                builder.OpenComponent<Floating>(index++);
                builder.AddAttribute(index++, nameof(Floating.Anchor), option.Anchor);
                builder.AddAttribute(index++, nameof(Floating.Selecter), option.Selecter);
                builder.AddAttribute(index++, nameof(Floating.Component), option.Component);
                builder.AddAttribute(index++, nameof(Floating.Wrapper), option.Element);
                builder.AddAttribute(index++, nameof(Floating.Offset), option.Offset);
                builder.AddAttribute(index++, nameof(Floating.Visible), true);
                builder.AddAttribute(index++, nameof(Floating.Placement), option.Placement);
                builder.AddAttribute(index++, nameof(Floating.Position), option.Position);
                builder.AddAttribute(index++, nameof(Floating.ChildContent), option.ChildContent);
                builder.AddAttribute(index++, nameof(Floating.ContainerId), option.ContainerId);
                builder.AddAttribute(index++, nameof(Floating.AutoHide), option.AutoHide);
                builder.AddAttribute(index++, nameof(Floating.AutoUpdate), option.AutoUpdate);
                builder.AddAttribute(index++, nameof(Floating.AxisOffset), option.AxisOffset);
                builder.AddAttribute(index++, nameof(Floating.UseFlip), option.UseFlip);
                builder.AddAttribute(index++, nameof(Floating.ShiftPadding), option.ShiftPadding);
                builder.AddAttribute(index++, nameof(Floating.UseArrow), option.UseArrow);
                builder.AddAttribute(index++, nameof(Floating.ArrowOffset), option.ArrowOffset);
                builder.AddAttribute(index++, nameof(Floating.ArrowStyleClass), option.ArrowStyleClass);
                builder.AddAttribute(index++, nameof(Floating.Class), option.Class);
                builder.AddAttribute(index++, nameof(Floating.Style), option.Style);
                builder.CloseComponent();
            };
        }

        await InvokeAsync(StateHasChanged);
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            FloatingService.UnRegister(this);
        }
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
