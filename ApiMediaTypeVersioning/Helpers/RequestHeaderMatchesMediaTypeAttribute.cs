﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace ApiMediaTypeVersioning.Helpers
{
    public class RequestHeaderMatchesMediaTypeAttribute : Attribute, IActionConstraint
    {
        private readonly string _requestHeaderToMatch;
        private readonly string[] _mediaTypes;

        public RequestHeaderMatchesMediaTypeAttribute(string requestHeaderToMatch, string[] mediaTypes)
        {
            _requestHeaderToMatch = requestHeaderToMatch;
            _mediaTypes = mediaTypes;
        }

        public bool Accept(ActionConstraintContext context)
        {
            var requestHeaders = context.RouteContext.HttpContext.Request.Headers;
            if (!requestHeaders.ContainsKey(_requestHeaderToMatch))
            {
                return false;
            }

            foreach (string mediaType in _mediaTypes)
            {
                var mediaTypeMatches =
                    string.Equals(requestHeaders[_requestHeaderToMatch].ToString(), mediaType, StringComparison.OrdinalIgnoreCase
                    );
                if (mediaTypeMatches)
                {
                    return true;
                }
            }

            return false;
        }

        public int Order { get; }
    }
}
