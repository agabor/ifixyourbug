#!/bin/bash

rm -rf bin
dotnet publish -c Release
cd bin/Release/net6.0/publish
zip -r ifyb.zip .
cd ../../../..
scp -i deploy.pem bin/Release/net6.0/publish/ifyb.zip admin@ifixyourbug.com:/home/admin/ifyb.zip
ssh -i deploy.pem admin@ifixyourbug.com /home/admin/setup.sh