#!/bin/sh
# Starts the Beggar My Neighbour solution on the Synology NAS.
#
# Prerequisites (copy these into the same directory on the NAS first):
#   - beggarmyneighbour-sim.tar      (built by build-prod.bat)
#   - beggarmyneighbour-web.tar      (built by build-prod.bat)
#   - docker-compose.prod.yml
#   - mosquitto/config/mosquitto.conf
#
# Optionally set SIM_COUNT to change the number of simulator instances (default 2):
#   SIM_COUNT=3 ./prod-up.sh
set -e

echo "Copy the .tar files over and press ENTER"
read _

echo "Loading images..."
sudo docker load -i beggarmyneighbour-sim.tar
sudo docker load -i beggarmyneighbour-web.tar

echo "Starting the stack..."
sudo docker compose -f docker-compose.prod.yml up -d

echo "Running containers:"
sudo docker compose -f docker-compose.prod.yml ps

echo "Done! Web app: http://$(hostname):5300"
