// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Web;

namespace UnitTest.Components;

public class FloatingTest : BootstrapBlazorTestBase
{
    [Fact]
    public void FloatingReference_Ok()
    {
        var cut = Context.Render(pb =>
        {
            pb.OpenElement(10, "div");
            pb.AddAttribute(11, "id", "anchor");
            pb.AddContent(12, "anchor element");
            pb.CloseElement();
            pb.OpenComponent<Floating>(13);
            pb.AddAttribute(14, nameof(Floating.Anchor), "anchor");
            pb.AddAttribute(15, nameof(Floating.Visible), true);
            pb.AddAttribute(15, nameof(Floating.Position), Position.Absolute);
            pb.AddAttribute(16, nameof(Floating.ChildContent), CreateContent("Anchor"));
            pb.CloseComponent();

            var wrapper = new ElementWrapper();
            pb.OpenElement(20, "div");
            pb.AddContent(21, "anchor element");
            pb.AddElementReferenceCapture(22, (r) => wrapper.Ref = r);
            pb.CloseElement();
            pb.OpenComponent<Floating>(23);
            pb.AddAttribute(24, nameof(Floating.Wrapper), wrapper);
            pb.AddAttribute(25, nameof(Floating.Visible), true);
            pb.AddAttribute(15, nameof(Floating.Position), Position.Fixed);
            pb.AddAttribute(26, nameof(Floating.ChildContent), CreateContent("Wrapper"));
            pb.CloseComponent();

            Button? button = new();
            var visible = true;
            pb.OpenComponent<Button>(30);
            pb.AddAttribute(31, nameof(Button.Text), "anchor element");
            pb.AddAttribute(32, nameof(Button.OnClick), EventCallback.Factory.Create<MouseEventArgs>(this, e => visible = !visible));
            pb.AddComponentReferenceCapture(33, (@ref) => button = @ref as Button);
            pb.CloseComponent();
            pb.OpenComponent<Floating>(34);
            pb.AddAttribute(35, nameof(Floating.Component), button);
            pb.AddAttribute(36, nameof(Floating.Visible), visible);
            pb.AddAttribute(37, nameof(Floating.ChildContent), CreateContent("Component"));
            pb.CloseComponent();

            pb.OpenElement(40, "div");
            pb.AddAttribute(41, "class", "anchor");
            pb.AddContent(42, "anchor element");
            pb.CloseElement();
            pb.OpenComponent<Floating>(43);
            pb.AddAttribute(44, nameof(Floating.Selecter), ".anchor");
            pb.AddAttribute(45, nameof(Floating.Visible), true);
            pb.AddAttribute(46, nameof(Floating.ChildContent), CreateContent("Selecter"));
            pb.CloseComponent();

            var offset = new Offset();
            pb.OpenComponent<Floating>(50);
            pb.AddAttribute(51, nameof(Floating.Offset), offset);
            pb.AddAttribute(52, nameof(Floating.Visible), true);
            pb.AddAttribute(53, nameof(Floating.ChildContent), CreateContent("Offset"));
            pb.CloseComponent();
        });
        //Anchor
        cut.Find("#Anchor").TextContent.MarkupMatches("Create floating content.");
        Assert.NotEmpty(cut.Find("#Anchor").ParentElement?.GetAttribute("data-floating-placement"));
        //Wrapper
        cut.Find("#Wrapper").TextContent.MarkupMatches("Create floating content.");
        Assert.NotEmpty(cut.Find("#Wrapper").ParentElement?.GetAttribute("data-floating-placement"));
        //Component
        cut.Find("#Component").TextContent.MarkupMatches("Create floating content.");
        Assert.NotEmpty(cut.Find("#Component").ParentElement?.GetAttribute("data-floating-placement"));
        //Selecter
        cut.Find("#Selecter").TextContent.MarkupMatches("Create floating content.");
        Assert.NotEmpty(cut.Find("#Selecter").ParentElement?.GetAttribute("data-floating-placement"));
        //Offset
        cut.Find("#Offset").TextContent.MarkupMatches("Create floating content.");
        Assert.NotEmpty(cut.Find("#Offset").ParentElement?.GetAttribute("data-floating-placement"));

        //Not set anchor Reference
        Assert.Throws<ArgumentNullException>(()
            => Context.RenderComponent<Floating>(pb =>
            {
                pb.Add(s => s.Visible, false);
            }));
    }

