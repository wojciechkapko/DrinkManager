﻿using BLL;
using Microsoft.EntityFrameworkCore;

namespace ReportingModule.API.Data
{
    public class ReportingApiContext : DbContext
    {
        public ReportingApiContext(DbContextOptions<ReportingApiContext> options) : base(options)
        {
        }
        public DbSet<UserActivity> UserActivities { get; set; }
        public void CheckIfReportingDatabaseExistsAndCreateIfNot()
        {
            Database.EnsureCreated();
            Database.Migrate();
        }
    }
}
