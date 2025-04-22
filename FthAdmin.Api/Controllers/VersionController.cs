#region code: fatih.unal date: 2025-04-22
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System;
using System.Collections.Generic;

namespace FthAdmin.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VersionController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var baseDir = Directory.GetCurrentDirectory();
            var layers = new List<string>
            {
                "FthAdmin.Api",
                "FthAdmin.Application",
                "FthAdmin.Core",
                "FthAdmin.Domain",
                "FthAdmin.Infrastructure",
                "FthAdmin.Persistence"
            };
            var result = new List<object>();
            foreach (var layer in layers)
            {
                var file = Path.Combine(baseDir, layer, "Version.txt");
                string version = null;
                string date = null;
                if (System.IO.File.Exists(file))
                {
                    version = System.IO.File.ReadAllText(file).Trim();
                    var info = new FileInfo(file);
                    date = info.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
                }
                result.Add(new { katman = layer, versiyon_tarihi = date, versiyon_no = version });
            }
            return Ok(result);
        }
    }
}
#endregion
