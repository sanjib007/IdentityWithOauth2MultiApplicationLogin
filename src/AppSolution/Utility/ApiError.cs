using System;

namespace Utility
{
    public class ApiError
    {
        public int statusCode { get; set; }
        public string message { get; set; }
        public string details { get; set; }
    }
}