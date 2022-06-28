using JwtWebApiTutorial.Requests.Leave;
using JwtWebApiTutorial.Requests.SubmissionLeave;
using JwtWebApiTutorial.Responses;
using JwtWebApiTutorial.Responses.Leave;
using JwtWebApiTutorial.Responses.SubmissionLeave;
using Sieve.Models;

namespace JwtWebApiTutorial.Services.Interface
{
    public interface ISubmissionLeaveService
    {
        Task<Response<string>> AddSubmissionLeave(PostLeaveRequest request);
        Task<Response<string>> PutApprovalLeave(PutApprovalLeaveRequest request);
        Task<Response<string>> PutSubmissionLeave(PutSubmissionLeaveRequest request);
        Task<Response<PaginatedResponse<GetApprovalLeaveResponse>>> GetPagedApprovalLeaveList(SieveModel sieveModel);
        Task<Response<IEnumerable<GetApprovalLeaveResponse>>> GetApprovalLeaveList(SieveModel sieveModel);
        Task<Response<PaginatedResponse<GetHistoryLeaveResponse>>> GetPagedLeaveList(SieveModel sieveModel);
        Task<Response<IEnumerable<GetHistoryLeaveResponse>>> GetLeaveList(SieveModel sieveModel);
        Task<Response<GetTotalDaysLeaveResponse>> GetTotalDaysLeave(int monthNumber);
    }
}
