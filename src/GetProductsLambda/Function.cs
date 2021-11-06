using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using LambdaLogger;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ProductCatalogue.Application.Queries;
using ProductCatalogue.Application.Setup;
using LambdaLogger.Setup;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace GetProducts
{
    public class Function
    {
        private readonly IServiceCollection _serviceCollection;
        private readonly ServiceProvider _serviceProvider;
        private readonly Guid _tenantId;
        private readonly Lazy<IMediator> _mediatr;

        public Function()
        {
            this._serviceCollection = new ServiceCollection()
                .AddApplicationServices()
                .AddLoggingService();
            this._serviceProvider = this._serviceCollection.BuildServiceProvider();

            this._mediatr = new Lazy<IMediator>(() => this._serviceProvider.GetRequiredService<IMediator>());

            // JUST FOR TESTING, forces the tenant ID to be a known one so the user doesn't have to remember it
            this._tenantId = Guid.Parse("743872ea-7e68-421b-9f98-e09f35d76117");
        }

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            // performs a query for all products for a tenant (tenant id would be from a parameter in real life).
            var logger = this._serviceProvider.GetRequiredService<ILogger>();
            logger.SetLoggerContext(context.Logger);
            logger.LogInfo($"Fetching all products for tenant: {this._tenantId}");

            try
            {
                var query = new GetProductsQuery(this._tenantId);
                var queryResponse = await this._mediatr.Value.Send(query);

                logger.LogInfo($"Returning {queryResponse.Count()} records");

                // return result
                return new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Body = JsonConvert.SerializeObject(queryResponse)
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
