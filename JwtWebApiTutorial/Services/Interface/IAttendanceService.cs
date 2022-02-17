using JwtWebApiTutorial.Requests.Attendance;
using JwtWebApiTutorial.Responses;

namespace JwtWebApiTutorial.Services.Interface
{
    public interface IAttendanceService
    {
        Task<Response> CheckinOnline(PostCheckinOnlineRequest postCheckinOnlineRequest);
        Task<Response> CheckoutOnline(PostCheckoutOnlineRequest postCheckoutOnlineRequest);
        Task<Response> CheckinOffline(PostCheckinOfflineRequest postCheckinOfflineRequest);
        Task<Response> CheckoutOffline(PostCheckoutOfflineRequest postCheckoutOfflineRequest);
    }
}
