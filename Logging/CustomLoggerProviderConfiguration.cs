﻿namespace APIRequest.Logging
{
    public class CustomLoggerProviderConfiguration
    {

        public LogLevel LogLevel { get; set; } = LogLevel.Warning;
        public int EventID { get; set; }
    }
}
