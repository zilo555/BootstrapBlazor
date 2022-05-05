// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// 浮动显示相关配置实体类
/// </summary>
public class FloatingOption: FloatingBase
{
    /// <summary>
    /// 获得/设置 浮动层的锚点元素ID
    /// </summary>
    public string? Anchor { get; set; }

    /// <summary>
    /// 获得/设置 浮动层的锚点元素选择器
    /// </summary>
    public string? Selecter { get; set; }

    /// <summary>
    /// 获得/设置 浮动层的锚点组件
    /// </summary>
    public IdComponentBase? Component { get; set; }

    /// <summary>
    /// 获得/设置 浮动层的锚点元素
    /// </summary>
    public ElementWrapper? Element { get; set; }

    /// <summary>
    /// 获得/设置 浮动层的锚点位置
    /// </summary>
    public Offset? Offset { get; set; }

    /// <summary>
    /// 获得/设置 内层子组件内容，为空时表示清除内容
    /// </summary>
    public RenderFragment? ChildContent { get; set; }
}
