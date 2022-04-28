using Google.Cloud.Vision.V1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FaceDetect : ControllerBase
    {

        private readonly ILogger<FaceDetect> _logger;

        public FaceDetect(ILogger<FaceDetect> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string GetFaceCoord(string url)
        {
            string poly = String.Empty;
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "key.json");
            ImageAnnotatorClient client = ImageAnnotatorClient.Create();
            IReadOnlyList<FaceAnnotation> result = client.DetectFaces(Image.FromUri(url));
            foreach (FaceAnnotation face in result)
            {
                poly = string.Join(" - ", face.BoundingPoly.Vertices.Select(v => $"({v.X}, {v.Y})"));

            }
            return poly;
        }
    }
}