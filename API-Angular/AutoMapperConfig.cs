
using API_Angular.Models.StudentCRUD;
using API_Angular.Models.StudentCRUDDTOs;
using AutoMapper;

namespace API_Angular;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<Student, StudentDTO>().ReverseMap();
        CreateMap<Student, StudentCreateDTO>().ReverseMap();
        CreateMap<Student, StudentUpdateDTO>().ReverseMap();

        CreateMap<Subject, SubjectDTO>().ReverseMap();
        CreateMap<Subject, SubjectCreateDTO>().ReverseMap();
        CreateMap<Subject, SubjectUpdateDTO>().ReverseMap();

    }
}
