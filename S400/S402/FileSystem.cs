using Microsoft.Extensions.FileProviders;
using System.Text;

namespace App
{
    public class FileSystem : IFileSystem
    {
        private readonly IFileProvider _fileProvider;
        public FileSystem(IFileProvider fileProvider) => _fileProvider = fileProvider;
        public void ShowStructure(Action<int, string> print)
        {
            int indent = -1;
            Print("");

            void Print(string subPath)
            {
                //增加层级
                indent++;
                //返回目录内容，则遍历所有子目录和文件
                foreach (var fileInfo in _fileProvider.GetDirectoryContents(subPath))
                {
                    print(indent, fileInfo.Name);
                    //判断是否为目录
                    if (fileInfo.IsDirectory)
                    {
                        Print($@"{subPath}\{fileInfo.Name}".TrimStart('\\'));
                    }
                }
                indent--;
            }
        }

        public async Task<string> ReadAllTextAsync(string path)
        {
            byte[] buffer;
            //读取文件的输出流
            using (var stream = _fileProvider.GetFileInfo(path).CreateReadStream())
            {
                buffer = new byte[stream.Length];
                //将输出流写入缓存中
                await stream.ReadAsync(buffer);
            }
            //将字节数组转化为字符串
            return Encoding.Default.GetString(buffer);
        }
    }
}