using AutoMapper;
using JwtWebApiTutorial.Data;
using JwtWebApiTutorial.Helpers;
using JwtWebApiTutorial.Models;
using JwtWebApiTutorial.Requests.Leave;
using JwtWebApiTutorial.Responses;
using JwtWebApiTutorial.Responses.Leave;
using JwtWebApiTutorial.Responses.SubmissionLeave;
using JwtWebApiTutorial.Services.Interface;
using JwtWebApiTutorial.Sieve;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using System.Diagnostics;
using AutoMapper.QueryableExtensions;
using JwtWebApiTutorial.Requests.SubmissionLeave;

namespace JwtWebApiTutorial.Services
{
    public class SubmissionLeaveService : ISubmissionLeaveService
    {
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ApplicationSieveProcessor _sieveProcessor;

        public SubmissionLeaveService(DataContext _context, IMapper mapper, ApplicationSieveProcessor sieveProcessor)
        {
            _dbContext = _context;
            _mapper = mapper;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<Response<string>> AddSubmissionLeave (PostLeaveRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.Attachment) && UploadImageHelper.IsBase64(request.Attachment))
            {
                request.Attachment = UploadImageHelper.UploadBase64File($"{DateTime.Now:yyyyMMddhhmmss}", request.Attachment, "Images/Leave");
            }

            var _user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);

            var _adminSetting = await _dbContext.ApplicationSettings.FirstOrDefaultAsync(x => x.SettingName == "AdminApprovalDefault");

            var _HRSetting = await _dbContext.ApplicationSettings.FirstOrDefaultAsync(x => x.SettingName == "HRApprovalDefault");

            if (_adminSetting == null || _HRSetting == null)
            {
                return new Response<string>
                {
                    Message = "Setting Approval Not found",
                    Status = 404,
                    Data = ""
                };
            }

            //Check either the table schedule is null or not
            if (_user == null)
            {
                return new Response<string>
                {
                    Message = "User Not found",
                    Status = 404,
                    Data = ""
                };
            }

            var submissionleave = _mapper.Map<SubmissionLeave>(request);
            var submissionAttribute = _mapper.Map<SubmissionAttribute>(request);
            submissionAttribute.SubmissionStatus = "Remaining";
            _dbContext.SubmissionLeaves.Add(submissionleave);
            _dbContext.SubmissionAttributes.Add(submissionAttribute);
            await _dbContext.SaveChangesAsync();
            submissionleave.SubmissionAttributeId = submissionAttribute.Id;
            submissionAttribute.SubmissionLeaveId = submissionleave.Id;

            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
            List<Approval> approvals = new List<Approval>();
            if (user.Role == "Employee")
            {
                var subordinateList = await _dbContext.Users.FirstOrDefaultAsync(x => x.SuperiorId == request.UserId);
                if (subordinateList == null)
                {
                    for (int i = 1; i<=3; i++)
                    {
                        Approval approval = new Approval();
                        if (i == 1)
                        {
                            approval.UserId = int.Parse(_adminSetting.SettingValue);
                        }
                        else if (i == 2)
                        {
                            approval.UserId = int.Parse(_HRSetting.SettingValue);
                        }
                        else
                        {
                            approval.UserId = user.SuperiorId;
                        }
                        approval.StatusApproval = "Remaining";
                        approval.SubmissionAttributeId = submissionAttribute.Id;
                        approvals.Add(approval);
                    }
                }
                else
                {
                    for (int i = 1; i <= 2; i++)
                    {
                        Approval approval = new Approval();
                        if (i == 1)
                        {
                            approval.UserId = int.Parse(_adminSetting.SettingValue);
                        }
                        else if (i == 2)
                        {
                            approval.UserId = int.Parse(_HRSetting.SettingValue);
                        }
                        approval.StatusApproval = "Remaining";
                        approval.SubmissionAttributeId = submissionAttribute.Id;
                        approvals.Add(approval);
                    }
                }
            }
            else
            {
                Approval approval = new Approval();
                approval.UserId = int.Parse(_adminSetting.SettingValue);
                approval.StatusApproval = "Remaining";
                approval.SubmissionAttributeId = submissionAttribute.Id;
                approvals.Add(approval);
            }

            _dbContext.Approvals.AddRange(approvals);
            await _dbContext.SaveChangesAsync();

