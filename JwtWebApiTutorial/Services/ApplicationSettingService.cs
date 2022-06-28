using AutoMapper;
using JwtWebApiTutorial.Data;
using JwtWebApiTutorial.Models;
using JwtWebApiTutorial.Requests.ApplicationSetting;
using JwtWebApiTutorial.Responses;
using JwtWebApiTutorial.Responses.ApplicationSettings;
using JwtWebApiTutorial.Services.Interface;
using JwtWebApiTutorial.Sieve;
using Microsoft.EntityFrameworkCore;

namespace JwtWebApiTutorial.Services
{
    public class ApplicationSettingService : IApplicationSettingService
    {
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ApplicationSieveProcessor _sieveProcessor;

        public ApplicationSettingService(DataContext _context, IMapper mapper, ApplicationSieveProcessor sieveProcessor)
        {
            _dbContext = _context;
            _mapper = mapper;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<Response<string>> Add(PostApplicationSettingRequest postApplicationSettingRequest)
        {
            var applicationSetting = _mapper.Map<ApplicationSetting>(postApplicationSettingRequest);

            if (!_dbContext.ApplicationSettings.Any(x => x.SettingName == applicationSetting.SettingName))
            {
                _dbContext.ApplicationSettings.Add(applicationSetting);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                return new Response<string>
                {
                    Message = "Conflict",
                    Status = 409,
                    Data = ""
                };
            }

            return new Response<string>
            {
                Message = "OK",
                Status = 200,
                Data = ""
            };
        }

        public async Task<Response<GetApplicationSettingResponse>> Get(int id)
        {
            var _applicationSetting = _dbContext.ApplicationSettings.FirstOrDefault(x => x.Id == id);
            GetApplicationSettingResponse response = new GetApplicationSettingResponse();

            //Check either the table schedule is null or not
            if (_applicationSetting == null)
            {
                return new Response<GetApplicationSettingResponse>
                {
                    Message = "Not found",
                    Status = 404,
                    Data = response
                };
            }

            response = _mapper.Map<GetApplicationSettingResponse>(_applicationSetting);

            return new Response<GetApplicationSettingResponse>
            {
                Message = "OK",
                Status = 200,
                Data = response
            };
        }

        public async Task<Response<string>> SetQRSetting(PostQRCodeRequest request)
        {
            if (request.TypeQRCode == "Checkin")
            {
                var _QRSetting = await _dbContext.ApplicationSettings.FirstOrDefaultAsync(x => x.SettingName == "QRCodeCheckin");
                _QRSetting.SettingValue = request.QRCodeValue;

                _dbContext.ApplicationSettings.Update(_QRSetting);
                await _dbContext.SaveChangesAsync();
                return new Response<string>
                {
                    Message = "OK",
                    Status = 200,
                    Data = ""
                };
            }
            else
            {
                var _QRSetting = await _dbContext.ApplicationSettings.FirstOrDefaultAsync(x => x.SettingName == "QRCodeCheckout");
                _QRSetting.SettingValue = request.QRCodeValue;

                _dbContext.ApplicationSettings.Update(_QRSetting);
                await _dbContext.SaveChangesAsync();
                return new Response<string>
                {
                    Message = "OK",
                    Status = 200,
                    Data = ""
                };
            }
        }

        public async Task<Response<GetQRSettingResponse>> GetQRSetting()
        {
            var _QRCheckinSetting = await _dbContext.ApplicationSettings.FirstOrDefaultAsync(x => x.SettingName == "QRCodeCheckin");
            var _QRCheckoutSetting = await _dbContext.ApplicationSettings.FirstOrDefaultAsync(x => x.SettingName == "QRCodeCheckout");
            GetQRSettingResponse response = new GetQRSettingResponse();

            if (_QRCheckinSetting == null || _QRCheckoutSetting == null)
            {
                return new Response<GetQRSettingResponse>
                {
                    Message = "Not found",
                    Status = 404,
                    Data = response
                };
            }

            response.BeginningQRCodeData = _QRCheckinSetting.SettingValue;
            response.EndQRCodeData = _QRCheckoutSetting.SettingValue;

            return new Response<GetQRSettingResponse>
            {
                Message = "OK",
                Status = 200,
                Data = response
            };
        }
    }
}
