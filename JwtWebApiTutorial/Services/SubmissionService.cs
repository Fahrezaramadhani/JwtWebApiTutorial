using AutoMapper;
using JwtWebApiTutorial.Data;
using JwtWebApiTutorial.Helpers;
using JwtWebApiTutorial.Models;
using JwtWebApiTutorial.Requests.Submission;
using JwtWebApiTutorial.Responses;
using JwtWebApiTutorial.Responses.Submission;
using JwtWebApiTutorial.Responses.SubmissionLeave;
using JwtWebApiTutorial.Services.Interface;
using JwtWebApiTutorial.Sieve;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using System.Diagnostics;

namespace JwtWebApiTutorial.Services
{
    public class SubmissionService : ISubmissionService
    {
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ApplicationSieveProcessor _sieveProcessor;

        public SubmissionService(DataContext _context, IMapper mapper, ApplicationSieveProcessor sieveProcessor)
        {
            _dbContext = _context;
            _mapper = mapper;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<Response<string>> AddSubmission(PostSubmissionRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.Attachment) && UploadImageHelper.IsBase64(request.Attachment))
            {
                request.Attachment = UploadImageHelper.UploadBase64File($"{DateTime.Now:yyyyMMddhhmmss}", request.Attachment, "Images/Submission");
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

            var submission = _mapper.Map<Submission>(request);
            var submissionAttribute = _mapper.Map<SubmissionAttribute>(request);
            submissionAttribute.SubmissionStatus = "Remaining";
            _dbContext.Submissions.Add(submission);
            _dbContext.SubmissionAttributes.Add(submissionAttribute);
            await _dbContext.SaveChangesAsync();
            submission.SubmissionAttributeId = submissionAttribute.Id;
            submissionAttribute.SubmissionId = submission.Id;

            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
            List<Approval> approvals = new List<Approval>();
            if (user.Role == "Employee")
            {
                var subordinateList = await _dbContext.Users.FirstOrDefaultAsync(x => x.SuperiorId == request.UserId);
                if (subordinateList == null)
                {
                    for (int i = 1; i <= 3; i++)
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

        public async Task<Response<string>> PutApprovalSubmission(PutApprovalSubmissionRequest request)
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

        public async Task<Response<string>> PutSubmission(PutSubmissionRequest request)
        {
            var _submission = _dbContext.Submissions.FirstOrDefault(x => x.Id == request.SubmissionId);
            var _submissionAttribute = _dbContext.SubmissionAttributes.FirstOrDefault(x => x.Id == _submission.SubmissionAttributeId);
            var _approvals = _dbContext.Approvals.Where(u => u.SubmissionAttributeId == _submissionAttribute.Id).ToList();

            //Check either the table schedule is null or not
            if (_submission == null || _submissionAttribute == null || _approvals == null)
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
                request.Attachment = UploadImageHelper.UploadBase64File($"{DateTime.Now:yyyyMMddhhmmss}", request.Attachment, "Images/Submission");
            }

            _submission.DatePerform = request.DatePerform;
            _submission.StartTime = request.StartTime;
            _submission.EndTime = request.EndTime;
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
            _dbContext.Submissions.Update(_submission);
            _dbContext.SubmissionAttributes.Update(_submissionAttribute);
            await _dbContext.SaveChangesAsync();
            return new Response<string>
            {
                Message = "OK",
                Status = 200,
                Data = ""
            };
        }

        public async Task<Response<PaginatedResponse<GetApprovalSubmissionResponse>>> GetPagedApprovalSubmissionList(SieveModel sieveModel)
        {
            var leaves = _dbContext.SubmissionAttributes.AsNoTracking()
                .Include(d => d.User)
                .Include(e => e.User.Position)
                .Include(b => b.Submission)
                .Include(c => c.Approvals)
                .SelectMany(a => a.Approvals)
                .Where(f => f.SubmissionAttribute.SubmissionId != null)
                .Select(a => new GetApprovalSubmissionResponse
                {
                    SubmissionId = a.SubmissionAttribute.SubmissionId,
                    SubmissionAttributeId = a.SubmissionAttributeId,
                    ApprovalId = a.Id,
                    OvertimeId = a.SubmissionAttribute.Submission.OvertimeId,
                    UserName = a.SubmissionAttribute.User.Name,
                    Position = a.SubmissionAttribute.User.Position.PositionName,
                    DateSubmit = a.SubmissionAttribute.DateSubmit,
                    DatePerform = a.SubmissionAttribute.Submission.DatePerform,
                    StartTime = a.SubmissionAttribute.Submission.StartTime,
                    EndTime = a.SubmissionAttribute.Submission.EndTime,
                    Description = a.SubmissionAttribute.Description,
                    SubmissionType = a.SubmissionAttribute.Submission.SubmissionType,
                    Attachment = a.SubmissionAttribute.Attachment,
                    StatusSubmission = a.SubmissionAttribute.SubmissionStatus,
                    UserIdApproval = a.UserId,
                    StatusApproval = a.StatusApproval,
                    DateApproval = a.DateApproval,
                });

            foreach (var leave in leaves)
            {
                Debug.WriteLine("SubmissionLeaveId : " + leave.SubmissionId);
            }

            leaves = _sieveProcessor.Apply(sieveModel, leaves, applyPagination: false);

            int count = await leaves.CountAsync();

            leaves = _sieveProcessor.Apply(sieveModel, leaves, applyPagination: true);

            var result = await PaginationHelper.GetPagedResultAsync<GetApprovalSubmissionResponse, GetApprovalSubmissionResponse>(leaves, sieveModel, _mapper, count);

            return new Response<PaginatedResponse<GetApprovalSubmissionResponse>>
            {
                Message = "OK",
                Status = 200,
                Data = result
            };
        }

        public async Task<Response<IEnumerable<GetApprovalSubmissionResponse>>> GetApprovalSubmissionList(SieveModel sieveModel)
        {
            var leaves = _dbContext.SubmissionAttributes.AsNoTracking()
                .Include(d => d.User)
                .Include(e => e.User.Position)
                .Include(b => b.Submission)
                .Include(c => c.Approvals)
                .SelectMany(a => a.Approvals)
                .Where(f => f.SubmissionAttribute.SubmissionId != null)
                .Select(a => new GetApprovalSubmissionResponse
                {
                    SubmissionId = a.SubmissionAttribute.SubmissionId,
                    SubmissionAttributeId = a.SubmissionAttributeId,
                    ApprovalId = a.Id,
                    OvertimeId = a.SubmissionAttribute.Submission.OvertimeId,
                    UserName = a.SubmissionAttribute.User.Name,
                    Position = a.SubmissionAttribute.User.Position.PositionName,
                    DateSubmit = a.SubmissionAttribute.DateSubmit,
                    DatePerform = a.SubmissionAttribute.Submission.DatePerform,
                    StartTime = a.SubmissionAttribute.Submission.StartTime,
                    EndTime = a.SubmissionAttribute.Submission.EndTime,
                    Description = a.SubmissionAttribute.Description,
                    SubmissionType = a.SubmissionAttribute.Submission.SubmissionType,
                    Attachment = a.SubmissionAttribute.Attachment,
                    StatusSubmission = a.SubmissionAttribute.SubmissionStatus,
                    UserIdApproval = a.UserId,
                    StatusApproval = a.StatusApproval,
                    DateApproval = a.DateApproval,
                });

            leaves = _sieveProcessor.Apply(sieveModel, leaves, applyPagination: false);

            var result = await leaves.Select(u => _mapper.Map<GetApprovalSubmissionResponse>(u)).ToListAsync();

            return new Response<IEnumerable<GetApprovalSubmissionResponse>>
            {
                Message = "OK",
                Status = 200,
                Data = result
            };
        }

        public async Task<Response<PaginatedResponse<GetHistorySubmissionResponse>>> GetPagedSubmissionList(SieveModel sieveModel)
        {
            IQueryable<Approval> query = new List<Approval>().AsQueryable();
            var leaves = _dbContext.SubmissionAttributes.AsNoTracking()
                        .Include(d => d.User)
                        .Include(b => b.Submission)
                        .Include(c => c.Approvals)
                        .Where(f => f.SubmissionId != null)
                        .Select(a => new GetHistorySubmissionResponse
                        {
                            SubmissionId = a.SubmissionId,
                            UserId = a.UserId,
                            OvertimeId = a.Submission.OvertimeId,
                            DateSubmit = a.DateSubmit,
                            DatePerform = a.Submission.DatePerform,
                            StartTime = a.Submission.StartTime,
                            EndTime = a.Submission.EndTime,
                            Description = a.Description,
                            SubmissionType = a.Submission.SubmissionType,
                            Attachment = a.Attachment,
                            StatusSubmission = a.SubmissionStatus,
                            HistoryApprovals = _mapper.Map<List<Approval>, List<GetHistoryApprovalResponse>>(a.Approvals)
                        });

            foreach (var leave in leaves)
            {
                Debug.WriteLine("SubmissionLeaveId : " + leave.SubmissionId);
            }

            leaves = _sieveProcessor.Apply(sieveModel, leaves, applyPagination: false);

            int count = await leaves.CountAsync();

            leaves = _sieveProcessor.Apply(sieveModel, leaves, applyPagination: true);

            var result = await PaginationHelper.GetPagedResultAsync<GetHistorySubmissionResponse, GetHistorySubmissionResponse>(leaves, sieveModel, _mapper, count);

            result.Data.ForEach(delegate (GetHistorySubmissionResponse response)
            {
                response.HistoryApprovals.Sort((x, y) => x.ApprovalId.CompareTo(y.ApprovalId));
                response.HistoryApprovals.ForEach(delegate (GetHistoryApprovalResponse approval)
                {
                    var user = _dbContext.Users.Where(u => u.Id == approval.UserId).FirstOrDefault();
                    approval.UserName = user.Name;
                });
            });

            return new Response<PaginatedResponse<GetHistorySubmissionResponse>>
            {
                Message = "OK",
                Status = 200,
                Data = result
            };
        }

        public async Task<Response<IEnumerable<GetHistorySubmissionResponse>>> GetSubmissionList(SieveModel sieveModel)
        {
            IQueryable<Approval> query = new List<Approval>().AsQueryable();
            var leaves = _dbContext.SubmissionAttributes.AsNoTracking()
                        .Include(d => d.User)
                        .Include(b => b.Submission)
                        .Include(c => c.Approvals)
                        .Where(f => f.SubmissionId != null)
                        .Select(a => new GetHistorySubmissionResponse
                        {
                            SubmissionId = a.SubmissionId,
                            UserId = a.UserId,
                            OvertimeId = a.Submission.OvertimeId,
                            DateSubmit = a.DateSubmit,
                            DatePerform = a.Submission.DatePerform,
                            StartTime = a.Submission.StartTime,
                            EndTime = a.Submission.EndTime,
                            Description = a.Description,
                            SubmissionType = a.Submission.SubmissionType,
                            Attachment = a.Attachment,
                            StatusSubmission = a.SubmissionStatus,
                            HistoryApprovals = _mapper.Map<List<Approval>, List<GetHistoryApprovalResponse>>(a.Approvals)
                        });

            leaves = _sieveProcessor.Apply(sieveModel, leaves, applyPagination: false);

            var result = await leaves.Select(u => _mapper.Map<GetHistorySubmissionResponse>(u)).ToListAsync();

            result.ForEach(delegate (GetHistorySubmissionResponse response)
            {
                response.HistoryApprovals.Sort((x, y) => x.ApprovalId.CompareTo(y.ApprovalId));
                response.HistoryApprovals.ForEach(delegate (GetHistoryApprovalResponse approval)
                {
                    var user = _dbContext.Users.Where(u => u.Id == approval.UserId).FirstOrDefault();
                    approval.UserName = user.Name;
                });
            });

            return new Response<IEnumerable<GetHistorySubmissionResponse>>
            {
                Message = "OK",
                Status = 200,
                Data = result
            };
        }
    }
}
