using Microsoft.AspNetCore.Components.Web;

namespace BootstrapBlazor.Components;

/// <summary>
/// 浮动层 触发事件约定
/// </summary>
/// <param name="e"></param>
internal delegate void FloatingTriggerEventHandler(FloatingEventArgs e);

/// <summary>
/// 浮动层 事件参数
/// </summary>
internal sealed class FloatingEventArgs : EventArgs
{
    /// <summary>
    /// 
    /// </summary>
    public bool Show { get; private set; }

    /// <summary>
    /// 源事件类型
    /// </summary>
    public string? Source { get; private set; }

    /// <summary>
    /// Gets or sets the type of the event.
    /// </summary>
    public string? Type { get; private set; }

    /// <summary>
    /// The X coordinate of the mouse pointer in local (DOM content) coordinates.
    /// </summary>
    public double ClientX { get; private set; }

    /// <summary>
    /// The Y coordinate of the mouse pointer in local (DOM content) coordinates.
    /// </summary>
    public double ClientY { get; private set; }

    public FloatingEventArgs(bool show) => Show = show;

    public FloatingEventArgs(MouseEventArgs args, bool show) : this(show)
    {
        this.Source = "Mouse";
        this.Type = args.Type;
        this.ClientX = args.ClientX;
        this.ClientY = args.ClientY;
    }

    public FloatingEventArgs(FocusEventArgs args, bool show) : this(show)
    {
        this.Source = "Focus";
        this.Type = args.Type;
    }

    public static FloatingEventArgs Create<TEventArgs>(TEventArgs args, bool show) where TEventArgs : EventArgs
    {
        return args switch
        {
            MouseEventArgs mouse => new(mouse, show),
            FocusEventArgs focus => new(focus, show),
            _ => new(show)
        };
    }
}
