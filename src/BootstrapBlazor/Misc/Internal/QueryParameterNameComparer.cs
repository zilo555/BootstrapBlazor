﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#if NET6_0_OR_GREATER
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components.Routing;

[ExcludeFromCodeCoverage]
internal sealed class QueryParameterNameComparer : IComparer<ReadOnlyMemory<char>>, IEqualityComparer<ReadOnlyMemory<char>>
{
    public static readonly QueryParameterNameComparer Instance = new();

    public int Compare(ReadOnlyMemory<char> x, ReadOnlyMemory<char> y)
        => x.Span.CompareTo(y.Span, StringComparison.OrdinalIgnoreCase);

    public bool Equals(ReadOnlyMemory<char> x, ReadOnlyMemory<char> y)
        => x.Span.Equals(y.Span, StringComparison.OrdinalIgnoreCase);

    public int GetHashCode([DisallowNull] ReadOnlyMemory<char> obj)
        => string.GetHashCode(obj.Span, StringComparison.OrdinalIgnoreCase);
}
#endif