    [Fact]
    public void FloatingPlacement_Ok()
    {
        foreach (var item in Enum.GetValues<Placement>())
        {
            var cut = Context.Render(pb =>
            {
                pb.OpenElement(0, "div");
                pb.AddAttribute(1, "id", "anchor");
                pb.AddContent(2, "anchor element");
                pb.CloseElement();
                pb.OpenComponent<Floating>(3);
                pb.AddAttribute(4, nameof(Floating.Anchor), "anchor");
                pb.AddAttribute(5, nameof(Floating.Visible), true);
                pb.AddAttribute(6, nameof(Floating.Placement), item);
                pb.AddAttribute(7, nameof(Floating.ChildContent), CreateContent("Anchor"));
                pb.CloseComponent();
            });

            var el = cut.Find("#Anchor").ParentElement;
            var placement = el?.GetAttribute("data-floating-placement");
            Assert.NotNull(el);
            Assert.Equal(item.ToDescriptionString(), placement);
        }
    }

    [Fact]
    public void FloatingPosition_Ok()
    {
        //only support Absolute or Fixed
        Assert.Throws<NotSupportedException>(()
            => Context.Render(pb =>
            {
                pb.OpenElement(0, "div");
                pb.AddAttribute(1, "id", "anchor");
                pb.AddContent(2, "anchor element");
                pb.CloseElement();
                pb.OpenComponent<Floating>(3);
                pb.AddAttribute(4, nameof(Floating.Anchor), "anchor");
                pb.AddAttribute(5, nameof(Floating.Visible), true);
                pb.AddAttribute(6, nameof(Floating.Position), Position.Static);
                pb.AddAttribute(7, nameof(Floating.ChildContent), CreateContent("Anchor"));
                pb.CloseComponent();
            }));

        Assert.Throws<NotSupportedException>(()
            => Context.Render(pb =>
            {
                pb.OpenElement(0, "div");
                pb.AddAttribute(1, "id", "anchor");
                pb.AddContent(2, "anchor element");
                pb.CloseElement();
                pb.OpenComponent<Floating>(3);
                pb.AddAttribute(4, nameof(Floating.Anchor), "anchor");
                pb.AddAttribute(5, nameof(Floating.Visible), true);
                pb.AddAttribute(6, nameof(Floating.Position), Position.Relative);
                pb.AddAttribute(7, nameof(Floating.ChildContent), CreateContent("Anchor"));
                pb.CloseComponent();
            }));

        Assert.Throws<NotSupportedException>(()
            => Context.Render(pb =>
            {
                pb.OpenElement(0, "div");
                pb.AddAttribute(1, "id", "anchor");
                pb.AddContent(2, "anchor element");
                pb.CloseElement();
                pb.OpenComponent<Floating>(3);
                pb.AddAttribute(4, nameof(Floating.Anchor), "anchor");
                pb.AddAttribute(5, nameof(Floating.Visible), true);
                pb.AddAttribute(6, nameof(Floating.Position), Position.Inherit);
                pb.AddAttribute(7, nameof(Floating.ChildContent), CreateContent("Anchor"));
                pb.CloseComponent();
            }));
    }

