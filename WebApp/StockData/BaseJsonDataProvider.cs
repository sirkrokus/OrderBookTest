using System;
using log4net;
using RestSharp;

namespace WebApp.StockData
{
    public class BaseJsonDataProvider
    {
        private ILog log = LogManager.GetLogger(typeof(BaseJsonDataProvider));

        private string apiUrl;
        private IRestClient restClient;
        
        public BaseJsonDataProvider(string apiUrl)
        {
            this.apiUrl = apiUrl;
            restClient = new RestClient(apiUrl);
            restClient.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            restClient.Timeout = -1;
            restClient.ReadWriteTimeout = -1;
            restClient.ConfigureWebRequest((r) =>
            {
                r.ServicePoint.Expect100Continue = false;
                r.KeepAlive = true;
            });
        }

        public string ApiUrl => apiUrl;
        
        protected IRestClient RestClient => restClient;

        protected IRestResponse Invoke(IRestRequest request)
        {
            long startTicks = DateTime.Now.Ticks;
            log.Info($">> send {request.Method} to {restClient.BaseUrl}{request.Resource}");
            request.Parameters.ForEach(
                    param =>
                    {
                        log.Info($"[{param.Type}] {param.Name}={param.Value}");
                    }
            );
            IRestResponse response = restClient.Execute(request);
            CheckResponse(request, response, startTicks);
            return response;
        }

        protected void CheckResponse(IRestRequest request, IRestResponse response, long startTimeTicks)
        {
            long durMs = (DateTime.Now.Ticks - startTimeTicks) / 10000;
            log.Info($"<< response on {request.Method} /{request.Resource}, [{response.StatusCode}] [Duration: {durMs} ms]");

            if (response.IsSuccessful)
            {
                return;
            }
            throw new Exception("ERROR: "+response.Content);
        }

    }
    
}