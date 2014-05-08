QuartzNetAPI
===========

Pluggable management API using OWIN and Web API for Quartz.NET

Usage
======
```C#
QuartzAPI.Configure(builder =>
           {
               builder.UseScheduler(container.GetInstance<IScheduler>()); //This is the scheduler you are using, in this case I'm pulling out the scheduler from my structuremap container
               builder.EnableCors();
           });
QuartzAPI.Start("http://localhost:9001/");


```

API
===========

Sample Output
------------------
*/api/triggers*

```
{
    "triggers": [
        {
            "State": "Normal",
            "Job": "DEFAULT.UpdateJobs",
            "Name": "UpdateJobsTrigger",
            "CronExpression": "0 0/5 * * * ?",
            "Id": "DEFAULT.UpdateJobsTrigger",
            "Group": "DEFAULT"
        }
    ]
}
```