            return new Response<string>
            {
                Message = "OK",
                Status = 200,
                Data = ""
            };
        }

        public async Task<Response<string>> PutApprovalLeave(PutApprovalLeaveRequest request)
        {
            var _approval = await _dbContext.Approvals.FirstOrDefaultAsync(x => x.Id == request.ApprovalId);

            //Check either the table schedule is null or not
            if (_approval == null)
            {
                return new Response<string>
                {
                    Message = "Not found",
                    Status = 404,
                    Data = ""
                };
            }

            _approval.StatusApproval = request.StatusApproval;
            _approval.DateApproval = request.DateApproval;
            var submissionAttibute = _dbContext.SubmissionAttributes.FirstOrDefault(u => u.Id == _approval.SubmissionAttributeId);
            var approvals = _dbContext.Approvals.Where(u => u.SubmissionAttributeId == _approval.SubmissionAttributeId).ToList();
            List<string> listStatusApproval = new List<string>();

            foreach (var approval in approvals)
            {
                listStatusApproval.Add(approval.StatusApproval);
            }

            if (!listStatusApproval.Any(x => x.Equals("Remaining")))
            {
                if (listStatusApproval.All(x => x.Equals("Approved")))
                {
                    submissionAttibute.SubmissionStatus = "Approved";
                }
                else
                {
                    submissionAttibute.SubmissionStatus = "Rejected";
                }
            }

            _dbContext.Approvals.Update(_approval);
            _dbContext.SubmissionAttributes.Update(submissionAttibute);
            await _dbContext.SaveChangesAsync();
            return new Response<string>
            {
                Message = "OK",
                Status = 200,
                Data = ""
            };
        }

        public async Task<Response<string>> PutSubmissionLeave(PutSubmissionLeaveRequest request)
        {
            var _submissionLeave = _dbContext.SubmissionLeaves.FirstOrDefault(x => x.Id == request.SubmissionLeaveId);
            var _submissionAttribute = _dbContext.SubmissionAttributes.FirstOrDefault(x => x.Id == _submissionLeave.SubmissionAttributeId);
            var _approvals = _dbContext.Approvals.Where(u => u.SubmissionAttributeId == _submissionAttribute.Id).ToList();

            //Check either the table schedule is null or not
            if (_submissionLeave == null || _submissionAttribute == null || _approvals == null)
            {
                return new Response<string>
                {
                    Message = "Not found",
                    Status = 404,
                    Data = ""
                };
            }

            if (!string.IsNullOrWhiteSpace(request.Attachment) && UploadImageHelper.IsBase64(request.Attachment))
            {
                UploadImageHelper.DeleteImage(_submissionAttribute.Attachment);
                request.Attachment = UploadImageHelper.UploadBase64File($"{DateTime.Now:yyyyMMddhhmmss}", request.Attachment, "Images/Leave");
            }

            _submissionLeave.DateStart = request.DateStart;
            _submissionLeave.DateEnd = request.DateEnd;
            _submissionLeave.LeaveType = request.Type;
            _submissionAttribute.DateSubmit = request.DateSubmit;
            _submissionAttribute.Description = request.Description;
            _submissionAttribute.Attachment = request.Attachment;
            _submissionAttribute.SubmissionStatus = "Remaining";
            //List<Approval> approvals = new List<Approval>();
            _approvals.ForEach(delegate (Approval data)
            {
                data.StatusApproval = "Remaining";
                data.DateApproval = new DateTime();
            });

            _dbContext.Approvals.UpdateRange(_approvals);
            _dbContext.SubmissionLeaves.Update(_submissionLeave);
            _dbContext.SubmissionAttributes.Update(_submissionAttribute);
            await _dbContext.SaveChangesAsync();
            return new Response<string>
            {
                Message = "OK",
                Status = 200,
                Data = ""
            };
        }

        public async Task<Response<PaginatedResponse<GetApprovalLeaveResponse>>> GetPagedApprovalLeaveList(SieveModel sieveModel)
        {
            var leaves = _dbContext.SubmissionAttributes.AsNoTracking()
                .Include(d => d.User)
                .Include(e => e.User.Position)
                .Include(b => b.SubmissionLeave)
                .Include(c => c.Approvals)
                .SelectMany(a => a.Approvals)
                .Where(f => f.SubmissionAttribute.SubmissionLeaveId != null)
                .Select(a => new GetApprovalLeaveResponse
                {
                    SubmissionLeaveId = a.SubmissionAttribute.SubmissionLeaveId,
                    SubmissionAttributeId = a.SubmissionAttributeId,
                    ApprovalId = a.Id,
                    UserName = a.SubmissionAttribute.User.Name,
                    Position = a.SubmissionAttribute.User.Position.PositionName,
                    DateSubmit = a.SubmissionAttribute.DateSubmit,
                    DateStart = a.SubmissionAttribute.SubmissionLeave.DateStart,
                    DateEnd = a.SubmissionAttribute.SubmissionLeave.DateEnd,
                    Description = a.SubmissionAttribute.Description,
                    Type = a.SubmissionAttribute.SubmissionLeave.LeaveType,
                    Attachment = a.SubmissionAttribute.Attachment,
                    StatusLeave = a.SubmissionAttribute.SubmissionStatus,
                    UserIdApproval = a.UserId,
                    StatusApproval = a.StatusApproval,
                    DateApproval = a.DateApproval,
                });

            foreach (var leave in leaves)
            {
                Debug.WriteLine("SubmissionLeaveId : " + leave.SubmissionLeaveId);
            }

            leaves = _sieveProcessor.Apply(sieveModel, leaves, applyPagination: false);

            int count = await leaves.CountAsync();

            leaves = _sieveProcessor.Apply(sieveModel, leaves, applyPagination: true);

            var result = await PaginationHelper.GetPagedResultAsync<GetApprovalLeaveResponse, GetApprovalLeaveResponse>(leaves, sieveModel, _mapper, count);

            return new Response<PaginatedResponse<GetApprovalLeaveResponse>>
            {
                Message = "OK",
                Status = 200,
                Data = result
            };
        }

        public async Task<Response<IEnumerable<GetApprovalLeaveResponse>>> GetApprovalLeaveList(SieveModel sieveModel)
        {
            var leaves = _dbContext.SubmissionAttributes.AsNoTracking()
                .Include(d => d.User)
                .Include(e => e.User.Position)
                .Include(b => b.SubmissionLeave)
                .Include(c => c.Approvals)
                .SelectMany(a => a.Approvals)
                .Where(f => f.SubmissionAttribute.SubmissionLeaveId != null)
                .Select(a => new GetApprovalLeaveResponse
                {
                    SubmissionLeaveId = a.SubmissionAttribute.SubmissionLeaveId,
                    SubmissionAttributeId = a.SubmissionAttributeId,
                    ApprovalId = a.Id,
                    UserName = a.SubmissionAttribute.User.Name,
                    Position = a.SubmissionAttribute.User.Position.PositionName,
                    DateSubmit = a.SubmissionAttribute.DateSubmit,
                    DateStart = a.SubmissionAttribute.SubmissionLeave.DateStart,
                    DateEnd = a.SubmissionAttribute.SubmissionLeave.DateEnd,
                    Description = a.SubmissionAttribute.Description,
                    Type = a.SubmissionAttribute.SubmissionLeave.LeaveType,
                    Attachment = a.SubmissionAttribute.Attachment,
                    StatusLeave = a.SubmissionAttribute.SubmissionStatus,
                    UserIdApproval = a.UserId,
                    StatusApproval = a.StatusApproval,
                    DateApproval = a.DateApproval,
                });

            leaves = _sieveProcessor.Apply(sieveModel, leaves, applyPagination: false);

            var result = await leaves.Select(u => _mapper.Map<GetApprovalLeaveResponse>(u)).ToListAsync();

            return new Response<IEnumerable<GetApprovalLeaveResponse>>
            {
                Message = "OK",
                Status = 200,
                Data = result
            };
        }

        public async Task<Response<PaginatedResponse<GetHistoryLeaveResponse>>> GetPagedLeaveList(SieveModel sieveModel)
        {
            var leaves = _dbContext.SubmissionAttributes.AsNoTracking()
                        .Include(d => d.User)
                        .Include(b => b.SubmissionLeave)
                        .Include(c => c.Approvals)
                        .Where(f => f.SubmissionLeaveId != null)
                        .Select(a => new GetHistoryLeaveResponse
                        {
                            SubmissionLeaveId = a.SubmissionLeaveId,
                            UserId = a.UserId,
                            DateSubmit = a.DateSubmit,
                            DateStart = a.SubmissionLeave.DateStart,
                            DateEnd = a.SubmissionLeave.DateEnd,
                            Description = a.Description,
                            Type = a.SubmissionLeave.LeaveType,
                            Attachment = a.Attachment,
                            StatusLeave = a.SubmissionStatus,
                            HistoryApprovals = _mapper.Map<List<Approval>, List<GetHistoryApprovalResponse>>(a.Approvals)
                        }) ;

            foreach (var leave in leaves)
            {
                Debug.WriteLine("SubmissionLeaveId : " + leave.SubmissionLeaveId);
            }

            leaves = _sieveProcessor.Apply(sieveModel, leaves, applyPagination: false);

            int count = await leaves.CountAsync();

            leaves = _sieveProcessor.Apply(sieveModel, leaves, applyPagination: true);

            var result = await PaginationHelper.GetPagedResultAsync<GetHistoryLeaveResponse, GetHistoryLeaveResponse>(leaves, sieveModel, _mapper, count);

            result.Data.ForEach(delegate (GetHistoryLeaveResponse response)
            {
                response.HistoryApprovals.Sort((x, y) => x.ApprovalId.CompareTo(y.ApprovalId));
                response.HistoryApprovals.ForEach(delegate (GetHistoryApprovalResponse approval)
                {
                    var user = _dbContext.Users.Where(u => u.Id == approval.UserId).FirstOrDefault();
                    approval.UserName = user.Name;
                });
            });

            return new Response<PaginatedResponse<GetHistoryLeaveResponse>>
            {
                Message = "OK",
                Status = 200,
                Data = result
            };
        }

        public async Task<Response<IEnumerable<GetHistoryLeaveResponse>>> GetLeaveList(SieveModel sieveModel)
        {
            IQueryable<Approval> query = new List<Approval>().AsQueryable();
            var leaves = _dbContext.SubmissionAttributes.AsNoTracking()
                        .Include(d => d.User)
                        .Include(b => b.SubmissionLeave)
                        .Include(c => c.Approvals)
                        .Where(f => f.SubmissionLeaveId != null)
                        .Select(a => new GetHistoryLeaveResponse
                        {
                            SubmissionLeaveId = a.SubmissionLeaveId,
                            UserId = a.UserId,
                            DateSubmit = a.DateSubmit,
                            DateStart = a.SubmissionLeave.DateStart,
                            DateEnd = a.SubmissionLeave.DateEnd,
                            Description = a.Description,
                            Type = a.SubmissionLeave.LeaveType,
                            Attachment = a.Attachment,
                            StatusLeave = a.SubmissionStatus,
                            HistoryApprovals = _mapper.Map<List<Approval>, List<GetHistoryApprovalResponse>>(a.Approvals)
                        });

            leaves = _sieveProcessor.Apply(sieveModel, leaves, applyPagination: false);

            var result = await leaves.Select(u => _mapper.Map<GetHistoryLeaveResponse>(u)).ToListAsync();

            result.ForEach(delegate (GetHistoryLeaveResponse response)
            {
                response.HistoryApprovals.Sort((x, y) => x.ApprovalId.CompareTo(y.ApprovalId));
                response.HistoryApprovals.ForEach(delegate (GetHistoryApprovalResponse approval)
                {
                    var user = _dbContext.Users.Where(u => u.Id == approval.UserId).FirstOrDefault();
                    approval.UserName = user.Name;
                });
            });

            return new Response<IEnumerable<GetHistoryLeaveResponse>>
            {
                Message = "OK",
                Status = 200,
                Data = result
            };
        }

        public async Task<Response<GetTotalDaysLeaveResponse>> GetTotalDaysLeave(int monthNumber)
        {
            var _submissionLeaves = await _dbContext.SubmissionLeaves.Where(s => s.DateStart.Month.Equals(monthNumber)
                                                                    && s.DateStart.Year.Equals(DateTime.Now.Year)).ToListAsync();
            GetTotalDaysLeaveResponse response = new GetTotalDaysLeaveResponse();

            if (_submissionLeaves == null)
            {
                return new Response<GetTotalDaysLeaveResponse>
                {
                    Message = "Not found",
                    Status = 404,
                    Data = response
                };
            }

            response.MonthNumber = monthNumber;
            response.TotalDays = 0;
            foreach (var _submissionLeave in _submissionLeaves)
            {
                if (!_submissionLeave.DateEnd.Month.Equals(monthNumber))
                {
                    var StartDate = _submissionLeave.DateStart;
                    var EndDate = new DateTime(StartDate.Year, StartDate.Month, DateTime.DaysInMonth(StartDate.Year, StartDate.Month));
                    response.TotalDays = response.TotalDays + (EndDate - StartDate).Days;
                }else
                {
                    response.TotalDays = response.TotalDays + (_submissionLeave.DateEnd - _submissionLeave.DateStart).Days;
                }
            }

            return new Response<GetTotalDaysLeaveResponse>
            {
                Message = "OK",
                Status = 200,
                Data = response
            };
        }
    }
}
