# Review application
A simple review system for different subjects (e.g. doctors, clinics)

## Build Status
| Build server| Platform       | Status      |
|-------------|----------------|-------------|
| appveyor    | Windows        |[![Build status](https://ci.appveyor.com/api/projects/status/84djajia77jann58?svg=true)](https://ci.appveyor.com/project/linuxchata/review-app/branch/master) [![BCH compliance](https://bettercodehub.com/edge/badge/linuxchata/review-app?branch=master)](https://bettercodehub.com/) |

## Technologies and frameworks
* ASP.NET Core 2.2
    * Autofac
    * nlog
    * Swagger
    * xunit / moq
* MongoDB
* Azure Service Bus
* Application Insights
* Docker
* ReactJS / React Router
* TypeScript
* Mobx
* Axios
* Webpack

## Docker support
Run docker images
```
docker run -it -p 5001:80 reviewappwebapi:latest
docker run -it -p 5002:80 reviewapplocationapi:latest
```
or
```
docker-compose up
```
