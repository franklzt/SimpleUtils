using System.IO;
using System;
using UnityEngine;

namespace GameDataTable
{
    public class WriteCodeToFile
    {
        public void WriteToFile(string fileName, string script)
        {
            FileStream fileStream = File.Create(fileName);
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(script);
            fileStream.Write(byteArray, 0, byteArray.Length);
            fileStream.Close();
        }

        public void WriteFile(string filepath, byte[] allContent,int byteCount = 512)
        {         
            FileStream fs = new FileStream(filepath, FileMode.Append, FileAccess.Write);
            int intSize = byteCount;
            if (allContent.Length < byteCount)
            {
                intSize = allContent.Length;
                fs.Write(allContent, 0, intSize);
                fs.Close();
                return;
            }

            byte[] btContent = new byte[allContent.Length];
            int offset = 0;
            int total = allContent.Length;
            bool isFinish = false;
            while (offset < total)
            {
                int temp = offset + byteCount;
                if (temp > total)
                {
                    intSize = total - offset;
                    isFinish = true;
                }

                Array.Copy(allContent, offset, btContent, offset, byteCount);
                fs.Write(btContent, offset, intSize);
                if (isFinish)
                {
                    fs.Close();
                    break;
                }
                offset += byteCount;
            }
        }

        public byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

        public Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }
    }
}


