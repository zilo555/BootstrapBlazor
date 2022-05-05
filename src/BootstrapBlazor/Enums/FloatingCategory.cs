// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 浮动 锚点元素类型枚举
/// </summary>
internal enum FloatingCategory
{
    /// <summary>
    /// 
    /// </summary>
    None,
    /// <summary>
    /// 标识ID
    /// </summary>
    Identifier,
    /// <summary>
    /// HTML元素
    /// </summary>
    Element,
    /// <summary>
    /// 组件
    /// </summary>
    Component,
    /// <summary>
    /// CSS选择器
    /// </summary>
    Selecter,
    /// <summary>
    /// 虚拟元素
    /// </summary>
    Virtual,
}
