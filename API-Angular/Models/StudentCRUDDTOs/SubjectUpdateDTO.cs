﻿namespace API_Angular.Models.StudentCRUDDTOs;

public class SubjectUpdateDTO
{
    public int SubjectId { get; set; }
    public string SubjectName { get; set; }
    public int Mark { get; set; }
    
    public int StudentId { get; set; }
}