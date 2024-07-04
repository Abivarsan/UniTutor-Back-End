using AutoMapper;
using UniTutor.DTO;
using UniTutor.Model;

namespace UniTutor.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Tutor, TutorRegistration>().ReverseMap();
            CreateMap<Student, StudentRegistration>().ReverseMap();
        }

    }
}
