﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System.Reflection;

namespace BootstrapBlazor.Components;

[ExcludeFromCodeCoverage]
internal class InternalTableColumn : ITableColumn
{
    private string FieldName { get; }

    public bool Sortable { get; set; }

    public bool DefaultSort { get; set; }

    public SortOrder DefaultSortOrder { get; set; }

    public bool Filterable { get; set; }

    public bool Searchable { get; set; }

    public int? Width { get; set; }

    public bool Fixed { get; set; }

    public bool Visible { get; set; } = true;

    public bool TextWrap { get; set; }

    public bool TextEllipsis { get; set; }

    /// <summary>
    /// 获得/设置 是否不进行验证 默认为 false
    /// </summary>
    public bool SkipValidate { get; set; }

    /// <summary>
    /// 获得/设置 新建时此列只读 默认为 false
    /// </summary>
    public bool IsReadonlyWhenAdd { get; set; }

    /// <summary>
    /// 获得/设置 编辑时此列只读 默认为 false
    /// </summary>
    public bool IsReadonlyWhenEdit { get; set; }

    /// <summary>
    /// 获得/设置 是否显示标签 Tooltip 多用于标签文字过长导致裁减时使用 默认 null
    /// </summary>
    public bool? ShowLabelTooltip { get; set; }

    public string? CssClass { get; set; }

    public BreakPoint ShownWithBreakPoint { get; set; }

    public RenderFragment<object>? Template { get; set; }

    public RenderFragment<object>? SearchTemplate { get; set; }

    public RenderFragment? FilterTemplate { get; set; }

    /// <summary>
    /// 获得/设置 表头模板
    /// </summary>
    public RenderFragment<ITableColumn>? HeaderTemplate { get; set; }

    public IFilter? Filter { get; set; }

    public string? FormatString { get; set; }

    /// <summary>
    /// 获得/设置 placeholder 文本 默认为 null
    /// </summary>
    public string? PlaceHolder { get; set; }

    public Func<object?, Task<string>>? Formatter { get; set; }

    public Alignment Align { get; set; }

    public bool ShowTips { get; set; }

    public Type PropertyType { get; }

    public bool Editable { get; set; } = true;

    public bool Readonly { get; set; }

    public object? Step { get; set; }

    public int Rows { get; set; }

    [NotNull]
    public string? Text { get; set; }

    public RenderFragment<object>? EditTemplate { get; set; }

    /// <summary>
    /// 获得/设置 组件类型 默认为 null
    /// </summary>
    public Type? ComponentType { get; set; }

    /// <summary>
    /// 获得/设置 组件自定义类型参数集合 默认为 null
    /// </summary>
    public IEnumerable<KeyValuePair<string, object>>? ComponentParameters { get; set; }

    /// <summary>
    /// 获得/设置 额外数据源一般用于下拉框或者 CheckboxList 这种需要额外配置数据源组件使用
    /// </summary>
    public IEnumerable<SelectedItem>? Items { get; set; }

    /// <summary>
    /// 获得/设置 显示顺序
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    /// 获得/设置 字典数据源 常用于外键自动转换为名称操作
    /// </summary>
    public IEnumerable<SelectedItem>? Lookup { get; set; }

    /// <summary>
    /// 获得/设置 字典数据源字符串比较规则 默认 StringComparison.OrdinalIgnoreCase 大小写不敏感 
    /// </summary>
    public StringComparison LookupStringComparison { get; set; } = StringComparison.OrdinalIgnoreCase;

    /// <summary>
    /// 获得/设置 LookupService 服务获取 Lookup 数据集合键值 常用于外键自动转换为名称操作
    /// </summary>
    public string? LookUpServiceKey { get; set; }

    /// <summary>
    /// 获得/设置 单元格回调方法
    /// </summary>
    public Action<TableCellArgs>? OnCellRender { get; set; }

    /// <summary>
    /// 获得/设置 自定义验证集合
    /// </summary>
    public List<IValidator>? ValidateRules { get; set; }

    /// <summary>
    /// 获得/设置 当前属性分组
    /// </summary>
    public string? GroupName { get; set; }

