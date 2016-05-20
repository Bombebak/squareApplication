using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SquareApplication.Models;

namespace SquareApplication.Controllers
{
    public class SetController : Controller
    {
        private SquaresEntities db = new SquaresEntities();
        // GET: Set
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Set/CreateCategory
        [Authorize(Roles = "Designer")]
        [HttpGet]
        public ActionResult CreateSet()
        {
            var username = User.Identity.Name;
            var user = db.Users.SingleOrDefault(u => u.name == username);
            var viewModel = new CreateSetViewModel()
            {
                UserId = user.user_id
            };
            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateSet", viewModel);
            }
            return PartialView("_CreateSet", viewModel);
        }

        [Authorize(Roles = "Designer")]
        [HttpPost]
        public ActionResult CreateSet(CreateSetViewModel viewModel, IEnumerable<HttpPostedFileBase> files)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (files.Count() == 0 || files.FirstOrDefault() == null)
            {
                return View();
            }

            var modelSet = new Set()
            {
                name = viewModel.SetTitle,
                price = viewModel.SetPrice,
                upload_date = DateTime.Now,
                user_id = viewModel.UserId
            };
            db.Sets.Add(modelSet);
            foreach (var file in files)
            {
                var supportedImages = new[] { ".jpg", ".jpeg", ".png"};

                if (file.ContentLength == 0) continue;
                var fileName = Guid.NewGuid().ToString();
                var extension = Path.GetExtension(file.FileName).ToLower();

                if (supportedImages.Contains(extension))
                {
                    using (var img = Image.FromStream(file.InputStream))
                    {
                        viewModel.TileUrl = String.Format("/Assets/SetUploads/{0}/{1}{2}{3}", viewModel.UserId,
                            viewModel.SetId, fileName, extension);

                        SaveToFolder(img, fileName, extension, new Size(200, 200), viewModel.TileUrl);

                        var modelTile = new Tile()
                        {
                            set_id = modelSet.set_id,
                            url = viewModel.TileUrl
                        };
                        db.Tiles.Add(modelTile);
                        try
                        {
                            db.SaveChanges();
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }
                }
                
                return RedirectToAction("UserProfile", "Account");

            }
            return PartialView("_CreateSet");
        }

        public ActionResult Details(int setId)
        {
            return View();
        }

        private void SaveToFolder(Image img, string fileName, string extension, Size newSize, string pathToSave)
        {
            // Get new resolution
            Size imgSize = NewImageSize(img.Size, newSize);
            using (Image newImg = new Bitmap(img, imgSize.Width, imgSize.Height))
            {
                newImg.Save(Server.MapPath(pathToSave), img.RawFormat);
            }
        }

        public Size NewImageSize(Size imageSize, Size newSize)
        {
            Size finalSize;
            double tempval;
            if (imageSize.Height > newSize.Height || imageSize.Width > newSize.Width)
            {
                if (imageSize.Height > imageSize.Width)
                    tempval = newSize.Height / (imageSize.Height * 1.0);
                else
                    tempval = newSize.Width / (imageSize.Width * 1.0);

                finalSize = new Size((int)(tempval * imageSize.Width), (int)(tempval * imageSize.Height));
            }
            else
                finalSize = imageSize; // video is already small size

            return finalSize;
        }
    }
}