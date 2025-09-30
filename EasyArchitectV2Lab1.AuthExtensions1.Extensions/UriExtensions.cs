using EasyArchitectCore.Core;
using Microsoft.AspNetCore.Http;

namespace EasyArchitectV2Lab1.AuthExtensions1.Extensions
{
    /// <summary>
    /// URI 擴展服務實作
    /// </summary>
    public class UriExtensions : IUriExtensions
    {
        /// <summary>
        /// 取得絕對URI
        /// </summary>
        /// <param name="httpRequest">HTTP請求物件</param>
        /// <returns>絕對URI字串</returns>
        public string GetAbsoluteUri(HttpRequest httpRequest)
        {
            return httpRequest.Scheme + "://" + 
                   httpRequest.Host.ToUriComponent() + 
                   httpRequest.PathBase.ToUriComponent() + 
                   httpRequest.Path.ToUriComponent() + 
                   httpRequest.QueryString.ToUriComponent();
        }

        /// <summary>
        /// 取得自訂路徑的絕對URI
        /// </summary>
        /// <param name="httpRequest">HTTP請求物件</param>
        /// <param name="customPathUri">自訂路徑</param>
        /// <returns>絕對URI字串</returns>
        public string GetAbsoluteUri(HttpRequest httpRequest, string customPathUri)
        {
            return httpRequest.Scheme + "://" + 
                   httpRequest.Host.ToUriComponent() + 
                   httpRequest.PathBase.ToUriComponent() + 
                   customPathUri + 
                   httpRequest.QueryString.ToUriComponent();
        }
    }
}