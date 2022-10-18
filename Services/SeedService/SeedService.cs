using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineAuction.Dtos.Product;
using OnlineAuction.Models;
using AutoMapper;
using OnlineAuction.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using Microsoft.AspNetCore.Hosting;

namespace OnlineAuction.Services.SeedService
{
    public class SeedService : ISeedService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _env;
        public SeedService(IMapper mapper, DataContext context, IWebHostEnvironment env)
        {
            _mapper = mapper;
            _context = context;
            _env = env;
        }        

    }
}