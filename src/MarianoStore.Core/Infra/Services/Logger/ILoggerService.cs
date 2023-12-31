﻿using System;
using System.Threading.Tasks;

namespace MarianoStore.Core.Infra.Services.Logger
{
    public interface ILoggerService
    {
        Task LogErrorRegisterAsync(Exception exception, string message);
    }
}
