@echo off
REM Builds the application images and exports them as .tar files for deployment to the
REM Synology NAS (load there with `docker load -i <file>.tar`, or via Container Manager).
REM
REM Produces:
REM   beggarmyneighbour-sim.tar  - the simulator image
REM   beggarmyneighbour-web.tar  - the web application image
REM
REM The Mosquitto broker uses the public eclipse-mosquitto:2 image, which the NAS can pull
REM directly, so it is not exported here. Pass -broker to also export it for an offline NAS.
setlocal

cd /d "%~dp0"

echo Checking Docker is running...
docker info >nul 2>&1
if errorlevel 1 (
    echo.
    echo ERROR: Docker does not appear to be running.
    echo Start Docker Desktop and try again.
    exit /b 1
)

echo Building application images...
docker compose build
if errorlevel 1 (
    echo.
    echo ERROR: docker compose build failed.
    exit /b 1
)

echo Exporting simulator image...
docker save beggarmyneighbour-beggarmyneighbour -o beggarmyneighbour-sim.tar
if errorlevel 1 (
    echo.
    echo ERROR: failed to export the simulator image.
    exit /b 1
)

echo Exporting web application image...
docker save beggarmyneighbour-beggarmyneighbourweb -o beggarmyneighbour-web.tar
if errorlevel 1 (
    echo.
    echo ERROR: failed to export the web application image.
    exit /b 1
)

if /i "%~1"=="-broker" (
    echo Exporting Mosquitto broker image...
    docker save eclipse-mosquitto:2 -o mosquitto.tar
    if errorlevel 1 (
        echo.
        echo ERROR: failed to export the Mosquitto image.
        exit /b 1
    )
)

echo.
echo Build artifacts (newest last):
dir /od *.tar

echo.
echo Done at:
time /t

endlocal
