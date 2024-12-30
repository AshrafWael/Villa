﻿using System.Net;

namespace VillaAPI.Responses
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public  List<string>?  Errors { get; set; } 
        public object? Result { get; set; } = null;
    }
}
