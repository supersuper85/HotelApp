using Moq;
using Moq.Protected;
using System.Net;

namespace HotelApp.UnitTests.ServicesTests.ServiceHelpers
{
    public class HttpClientConfiguration
    {
        public HttpClient GetHttpClient()
        {
            var outputResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{ ""id"": 1, ""entityId"": 1}"),
            };

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(outputResponse)
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object);
            return httpClient;
        }
    }
}
