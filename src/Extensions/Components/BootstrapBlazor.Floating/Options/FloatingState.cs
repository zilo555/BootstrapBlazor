namespace BootstrapBlazor.Components;

/// <summary>
///  浮动层 位置参数
/// </summary>
internal record FloatingState
{
    /// <summary>
    /// 
    /// </summary>
    public decimal? FloatingTop { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public decimal? FloatingLeft { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public decimal? ArrowTop { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public decimal? ArrowLeft { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? ArrowOffset { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="floatingLeft"></param>
    /// <param name="floatingTop"></param>
    public void UpdateFloating(decimal? floatingLeft, decimal? floatingTop)
    {
        FloatingLeft = floatingLeft;
        FloatingTop = floatingTop;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="arrowTop"></param>
    /// <param name="arrowLeft"></param>
    /// <param name="arrowOffset"></param>
    public void UpdateArrow(decimal? arrowLeft, decimal? arrowTop, string? arrowOffset)
    {
        ArrowTop = arrowTop;
        ArrowLeft = arrowLeft;
        ArrowOffset = arrowOffset;
    }

    /// <summary>
    /// 
    /// </summary>
    public void Reset()
    {
        FloatingTop = null;
        FloatingLeft = null;
        ArrowTop = null;
        ArrowLeft = null;
        ArrowOffset = null;
    }
}
