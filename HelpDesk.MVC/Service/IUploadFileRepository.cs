using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.MVC.Service
{
    public interface IUploadFileRepository
    {
        string UplaodFile(IFormFile file,string innerPath);
    }
}
