// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.JSInterop;
using System.Reflection;

namespace BootstrapBlazor.Shared.Components;

public partial class Test
{
    [NotNull]
    private IJSObjectReference? Module { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            Module = await JSRuntime.InvokeAsync<IJSObjectReference>(identifier: "import", $"./_content/BootstrapBlazor/ts/yyy.js");
        }
    }

    private async Task OnClick()
    {
        await Module.InvokeVoidAsync("test");
    }
}
