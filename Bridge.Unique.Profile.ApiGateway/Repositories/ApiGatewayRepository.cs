using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon;
using Amazon.APIGateway;
using Amazon.APIGateway.Model;
using Brx.Unique.Profile.Domain.Repositories.Contracts;
using Brx.Unique.Profile.System.Settings;

namespace Brx.Unique.Profile.ApiGateway.Repositories
{
    public class ApiGatewayRepository : IApiGatewayRepository
    {
        private readonly AppSettings _appSettings;
        private readonly IAmazonAPIGateway _amazonApiGatewayClient;

        public ApiGatewayRepository(AppSettings appSettings)
        {
            _appSettings = appSettings;
            _amazonApiGatewayClient = new AmazonAPIGatewayClient(
                _appSettings.AmazonGlobal.AccessKey,
                _appSettings.AmazonGlobal.SecretKey,
                RegionEndpoint.GetBySystemName(_appSettings.AmazonGlobal.Region));
        }
        
        public async Task<Domain.Models.ApiGateway> CreateHom(string clientName, string apiName)
        {
            var apiKeyResult = await CreateApiKey(clientName, apiName);

            var usagePlanKeyResult = await CreateUsagePlanKey(apiKeyResult.Id);
            
            // Chave da api
            return new Domain.Models.ApiGateway
            {
                Id = apiKeyResult.Id,
                ApiKey = apiKeyResult.Value
            };
        }
        
        public async Task<Domain.Models.ApiGateway> CreateProd(string clientName, string apiName)
        {
            var apiKeyResult = await CreateApiKey(clientName, apiName);

            var usagePlanResult = await CreateUsagePlan(clientName, apiKeyResult.Id);

            // Chave da api
            return new Domain.Models.ApiGateway
            {
                Id = apiKeyResult.Id,
                ApiKey = apiKeyResult.Value
            };
        }
        
        public async Task<string> Get(string apiKeyId)
        {
            try
            {
                var apiKeyResult = await _amazonApiGatewayClient.GetApiKeyAsync(new GetApiKeyRequest()
                {
                    ApiKey = apiKeyId,
                    IncludeValue = true
                });

                // Chave da api
                return apiKeyResult != null ? apiKeyResult.Value : "";
            }
            catch (Exception)
            {
                return "";
            }
        }
        
        public async Task<bool> Delete(string apiKeyId)
        {
            var apiKeyResult = await _amazonApiGatewayClient.DeleteApiKeyAsync(new DeleteApiKeyRequest()
            {
                ApiKey = apiKeyId
            });
            
            return true;
        }
        
        #region Private

        private async Task<CreateApiKeyResponse> CreateApiKey(string clientName, string apiName)
        {
            var suffix = _appSettings.Environment.Equals("Production") ? "-PROD" : "-HOM";
            
            var requestApiKey = new CreateApiKeyRequest()
            {
                Name = apiName.Trim().Replace(" ","_") + "_" + clientName.Trim().Replace(" ","_") + suffix,
                Description = "Api Key do cliente: " + clientName,
                Enabled = true,
                //Value = "",
                //GenerateDistinctId = false
            };

            var resultCreateApiKey = await _amazonApiGatewayClient.CreateApiKeyAsync(requestApiKey);
            return resultCreateApiKey;
        }
        private async Task<CreateUsagePlanKeyResponse> CreateUsagePlanKey(string apiKeyId)
        {
            // Em homologação é usado o mesmo plano de uso para todos os clientes,o BASIC-HOM (id = k2gl0b)
            var resultCreateUsagePlanKey = await
                _amazonApiGatewayClient.CreateUsagePlanKeyAsync(new CreateUsagePlanKeyRequest()
                {
                    KeyId = apiKeyId,
                    KeyType = "API_KEY",
                    UsagePlanId = "k2gl0b"
                });

            return resultCreateUsagePlanKey;
        }

