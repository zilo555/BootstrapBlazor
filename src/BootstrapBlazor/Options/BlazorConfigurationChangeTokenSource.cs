// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace BootstrapBlazor.Components;

/// <summary>
/// ConfigurationChangeTokenSource 配置改变监控类
/// </summary>
/// <typeparam name="TOptions"></typeparam>
public class BlazorConfigurationChangeTokenSource<TOptions> : ConfigurationChangeTokenSource<TOptions>
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="config"></param>
    public BlazorConfigurationChangeTokenSource(IConfiguration config)
        : base(config.GetSection(typeof(TOptions).Name))
    {

    }
}
