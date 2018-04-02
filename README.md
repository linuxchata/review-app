# Review application
A simple review system for different subjects (e.g. doctors, clinics)

## Build Status
| Build server| Platform       | Status      |
|-------------|----------------|-------------|
| appveyor    | Windows        |[![Build status](https://ci.appveyor.com/api/projects/status/84djajia77jann58?svg=true)](https://ci.appveyor.com/project/linuxchata/review-app/branch/master) [![BCH compliance](https://bettercodehub.com/edge/badge/linuxchata/review-app?branch=master)](https://bettercodehub.com/) |

## Technologies and frameworks
* ASP.NET Core 2.0
    * Autofac
    * nlog
    * Swagger
    * xunit / moq
* MongoDB
* Azure Service Bus
* Application Insights
* ReactJS / React Router
* TypeScript
* Mobx
* Axios
* Webpack

## Docker
Public repository:
https://hub.docker.com/r/linuxchata

Pull image:
`docker pull linuxchata/reviewapp-webapi:1.0.0`

Run image:
`docker run -p 5000:80 linuxchata/reviewapp-webapi:1.0.0`
