// <copyright file="ApplicationSieveProcessor.cs" company="CV Garuda Infinity Kreasindo">
// Copyright (c) CV Garuda Infinity Kreasindo. All rights reserved.
// </copyright>

using JwtWebApiTutorial.Models;
using JwtWebApiTutorial.Responses.Leave;
using JwtWebApiTutorial.Responses.Submission;
using JwtWebApiTutorial.Responses.SubmissionLeave;
using Microsoft.Extensions.Options;
using Sieve.Models;
using Sieve.Services;

namespace JwtWebApiTutorial.Sieve
{
    public class ApplicationSieveProcessor : SieveProcessor
    {
        public ApplicationSieveProcessor(IOptions<SieveOptions> options, ISieveCustomSortMethods sieveCustomSortMethods, ISieveCustomFilterMethods sieveCustomFilterMethods)
            : base(options, sieveCustomSortMethods, sieveCustomFilterMethods)
        {
        }

        protected override SievePropertyMapper MapProperties(SievePropertyMapper mapper)
        {
            /* USER */
            mapper.Property<User>(x => x.Name)
                .CanFilter()
                .CanSort();
            mapper.Property<User>(x => x.Id)
                .CanFilter()
                .CanSort();

            /* Attendance */
            mapper.Property<Attendance>(x => x.Id)
                .CanFilter()
                .CanSort();
            mapper.Property<Attendance>(x => x.UserId)
                .CanFilter()
                .CanSort();
            mapper.Property<Attendance>(x => x.CreatedAt)
                .CanFilter()
                .CanSort();


            /* Activity Record */
            mapper.Property<ActivityRecord>(x => x.Id)
                .CanFilter()
                .CanSort();
            mapper.Property<ActivityRecord>(x => x.UserId)
                .CanFilter()
                .CanSort();
            mapper.Property<ActivityRecord>(x => x.CreatedAt)
                .CanFilter()
                .CanSort();


            /* SubmissionLeave */
            //Filter for Approval
            mapper.Property<GetApprovalLeaveResponse>(x => x.SubmissionLeaveId)
                .CanFilter()
                .CanSort();
            mapper.Property<GetApprovalLeaveResponse>(x => x.UserIdApproval)
                .CanFilter()
                .CanSort();
            mapper.Property<GetApprovalLeaveResponse>(x => x.StatusApproval)
                .CanFilter()
                .CanSort();
            //filter for history
            mapper.Property<GetHistoryLeaveResponse>(x => x.UserId)
                .CanFilter()
                .CanSort();
            mapper.Property<GetHistoryLeaveResponse>(x => x.SubmissionLeaveId)
                .CanFilter()
                .CanSort();

            /* Submission */
            //Filter for Approval
            mapper.Property<GetApprovalSubmissionResponse>(x => x.SubmissionId)
                .CanFilter()
                .CanSort();
            mapper.Property<GetApprovalSubmissionResponse>(x => x.SubmissionType)
                .CanFilter()
                .CanSort();
            mapper.Property<GetApprovalSubmissionResponse>(x => x.UserIdApproval)
                .CanFilter()
                .CanSort();
            mapper.Property<GetApprovalSubmissionResponse>(x => x.StatusApproval)
                .CanFilter()
                .CanSort();
            mapper.Property<GetApprovalSubmissionResponse>(x => x.OvertimeId)
                .CanFilter()
                .CanSort();
            //filter for history
            mapper.Property<GetHistorySubmissionResponse>(x => x.OvertimeId)
                .CanFilter()
                .CanSort();
            mapper.Property<GetHistorySubmissionResponse>(x => x.UserId)
                .CanFilter()
                .CanSort();
            mapper.Property<GetHistorySubmissionResponse>(x => x.SubmissionId)
                .CanFilter()
                .CanSort();
            mapper.Property<GetHistorySubmissionResponse>(x => x.SubmissionType)
                .CanFilter()
                .CanSort();

            return mapper;
        }
    }
}
