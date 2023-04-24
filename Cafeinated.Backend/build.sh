#!/bin/bash

docker build . -t 188.34.177.197:32000/cafeinated-backend 
docker push 188.34.177.197:32000/cafeinated-backend:latest
