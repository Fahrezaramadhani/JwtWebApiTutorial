using AutoMapper;
using JwtWebApiTutorial.Models;
using JwtWebApiTutorial.Requests.Leave;
using JwtWebApiTutorial.Requests.SubmissionLeave;
using JwtWebApiTutorial.Responses.SubmissionLeave;

namespace JwtWebApiTutorial.Mappers
{
    public class SubmissionLeaveProfile : Profile
    {
        public SubmissionLeaveProfile ()
        {
            CreateMap<PostLeaveRequest, SubmissionLeave>()
                .ForMember(dest =>
                    dest.LeaveType,
                    opt => opt.MapFrom(src => src.Type));

            CreateMap<Approval, GetHistoryApprovalResponse>()
                .ForMember(dest =>
                    dest.ApprovalId,
                    opt => opt.MapFrom(src => src.Id));

            CreateMap<PutApprovalLeaveRequest, Approval>()
                .ForMember(dest =>
                    dest.Id,
                    opt => opt.MapFrom(src => src.ApprovalId));
        }
    }
}
