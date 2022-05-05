﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Shared.Shared;

/// <summary>
///
/// </summary>
public sealed partial class NavMenu
{
    private bool collapseNavMenu = true;

    private bool IsAccordion { get; set; }

    private bool IsExpandAll { get; set; }

    [NotNull]
    private string? AccordionText { get; set; }

    [NotNull]
    private string? ExpandAllText { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<App>? AppLocalizer { get; set; }

    [Inject]
    [NotNull]
    private TitleService? TitleService { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<NavMenu>? Localizer { get; set; }

    private string? NavMenuCssClass => CssBuilder.Default("sidebar-content")
        .AddClass("collapse", collapseNavMenu)
        .Build();

    private List<MenuItem> Menus { get; set; } = new List<MenuItem>(100);

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        InitMenus();

        AccordionText ??= Localizer["MenuAccordion"];
        ExpandAllText ??= Localizer["MenuExpandAll"];
    }

    private async Task OnClickMenu(MenuItem item)
    {
        if (!item.Items.Any())
        {
            ToggleNavMenu();
            StateHasChanged();
        }

        if (!item.Items.Any() && !string.IsNullOrEmpty(item.Text))
        {
            await TitleService.SetTitle($"{item.Text} - {AppLocalizer["Title"]}");
        }
    }

    private void ToggleNavMenu()
    {
        collapseNavMenu = !collapseNavMenu;
    }

    private void InitMenus()
    {
        // 快速入门
        var item = new DemoMenuItem()
        {
            Text = Localizer["GetStarted"],
            Icon = "fa fa-fw fa-fa"
        };
        AddQuickStar(item);

        item = new DemoMenuItem()
        {
            Text = Localizer["LayoutComponents"],
            Icon = "fa fa-fw fa-desktop"
        };
        AddLayout(item);

        item = new DemoMenuItem()
        {
            Text = Localizer["NavigationComponents"],
            Icon = "fa fa-fw fa-bars"
        };
        AddNavigation(item);

        item = new DemoMenuItem()
        {
            Text = Localizer["FormsComponents"],
            Icon = "fa fa-fw fa-cubes"
        };
        AddForm(item);

        item = new DemoMenuItem()
        {
            Text = Localizer["DataComponents"],
            Icon = "fa fa-fw fa-database"
        };
        AddData(item);

        item = new DemoMenuItem()
        {
            Text = Localizer["Charts"],
            Icon = "fa fa-fw fa-line-chart"
        };
        AddChart(item);

        item = new DemoMenuItem()
        {
            Text = Localizer["NotificationComponents"],
            Icon = "fa fa-fw fa-comments"
        };
        AddNotice(item);

        item = new DemoMenuItem()
        {
            Text = Localizer["SpeechComponents"],
            Icon = "fa fa-fw fa-microphone"
        };
        AddSpeech(item);

        item = new DemoMenuItem()
        {
            Text = Localizer["Components"],
            Icon = "fa fa-fw fa-fa",
            Url = "components"
        };
        AddSummary(item);
    }

    private void AddSpeech(DemoMenuItem item)
    {
        item.Items = new List<DemoMenuItem>
        {
            new()
            {
                IsNew = true,
                Text = Localizer["SpeechIntro"],
                Url = "speechs"
            },
            new()
            {
                Text = Localizer["Recognizer"],
                Url = "recognizers"
            },
            new()
            {
                Text = Localizer["Synthesizer"],
                Url = "synthesizers"
            },
            new()
            {
                Text = Localizer["SpeechWave"],
                Url = "speechwaves"
            }
        };
        AddBadge(item);
    }

    private void AddQuickStar(DemoMenuItem item)
    {
        item.Items = new List<DemoMenuItem>
        {
            new()
            {
                Text = Localizer["Introduction"],
                Url = "introduction"
            },
            new()
            {
                Text = Localizer["Install"],
                Url = "install",
                Match = NavLinkMatch.All
            },
            new()
            {
                Text = Localizer["ProjectTemplate"],
                Url = "template"
            },
            new()
            {
                Text = Localizer["Globalization"],
                Url = "globalization"
            },
            new()
            {
                Text = Localizer["Localization"],
                Url = "localization"
            },
            new()
            {
                Text = Localizer["Labels"],
                Url = "labels"
            },
            new()
            {
                Text = Localizer["GlobalException"],
                Url = "globalexception"
            },
            new()
            {
                Text = Localizer["ServerBlazor"],
                Url = "install-server",
                Match = NavLinkMatch.All
            },
            new()
            {
                Text = Localizer["ClientBlazor"],
                Url = "install-wasm",
                Match = NavLinkMatch.All
            },
            new()
            {
                IsNew = true,
                Text = Localizer["Breakpoints"],
                Url = "breakpoints"
            },
            new()
            {
                Text = Localizer["ZIndex"],
                Url = "layout"
            },
            new()
            {
                Text = Localizer["Theme"],
                Url = "themes"
            },
            new()
            {
                Text = Localizer["FAIcon"],
                Url = "fa-icons"
            },
            new()
            {
                Text = Localizer["LayoutPage"],
                Url = "layout-page"
            }
        };
        AddBadge(item, count: 0);
    }

    private void AddForm(DemoMenuItem item)
    {
        item.Items = new List<DemoMenuItem>
        {
            new()
            {
                Text = Localizer["AutoComplete"],
                Url = "autocompletes"
            },
            new()
            {
                Text = Localizer["AutoFill"],
                Url = "autofills"
            },
            new()
            {
                Text = Localizer["Button"],
                Url = "buttons"
            },
            new()
            {
                Text = Localizer["Cascader"],
                Url = "cascaders"
            },
            new()
            {
                Text = Localizer["Checkbox"],
                Url = "checkboxs"
            },
            new()
            {
                Text = Localizer["CheckboxList"],
                Url = "checkboxlists"
            },
            new()
            {
                Text = Localizer["ColorPicker"],
                Url = "colorpickers"
            },
            new()
            {
                Text = Localizer["DateTimePicker"],
                Url = "datetimepickers"
            },
            new()
            {
                Text = Localizer["DateTimeRange"],
                Url = "datetimeranges"
            },
            new()
            {
                Text = Localizer["Editor"],
                Url = "editors"
            },
            new()
            {
                Text = Localizer["EditorForm"],
                Url = "editorforms"
            },
            new()
            {
                Text = Localizer["FloatingLabel"],
                Url = "floatinglabels"
            },
            new()
            {
                Text = Localizer["Input"],
                Url = "inputs"
            },
            new()
            {
                Text = Localizer["InputNumber"],
                Url = "inputnumbers"
            },
            new()
            {
                Text = Localizer["InputGroup"],
                Url = "inputgroups"
            },
            new()
            {
                Text = Localizer["Ip"],
                Url = "ips"
            },
            new()
            {
                Text = Localizer["Markdown"],
                Url = "markdowns"
            },
            new()
            {
                Text = Localizer["MultiSelect"],
                Url = "multiselects"
            },
            new()
            {
                Text = Localizer["OnScreenKeyboard"],
                Url = "onscreenkeyboards"
            },
            new()
            {
                Text = Localizer["Radio"],
                Url = "radios"
            },
            new()
            {
                Text = Localizer["Rate"],
                Url = "rates"
            },
            new()
            {
                Text = Localizer["Select"],
                Url = "selects"
            },
            new()
            {
                Text = Localizer["Slider"],
                Url = "sliders"
            },
            new()
            {
                Text = Localizer["Switch"],
                Url = "switchs"
            },
            new()
            {
                Text = Localizer["Textarea"],
                Url = "textareas"
            },
            new()
            {
                Text = Localizer["Toggle"],
                Url = "toggles"
            },
            new()
            {
                Text = Localizer["Transfer"],
                Url = "transfers"
            },
            new()
            {
                Text = Localizer["Upload"],
                Url = "uploads"
            },
            new()
            {
                Text = Localizer["ValidateForm"],
                Url = "validateforms"
            }
        };
        AddBadge(item);
    }

    private void AddData(DemoMenuItem item)
    {
        var tableItem = new DemoMenuItem()
        {
            Text = Localizer["Table"],
            Items = TableItems()
        };
        AddBadge(tableItem, false);

        item.Items = new List<DemoMenuItem>
        {
            new()
            {
                Text = Localizer["Ajax"],
                Url = "ajaxs"
            },
            new()
            {
                Text = Localizer["Avatar"],
                Url = "avatars"
            },
            new()
            {
                Text = Localizer["Badge"],
                Url = "badges"
            },
            new()
            {
                Text = Localizer["BarcodeReader"],
                Url = "barcodereaders"
            },
            new()
            {
                Text = Localizer["Block"],
                Url = "blocks"
            },
            new()
            {
                Text = Localizer["Card"],
                Url = "cards"
            },
            new()
            {
                Text = Localizer["Calendar"],
                Url = "calendars"
            },
            new()
            {
                Text = Localizer["Camera"],
                Url = "Cameras"
            },
            new()
            {
                Text = Localizer["Captcha"],
                Url = "captchas"
            },
            new()
            {
                Text = Localizer["Carousel"],
                Url = "carousels"
            },
            new()
            {
                Text = Localizer["Client"],
                Url = "client"
            },
            new()
            {
                Text = Localizer["Circle"],
                Url = "circles"
            },
            new()
            {
                Text = Localizer["Collapse"],
                Url = "collapses"
            },
            new()
            {
                Text = Localizer["Display"],
                Url = "displays"
            },
            new()
            {
                Text = Localizer["DropdownWidget"],
                Url = "dropdownwidgets"
            },
            new ()
            {
                Text=Localizer["Empty"],
                Url = "empties"
            },
            new()
            {
                Text = Localizer["Geolocation"],
                Url = "geolocations"
            },
            new()
            {
                Text = Localizer["GroupBox"],
                Url = "groupboxs"
            },
            new()
            {
                Text = Localizer["HandwrittenPage"],
                Url = "handwrittenpage"
            },
            new()
            {
                Text = Localizer["LinkButton"],
                Url = "linkbuttons"
            },
            new()
            {
                Text = Localizer["ListView"],
                Url = "listviews"
            },
            new()
            {
                Text = Localizer["Locator"],
                Url = "locators"
            },
            new()
            {
                Text = Localizer["ImageViewer"],
                Url = "imageviewers"
            },
            new()
            {
                Text = Localizer["Print"],
                Url = "prints"
            },
            new()
            {
                Text = Localizer["QRCode"],
                Url = "qrcodes"
            },
            new()
            {
                Text = Localizer["Recognizer"],
                Url = "recognizers"
            },
            new()
            {
                Text = Localizer["Search"],
                Url = "searchs"
            },
            new()
            {
                Text = Localizer["SignaturePad"],
                Url = "signaturepads",
            },
            new()
            {
                Text = Localizer["SpeechWave"],
                Url = "speechwaves"
            },
            tableItem,
            new()
            {
                Text = Localizer["Tag"],
                Url = "tags"
            },
            new()
            {
                Text = Localizer["Timeline"],
                Url = "timelines"
            },
            new()
            {
                Text = Localizer["Title"],
                Url = "titles"
            },
            new()
            {
                Text = Localizer["Download"],
                Url = "downloads"
            },
            new()
            {
                Text = Localizer["Tree"],
                Url = "trees"
            },
            new()
            {
                Text = Localizer["Transition"],
                Url = "transitions"
            }
        };
        AddBadge(item);
    }

    private void AddChart(DemoMenuItem item)
    {
        item.Items = new List<DemoMenuItem>
        {
            new()
            {
                Text = Localizer["ChartSummary"],
                Url = "charts/index"
            },
            new()
            {
                Text = Localizer["ChartLine"],
                Url = "charts/line"
            },
            new()
            {
                Text = Localizer["ChartBar"],
                Url = "charts/bar"
            },
            new()
            {
                Text = Localizer["ChartPie"],
                Url = "charts/pie"
            },
            new()
            {
                Text = Localizer["ChartDoughnut"],
                Url = "charts/doughnut"
            },
            new()
            {
                Text = Localizer["ChartBubble"],
                Url = "charts/bubble"
            }
        };
        AddBadge(item, count: 5);
    }

    private IEnumerable<DemoMenuItem> TableItems()
    {
        var item = new List<DemoMenuItem>
        {
            new()
            {
                Text = Localizer["TableBase"],
                Url = "tables",
                Match = NavLinkMatch.All
            },
            new()
            {
                Text = Localizer["TableColumn"],
                Url = "tables/column"
            },
            new()
            {
                Text = Localizer["TableCell"],
                Url = "tables/cell"
            },
            new()
            {
                Text = Localizer["TableLookup"],
                Url = "tables/lookup"
            },
            new()
            {
                Text = Localizer["TableRow"],
                Url = "tables/row"
            },
            new()
            {
                Text = Localizer["TableDetail"],
                Url = "tables/detail"
            },
            new()
            {
                Text = Localizer["TableDynamic"],
                Url = "tables/dynamic"
            },
            new()
            {
                Text = Localizer["TableSearch"],
                Url = "tables/search"
            },
            new()
            {
                Text = Localizer["TableFilter"],
                Url = "tables/filter"
            },
            new()
            {
                Text = Localizer["TableFixHeader"],
                Url = "tables/header"
            },
            new()
            {
                Text = Localizer["TableHeaderGroup"],
                Url = "tables/multi-header"
            },
            new()
            {
                Text = Localizer["TableFixColumn"],
                Url = "tables/fix-column"
            },
            new()
            {
                Text = Localizer["TablePage"],
                Url = "tables/pages"
            },
            new()
            {
                Text = Localizer["TableToolbar"],
                Url = "tables/toolbar"
            },
            new()
            {
                Text = Localizer["TableEdit"],
                Url = "tables/edit"
            },
            new()
            {
                Text = Localizer["TableExcel"],
                Url = "tables/excel"
            },
            new()
            {
                Text = Localizer["TableDynamicExcel"],
                Url = "tables/dynamicexcel"
            },
            new()
            {
                Text = Localizer["TableExport"],
                Url = "tables/export"
            },
            new()
            {
                Text = Localizer["TableSelection"],
                Url = "tables/selection"
            },
            new()
            {
                Text = Localizer["TableAutoRefresh"],
                Url = "tables/autorefresh"
            },
            new()
            {
                Text = Localizer["TableFooter"],
                Url = "tables/footer"
            },
            new()
            {
                Text = Localizer["TableDialog"],
                Url = "tables/dialog"
            },
            new()
            {
                Text = Localizer["TableWrap"],
                Url = "tables/wrap"
            },
            new()
            {
                Text = Localizer["TableTree"],
                Url = "tables/tree"
            },
            new()
            {
                Text = Localizer["TableLaoding"],
                Url = "tables/loading"
            },
            new()
            {
                Text = Localizer["TableVirtualization"],
                Url = "tables/virtualization"
            }
        };

        return item;
    }

    private void AddNotice(DemoMenuItem item)
    {
        item.Items = new List<DemoMenuItem>
        {
            new()
            {
                Text = Localizer["Alert"],
                Url = "alerts"
            },
            new()
            {
                Text = Localizer["Console"],
                Url = "consoles"
            },
            new()
            {
                Text = Localizer["Dialog"],
                Url = "dialogs"
            },
            new()
            {
                Text = Localizer["Dispatch"],
                Url = "dispatchs"
            },
            new()
            {
                Text = Localizer["Drawer"],
                Url = "drawers"
            },
            new()
            {
                Text = Localizer["EditDialog"],
                Url = "editdialogs"
            },
            new()
            {
                Text = Localizer["Light"],
                Url = "lights"
            },
            new()
            {
                Text = Localizer["Message"],
                Url = "messages"
            },
            new()
            {
                Text = Localizer["Modal"],
                Url = "modals"
            },
            new()
            {
                Text = Localizer["Notification"],
                Url = "notifications"
            },
            new()
            {
                Text = Localizer["Popconfirm"],
                Url = "popconfirms"
            },
            new()
            {
                Text = Localizer["Popover"],
                Url = "popovers"
            },
            new()
            {
                Text = Localizer["Progress"],
                Url = "progresss"
            },
            new()
            {
                IsNew = true,
                Text = Localizer["Reconnector"],
                Url = "reconnectors"
            },
            new()
            {
                IsNew = true,
                Text = Localizer["Responsive"],
                Url = "responsives"
            },
            new()
            {
                Text = Localizer["SearchDialog"],
                Url = "searchdialogs"
            },
            new()
            {
                Text = Localizer["Spinner"],
                Url = "spinners"
            },
            new()
            {
                Text = Localizer["SweetAlert"],
                Url = "swals"
            },
            new()
            {
                Text = Localizer["Timer"],
                Url = "timers"
            },
            new()
            {
                Text = Localizer["Toast"],
                Url = "toasts"
            },
            new()
            {
                Text = Localizer["Tooltip"],
                Url = "tooltips"
            }
        };
        AddBadge(item);
    }

    private void AddNavigation(DemoMenuItem item)
    {
        item.Items = new List<DemoMenuItem>
        {
            new()
            {
                Text = Localizer["Anchor"],
                Url = "anchors"
            },
            new()
            {
                Text = Localizer["AnchorLink"],
                Url = "anchorlinks"
            },
            new()
            {
                Text = Localizer["Breadcrumb"],
                Url = "breadcrumbs"
            },
            new()
            {
                Text = Localizer["Dropdown"],
                Url = "dropdowns"
            },
            new()
            {
                Text = Localizer["FullScreen"],
                Url = "fullscreens"
            },
            new()
            {
                Text = Localizer["GoTop"],
                Url = "gotops"
            },
            new()
            {
                Text = Localizer["Logout"],
                Url = "logouts"
            },
            new()
            {
                Text = Localizer["Menu"],
                Url = "menus"
            },
            new()
            {
                Text = Localizer["Nav"],
                Url = "navs"
            },
            new()
            {
                Text = Localizer["Pagination"],
                Url = "paginations"
            },
            new()
            {
                Text = Localizer["Steps"],
                Url = "stepss"
            },
            new()
            {
                Text = Localizer["Tab"],
                Url = "tabs"
            }
        };

        AddBadge(item);
    }

    private void AddLayout(DemoMenuItem item)
    {
        item.Items = new List<DemoMenuItem>
        {
            new()
            {
                Text = Localizer["Divider"],
                Url = "dividers"
            },
            new()
            {
                IsNew = true,
                Text = Localizer["Dragdrop"],
                Url = "dragdrops"
            },
            new()
            {
                Text = Localizer["Layout"],
                Url = "layouts"
            },
            new()
            {
                Text = Localizer["Footer"],
                Url = "footers"
            },
            new()
            {
                Text = Localizer["Row"],
                Url = "rows"
            },
            new()
            {
                Text = Localizer["Scroll"],
                Url = "scrolls"
            },
            new()
            {
                Text = Localizer["Skeleton"],
                Url = "skeletons"
            },
            new()
            {
                Text = Localizer["Split"],
                Url = "splits"
            }
        };
        AddBadge(item);
    }

    private void AddSummary(DemoMenuItem item)
    {
        // 计算组件总数
        var count = 0;
        count = Menus.Aggregate(count, (c, item) => { c += item.Items.Count(); return c; }, c => c - Menus[0].Items.Count());
        AddBadge(item, false, count);
        Menus.Insert(1, item);
    }

    private void AddBadge(DemoMenuItem item, bool append = true, int? count = null)
    {
        item.Template = CreateBadge(count ?? item.Items.Count(),
           isNew: item.Items.OfType<DemoMenuItem>().Any(i => i.IsNew),
           isUpdate: item.Items.OfType<DemoMenuItem>().Any(i => i.IsUpdate)).Render();
        foreach (var menu in item.GetAllSubItems().OfType<DemoMenuItem>().Where(i => ShouldBadge(i)))
        {
            menu.Template = CreateBadge(0, menu.IsNew, menu.IsUpdate).Render();
        }
        if (append)
        {
            Menus.Add(item);
        }

        static bool ShouldBadge(DemoMenuItem? item) => item != null && (item.IsNew || item.IsUpdate);
    }

    private static BootstrapDynamicComponent CreateBadge(int count, bool isNew = false, bool isUpdate = false) => BootstrapDynamicComponent.CreateComponent<State>(new Dictionary<string, object?>
    {
        [nameof(State.Count)] = count,
        [nameof(State.IsNew)] = isNew,
        [nameof(State.IsUpdate)] = isUpdate
    });

    private class DemoMenuItem : MenuItem
    {
        public bool IsNew { get; set; }

        public bool IsUpdate { get; set; }
    }
}
