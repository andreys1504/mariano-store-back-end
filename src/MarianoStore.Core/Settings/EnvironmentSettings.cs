using MarianoStore.Core.Settings.RabbitMq;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace MarianoStore.Core.Settings
{
    public class EnvironmentSettings
    {
        public EnvironmentSettings(
            string currentEnvironment,
            string projectName,
            string currentContext,
            string applicationLayer,
            string domainLayer)
        {
            currentEnvironment = currentEnvironment.ToLower();
            MarianoStore.Environment? environment = MarianoStore.Environments.GetEnvironments.FirstOrDefault(env_ => env_.ToString().ToLower() == currentEnvironment);
            if (environment == null)
                throw new System.Exception($"Ambiente \"{CurrentEnvironment}\" inválido");

            CurrentEnvironment = environment.Value;
            ProjectName = projectName;
            CurrentContext = currentContext;
            ApplicationLayer = applicationLayer;
            DomainLayer = domainLayer;
        }

        public MarianoStore.Environment CurrentEnvironment { get; set; }
        public string ProjectName { get; set; }
        public string CurrentContext { get; set; }
        public string ApplicationLayer { get; set; }
        public string DomainLayer { get; set; }
        public string SqlServerConnectionString { get; set; }
        public RabbitMqSettings RabbitMq { get; set; } = new RabbitMqSettings();

        public void GetConfigurationsInEnvironment(IConfiguration config)
        {
            if (string.IsNullOrWhiteSpace(SqlServerConnectionString))
                SqlServerConnectionString = config["SqlServerConnectionString"];


            #region RabbitMq

            if (string.IsNullOrWhiteSpace(RabbitMq.HostName))
                RabbitMq.HostName = config["RabbitMq_HostName"];

            if (RabbitMq.Port == 0)
                RabbitMq.Port = Convert.ToInt32(config["RabbitMq_Port"]);

            if (string.IsNullOrWhiteSpace(RabbitMq.UserName))
                RabbitMq.UserName = config["RabbitMq_UserName"];

            if (string.IsNullOrWhiteSpace(RabbitMq.Password))
                RabbitMq.Password = config["RabbitMq_Password"];

            #endregion
        }
    }
}
