using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Retry;
using System;

namespace MarianoStore.Core.Ioc
{
    public static partial class RetryPoliciesExtensions
    {
        public static IServiceCollection AddSingletonWithRetry<TService, TKnowException>(this IServiceCollection services, Func<IServiceProvider, TService> implementationFactory)
            where TKnowException : Exception
            where TService : class
        {
            return services.AddSingleton(serviceProvider =>
            {
                TService returnValue = default;

                BuildPolicy<TKnowException>().Execute(() =>
                {
                    returnValue = implementationFactory(serviceProvider);
                });

                return returnValue;

            });
        }

        public static IServiceCollection AddTransientWithRetry<TService, TKnowException>(this IServiceCollection services, Func<IServiceProvider, TService> implementationFactory)
            where TKnowException : Exception
            where TService : class
        {
            return services.AddTransient(serviceProvider =>
            {
                TService returnValue = default;

                BuildPolicy<TKnowException>().Execute(() =>
                {
                    returnValue = implementationFactory(serviceProvider);
                });

                return returnValue;

            });
        }


        //
        private static RetryPolicy BuildPolicy<TKnowException>(int retryCount = 5) where TKnowException : Exception
        {
            return Policy
                .Handle<TKnowException>()
                .WaitAndRetry(retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
            );
        }
    }
}