    [Fact]
    public void FloatingContainer_Ok()
    {
        var cut01 = Context.Render(pb =>
        {
            pb.OpenComponent<FloatingContainer>(0);
            pb.AddAttribute(1, nameof(FloatingContainer.Id), "Container1");
            pb.CloseComponent();

            pb.OpenElement(2, "div");
            pb.AddAttribute(3, "id", "anchor");
            pb.AddContent(4, "anchor element");
            pb.CloseElement();

            pb.OpenComponent<Floating>(5);
            pb.AddAttribute(6, nameof(Floating.Anchor), "anchor");
            pb.AddAttribute(7, nameof(Floating.Visible), true);
            pb.AddAttribute(8, nameof(Floating.ContainerId), "Container1");
            pb.AddAttribute(9, nameof(Floating.ChildContent), CreateContent("Anchor"));
            pb.CloseComponent();
        });
        //判断是否包含在容器中
        var p01 = cut01.Find("#Container1").CompareDocumentPosition(cut01.Find("#Anchor").ParentElement!);
        Assert.True(p01.HasFlag(AngleSharp.Dom.DocumentPositions.ContainedBy));

        var cut02 = Context.Render(pb =>
        {
            pb.OpenComponent<FloatingContainer>(0);
            pb.AddAttribute(1, nameof(FloatingContainer.Id), "Container2");
            pb.CloseComponent();

            pb.OpenElement(2, "div");
            pb.AddAttribute(3, "id", "anchor");
            pb.AddContent(4, "anchor element");
            pb.CloseElement();

            pb.OpenComponent<Floating>(5);
            pb.AddAttribute(6, nameof(Floating.Anchor), "anchor");
            pb.AddAttribute(7, nameof(Floating.Visible), true);
            pb.AddAttribute(8, nameof(Floating.ContainerId), "xxxxxx");
            pb.AddAttribute(9, nameof(Floating.ChildContent), CreateContent("Anchor"));
            pb.CloseComponent();
        });
        //设置了一个错误的ContainerId，判断是否包含在容器中
        var p02 = cut02.Find("#Container2").CompareDocumentPosition(cut02.Find("#Anchor").ParentElement!);
        Assert.False(p02.HasFlag(AngleSharp.Dom.DocumentPositions.ContainedBy));
        Assert.Equal(AngleSharp.Dom.DocumentPositions.Following, p02);

        //判断容器ID必须唯一
        Assert.Throws<ArgumentException>(() => Context.Render(pb =>
          {
              pb.OpenComponent<FloatingContainer>(0);
              pb.AddAttribute(1, nameof(FloatingContainer.Id), "Container3");
              pb.CloseComponent();

              pb.OpenComponent<FloatingContainer>(0);
              pb.AddAttribute(1, nameof(FloatingContainer.Id), "Container3");
              pb.CloseComponent();
          }));
    }

    [Fact]
    public void FloatingCompute_Ok()
    {
        object floating = new();
        var cut = Context.Render(pb =>
        {
            pb.OpenElement(0, "div");
            pb.AddAttribute(1, "id", "anchor");
            pb.AddContent(2, "anchor element");
            pb.CloseElement();
            pb.OpenComponent<Floating>(3);
            pb.AddAttribute(4, nameof(Floating.Anchor), "anchor");
            pb.AddAttribute(5, nameof(Floating.Visible), true);
            pb.AddAttribute(5, nameof(Floating.Placement), Placement.Bottom);
            pb.AddAttribute(6, nameof(Floating.AutoHide), true);
            pb.AddAttribute(6, nameof(Floating.AutoUpdate), true);
            pb.AddAttribute(6, nameof(Floating.AxisOffset), 5);
            pb.AddAttribute(6, nameof(Floating.UseFlip), true);
            pb.AddAttribute(6, nameof(Floating.ShiftPadding), 6);
            pb.AddAttribute(7, nameof(Floating.ChildContent), CreateContent("Anchor"));
            pb.AddComponentReferenceCapture(8, (@ref) => floating = @ref);
            pb.CloseComponent();
        });

        Assert.IsType<Floating>(floating);
        var config = (floating as Floating)?.FloatingConfig ?? string.Empty;
        Assert.Contains("Visible", config);
        Assert.Contains("Category", config);
        Assert.Contains("position", config);
        Assert.Contains("placement", config);
        Assert.Contains("AxisOffset", config);
        Assert.Contains("UseFlip", config);
        Assert.Contains("ShiftPadding", config);
        Assert.Contains("AutoHide", config);
        Assert.Contains("AutoUpdate", config);
        Assert.Contains("UseArrow", config);
        Assert.Contains("ArrowOffset", config);
    }

