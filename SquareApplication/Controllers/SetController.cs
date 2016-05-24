using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            //if (!ModelState.IsValid)
            //{
            //    return View();
            //}
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
                var supportedImages = new[] { ".jpg", ".jpeg", ".png" };

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
                        //Adding tiles to the set
                        var modelTile = new Tile()
                        {
                            set_id = modelSet.set_id,
                            url = viewModel.TileUrl
                        };
                        db.Tiles.Add(modelTile);
                        //Adding tags to the set
                        string[] enteredTagsList = viewModel.TagTitle.Split(',');
                        foreach (string item in enteredTagsList)
                        {
                            var modelTag = new Tag()
                            {
                                name = item
                            };
                            db.Tags.Add(modelTag);
                            var modelSetTag = new Set_Tag()
                            {
                                set_id = modelSet.set_id,
                                tag_id = modelTag.tag_id
                            };
                            db.Set_Tag.Add(modelSetTag);
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
                }

                return RedirectToAction("Details", "Set", new { setId = modelSet.set_id });

            }
            return PartialView("_CreateSet");
        }

        public ActionResult Details(int setId)
        {
            
            var viewModel = (from s in db.Sets

                             where s.set_id == setId
                             select new DetailsSetViewModel()
                             {
                                 SetId = setId,
                                 SetCost = (int)s.price,
                                 UploadedTime = (DateTime)s.upload_date,
                                 UserId = (int)s.user_id,
                                 SetTitle = s.name
                             }).SingleOrDefault();

            if (viewModel == null)
            {
                return HttpNotFound();
            }

            var tags = from st in db.Set_Tag
                       join t in db.Tags
                           on st.tag_id equals t.tag_id
                           where st.set_id == setId
                       select new TagsViewModel()
                       {
                           SetId = setId,
                           TagId = t.tag_id,
                           TagName = t.name
                       };
            List<TagsViewModel> enteredTagsList = new List<TagsViewModel>();
            if (tags.Any())
            {
                foreach (var item in tags)
                {
                    enteredTagsList.Add(item);
                }
                viewModel.SetTagsList = enteredTagsList;
            }

            return View(viewModel);
        }

        [Authorize(Roles = "Designer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSet(EditSetViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var model = (from s in db.Sets
                             where s.set_id == viewModel.SetId
                             select s).SingleOrDefault();
                if (model != null)
                {
                    model.price = viewModel.SetCost;
                    if (viewModel.TagTitle != null)
                    {
                        string[] enteredTagsList = viewModel.TagTitle.Split(',');
                        foreach (string item in enteredTagsList)
                        {
                            var modelTag = new Tag()
                            {
                                name = item
                            };
                            db.Tags.Add(modelTag);
                            var modelSetTag = new Set_Tag()
                            {
                                set_id = viewModel.SetId,
                                tag_id = modelTag.tag_id
                            };
                            db.Set_Tag.Add(modelSetTag);
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
                   

                    try
                    {
                        db.Entry(model).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    TempData["TempSetMsg"] = "<div class='alert alert-success'><p>Update succesfull" + "</p>" +
                                                        "<p><span class='glyphicon glyphicon-ok'></span> You have succesfully updated your set. </p></div>";

                    return RedirectToAction("Details", new { setId = viewModel.SetId });

                }
                TempData["TempSetMsg"] = "<div class='alert alert-danger'><p>Update succesfull" + "</p>" +
                                                        "<p><span class='glyphicon glyphicon-remove'></span> An error has occured when trying to update your set. </p></div>";
            }
            return RedirectToAction("UserProfile", "Account");
        }
        [Authorize(Roles = "Designer")]
        [HttpGet]
        public ActionResult DeleteSet(int setId)
        {
            var viewModel = (from s in db.Sets
                             where s.set_id == setId
                             select new DeleteSetViewModel()
                             {
                                 SetId = setId,
                                 UserId = (int)s.user_id,
                                 SetTitle = s.name,
                                 TileCount = s.Tiles.Count()
                             }).SingleOrDefault();
            if (viewModel != null)
            {
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_DeleteSet", viewModel);
                }
                return View("DeleteSet", viewModel);
            }
            TempData["TempUserMsg"] = "<div class='alert alert-danger'><p>Delete unsuccesfull" + "</p>" +
                                      "<p><span class='glyphicon glyphicon-remove'></span> A problem occured when trying to delete your set.</p>" +
                                      "<p>Try again later or contact the adminstrator.</p></div>";

            return RedirectToAction("UserProfile", "Account");
        }

        [HttpPost]
        public ActionResult DeleteSet(DeleteSetViewModel viewModel)
        {
            if (viewModel != null)
            {
                var model = (from s in db.Sets
                             where s.set_id == viewModel.SetId
                             select s).SingleOrDefault();

                if (model == null)
                {
                    TempData["TempUserMsg"] = "<div class='alert alert-danger'><p>Delete unsuccesfull" + "</p>" +
                                      "<p><span class='glyphicon glyphicon-remove'></span> A problem occured when trying to delete your set.</p>" +

                                      "<p>Try again later or contact the adminstrator.</p></div>";
                    return RedirectToAction("UserProfile", "Account");
                }

                db.Sets.Remove(model);

                var tiles = from t in db.Tiles
                            where t.set_id == viewModel.SetId
                            select t;

                foreach (var item in tiles)
                {
                    string imagePath = Request.MapPath(item.url);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                    db.Tiles.Remove(item);
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                }

                TempData["TempUserMsg"] = "<div class='alert alert-success'><p>Delete succesfull" + "</p>" +
                                                       "<p><span class='glyphicon glyphicon-ok'></span> You have succesfully deleted your set. </p></div>";

                return RedirectToAction("UserProfile", "Account");
            }
            return RedirectToAction("UserProfile", "Account");
        }

        [Authorize(Roles = "Designer")]
        [HttpGet]
        public ActionResult DeleteTag(int tagId, int setId)
        {
            var model = (from t in db.Tags
                where t.tag_id == tagId
                select t).SingleOrDefault();
            if (model != null)
            {
                var setTag = (from st in db.Set_Tag
                    where st.tag_id == tagId
                    select st).FirstOrDefault();

                db.Set_Tag.Remove(setTag);
                db.SaveChanges();
                db.Tags.Remove(model);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
                TempData["TempTagMsg"] = "<div class='alert alert-success'><p>Delete succesfull" + "</p>" +
                                                       "<p><span class='glyphicon glyphicon-ok'></span> You have succesfully deleted a tag. </p></div>";

                return RedirectToAction("Details", new { setId });
            }
            TempData["TempTagMsg"] = "<div class='alert alert-danger'><p>Delete unsuccesfull" + "</p>" +
                                                       "<p><span class='glyphicon glyphicon-remove'></span> A error has occured when tryin to delete a tag. </p></div>";
            return RedirectToAction("Details", new{ setId });
        }

        #region helperMethods
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
        #endregion
    }
}