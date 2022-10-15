using GroupDocs.Viewer;
using GroupDocs.Viewer.Options;
using Microsoft.AspNetCore.Mvc;
using PreviewDocument.Models;
using System.Diagnostics;

namespace PreviewDocument.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ViewDocument()
        {
            string fileName = Request.Form["fileToView"];
            string outputDirectory = ("OutPut/");
            string outputFilePath = Path.Combine(outputDirectory,"output.pdf");
            using (Viewer viewer=new Viewer("SourceDocuments/"+fileName))
            {
                PdfViewOptions options = new PdfViewOptions(outputFilePath);
                viewer.View(options);
            }
            var fileStream = new FileStream("OutPut/" + "output.pdf",
                FileMode.Open,
                FileAccess.Read
                );
            var fsResult = new FileStreamResult(fileStream, "application/pdf");
            return fsResult;
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}