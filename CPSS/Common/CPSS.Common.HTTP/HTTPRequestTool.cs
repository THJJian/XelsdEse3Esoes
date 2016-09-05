using System;
using System.Configuration;
using System.IO;
using System.Net;
using CPSS.Common.Core.Helper.Config;
using CPSS.Common.Core.Helper.Extension;
using CPSS.Common.Core.Helper.GZip;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CPSS.Common.Core.HTTP
{
    /// <summary>
    /// 模拟HttpWebRequest请求
    /// </summary>
    public class HTTPRequestTool
    {
        
        private readonly string mUrl;

        public HTTPRequestTool(string url)
        {
            this.mUrl = url;
        }

        /// <summary>
        /// 创建HttpWebRequest对象
        /// </summary>
        /// <param name="contentLength">
        /// 获取或设置 Content-lengthHTTP 标头。
        /// 返回结果: 要发送到 Internet 资源的数据的字节数。 默认值为 -1，该值指示尚未设置该属性，并且没有要发送的请求数据。
        /// </param>
        /// <returns></returns>
        private HttpWebRequest CreateHttpWebRequest(int contentLength)
        {
            var request = (HttpWebRequest) WebRequest.Create(this.mUrl);
            request.Method = "Post";
            request.ContentType = "text/plain";
            request.Timeout = (int)TimeSpan.FromMinutes(6).TotalMilliseconds;
            request.ContentLength = contentLength;
            return request;
        }

        /// <summary>
        /// 读取服务端返回的数据
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private byte[] ReadData(WebResponse response)
        {
            using (var respondStream = response.GetResponseStream())
            {
                var buffer = new byte[1024];
                var result = new MemoryStream();
                while (true)
                {
                    if (respondStream == null)
                    {
                        continue;
                    }
                    var readLength = respondStream.Read(buffer, 0, buffer.Length);
                    if (readLength <= 0)
                        break;
                    result.Write(buffer, 0, readLength);
                }
                return result.ToArray();
            }
        }

        /// <summary>
        /// 发送数据并接收服务端返回的数据。
        /// 注：只有压缩请求数据包和解压请求返回的数据包
        /// </summary>
        /// <param name="originalDataBytes"></param>
        /// <returns></returns>
        public byte[] SendData(byte[] originalDataBytes)
        {
            var compressData = GZipHelper.GetCompressedDataBytes(originalDataBytes);
            var request = this.CreateHttpWebRequest(compressData.Length);
            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(compressData, 0, compressData.Length);
            }

            HttpWebResponse response;
            try
            {
                response = request.GetResponse() as HttpWebResponse;
            }
            catch (WebException exception)
            {
                response = exception.Response as HttpWebResponse;
            }
            var responseData = this.ReadData(response);
            var decompressData = GZipHelper.GetDeCompressedDataBytes(responseData);
            return decompressData;
        }

        /// <summary>
        /// 发送数据并接收服务端返回的数据。
        /// 注： 对请求数据包进行加密并压缩和对请求返回的数据包进行解压缩并解密
        /// </summary>
        /// <param name="jObject"></param>
        /// <param name="appSettingKey">取RAS非对称加密配置文件的密钥对。默认appsetting的key为"RSA.Default.Config.FilePath"</param>
        /// <returns></returns>
        public JObject SendData(JObject jObject, string appSettingKey = "RSA.Default.Config.FilePath")
        {
            #region 获取RSA非对称加密的配置文件

            var rsaDefaultConfigFilePath = ConfigurationManager.AppSettings[appSettingKey];
            if(string.IsNullOrEmpty(rsaDefaultConfigFilePath)) throw new Exception("RSA非对称加密的配置文件不存在。");
            var config = ConfigHelper.GetConfig<RSASecretConfig>(rsaDefaultConfigFilePath, 60);

            #endregion

            #region 请求数据包加密操作

            var dataPacketManager = new DataPacketManager(config);
            dataPacketManager.EncryptData(jObject);

            #endregion

            var buffer = jObject.ToString(Formatting.None).ToUTF8Bytes();
            var respond = this.SendData(buffer);

            #region 请求返回数据包解密操作

            var respondData = respond.ToUTF8String();
            var respondJObjectData = JObject.Parse(respondData);
            dataPacketManager.DecryptData(respondJObjectData);

            #endregion

            return respondJObjectData;
        } 
    }
}