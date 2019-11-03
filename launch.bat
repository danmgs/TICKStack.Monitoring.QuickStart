@ECHO OFF
TITLE launch.bat - TICKStack.Monitoring.QuickStart

SET interactive=1
IF %ERRORLEVEL% == 0 SET interactive=0

IF "%1"=="up" (
    ECHO Launching...
    ECHO If this is your first time starting the project this might take a minute...

    REM Uncomment to overrides .env configuration here
    REM SET ENV_CONSOLE_IMAGE_TAG=danmgs/tickstack-monitoring-console:latest
    REM SET ENV_INFLUXDB_URL=http://influxdb:8086
    REM SET WRITE_INTERVAL_IN_SECONDS: $ENV_WRITE_INTERVAL_IN_SECONDS
    REM SET ENV_INFLUXDB_DATABASE_NAME=price
    REM SET TELEGRAF_TAG=telegraf:latest
    REM SET INFLUXDB_TAG=influxdb:latest
    REM SET CHRONOGRAF_TAG=chronograf:latest
    REM SET KAPACITOR_TAG=kapacitor:latest

    docker-compose up -d --build
    ECHO Opening tabs in browser...
    timeout /t 5 /nobreak > NUL
    START "" http://localhost:8888
    GOTO End
)

IF "%1"=="down" (
    ECHO Stopping and removing running sandbox containers...
    docker-compose down
    GOTO End
)

IF "%1"=="influxdb" (
    ECHO Entering the influx cli...
    docker-compose exec influxdb /usr/bin/influx
    GOTO End
)

IF "%1"=="delete-data" (
    ECHO Deleting all influxdb, kapacitor and chronograf data...
    rmdir /S /Q kapacitor\data influxdb\data chronograf\data
    GOTO End
)

ECHO commands:
ECHO   up           -^> spin up the sandbox environment
ECHO   down         -^> tear down the sandbox environment
ECHO   influxdb     -^> attach to the influx cli
ECHO   delete-data  -^> delete all data created by the TICK Stack

:End
IF "%interactive%"=="0" PAUSE
EXIT /B 0
