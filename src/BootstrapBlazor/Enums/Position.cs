// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// 定位布局
/// </summary>
public enum Position
{
    /// <summary>
    /// 默认值，元素按照标准流正常的显示
    /// </summary>
    [Description("static")]
    Static,

    /// <summary>
    /// 相对定位，元素依然处于正常的文档流中，可以通过left、right、bottom、top改变元素的位置
    /// </summary>
    [Description("relative")]
    Relative,

    /// <summary>
    /// 绝对定位，元素脱离文档流，可以通过 left、right、bottom、top改变元素的位置，它会基于游览器的四个边角进行定位
    /// </summary>
    [Description("absolute")]
    Absolute,

    /// <summary>
    /// 固定定位，使用 top，left，right，bottom 定位，会脱离正常文档流，不受标准流的约束，并拥有层级的概念
    /// </summary>
    [Description("fixed")]
    Fixed,

    /// <summary>
    /// 会继承父元素的属性
    /// </summary>
    [Description("inherit")]
    Inherit,
}
