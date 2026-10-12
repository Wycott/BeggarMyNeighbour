@echo off
REM Stops and removes all project containers (Mosquitto broker, web app, and simulators).
REM Named volumes (broker data/logs) are preserved. Pass -v to also remove them.
REM Usage: docker-down.bat [-v]
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

if /i "%~1"=="-v" (
    echo Stopping containers and removing volumes...
    docker compose down -v
) else (
    echo Stopping containers...
    docker compose down
)
if errorlevel 1 (
    echo.
    echo ERROR: docker compose down failed.
    exit /b 1
)

echo.
echo Done.

endlocal