    [Fact]
    public void FloatingArrow_Ok()
    {
        var cut = Context.Render(pb =>
        {
            pb.OpenElement(0, "div");
            pb.AddAttribute(1, "id", "anchor");
            pb.AddContent(2, "anchor element");
            pb.CloseElement();
            pb.OpenComponent<Floating>(3);
            pb.AddAttribute(4, nameof(Floating.Anchor), "anchor");
            pb.AddAttribute(5, nameof(Floating.Visible), true);
            pb.AddAttribute(6, nameof(Floating.UseArrow), true);
            pb.AddAttribute(6, nameof(Floating.ArrowOffset), -4);
            pb.AddAttribute(6, nameof(Floating.ArrowStyleClass), "test-class");
            pb.AddAttribute(7, nameof(Floating.ChildContent), CreateContent("Anchor"));
            pb.CloseComponent();
        });

        var arrow = cut.Find("#Anchor").NextElementSibling;
        Assert.NotNull(arrow);
        Assert.Equal("arrow", arrow?.GetAttribute("data-element"));
        Assert.Contains("test-class", arrow?.GetAttribute("class"));
    }

    [Fact]
    public void FloatingStyleClass_Ok()
    {
        var cut = Context.Render(pb =>
        {
            pb.OpenElement(0, "div");
            pb.AddAttribute(1, "id", "anchor");
            pb.AddContent(2, "anchor element");
            pb.CloseElement();
            pb.OpenComponent<Floating>(3);
            pb.AddAttribute(4, nameof(Floating.Anchor), "anchor");
            pb.AddAttribute(5, nameof(Floating.Visible), true);
            pb.AddAttribute(6, nameof(Floating.Class), "test-class");
            pb.AddAttribute(6, nameof(Floating.Style), "margin:0;");
            pb.AddAttribute(7, nameof(Floating.ChildContent), CreateContent("Anchor"));
            pb.CloseComponent();
        });

        var floating = cut.Find("#Anchor").ParentElement;
        Assert.NotNull(floating);
        Assert.Contains("margin:0;", floating?.GetAttribute("style"));
        Assert.Contains("test-class", floating?.GetAttribute("class"));
    }

    [Fact]
    public void FloatingOption_Defaults()
    {
        var option = new FloatingOption() { Anchor = "Test" };
        Assert.Equal("Test", option.Anchor);
        Assert.Null(option.Selecter);
        Assert.Null(option.Component);
        Assert.Null(option.Element);
        Assert.Null(option.Offset);
        Assert.Null(option.ChildContent);
        Assert.Equal(Position.Absolute, option.Position);
        Assert.Equal(Placement.Bottom, option.Placement);
        Assert.Null(option.ContainerId);
        Assert.Null(option.AxisOffset);
        Assert.False(option.UseFlip);
        Assert.Null(option.ShiftPadding);
        Assert.True(option.AutoHide);
        Assert.False(option.AutoUpdate);
        Assert.False(option.UseArrow);
        Assert.Equal(0, option.ArrowOffset);
        Assert.Null(option.ArrowStyleClass);
        Assert.Null(option.Class);
        Assert.Null(option.Style);
    }

    private static RenderFragment CreateContent(string uniqueKey)
        => builder =>
        {
            builder.OpenElement(0, "span");
            builder.AddAttribute(1, "id", uniqueKey);
            builder.AddContent(2, "Create floating content.");
            builder.CloseElement();
        };

}
