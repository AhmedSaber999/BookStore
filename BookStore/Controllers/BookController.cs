﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Models.Repositories;
using BookStore.ModelView;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace bsm_Allah.Controllers
{
    public class BookController : Controller
    {
        private IBookRepository<Book> bookRepository { get; }
        private IBookRepository<Auther> authorRepository { get; }
        private IHostEnvironment hosting { get; }

        // GET: Book
        public BookController(IBookRepository<Book> bookRepository, 
            IBookRepository<Auther> authorRepository,
            IHostEnvironment hosting)
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
            this.hosting = hosting;
        }
        public ActionResult Index()
        {
            var books = bookRepository.List();
            return View(books);
        }

        // GET: Book/Details/5
        public ActionResult Details(int id)
        {
            var book = bookRepository.Find(id);
            return View(book);
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            return View(GetModel());
        }

        // POST: Book/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAutherViewModel book)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string image = string.Empty;
                    if (book.image != null)
                    {
                        string images_folder_path = Path.Combine(hosting.ContentRootPath, "Content");
                        image = book.image.FileName;
                        string path = Path.Combine(images_folder_path, image);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            book.image.CopyTo(fileStream);
                        }
                    }
                    //  Console.WriteLine(image);
                    if (book.author_id == -1)
                    {
                        ViewBag.message = "Please select an auther";
                        return View(GetModel());
                    }
                    var author = authorRepository.Find(book.author_id);
                    var new_book = new Book
                    {
                        id = book.book_id,
                        description = book.book_description,
                        title = book.book_name,
                        auther = author,
                        image_path = image
                    };
                    bookRepository.Add(new_book);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            return View(GetModel());
        }

        // GET: Book/Edit/5
       private BookAutherViewModel Get_BookAutherView_model(int id)
        {
            var book = bookRepository.Find(id);
            int author_id = book.auther == null ? 0 : book.auther.id;
            var book_model = new BookAutherViewModel
            {
                book_id = book.id,
                book_name = book.title,
                book_description = book.description,
                author_id = author_id,
                image_path = book.image_path,
                authors = GetAuthors()
                
            };
            return book_model;
        }
        public ActionResult Edit(int id)
        {
            return View(Get_BookAutherView_model(id));
        }

        // POST: Book/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BookAutherViewModel uBook)
        {
            try
            {
                if (uBook.author_id == -1)
                {
                    ViewBag.message = "Please select an auther";
                    return View(Get_BookAutherView_model(id));
                }
                string image = string.Empty;
                if (uBook.image != null)
                {
                    string images_folder_path = Path.Combine(hosting.ContentRootPath, "Content");
                    image = uBook.image.FileName;
                    string path = Path.Combine(images_folder_path, image);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        uBook.image.CopyTo(fileStream);
                    }
                }
                Console.WriteLine("dddd "+image);
                string previousImage = bookRepository.Find(id).image_path;
                string _Path = "Content/" + previousImage;
                Console.WriteLine("dddd " + _Path);
                FileInfo file = new FileInfo(_Path);
                if (file.Exists)
                {
                    file.Delete();
                }
                var book = new Book
                {
                    auther = authorRepository.Find(uBook.author_id),
                    id = uBook.book_id,
                    description = uBook.book_description,
                    title = uBook.book_name,
                    image_path = image
                };
                bookRepository.Update(id, book);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int id)
        {
            var book = bookRepository.Find(id);
            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _Delete(int id)
        {
            try
            {
                bookRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        List<Auther> GetAuthors()
        {
            var authors = authorRepository.List().ToList();
            authors.Insert(0, new Auther { id = -1, name = "Choose Author" });
            return authors.ToList();
        }
        BookAutherViewModel GetModel()
        {
            var model = new BookAutherViewModel
            {
                authors = GetAuthors()
            };
            return model;
        }
    }
}