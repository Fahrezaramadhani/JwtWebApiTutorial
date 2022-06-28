using JwtWebApiTutorial.Requests.ApplicationSetting;
using JwtWebApiTutorial.Requests.Attendance;
using JwtWebApiTutorial.Responses;
using JwtWebApiTutorial.Responses.Attendances;
using Sieve.Models;

namespace JwtWebApiTutorial.Services.Interface
{
    public interface IAttendanceService
    {
        Task<Response<string>> CheckinOnline(PostCheckinOnlineRequest postCheckinOnlineRequest);
        Task<Response<string>> CheckoutOnline(PostCheckoutOnlineRequest postCheckoutOnlineRequest);
        Task<Response<string>> CheckinOffline(PostCheckinOfflineRequest postCheckinOfflineRequest);
        Task<Response<string>> CheckoutOffline(PostCheckoutOfflineRequest postCheckoutOfflineRequest);
        Task<Response<PaginatedResponse<GetAllAttendanceResponse>>> GetPagedAttendanceList(SieveModel sieveModel);
        Task<Response<IEnumerable<GetAllAttendanceResponse>>> GetAttendanceList(SieveModel sieveModel);
        Task<Response<string>> CheckQRCode(PostQRCodeRequest request);
        Task<Response<string>> CheckinStatus(DateTime request);
    }
}
