// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Microsoft.Extensions.Primitives;

namespace Microsoft.AspNetCore.Http
{
    public static class IReadableStringCollectionExtensions
    {
        public static NameValueCollection AsNameValueCollection(
            this IEnumerable<KeyValuePair<string, StringValues>> collection)
        {
            var nv = new NameValueCollection();

            foreach (var field in collection)
            {
                nv.Add(field.Key, field.Value.First());
            }

            return nv;
        }
    }
}
