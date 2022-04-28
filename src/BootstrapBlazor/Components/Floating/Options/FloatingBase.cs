// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 浮动显示相关配置基类
/// </summary>
public class FloatingBase
{
    /// <summary>
    /// 获得/设置 浮动元素和参考元素之间的距离
    /// </summary>
    public int? AxisOffset { get; set; }

    /// <summary>
    /// 获得/设置 是否自动更改浮动元素的位置以使其处于可见状态。
    /// </summary>
    public bool UseFlip { get; set; }

    /// <summary>
    /// 获得/设置 浮动元素填充宽度，以使其保持在视图中
    /// </summary>
    public int? ShiftPadding { get; set; }

    /// <summary>
    /// 获得/设置 当与锚点元素不在同一个剪辑上下文中时是否自动隐藏
    /// </summary>
    public bool AutoHide { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否自动更新位置，默认true
    /// </summary>
    public bool AutoUpdate { get; set; }

    /// <summary>
    /// 获得/设置 是否使用提示箭头，浮动层必须预先设置为绝对定位
    /// </summary>
    public bool UseArrow { get; set; }

    /// <summary>
    /// 获得/设置 提示箭头的偏移量
    /// </summary>
    public int ArrowOffset { get; set; }

    /// <summary>
    /// 获得/设置 提示箭头的样式
    /// </summary>
    [JsonIgnore]
    public string? ArrowStyleClass { get; set; }

    /// <summary>
    /// 获得/设置 组件样式类
    /// </summary>
    [JsonIgnore]
    public string? @Class { get; set; }

    /// <summary>
    /// 获得/设置 组件样式
    /// </summary>
    [JsonIgnore]
    public string? @Style { get; set; }
}