        private async Task<CreateUsagePlanResponse> CreateUsagePlan(string clientName, string apiKeyId)
        {
            // Cria um plano de uso específico para o cliente
            var resultUsagePlan = await
                _amazonApiGatewayClient.CreateUsagePlanAsync(new CreateUsagePlanRequest()
                {
                    Name = clientName + "-PROD",
                    Description = "Plano de uso do cliente: " + clientName,
                    // Throttle = new ThrottleSettings()
                    // {
                    //     BurstLimit = 1000,
                    //     RateLimit = 1000
                    // },
                    ApiStages = new List<ApiStage>()
                    {
                        new ApiStage()
                        {
                            ApiId = apiKeyId,
                            Stage = "prod"
                        }
                    }
                });
            return resultUsagePlan;
        }
        
        
        // private void CreateAwsTest(string clientName)
        // {
        //     #region "AWS Create ApiKey"
        //
        //     if (_appSettings.Environment.Equals("Homolog") || _appSettings.Environment.Equals("Production"))
        //     {
        //         // Pangeia_Condor-HOM
        //         // Pangeia_Delivery_Condor-HOM
        //         // Pangeia_Portal_API_Condor-HOM
        //
        //         var requestApiKey = new CreateApiKeyRequest()
        //         {
        //             Name = "Pangeia_" + clientName + "-HOM",
        //             Description = "Api Key do cliente: " + clientName,
        //             Enabled = true,
        //             //Value = "",
        //             //GenerateDistinctId = false
        //         };
        //
        //         var resultCreateApiKey = Task.Run(() => _amazonApiGatewayClient.CreateApiKeyAsync(requestApiKey)).Result;
        //         var newApiKeyId = resultCreateApiKey.Id;
        //         
        //         // Cadastrar na tabela ApiClient
        //         var newApiKey = resultCreateApiKey.Value;
        //
        //         // var resultUpdateUsagePlan = Task.Run(() => 
        //         //     _amazonApiGatewayClient.UpdateUsagePlanAsync(new UpdateUsagePlanRequest()
        //         //     {
        //         //         UsagePlanId = "k2gl0b", // k2gl0b = Basic-HOM
        //         //         PatchOperations = new List<PatchOperation>()
        //         //         {
        //         //             new PatchOperation()
        //         //             {
        //         //                 Op = Op.Add,
        //         //                 Path = "/apiStages",
        //         //                 Value = newApiKeyId + ":hom"
        //         //             }
        //         //         }
        //         //     })).Result;
        //
        //         if (_appSettings.Environment.Equals("Homolog"))
        //         {
        //             // Em homologação é usado o mesmo plano de uso para todos os clientes,o BASIC-HOM (id = k2gl0b)
        //             var resultCreateUsagePlanKey = Task.Run(() =>
        //                 _amazonApiGatewayClient.CreateUsagePlanKeyAsync(new CreateUsagePlanKeyRequest()
        //                 {
        //                     KeyId = newApiKeyId,
        //                     KeyType = "API_KEY",
        //                     UsagePlanId = "k2gl0b"
        //                 }));
        //         }
        //         else if (_appSettings.Environment.Equals("Production"))
        //         {
        //             // Cria um plano de uso específico para o cliente
        //             var resultUsagePlan = Task.Run(() =>
        //                 _amazonApiGatewayClient.CreateUsagePlanAsync(new CreateUsagePlanRequest()
        //                 {
        //                     Name = clientName + "-PROD",
        //                     Description = "Plano de uso do cliente: " + clientName,
        //                     // Throttle = new ThrottleSettings()
        //                     // {
        //                     //     BurstLimit = 1000,
        //                     //     RateLimit = 1000
        //                     // },
        //                     ApiStages = new List<ApiStage>()
        //                     {
        //                         new ApiStage()
        //                         {
        //                             ApiId = newApiKeyId,
        //                             Stage = "prod"
        //                         }
        //                     }
        //                 })).Result;
        //             var newUsagePlanId = resultUsagePlan.Id;
        //         }
        //     }
        //
        //     #endregion
        // }
        
        #endregion
    }
}