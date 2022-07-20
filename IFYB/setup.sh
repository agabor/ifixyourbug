#!/bin/bash

sudo systemctl stop kestrel-ifyb.service
rm -rf /var/www/ifyb/*
unzip ifyb.zip -d /var/www/ifyb
cp appsettings.json /var/www/ifyb/appsettings.json
sudo systemctl start kestrel-ifyb.service
