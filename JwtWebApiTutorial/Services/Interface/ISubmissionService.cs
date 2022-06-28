using JwtWebApiTutorial.Requests.Submission;
using JwtWebApiTutorial.Responses;
using JwtWebApiTutorial.Responses.Submission;
using Sieve.Models;

namespace JwtWebApiTutorial.Services.Interface
{
    public interface ISubmissionService
    {
        Task<Response<string>> AddSubmission(PostSubmissionRequest request);
        Task<Response<string>> PutApprovalSubmission(PutApprovalSubmissionRequest request);
        Task<Response<string>> PutSubmission(PutSubmissionRequest request);
        Task<Response<PaginatedResponse<GetApprovalSubmissionResponse>>> GetPagedApprovalSubmissionList(SieveModel sieveModel);
        Task<Response<IEnumerable<GetApprovalSubmissionResponse>>> GetApprovalSubmissionList(SieveModel sieveModel);
        Task<Response<PaginatedResponse<GetHistorySubmissionResponse>>> GetPagedSubmissionList(SieveModel sieveModel);
        Task<Response<IEnumerable<GetHistorySubmissionResponse>>> GetSubmissionList(SieveModel sieveModel);
    }
}
