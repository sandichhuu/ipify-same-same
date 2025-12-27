@echo off
echo Remove old ipify image
docker rmi apify:latest

echo Remove old build
if exist ipify.tar ( del ipify.tar )
 
echo Build apify
docker build -t ipify:latest .

echo Export tar
docker save ipify:latest -o ipify.tar

echo Finish
pause