﻿using ef.intro.wwwapi.Models;
using ef.intro.wwwapi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ef.intro.wwwapi.EndPoint
{
    public static class BookApi
    {
        public static void ConfigureBookApi(this WebApplication app)
        {
            app.MapPost("/books", AddBook);
            app.MapGet("/books", GetBooks);
            app.MapGet("/books/{id}", GetBook);
            app.MapPut("/books/{id}", UpdateBook);
            app.MapDelete("/books/{id}", DeleteBook);
        }

        private static async Task<IResult> GetBooks(ILibraryRepository service)
        {
            try
            {
                return await Task.Run(() => 
                {
                    return Results.Ok(service.GetAllBooks());
                });
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> GetBook(int id, ILibraryRepository service)
        {
            try
            {
                return await Task.Run(() =>
                {
                    var book = service.GetBook(id);
                    if (book == null) return Results.NotFound();
                    return Results.Ok(book);
                });
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        private static async Task<IResult> AddBook(Book book, ILibraryRepository service)
        {
            try
            {
                if (service.AddBook(book)) return Results.Ok(book);
                return Results.NotFound();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        private static async Task<IResult> UpdateBook(Book book, ILibraryRepository service)
        {
            try
            {
                return await Task.Run(() =>
                {
                    if (service.UpdateBook(book)) return Results.Ok(book);
                    return Results.NotFound();
                });
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> DeleteBook(int id, ILibraryRepository service)
        {
            try
            {
                if (service.DeleteBook(id)) return Results.Ok();
                return Results.NotFound();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
    }
}
