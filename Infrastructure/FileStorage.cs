using System;
using System.IO;
using System.Threading.Tasks;
using Domain.Abstractions;

namespace Infrastructure
{
    public class FileStorage : IFileStorage
    {
        private readonly string _dir;

        public FileStorage()
        {
            _dir = Path.Combine(Directory.GetCurrentDirectory(), "imgs");

            if (!Directory.Exists(_dir)) 
            {
                Directory.CreateDirectory(_dir);
            } 
        }

        public async Task<string> SaveFile(byte[] bytes, string extension)
        {
            var fileName = Guid.NewGuid().ToString("N") + "." + extension;

            await File.WriteAllBytesAsync(Path.Combine(_dir, fileName), bytes);

            return fileName;
        }

        public async Task<byte[]> GetAsync(string filename)
        {
            try {
                var bytes = await File.ReadAllBytesAsync(Path.Combine(_dir, filename));

                return bytes;
            }
            catch {
                return new byte[0];
            }
        }
    }
}