    /// <summary>
    /// 获得/设置 当前属性分组排序 默认 0
    /// </summary>
    public int GroupOrder { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="fieldName">字段名称</param>
    /// <param name="fieldType">字段类型</param>
    /// <param name="fieldText">显示文字</param>
    public InternalTableColumn(string fieldName, Type fieldType, string? fieldText = null)
    {
        FieldName = fieldName;
        PropertyType = fieldType;
        Text = fieldText;
    }

    public string GetDisplayName() => Text;

    public string GetFieldName() => FieldName;

    /// <summary>
    /// 通过泛型模型获取模型属性集合
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static IEnumerable<ITableColumn> GetProperties<TModel>(IEnumerable<ITableColumn>? source = null) => GetProperties(typeof(TModel), source);

    /// <summary>
    /// 通过特定类型模型获取模型属性集合
    /// </summary>
    /// <param name="type"></param>
    /// <param name="source"></param>
    /// <returns></returns>
    public static IEnumerable<ITableColumn> GetProperties(Type type, IEnumerable<ITableColumn>? source = null)
    {
        var cols = new List<ITableColumn>(50);
        var attrModel = type.GetCustomAttribute<AutoGenerateClassAttribute>(true);
        var props = type.GetProperties();
        foreach (var prop in props)
        {
            ITableColumn? tc;
            var attr = prop.GetCustomAttribute<AutoGenerateColumnAttribute>(true);

            // Issue: 增加定义设置标签 AutoGenerateClassAttribute
            // https://gitee.com/LongbowEnterprise/BootstrapBlazor/issues/I381ED
            var displayName = attr?.Text ?? Utility.GetDisplayName(type, prop.Name);
            if (attr == null)
            {
                tc = new InternalTableColumn(prop.Name, prop.PropertyType, displayName);

                if (attrModel != null)
                {
                    InheritValue(attrModel, tc);
                }
            }
            else
            {
                if (attr.Ignore) continue;

                attr.Text = displayName;
                attr.FieldName = prop.Name;
                attr.PropertyType = prop.PropertyType;

                if (attrModel != null)
                {
                    InheritValue(attrModel, attr);
                }
                tc = attr;
            }

            // 替换属性 手写优先
            var col = source?.FirstOrDefault(c => c.GetFieldName() == tc.GetFieldName());
            if (col != null)
            {
                CopyValue(col, tc);
            }
            cols.Add(tc);
        }

        return cols.Where(a => a.Order > 0).OrderBy(a => a.Order)
            .Concat(cols.Where(a => a.Order == 0))
            .Concat(cols.Where(a => a.Order < 0).OrderBy(a => a.Order));
    }

    /// <summary>
    /// 集成 class 标签中设置的参数值
    /// </summary>
    /// <param name="source"></param>
    /// <param name="dest"></param>
    private static void InheritValue(AutoGenerateClassAttribute source, ITableColumn dest)
    {
        if (source.Align != Alignment.None) dest.Align = source.Align;
        if (source.TextWrap) dest.TextWrap = source.TextWrap;
        if (!source.Editable) dest.Editable = source.Editable;
        if (source.Filterable) dest.Filterable = source.Filterable;
        if (source.Readonly) dest.Readonly = source.Readonly;
        if (source.Searchable) dest.Searchable = source.Searchable;
        if (source.ShowTips) dest.ShowTips = source.ShowTips;
        if (source.Sortable) dest.Sortable = source.Sortable;
        if (source.TextEllipsis) dest.TextEllipsis = source.TextEllipsis;
    }

    /// <summary>
    /// 属性赋值方法
    /// </summary>
    /// <param name="source"></param>
    /// <param name="dest"></param>
    private static void CopyValue(ITableColumn source, ITableColumn dest)
    {
        if (source.Align != Alignment.None) dest.Align = source.Align;
        if (source.TextWrap) dest.TextWrap = source.TextWrap;
        if (source.ComponentType != null) dest.ComponentType = source.ComponentType;
        if (source.ComponentParameters != null) dest.ComponentParameters = source.ComponentParameters;
        if (!string.IsNullOrEmpty(source.CssClass)) dest.CssClass = source.CssClass;
        if (source.DefaultSort) dest.DefaultSort = source.DefaultSort;
        if (source.DefaultSortOrder != SortOrder.Unset) dest.DefaultSortOrder = source.DefaultSortOrder;
        if (!source.Editable) dest.Editable = source.Editable;
        if (source.EditTemplate != null) dest.EditTemplate = source.EditTemplate;
        if (source.Filter != null) dest.Filter = source.Filter;
        if (source.Filterable) dest.Filterable = source.Filterable;
        if (source.FilterTemplate != null) dest.FilterTemplate = source.FilterTemplate;
        if (source.Fixed) dest.Fixed = source.Fixed;
        if (source.FormatString != null) dest.FormatString = source.FormatString;
        if (source.Formatter != null) dest.Formatter = source.Formatter;
        if (source.HeaderTemplate != null) dest.HeaderTemplate = source.HeaderTemplate;
        if (source.Items != null) dest.Items = source.Items;
        if (source.Lookup != null) dest.Lookup = source.Lookup;
        dest.LookupStringComparison = source.LookupStringComparison;
        if (source.LookUpServiceKey != null) dest.LookUpServiceKey = source.LookUpServiceKey;
        if (source.IsReadonlyWhenAdd) dest.IsReadonlyWhenAdd = source.IsReadonlyWhenAdd;
        if (source.IsReadonlyWhenEdit) dest.IsReadonlyWhenEdit = source.IsReadonlyWhenEdit;
        if (source.OnCellRender != null) dest.OnCellRender = source.OnCellRender;
        if (source.Readonly) dest.Readonly = source.Readonly;
        if (source.Rows > 0) dest.Rows = source.Rows;
        if (source.Searchable) dest.Searchable = source.Searchable;
        if (source.SearchTemplate != null) dest.SearchTemplate = source.SearchTemplate;
        if (source.ShownWithBreakPoint != BreakPoint.None) dest.ShownWithBreakPoint = source.ShownWithBreakPoint;
        if (source.ShowTips) dest.ShowTips = source.ShowTips;
        if (source.SkipValidate) dest.SkipValidate = source.SkipValidate;
        if (source.Sortable) dest.Sortable = source.Sortable;
        if (source.Template != null) dest.Template = source.Template;
        if (!string.IsNullOrEmpty(source.Text)) dest.Text = source.Text;
        if (source.TextEllipsis) dest.TextEllipsis = source.TextEllipsis;
        if (!source.Visible) dest.Visible = source.Visible;
        if (source.Width != null) dest.Width = source.Width;
        if (source.ValidateRules != null) dest.ValidateRules = source.ValidateRules;
        if (source.ShowLabelTooltip != null) dest.ShowLabelTooltip = source.ShowLabelTooltip;
        if (!string.IsNullOrEmpty(source.GroupName)) dest.GroupName = source.GroupName;
        if (source.GroupOrder != 0) dest.GroupOrder = source.GroupOrder;
    }
}
