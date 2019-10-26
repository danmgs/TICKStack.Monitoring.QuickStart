# TICKStack.Monitoring.QuickStart (2019)

A Console Application to collect measures for TICK Stack.

![alt capture1](https://github.com/danmgs/TICKStack.Monitoring.QuickStart/blob/master/img/chronograf.gif)

# <span style="color:green">Folder Organization</span>

```
.env                                        -> Environment variables configuration
docker-compose.yml                          -> Docker
launch.bat                                  -> The launcher of the project
mydata                                      -> Configuration files for TICK Stack and data storage when running the stack.
TICKStack.Monitoring.QuickStart/
    | -- Dockerfile                         -> Docker

    | -- App.config                         -> A configuration file to configure influxdb stuff.
    | -- Program.cs                         -> Entry point of the application

    | -- /Jobs
        | -- AbstractMonitoringJob.cs       -> common abstract class
        | -- HealthMonitoringJob.cs         -> Job "Health" inserts entries in influxdb database "health"
        | -- IJob.cs                        -> Job Interface
        | -- PriceMonitoringJob.cs          -> Job "Price" inserts entries in influxdb database "price"
```

# <span style="color:green">Launch the Docker version</span>

## Start the dockerized version

You can configure some environment variables in the .env file.
At the root of the solution, run the command :

```
launch.bat up
```

It will docker-compose to run the TICK Stack and the C# console application, It will open [chronograf](http://localhost:8888) in a browser.

At first time launching chronograf, you will need to configure connexion to influxdb and kapacitor using the hostname:port 'influxdb:8086" + "kapacitor:9092" (docker urls) instead of "localhost".

## Stop the dockerized version

```
launch.bat down
```

## Connect to influxdb client to execute some queries

```
launch.bat influxdb
```

## Custom notes for Docker

### Build image for docker hub

In the root directory (at the **data** directory level):

```
docker build -t danmgs/tickstack-monitoring-console -f TICKStack.Monitoring.QuickStart/Dockerfile .
docker push danmgs/tickstack-monitoring-console:latest
```

### Pull image from docker hub

You can find the docker image [my repository on DockerHub](https://hub.docker.com/r/danmgs/tickstack-monitoring-console).


Note :
This way, instead of building the image, you are able to configure **docker-compose.yml** to pull the image like so:

```
  tickstack-monitoring-console:
    image: danmgs/tickstack-monitoring-console:latest
    environment:
      INFLUXDB_URL: $ENV_INFLUXDB_URL
      INFLUXDB_DATABASE_NAME: $ENV_INFLUXDB_DATABASE_NAME
      WRITE_INTERVAL_IN_SECONDS: $ENV_WRITE_INTERVAL_IN_SECONDS
    links:
      - influxdb
    restart: always
    depends_on:
      - kapacitor
      - influxdb
      - telegraf

```

# <span style="color:green">Create the influxdb database</span>

## Database "price"

You will need to create the database in order to the console to store the datapoint.

You can create the database via [Chronograf admin interface](http://localhost:8888/sources/10000/admin-influxdb/databases) or via influxdb cli.

- Create database in influxdb with retention days (by default infinite)

```
CREATE DATABASE "price"
CREATE RETENTION POLICY "seven_days" ON "price" DURATION 7d REPLICATION 1 DEFAULT

```

# <span style="color:green">Launch the console locally</span>

## Configure the C# console application to run and/or debug locally

Configure the app.config with the influxdb url, time interval, and database name :
```
<add key="WRITE_INTERVAL_IN_SECONDS" value="2"/>
<add key="INFLUXDB_URL" value="http://localhost:8086"/>
<add key="INFLUXDB_DATABASE_NAME" value="price"/>
```



