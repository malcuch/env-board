namespace EnvBoard.Web.Model
{
    using System;
    using System.Collections.Generic;
    
    public class State
    {
        public List<Environment> Environments = new List<Environment>();

        public DateTime LastUpdateAt { get; set; }
        public void UpdateEnvironment(Environment environment)
        {
            Environments.RemoveAll(x => x.Name.Equals(environment.Name, StringComparison.OrdinalIgnoreCase));
            Environments.Add(environment);
            LastUpdateAt = DateTime.Now;
        }

        public Environment GetEnvironmentByName(string name)
        {
            return Environments.Find(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                   ?? Environment.Default(name);
        }
    }


    public class Environment
    {
        public string Name { get; set; }

        public Deployment Deployment { get; set; }

        public static Environment Default(string name)
        {
            return new Environment {Name = name};
        }
    }


    public class Deployment
    {
        public string Build { get; set; }

        public string BranchName { get; set; }

        public string CommitId { get; set; }

        public string CommitMessage { get; set; }

        public string DeployedBy { get; set; }

        public DateTime DeployedAt { get; set; } = DateTime.Now;

        public DeploymentStatus Status { get; set; }
    }

    public enum DeploymentStatus
    {
        Canceled,
        Failed,
        Succeeded,
        SucceededWithIssues,
    }
}