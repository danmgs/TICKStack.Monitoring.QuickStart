# TICKStack.Monitoring.QuickStart (2019)

A Console Application to collect measures for TICK Stack.

![alt capture1](https://github.com/danmgs/TICKStack.Monitoring.QuickStart/blob/master/img/chronograf.gif)

## <span style="color:green">FOLDER ORGANIZATION</span>

```
| -- App.config                 -> A configuration file to configure influxdb stuff.
| -- Program.cs                 -> Entry point of the application

| -- /Jobs
     | -- AbstractMonitoringJob.cs      -> common abstract class
     | -- HealthMonitoringJob.cs        -> Job "Health" inserts entries in influxdb database "health"
     | -- IJob.cs                       -> Job Interface
     | -- PriceMonitoringJob.cs         -> Job "Price" inserts entries in influxdb database "price"
```

## <span style="color:green">Launch the TICK STACK</span>

Use the [sandbox project](https://github.com/danmgs/sandbox) to launch the TICK Stack. It is a dockerized version.

## <span style="color:green">Configure the C# console application</span>

- Configure the app.config with the influxdb url and time interval for the Metric Collector
```
	<add key="WRITE_INTERVAL_IN_SECONDS" value="2"/>
    <add key="INFLUXDB_URL" value="http://localhost:8086"/>
```

## <span style="color:green">Configure the influxdb database to put entries</span>

### <span style="color:green">Database "price"</span>

Note:
You can create them via [Chronograf admin interface](http://localhost:8888/sources/10000/admin-influxdb/databases) or via influxdb cli.

- Create database in influx db with retention days (by default infinite)

```
CREATE DATABASE "price"
CREATE RETENTION POLICY "seven_days" ON "price" DURATION 7d REPLICATION 1 DEFAULT

```

- Configure the app.config with the database name

```
    <add key="INFLUXDB_DATABASE_NAME" value="price"/>
```

### <span style="color:green">Database "health"</span>

- Create database in influx db with retention days (by default infinite)

```
CREATE DATABASE "health"
CREATE RETENTION POLICY "seven_days" ON "health" DURATION 7d REPLICATION 1 DEFAULT

```

- Configure the app.config with the database name

```
    <add key="InfluxDB.Database.Name2" value="health"/>
```
