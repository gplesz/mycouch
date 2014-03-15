﻿using System;

namespace MyCouch.Responses
{
#if !NETFX_CORE
    [Serializable]
#endif
    public class DocumentResponse : TextResponse
    {
        public string Id { get; set; }
        public string Rev { get; set; }

        public override string ToStringDebugVersion()
        {
            return string.Format("{1}{0}Id: {2}{0}Rev: {3}",
                Environment.NewLine,
                base.ToStringDebugVersion(),
                Id ?? NullValueForDebugOutput,
                Rev ?? NullValueForDebugOutput);
        }
    }
}