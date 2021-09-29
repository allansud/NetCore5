﻿using Application.App.CommandHandler;
using Application.App.Commands.Users;
using Application.Data.Context;
using Application.Domain.Core.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application.IoC
{
    public static class DependencyContainer
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseMySql(configuration.GetConnectionString("DefaultConnection"),
                ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection"))));

            services.AddMediatR(Assembly.GetExecutingAssembly());

            //commands
            services.AddScoped<IRequestHandler<LoginUserCommand, ResponseResult>, LoginUserHandler>();
            services.AddScoped<IRequestHandler<RegisterUserCommand, ResponseResult>, RegisterUserHandler>();
        }
    }
}
