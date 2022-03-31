// <copyright file="ApplicationSieveProcessor.cs" company="CV Garuda Infinity Kreasindo">
// Copyright (c) CV Garuda Infinity Kreasindo. All rights reserved.
// </copyright>

using JwtWebApiTutorial.Models;
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

            /* Schedule */
            mapper.Property<AttendanceSchedule>(x => x.Id)
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

            return mapper;
        }
    }
}
