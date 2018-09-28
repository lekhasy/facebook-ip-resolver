using System;
using System.Text;
using System.IO;

namespace Codes
{
    class HostFile
    {
        public static string FilePath = @"C:\Windows\System32\drivers\etc\hosts";

        public static FileInfo File = new FileInfo(FilePath);

        /// <summary>
        /// đọc những dòng của file hosts mà không chứa teen host
        /// </summary>
        /// <returns></returns>
        public static string ReadDataWithoughtanstring(string host)
        {
            StreamReader reader = new StreamReader(HostFile.FilePath);
            string data = string.Empty;
            while (true)
            {
                string buff = reader.ReadLine();
                if (buff == null) // đọc hết file
                {
                    break;
                }
                if (!buff.Contains(host)) // nếu dòng này không chứa host
                {
                    data += buff + Environment.NewLine;
                }
            }
            reader.Close();
            return data;
        }

        /// <summary>
        /// ghi toàn bộ chuỗi data xuống file hosts, ghi đè lên tất cả
        /// </summary>
        /// <param name="data">chuỗi cần ghi</param>
        public static void WriteHostData(string data)
        {
            FileStream stream = File.OpenWrite();
            stream.Write(Encoding.ASCII.GetBytes(data), 0, data.Length);
            stream.Close();
        }
    }
}
