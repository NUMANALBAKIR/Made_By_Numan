﻿using System.Net;

namespace API.Models;

public class APIResponse
{
    public bool IsSuccess { get; set; } = true;
    public object? Data { get; set; }
    public List<string>? Messages { get; set; } = new List<string>();
}
