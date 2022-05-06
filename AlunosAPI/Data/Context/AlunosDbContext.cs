using AlunosAPI.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AlunosAPI.Data.Context
{
    public class AlunosDbContext : IdentityDbContext<IdentityUser>
    {
        public AlunosDbContext(DbContextOptions<AlunosDbContext> options) : base(options)
        {

        }

        public DbSet<Alunos> Alunos { get; set; }
    }
}
