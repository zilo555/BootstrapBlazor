// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// <see cref="NavbarDropdownItem"/> 组件
/// 可出现在 <see cref="NavbarDropdownItem"/> 旁边的小文本。
/// </summary>
public partial class NavbarLabel
{
    /// <summary>
    /// 获得 样式类集合 
    /// </summary>
    /// <returns></returns>
    private string? ClassString => CssBuilder.Default("b-bar-label")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得/设置 子组件内容
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
}
