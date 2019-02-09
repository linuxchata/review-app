# Review application
A simple review system for different subjects (e.g. doctors, clinics)

## Build Status
| Build server | Target       | Platform    | Status      |
|--------------|--------------|-------------|-------------|
| AppVeyor     | CI           | Windows     |[![Build status](https://ci.appveyor.com/api/projects/status/84djajia77jann58?svg=true)](https://ci.appveyor.com/project/linuxchata/review-app/branch/master) |
| Azure DevOps | CI           | Windows     | [![Build status](https://linuxchata.visualstudio.com/review-app/_apis/build/status/ReviewAppLocation%20-%20CI)](https://linuxchata.visualstudio.com/review-app/_build/latest?definitionId=11)
| Azure DevOps | Docker       | Linux       | [![Build status](https://linuxchata.visualstudio.com/review-app/_apis/build/status/review-app-api-docker)](https://linuxchata.visualstudio.com/review-app/_build/latest?definitionId=10) |
| Better Code  | Code quality |             | [![BCH compliance](https://bettercodehub.com/edge/badge/linuxchata/review-app?branch=master)](https://bettercodehub.com/) |

## Technologies and frameworks
* ASP.NET Core 2.2
    * Autofac
    * nlog
    * Swagger
    * xunit / moq
* MongoDB
* Azure Service Bus
* Application Insights
* Docker (Linux containers only)
* ReactJS / React Router
* TypeScript
* Mobx
* Axios
* Webpack

## Docker support
Run application on Docker (Linux containers only)
```
docker-compose up
```

## Roadmap
* Docker build on Azure DevOps with pushing images to docker hub
* Add health check monitoring
* Implement using RabbitMQ instead of ServiceBus
* Add Sonar code analysis
