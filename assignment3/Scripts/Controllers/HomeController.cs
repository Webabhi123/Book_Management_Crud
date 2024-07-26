using assignment3.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace assignment3.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly DbContext db = new bookcontext();
        public HomeController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        // GET: Home
        public ActionResult Index(string searchby)
        {
            
            List<tblbook> book;
            if (!string.IsNullOrEmpty(searchby))
            {
                searchby = searchby.ToLower();

                book = _bookRepository.GetBooks()
                        .Where(books =>
                        books.Bookname.ToLower().Contains(searchby) ||
                        books.Bookedition.Contains(searchby) ||
                        books.Publishername.ToLower().Contains(searchby) ||
                        books.Bookid.ToString().Contains(searchby)
                        )
                    .OrderByDescending(books => books.Timestamp) // Sort by Timestamp in descending order
                    .ToList();

            }

            else
            {
                book = _bookRepository.GetQueryableBooks().ToList();
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView("_BookList", book);
            }
            return View(book);


        }
        public ActionResult Create()
        {

            //return View(viewModel);
            return View(new tblbook());
        }
        [HttpPost]
        public ActionResult Create(tblbook book_)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase file = Request.Files[0];
                    if (file != null && file.ContentLength > 0)
                    {
                        // Read the file data and store it in the model
                        using (var binaryReader = new BinaryReader(file.InputStream))
                        {
                            book_.Image = binaryReader.ReadBytes(file.ContentLength);
                        }
                        book_.ContentType = file.ContentType;
                    }
                    _bookRepository.insert(book_);
                    _bookRepository.savechanges();

                    return RedirectToAction("Index");

                }
                return View(book_);
            }
            return View(book_);


        }
        public int UploadImageInDataBase(HttpPostedFileBase file, tblbook book_)
        {
            if (file != null && file.ContentLength > 0)
            {
                using (var binaryReader = new BinaryReader(file.InputStream))
                {
                    book_.Image = binaryReader.ReadBytes(file.ContentLength);
                }

                book_.ContentType = file.ContentType;
                db.Set<tblbook>().Add(book_);
               
                int i = db.SaveChanges();

                if (i == 1)
                {
                    return 1;
                }
                else
                {
                    return 0; // Error
                }
            }

            return -1;
        }
        public byte[] ConvertToBytes(HttpPostedFile image)
        {
            byte[] imagebytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imagebytes = reader.ReadBytes((int)image.ContentLength);
            return imagebytes;
        }
        public ActionResult DisplayImage(int bookId)
        {
            // Retrieve the specific book by ID
            tblbook book = _bookRepository.GetBookByID(bookId);

            if (book != null)
            {
                // Convert the image data to a base64-encoded string
                string base64Image = Convert.ToBase64String(book.Image);
                string imgSrc = $"data:{book.ContentType};base64,{base64Image}";

                ViewBag.ImageSrc = imgSrc; // Pass the base64-encoded image data to the view
            }

            return View();
        }

        //return View(book_);

       
        public ActionResult update(int Id)
        {
            tblbook book = _bookRepository.GetBookByID(Id);

            if (book == null)
            {
                // Handle the case where the book is not found, e.g., return a not found view.
                return HttpNotFound();
            }

            // Generate a unique identifier for the image (e.g., image filename) and store it in ViewBag
            ViewBag.ImageIdentifier = GenerateUniqueImageIdentifier();

            // Pass the book model to the Edit view
            return View(book);
            //return View(_bookRepository.GetBookByID(Id));
        }
        string GenerateUniqueImageIdentifier()
        {
            string uniqueIdentifier = Guid.NewGuid().ToString("N");
            return uniqueIdentifier;
        }
        
        [HttpPost]
        public ActionResult update(tblbook book_, HttpPostedFileBase imageData)
        {
            if (ModelState.IsValid)
            {
                if (imageData != null && imageData.ContentLength > 0)
                {
                    // Read the file data and store it in the model
                    using (var binaryReader = new BinaryReader(imageData.InputStream))
                    {
                        book_.Image = binaryReader.ReadBytes(imageData.ContentLength);
                    }
                    book_.ContentType = imageData.ContentType;
                }
                
                book_.Timestamp = DateTime.Now;

                _bookRepository.update(book_);
                _bookRepository.savechanges();

                    return RedirectToAction("Index");
                }

                // If ModelState is not valid, return to the edit view with validation errors
                return View(book_);
            }

          
        
        public ActionResult Displayimage(int bookId)
        {
            // Retrieve the specific book by ID
            tblbook book = _bookRepository.GetBookByID(bookId);

            if (book != null)
            {
                // Convert the image data to a base64-encoded string
                string base64Image = Convert.ToBase64String(book.Image);
                string imgSrc = $"data:{book.ContentType};base64,{base64Image}";

                ViewBag.ImageSrc = imgSrc; // Pass the base64-encoded image data to the view
            }

            return View();
        }

        public ActionResult Delete(int Id) 
        {
            _bookRepository.delete(Id);
            _bookRepository.savechanges();
            return RedirectToAction("Index");
        }

    }
}