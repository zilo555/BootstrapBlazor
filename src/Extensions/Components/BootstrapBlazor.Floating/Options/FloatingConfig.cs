using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 浮动层 显示状态
/// </summary>
internal record FloatingConfig
{
    /// <summary>
    /// 浮动层显示状态控制
    /// </summary>
    public bool Show { get; set; }

    /// <summary>
    /// 使用虚拟元素
    /// </summary>
    public bool UseVirtualElement { get; set; }

    /// <summary>
    /// 浮动层 参照元素类型
    /// </summary>
    [JsonIgnore]
    public FloatingRefType ReferenceType { get; set; }

    [JsonPropertyName("referenceType")]
    public string ReferenceType_ => ReferenceType.ToDescriptionString();

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    public FloatingState InitialState { get; set; } = FloatingState.Hidden;

    [JsonPropertyName("initialState")]
    public string InitialState_ => InitialState.ToDescriptionString();

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    public PositionStrategy Strategy { get; set; } = PositionStrategy.Absolute;

    [JsonPropertyName("strategy")]
    public string Strategy_=> Strategy.ToDescriptionString();

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    public Placement Placement { get; set; } = Placement.Right;

    [JsonPropertyName("placement")]
    public string Placement_ => Placement.ToDescriptionString();

    /// <summary>
    /// 
    /// </summary>
    public int MainAxis { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int ShiftPadding { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool UseArrow { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int ArrowOffset { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool AutoUpdate { get; set; }

    /// <summary>
    /// 只有在参照对象是IdComponentBase组件时才存在
    /// </summary>
    public string? RefElementId { get; set; }

    /// <summary>
    /// 客户端回传的style Top
    /// </summary>
    [JsonIgnore]
    public decimal? Top { get; set; }

    /// <summary>
    /// 客户端回传的style Left
    /// </summary>
    [JsonIgnore]
    public decimal? Left { get; set; }

    /// <summary>
    /// 客户端回传的Arrow style Top
    /// </summary>
    [JsonIgnore]
    public decimal? ArrowTop { get; set; }

    /// <summary>
    /// 客户端回传的Arrow style Left
    /// </summary>
    [JsonIgnore]
    public decimal? ArrowLeft { get; set; }

    /// <summary>
    /// 客户端回传的Arrow style Position
    /// </summary>
    [JsonIgnore]
    public string? ArrowSide { get; set; }

    /// <summary>
    /// The X coordinate of the mouse pointer in local (DOM content) coordinates.
    /// </summary>
    public double ClientX { get; set; }

    /// <summary>
    /// The Y coordinate of the mouse pointer in local (DOM content) coordinates.
    /// </summary>
    public double ClientY { get; set; }

}
