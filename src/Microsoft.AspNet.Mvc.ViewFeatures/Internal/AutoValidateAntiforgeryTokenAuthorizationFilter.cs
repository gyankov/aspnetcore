﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNet.Antiforgery;
using Microsoft.AspNet.Mvc.Filters;

namespace Microsoft.AspNet.Mvc.ViewFeatures.Internal
{
    public class AutoValidateAntiforgeryTokenAuthorizationFilter : ValidateAntiforgeryTokenAuthorizationFilter
    {
        public AutoValidateAntiforgeryTokenAuthorizationFilter(IAntiforgery antiforgery)
            : base(antiforgery)
        {
        }

        protected override bool ShouldValidate(AuthorizationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var method = context.HttpContext.Request.Method;
            if (string.Equals("GET", method, StringComparison.OrdinalIgnoreCase) ||
                string.Equals("HEAD", method, StringComparison.OrdinalIgnoreCase) ||
                string.Equals("TRACE", method, StringComparison.OrdinalIgnoreCase) ||
                string.Equals("OPTIONS", method, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            // Anything else requires a token.
            return true;
        }
    }
}
