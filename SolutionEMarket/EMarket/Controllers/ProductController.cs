using EMarket.Middlewares;
using Microsoft.AspNetCore.Mvc;
using SolutionEMarket.Core.Application.Interfaces.Services;
using SolutionEMarket.Core.Application.ViewModels.Product;
using SolutionEMarket.Core.Application.ViewModels.User;

namespace EMarket.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly UserMiddleware _userMiddleware;

        public ProductController(IProductService service, ICategoryService categoryService, UserMiddleware userMiddleware)
        {
            _productService = service;
            _categoryService = categoryService;
            _userMiddleware = userMiddleware;
        }

        public async Task<IActionResult> Index()
        {
            if (!_userMiddleware.IsLogin())
            {
                return RedirectToRoute(new
                {
                    Controller = "Home",
                    Action = "Index"
                });
            }

            var productList = await _productService.GetViewModel();
            return View(productList);
        }

        #region Create Methods 

        public async Task<IActionResult> Create()
        {
            if (!_userMiddleware.IsLogin())
            {
                return RedirectToRoute(new
                {
                    Controller = "Home",
                    Action = "Index"
                });
            }

            ViewBag.Categories = await _categoryService.GetViewModel();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveProductViewModel createProduct)
        {
            if (!_userMiddleware.IsLogin())
            {
                return RedirectToRoute(new
                {
                    Controller = "Home",
                    Action = "Index"
                });
            }

            ViewBag.Categories = await _categoryService.GetViewModel();

            if (ModelState.IsValid)
            {
                SaveProductViewModel productvm = await _productService.CreateSaveViewModel(createProduct);
                if (productvm != null && productvm.Id != 0)
                {
                    string[] notNecesarialy = new string[4];
                    var urls = UploadFile(file: createProduct.File, id: productvm.Id, imageUrl: notNecesarialy);
                    productvm.ImagePath1 = urls[0]; 
                    productvm.ImagePath2 = urls[1] == null ? "" : urls[1]; 
                    productvm.ImagePath3 = urls[2] == null ? "" : urls[2];
                    productvm.ImagePath4 = urls[3] == null ? "" : urls[3];

                    await _productService.UpdateSaveViewModel(productvm);
                }

                return RedirectToRoute(new
                {
                    Controller = "Product",
                    Action = "Index"
                });
            }

            return View(createProduct);
        }

        #endregion 

        #region Update Methods

        public async Task<IActionResult> Update(int id)
        {
            if (!_userMiddleware.IsLogin())
            {
                return RedirectToRoute(new
                {
                    Controller = "Home",
                    Action = "Index"
                });
            }

            ViewBag.Categories = await _categoryService.GetViewModel();
            var updateProduct = await _productService.GetSpecificSaveViewModel(id);
            return View(updateProduct);
        }

        [HttpPost]
        public async Task<IActionResult> Update(SaveProductViewModel updateProduct, SaveUrlsProductViewModel urls)
        {
            if (!_userMiddleware.IsLogin())
            {
                return RedirectToRoute(new
                {
                    Controller = "Home",
                    Action = "Index"
                });
            }

            ViewBag.Categories = await _categoryService.GetViewModel();

            #region Validation

            if (updateProduct.File == null)
            {
                ModelState.Remove("File");
            }

            if (updateProduct.ImagePath1 == null)
            {
                ModelState.Remove("ImagePath1");
            }

            if (updateProduct.ImagePath2 == null)
            {
                ModelState.Remove("ImagePath2");
            }

            if (updateProduct.ImagePath3 == null)
            {
                ModelState.Remove("ImagePath3");
            }

            if (updateProduct.ImagePath4 == null)
            {
                ModelState.Remove("ImagePath4");
            }

            if (urls.File1 == null)
            {
                ModelState.Remove("File1");
            }

            if (urls.File2 == null)
            {
                ModelState.Remove("File2");
            }

            if (urls.File3 == null)
            {
                ModelState.Remove("File3");
            }

            if (urls.File4 == null)
            {
                ModelState.Remove("File4");
            }
            #endregion

            if (ModelState.IsValid)
            {
                SaveProductViewModel productvm = await _productService.GetSpecificSaveViewModel(updateProduct.Id);
                string[] actualUrls = new string[4];
                IFormFile[]? form = new IFormFile[4];

                actualUrls[0] = productvm.ImagePath1;
                actualUrls[1] = productvm.ImagePath2 == null ? "" : productvm.ImagePath2;
                actualUrls[2] = productvm.ImagePath3 == null ? "" : productvm.ImagePath3;
                actualUrls[3] = productvm.ImagePath4 == null ? "" : productvm.ImagePath4;

                //form
                form[0] = urls.File1;
                form[1] = urls.File2;
                form[2] = urls.File3;
                form[3] = urls.File4;

                if (form.Where(x => x == null).Count() == 4)
                {
                    return View(updateProduct);
                }

                /*= updateProduct.ImagePath =*/
                var updatedUrls = UploadFile(form, productvm.Id, actualUrls, true);

                updateProduct.ImagePath1 = updatedUrls[0];
                updateProduct.ImagePath2 = updatedUrls[1];
                updateProduct.ImagePath3 = updatedUrls[2];
                updateProduct.ImagePath4 = updatedUrls[3];

                await _productService.UpdateSaveViewModel(updateProduct);
                return RedirectToRoute(new
                {
                    Controller = "Product",
                    Action = "Index"
                });
            }


            return View(updateProduct);
        }

        #endregion 

        #region Delete Methods

        public async Task<IActionResult> Delete(int id)
        {
            if (!_userMiddleware.IsLogin())
            {
                return RedirectToRoute(new
                {
                    Controller = "Home",
                    Action = "Index"
                });
            }

            var deleteProduct = await _productService.GetSpecificSaveViewModel(id);
            return View(deleteProduct);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!_userMiddleware.IsLogin())
            {
                return RedirectToRoute(new
                {
                    Controller = "Home",
                    Action = "Index"
                });
            }

            var deleteProduct = await _productService.GetSpecificSaveViewModel(id);

            if (ModelState.IsValid)
            {
                await _productService.DeleteSaveViewModel(id);

                string basePath = $"/resources/uploading/Products/{id}";
                string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

                //create folder if not exists
                if (Directory.Exists(path))
                {
                    DirectoryInfo directory = new(path);
                    foreach(FileInfo file in directory.GetFiles())
                    {
                        file.Delete();
                    }

                    foreach (DirectoryInfo folder in directory.GetDirectories())
                    {
                        folder.Delete(true);
                    }

                    Directory.Delete(path);
                }

                return RedirectToRoute(new 
                {
                    Controller = "Product",
                    Action = "Index"
                });
            }

            return View();
        }

        #endregion

        #region Other Methods

        private string[] UploadFile(IFormFile[] file, int id, string[] imageUrl, bool isEditMode = false)
        {
            if (isEditMode && file == null)
            {
                return imageUrl;
            }

            //get directory path
            string basePath = $"/resources/uploading/Products/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            //create folder if not exists
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string[] ImageUrls = new string[4];

            for (int i = 0; i < file.Length; i++)
            {
                if (file[i] != null)
                {
                    //get file path
                    Guid guid = Guid.NewGuid();
                    FileInfo fileInfo = new(file[i].FileName);
                    string filename = guid + fileInfo.Extension;

                    string fileNameWithPath = Path.Combine(path, filename);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        file[i].CopyTo(stream);
                    }

                    if (isEditMode)
                    {
                        foreach (var image in imageUrl)
                        {
                            string[] allImageParts = image.Split("/");
                            string oldImageName = allImageParts[^1];
                            string completeImageOldPath = Path.Combine(path, oldImageName);

                            if (System.IO.File.Exists(completeImageOldPath))
                            {
                                System.IO.File.Delete(completeImageOldPath);
                            }
                        }
                    }

                    ImageUrls[i] = ($"{basePath}/{filename}");
                } 
                else
                {
                    ImageUrls[i] = imageUrl[i];
                }


            }


            return ImageUrls;
        }

        #endregion
    }
}
