using Microsoft.AspNetCore.Components.Routing;

namespace BootstrapBlazor.Components;

/// <summary>
/// 定义 <see cref="NavLink"/> 组件，指示链接文档在何处打开。
/// </summary>
public class NavLinkTarget
{
    private string? Target { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="target"></param>
    private NavLinkTarget(string? target) => Target = target;

    /// <summary>
    /// 转换为字符串
    /// </summary>
    /// <returns></returns>
    public override string? ToString() => Target;

    /// <summary>
    /// 不指定，默认同 Self
    /// </summary>
    public static NavLinkTarget None => new(null);

    /// <summary>
    /// 在相同的框架中打开被链接文档
    /// </summary>
    public static NavLinkTarget Self => new("_self");

    /// <summary>
    /// 在新窗口中打开被链接文档。
    /// </summary>
    public static NavLinkTarget Blank => new("_blank");

    /// <summary>
    /// 在父框架集中打开被链接文档。
    /// </summary>
    public static NavLinkTarget Parent => new("_parent");

    /// <summary>
    /// 在整个窗口中打开被链接文档。
    /// </summary>
    public static NavLinkTarget Top => new("_top");

    /// <summary>
    /// 在指定的框架中打开被链接文档。
    /// </summary>
    /// <param name="name">框架名称</param>
    /// <returns></returns>
    public static NavLinkTarget Frame(string name) => new(name);
}
