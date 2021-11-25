VERSION=${1:-latest}

echo $VERSION

# publish .net server applications
cd server

docker build -f ./WebApi/Dockerfile --force-rm -t antonybaasan/robogachi.webapi:$VERSION .
docker push antonybaasan/robogachi.webapi:$VERSION

read -p "Press enter to continue"