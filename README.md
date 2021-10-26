**Bridge.Unique.Profile - (SSO) Single Sign-On - Microservice**
* It's a basic example of a microservice in DDD, several functionalities were set aside.
* Google, Facebook and Apple logins implemented.
* This code repository is for portfolio purposes only, not meant to work for any organization.

**This microservice runs on Linux or Windows.**
* All its dependencies are available at nuget.org or at feedz.io (repository bellow).
* The communication and wrapper packages are available at https://f.feedz.io/bridge-core/bridge-nuget/nuget/index.json
* Use the file .Dockerfile to publish
* Use the file pipeline.yml as reference for DevOps (Bitbucket in this case)

| Library                                | Description |
|----------------------------------------|-------------|
|**Bridge.Unique.Profile.API**           | The API endpoint |
|**Bridge.Unique.Profile.Communication** | Library with the common communication models (income and outcome or request and results), resources, validators, enums. Anything that is used inside the microservice and are needed in its Wrapper. |
|**Bridge.Unique.Profile.Domain**        | The domain of the microservice, contracts (interfaces and abstractions), models, validations of models, etc. |
|**Bridge.Unique.Profile.IOC**           | The Inversion of Control - Dependency Injection Container. |
|**Bridge.Unique.Profile.Postgres**      | The repository for PostgreSQL - With EntityFramework Code First |
|**Bridge.Unique.Profile.Redis**         | The repository for Redis (Cache) |
|**Bridge.Unique.Profile.System**        | Commons for this microservice alone. Enums, Helpers, Models, Resources, Settings, etc. |
|**Bridge.Unique.Profile.Wrapper**       | Wrapper to make easier to integrate this microservice in other services. |

**DDD structure:**

| Folder | Description |
|--------|-------------|
| Application | APIs endpoints |
| Domain | The microservice domain, rules, contracts, etc. |
| Docker | Dockerfile, DevOps file, certificate, nuget.config, etc. |
| Crosscutting | Libraries that can be used in any layer of the microservice |
| DependencyInjection | Inversion of Control Container |
| Repositories | Repositories and external services (Databases, Caches, Search engines, etc.) |
| Wrapper | Wrapper Library to integration within other services |

Cloud Technologies used:
* AWS ECR - Elastic Container Registry
* AWS ECS - Elastic Container Service
* AWS Fargate - Container Orchestrator
* AWS CloudWatch - Logs and auditing and insights
* AWS ELB - Elastic Load Balancing (Network and Application)
* AWS Elasticache (Redis)
* AWS RDS - Relational Database Service
* AWS Aurora
* AWS WAF - Web Application Firewall
* AWS API Gateway
* AWS ACM - Certificate Manager
* AWS S3 - Simple Storage Service
* AWS SNS - Simple Notification Service
* AWS Lambda
* AWS Auto Scaling

Other Technologies used:
* JWT - Json Web Token
* Serilog
* ReDoc

**NOTE:** Why not just use AWS Cognito or AWS Single Sign-On or Azure Active Directory B2C for example?
Due to the necessary customizations, this microservice could use any of these cloud services, as a middleware service or wrapper, or even implement it all by itself. As we needed major customizations to serve each of our customers, I chose to implement our own Single Sign-On microservice, as no cloud service would meet our needs.
