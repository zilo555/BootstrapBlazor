// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// 组件间传递元素的包装器
/// </summary>
public record ElementWrapper
{
    /// <summary>
    /// 用于在组件之间传递的渲染元素
    /// </summary>
    public ElementReference Ref { get; set; }
}
