using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace HelpDesk.MVC.Service
{
    public class UploadFileRepository : IUploadFileRepository
    {
        private readonly IHostingEnvironment _environment;
        public UploadFileRepository(IHostingEnvironment environment)
        {
            this._environment = environment;
        }
        public string UplaodFile(IFormFile file, string innerPath)
        {
            string path_root = _environment.WebRootPath;
            string newFileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
            string path_to_file = path_root + innerPath + newFileName;
            using (var stream = new FileStream(path_to_file, FileMode.Create))
            {
                file.CopyTo(stream);
                
            }
            return newFileName;
        }
    }
}
