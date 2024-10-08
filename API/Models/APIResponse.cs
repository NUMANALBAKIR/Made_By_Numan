﻿namespace API.Models;

public class APIResponse : IAPIResponse
{
    public bool IsSuccess { get; set; } = true;
    public object? Data { get; set; }
    public string ErrorMessage { get; set; } = "None.";
}

