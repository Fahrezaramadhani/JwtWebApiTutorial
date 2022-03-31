using AutoMapper;
using JwtWebApiTutorial.Models;
using JwtWebApiTutorial.Responses.Attendances;

namespace JwtWebApiTutorial.Mappers
{
    public class AttendanceProfile : Profile
    {
        public AttendanceProfile()
        {
            CreateMap<Attendance, GetAllAttendanceResponse>();
        }
    }
}
