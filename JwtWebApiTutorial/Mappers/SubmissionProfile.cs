using AutoMapper;
using JwtWebApiTutorial.Models;
using JwtWebApiTutorial.Requests.Submission;
using JwtWebApiTutorial.Responses.Submission;
using JwtWebApiTutorial.Responses.SubmissionLeave;

namespace JwtWebApiTutorial.Mappers
{
    public class SubmissionProfile : Profile
    {
        public SubmissionProfile()
        {
            CreateMap<PostSubmissionRequest, Submission>();

            CreateMap<Approval, GetHistoryApprovalResponse>()
                .ForMember(dest =>
                    dest.ApprovalId,
                    opt => opt.MapFrom(src => src.Id));

            CreateMap<PutApprovalSubmissionRequest, Approval>()
                .ForMember(dest =>
                    dest.Id,
                    opt => opt.MapFrom(src => src.ApprovalId));
        }
    }
}
