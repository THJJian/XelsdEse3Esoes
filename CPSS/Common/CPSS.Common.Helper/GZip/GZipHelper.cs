using System.IO;
using System.IO.Compression;

namespace CPSS.Common.Helper.GZip
{
    /// <summary>
    /// GZipHelper压缩/解压缩帮助类
    /// </summary>
    public class GZipHelper
    {
        /// <summary>
        ///     压缩数据，并返回压缩后的数据
        /// </summary>
        /// <param name="originalDataBytes">需要压缩的数据</param>
        /// <returns></returns>
        public static byte[] GetCompressedDataBytes(byte[] originalDataBytes)
        {
            using (var compressStream = new MemoryStream())
            {
                using (var gzipStream = new GZipStream(compressStream, CompressionMode.Compress))
                {
                    gzipStream.Write(originalDataBytes, 0, originalDataBytes.Length);
                    var compressedData = compressStream.ToArray();
                    return compressedData;
                }
            }
        }

        /// <summary>
        ///     解压数据，并返回解压后的数据
        /// </summary>
        /// <param name="compressDataBytes"></param>
        /// <returns></returns>
        public static byte[] GetDeCompressedDataBytes(byte[] compressDataBytes)
        {
            using (var compressStream = new MemoryStream(compressDataBytes))
            {
                using (var gzipStream = new GZipStream(compressStream, CompressionMode.Decompress))
                {
                    var buffer = new byte[1024];
                    using (var decompressStream = new MemoryStream())
                    {
                        var readLength = gzipStream.Read(buffer, 0, buffer.Length);
                        while (readLength > 0)
                        {
                            decompressStream.Write(buffer, 0, buffer.Length);
                            readLength = gzipStream.Read(buffer, 0, buffer.Length);
                        }
                        return decompressStream.ToArray();
                    }
                }
            }
        }
    }
}