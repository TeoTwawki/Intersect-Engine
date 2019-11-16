﻿using System;

using JetBrains.Annotations;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Intersect.ErrorHandling
{
    [Serializable]
    public struct ExceptionInfo
    {
        public int DataKeys { get; }
        
        public int MessageLength { get; }

        public int StackTraceLength { get; }

        public bool InnerException { get; }

        public string Source { get; }

        public string TargetSite { get; }

        public string TargetSiteDeclaringType { get; }

        public string Type { get; }

        public ExceptionInfo([NotNull] Exception exception)
        {
            DataKeys = exception.Data.Count;
            MessageLength = exception.Message?.Length ?? -1;
            StackTraceLength = exception.StackTrace?.Length ?? -1;
            InnerException = exception.InnerException != null;
            Source = exception.Source;
            TargetSite = exception.TargetSite?.Name;
            TargetSiteDeclaringType = exception.TargetSite?.DeclaringType?.FullName;
            Type = exception.GetType().FullName;
        }

        [NotNull]
        public static implicit operator JObject(ExceptionInfo exceptionInfo) => new JObject
        {
            [nameof(DataKeys)] = exceptionInfo.DataKeys,
            [nameof(MessageLength)] = exceptionInfo.MessageLength,
            [nameof(StackTraceLength)] = exceptionInfo.StackTraceLength,
            [nameof(InnerException)] = exceptionInfo.InnerException,
            [nameof(Source)] = exceptionInfo.Source,
            [nameof(TargetSite)] = exceptionInfo.TargetSite,
            [nameof(TargetSiteDeclaringType)] = exceptionInfo.TargetSiteDeclaringType,
            [nameof(Type)] = exceptionInfo.Type
        };

        [NotNull]
        public static implicit operator string(ExceptionInfo exceptionInfo) => exceptionInfo.ToString();

        public override string ToString() => ((JObject) this).ToString(Formatting.None) ?? base.ToString();

        public string ToString(Formatting formatting) => ((JObject) this).ToString(formatting);

    }
}
