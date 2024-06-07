#!/bin/sh
curl -sSL https://dot.net/v1/dotnet-install.sh > dotnet-install.sh
chmod +x dotnet-install.sh
./dotnet-install.sh -c 8.0 -InstallDir ./dotnet
./dotnet/dotnet --version
curl -sL https://deb.nodesource.com/setup_16.x | bash -
apt-get install -y nodejs

# Install npm dependencies
npm install

# Build CSS with Tailwind
npx tailwindcss -i wwwroot/css/app.css -o wwwroot/css/app.min.css
./dotnet/dotnet publish . -c Release -o output --version-suffix $CF_PAGES_COMMIT_SHA