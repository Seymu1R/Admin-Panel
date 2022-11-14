﻿using Microsoft.EntityFrameworkCore.Metadata;

namespace AspNetMVC_P324.Data
{
    public class MailSettings
    {
        public string Mail { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string Host { get; set; }
    }
}
