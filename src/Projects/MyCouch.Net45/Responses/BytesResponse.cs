﻿using System;

namespace MyCouch.Responses
{
#if !NETFX_CORE
    [Serializable]
#endif
    public class BytesResponse : ContentResponse<byte[]>
    {
        public override bool IsEmpty
        {
            get { return Content == null || Content.Length < 1; }
        }

        public override string ToStringDebugVersion()
        {
            return string.Format("{1}{0}Content: {2}",
                Environment.NewLine,
                base.ToStringDebugVersion(),
                Content == null ? NullValueForDebugOutput : "<BYTES>");
        }
    }
}