# Build tagged version
docker build -t staticola/beggarmyneighbour:$(git rev-parse HEAD) .

# Push tagged version
docker push staticola/beggarmyneighbour:$(git rev-parse HEAD)

# Tag tagged version as latest
docker tag staticola/beggarmyneighbour:$(git rev-parse HEAD) staticola/beggarmyneighbour:latest

# Push latest tagged version
docker push staticola/beggarmyneighbour:latest
