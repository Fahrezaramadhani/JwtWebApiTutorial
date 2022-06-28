using AutoMapper;
using JwtWebApiTutorial.Models;
using JwtWebApiTutorial.Requests.Leave;
using JwtWebApiTutorial.Requests.Submission;

namespace JwtWebApiTutorial.Mappers
{
    public class SubmissionAttributeProfile : Profile
    {
        public SubmissionAttributeProfile()
        {
            CreateMap<PostLeaveRequest, SubmissionAttribute>();
            CreateMap<PostSubmissionRequest, SubmissionAttribute>();
        }
    }
}
