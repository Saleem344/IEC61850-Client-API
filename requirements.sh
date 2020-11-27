wget https://packages.microsoft.com/config/ubuntu/18.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
sudo apt-get update
sudo apt-get install -y apt-transport-https
sudo apt-get update
sudo apt-get install -y dotnet-sdk-3.1
sudo apt-get install -y aspnetcore-runtime-3.1
sudo apt-get install -y dotnet-runtime-3.1
sudo mv libiec61850.so /usr/lib
sudo mv libiec61850.so.1.4.2 /usr/lib
