using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// 浮动层 参照元素类型
/// </summary>
public enum FloatingRefType
{
    /// <summary>
    /// CSS选择器
    /// </summary>
    [Description("cssSelecter")]
    CssSelecter,

    /// <summary>
    /// HTML元素
    /// </summary>
    [Description("elementReference")]
    ElementReference,

    /// <summary>
    /// Id组件
    /// </summary>
    [Description("idComponentBase")]
    IdComponentBase,
}
