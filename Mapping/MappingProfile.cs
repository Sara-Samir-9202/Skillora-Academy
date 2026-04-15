using AutoMapper;
using StudentSystem.Models;
using StudentSystem.ViewModels;

namespace StudentSystem.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ===================== Student -> StudentVM =====================
            CreateMap<Student, StudentVM>()
                .ForMember(d => d.DepartmentName,
                    o => o.MapFrom(s => s.Department != null ? s.Department.Name : "No Department"))
                .ForMember(d => d.Phone,
                    o => o.MapFrom(s => s.Phone))
                .ForMember(d => d.Image,
                    o => o.MapFrom(s => s.Image));

            // ===================== Student -> Details VM =====================
            CreateMap<Student, StudentDetailsVM>()
                .ForMember(d => d.DepartmentName,
                    o => o.MapFrom(s => s.Department != null ? s.Department.Name : "No Department"))
                .ForMember(d => d.Courses,
                    o => o.MapFrom(s => s.StudentCourses))
                .ForMember(d => d.Image,
                    o => o.MapFrom(s => s.Image))
                .ForMember(d => d.Phone,
                    o => o.MapFrom(s => s.Phone));

            // ===================== StudentCourse -> CourseDegreeVM =====================
            CreateMap<StudentCourse, CourseDegreeVM>()
                .ForMember(d => d.CourseName,
                    o => o.MapFrom(s => s.Course != null ? s.Course.Name : ""))
                .ForMember(d => d.Degree,
                    o => o.MapFrom(s => s.Degree));
        }
    }
}