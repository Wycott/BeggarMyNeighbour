@echo off
REM Starts the whole stack in Docker Desktop: Mosquitto broker, the web app, and N simulators.
REM
REM Usage:
REM   docker-up.bat        Start with the default simulator count (2, from .env)
REM   docker-up.bat 3      Start with 3 simulators
setlocal

cd /d "%~dp0"

REM Simulator count: use the argument if supplied, otherwise fall back to .env (SIM_COUNT=2).
set "SIM_COUNT=%~1"

echo Checking Docker is running...
docker info >nul 2>&1
if errorlevel 1 (
    echo.
    echo ERROR: Docker does not appear to be running.
    echo Start Docker Desktop and try again.
    exit /b 1
)

if defined SIM_COUNT (
    echo Building and starting stack with %SIM_COUNT% simulator^(s^)...
    docker compose up -d --build --scale beggarmyneighbour=%SIM_COUNT%
) else (
    echo Building and starting stack with the default simulator count...
    docker compose up -d --build
)
if errorlevel 1 (
    echo.
    echo ERROR: docker compose up failed.
    exit /b 1
)

echo.
echo Stack started. Web app: http://localhost:8080
echo Current status:
docker compose ps

endlocal
