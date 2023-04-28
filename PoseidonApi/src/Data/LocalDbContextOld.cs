using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PoseidonApi.Domain;
using PoseidonApi.Models;

namespace PoseidonApi.Data
{
    public class LocalDbContextOld : DbContext
    {
        //public DbSet<UserDTO> Users { get; set;}
    }
}