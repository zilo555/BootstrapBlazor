// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <see cref="Floating"/> 组件状态
/// </summary>
internal class FloatingState : FloatingBase
{
    /// <summary>
    /// 获得/设置 浮动层显示状态，默认隐藏
    /// </summary>
    public bool Visible { get; set; }

    /// <summary>
    /// 获得/设置 浮动层显示时使用的定位模式，默认绝对定位
    /// </summary>
    [JsonIgnore]
    public Position Position { get; set; } = Position.Absolute;

    /// <summary>
    /// 获得/设置 浮动位置，默认右侧
    /// </summary>
    [JsonIgnore]
    public Placement Placement { get; set; } = Placement.Bottom;

    /// <summary>
    /// 获得/设置 锚点元素类型
    /// </summary>
    public FloatingCategory Category { get; set; } = FloatingCategory.Identifier;

    /// <summary>
    /// 获得/设置 浮动层容器编号
    /// </summary>
    [JsonIgnore]
    public string? ContainerId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("position")]
    public string Position_ => Position.ToDescriptionString();

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("placement")]
    public string Placement_ => Placement.ToDescriptionString();
}
