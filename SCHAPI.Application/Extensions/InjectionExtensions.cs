﻿using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SCHAPI.Application.Interfaces;
using SCHAPI.Application.Services;
using System.Reflection;
using WatchDog;
using WatchDog.src.Enums;

namespace SCHAPI.Application.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddIjectionApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddWatchDogServices(options =>
            {
                options.IsAutoClear = true;
                options.ClearTimeSchedule = WatchDogAutoClearScheduleEnum.Daily;
            });

            services.AddScoped<ITeacherApplication, TeacherApplication>();
            services.AddScoped<IStudentApplication, StudentApplication>();
            services.AddScoped<ISubjectApplication, SubjectApplication>();
            services.AddScoped<IClassroomApplication, ClassroomApplication>();
            services.AddScoped<ILessonApplication, LessonApplication>();
            services.AddScoped<IScheduleApplication, ScheduleApplication>();
            services.AddScoped<ILessonStudentApplication, LessonStudentApplication>();

            return services;
        }
    }
}
