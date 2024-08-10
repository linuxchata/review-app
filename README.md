# Review application
A simple review system for different subjects (e.g. doctors, clinics, companies, services)

## Build Status
| Build server | Target       | Platform    | Status      |
|--------------|--------------|-------------|-------------|
| AppVeyor     | CI           | Windows     |[![Build status](https://ci.appveyor.com/api/projects/status/84djajia77jann58?svg=true)](https://ci.appveyor.com/project/linuxchata/review-app/branch/master) |
| Azure DevOps | CI           | Windows     | [![Build status](https://linuxchata.visualstudio.com/review-app/_apis/build/status/ReviewAppLocation%20-%20CI)](https://linuxchata.visualstudio.com/review-app/_build/latest?definitionId=11)
| Azure DevOps | Docker       | Linux       | [![Build status](https://linuxchata.visualstudio.com/review-app/_apis/build/status/review-app-api-docker)](https://linuxchata.visualstudio.com/review-app/_build/latest?definitionId=10) |
| SonarCloud   | Code quality |             | [![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=linuxchata_review-app&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=linuxchata_review-app) |

## Technologies and frameworks
* ASP.NET Core 3.0
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
docker-compose pull
docker-compose up
```

## Roadmap
* Merge unit test projects :heavy_check_mark:
* Docker build on Azure DevOps with pushing images to Docker Hub :heavy_check_mark:
* Add health check monitoring :heavy_check_mark:
* HTTPS :heavy_check_mark:
* Implement using RabbitMQ instead of ServiceBus
* Add Sonar code analysis :heavy_check_mark:
