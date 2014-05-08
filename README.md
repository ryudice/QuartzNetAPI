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
#### /api/triggers
Returns all triggers

#### /api/jobs
Returns all jobs

#### /api/jobs/{id}/trigger
Fires the job

#### /api/triggers/{id}/resume
Resumes the trigger if it's state is not "normal".

Parameters
===========
*id* should be the Id field returned from the server in both get requests for triggers, and jobs.



Sample Output
------------------
### /api/triggers

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

### /api/jobs

```
{
    "jobs": [
        {
            "Name": "UpdateJobs",
            "Schedule": "08/05/2014 09:23:00 a.m. -06:00",
            "Id": "UpdateJobs",
            "Group": "DEFAULT"
        }
    ]
}
```

