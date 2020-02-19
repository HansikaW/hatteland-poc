### STAGE : Build ###
FROM node:12.8-alpine AS build
WORKDIR /src/app
COPY package.json ./
RUN npm install --verbose
COPY . .
RUN npm run build