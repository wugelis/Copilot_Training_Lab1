using EasyArchitectCore.Core;
using Microsoft.AspNetCore.Http;

namespace EasyArchitectV2Lab1.AuthExtensions1.Extensions
{
    /// <summary>
    /// URI �X�i�A�ȹ�@
    /// </summary>
    public class UriExtensions : IUriExtensions
    {
        /// <summary>
        /// ���o����URI
        /// </summary>
        /// <param name="httpRequest">HTTP�ШD����</param>
        /// <returns>����URI�r��</returns>
        public string GetAbsoluteUri(HttpRequest httpRequest)
        {
            return httpRequest.Scheme + "://" + 
                   httpRequest.Host.ToUriComponent() + 
                   httpRequest.PathBase.ToUriComponent() + 
                   httpRequest.Path.ToUriComponent() + 
                   httpRequest.QueryString.ToUriComponent();
        }

        /// <summary>
        /// ���o�ۭq���|������URI
        /// </summary>
        /// <param name="httpRequest">HTTP�ШD����</param>
        /// <param name="customPathUri">�ۭq���|</param>
        /// <returns>����URI�r��</returns>
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