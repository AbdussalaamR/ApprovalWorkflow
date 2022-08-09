using MEMOJET.Entities;
using MEMOJET.Identity;
using Microsoft.EntityFrameworkCore;

namespace MEMOJET.Context
{
    
    public class ApplicationContext:DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            
        }
        public DbSet<Approval> Approvals { get; set; }
        public DbSet<ApprovalRespoCentre> ApprovalRespoCentres { get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<ResponsibilityCentre> ResponsibilityCentres { get; set; }
        public DbSet<UserForm> UserForms { get; set; }
        public DbSet<Role>Roles { get; set; }
        public DbSet<User>Users { get; set; }
        public DbSet<UserRole>UserRoles { get; set; }
        public DbSet<Comment>Comments { get; set; }
        public DbSet<UploadedDoc>UploadedDocs { get; set; }
    }
    
}