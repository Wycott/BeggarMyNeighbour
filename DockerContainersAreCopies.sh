#!/bin/bash
id1=$(docker inspect --format='{{.Image}}' "$1")
id2=$(docker inspect --format='{{.Image}}' "$2")

if [ "$id1" = "$id2" ]; then
    echo "Same image ($id1)"
else
    echo "Different images:"
    echo "$1 → $id1"
    echo "$2 → $id2"
fi
