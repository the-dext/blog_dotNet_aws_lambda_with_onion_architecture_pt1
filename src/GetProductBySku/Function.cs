using System;
using System.Net;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Ardalis.GuardClauses;
using LambdaLogger;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ProductCatalogue.Application.Queries;
using ProductCatalogue.Application.Setup;
using LambdaLogger.Setup;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace GetProductBySku
{
    public class Function
    {
        private readonly IServiceCollection _serviceCollection;
        private readonly ServiceProvider _serviceProvider;
        private readonly Guid _tenantId;
        private readonly Lazy<IMediator> _mediatr;

        // example payload { "tenantId": "743872ea-7e68-421b-9f98-e09f35d76117", "sku": "HOU/IN/82" }

        public Function()
        {
            this._serviceCollection = new ServiceCollection()
                .AddApplicationServices()
                .AddLoggingService();
            this._serviceProvider =  this._serviceCollection.BuildServiceProvider();

            this._mediatr = new Lazy<IMediator>(() => this._serviceProvider.GetRequiredService<IMediator>());

            // JUST FOR TESTING, forces the tenant ID to be a known one so the user doesn't have to remember it
            this._tenantId = Guid.Parse("743872ea-7e68-421b-9f98-e09f35d76117");
        }

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        /// was..public async Task<ProductDto> FunctionHandler(GetProductBySkuQuery query, ILambdaContext context)
        public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var logger = this._serviceProvider.GetRequiredService<ILogger>();
            logger.SetLoggerContext(context.Logger);
            logger.LogInfo($"Fetching product by SKU");

            try
            {
                // just for testing, the tenant id would be a guid in a real app
                Guard.Against.Null(request, nameof(request));
                var query = JsonConvert.DeserializeObject<GetProductBySkuQuery>(request.Body);

                // fire command (tenantId should come from the data passed but this is for testing)
                query.TenantId = this._tenantId;

                logger.LogInfo($"Fetching product by SKU, tenant: {this._tenantId}, sku: {query.Sku}");
                var queryResult = await this._mediatr.Value.Send(new GetProductBySkuQuery(this._tenantId, query.Sku));

                // return result
                return new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Body = JsonConvert.SerializeObject(queryResult)
                };
            }
            catch (Exception ex)
            {
                logger.LogError($"exception; {ex.Message}");
                return new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                };
            }
        }
    }
}
