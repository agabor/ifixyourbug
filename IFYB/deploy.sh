#!/bin/bash

rm -rf bin
dotnet publish -c Release
cd bin/Release/net6.0/publish
zip -r ifyb.zip .
cd ../../../..
scp -i deploy.pem bin/Release/net6.0/publish/ifyb.zip admin@ifixyourbug.com:/home/admin/ifyb.zip
scp -i deploy.pem setup.sh admin@ifixyourbug.com:/home/admin/setup.sh
ssh -i deploy.pem admin@ifixyourbug.com chmod +x /home/admin/setup.sh
ssh -i deploy.pem admin@ifixyourbug.com /home/admin/setup.sh