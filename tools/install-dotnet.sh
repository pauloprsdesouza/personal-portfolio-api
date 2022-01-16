#!/bin/bash

set -e

version=$1
tar_gz="https://dotnetcli.blob.core.windows.net/dotnet/Sdk/$version/dotnet-sdk-$version-linux-x64.tar.gz"

curl -SL -o dotnet.tar.gz $tar_gz
sudo mkdir -p /usr/share/dotnet
sudo tar -zxf dotnet.tar.gz -C /usr/share/dotnet
sudo ln -s /usr/share/dotnet/dotnet /usr/bin/dotnet

sudo dotnet tool install --global dotnet-reportgenerator-globaltool

export PATH="$PATH:/home/circleci/.dotnet/tools"
