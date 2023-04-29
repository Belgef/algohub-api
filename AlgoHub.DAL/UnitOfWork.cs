using AlgoHub.DAL.Context;
using AlgoHub.DAL.Entities;
using AlgoHub.DAL.Interfaces;
using AlgoHub.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoHub.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AlgoHubDbContext _dbContext;

        private IUserRepository? _userRepository;

        public UnitOfWork(AlgoHubDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IUserRepository UserRepository 
            => _userRepository ??= new UserRepository(_dbContext);
    }
}
