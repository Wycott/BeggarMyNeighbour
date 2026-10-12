#!/bin/sh
# Tears down the Beggar My Neighbour solution on the Synology NAS: stops the containers,
# removes the application images, and deletes the .tar files.
#
# Named volumes (Mosquitto data/logs) are preserved. Pass -v to also remove them.
set -e

echo "This will stop the containers, remove the app images, and delete the .tar files."
printf "Press ENTER when ready (Ctrl+C to cancel). "
read _

echo "Stopping the stack..."
if [ "$1" = "-v" ]; then
    sudo docker compose -f docker-compose.prod.yml down -v
else
    sudo docker compose -f docker-compose.prod.yml down
fi

echo "Removing application images..."
sudo docker rmi beggarmyneighbour-beggarmyneighbour:latest || true
sudo docker rmi beggarmyneighbour-beggarmyneighbourweb:latest || true

echo "Deleting .tar files..."
rm -f beggarmyneighbour-sim.tar beggarmyneighbour-web.tar mosquitto.tar

echo "Done!"
