using Kutuphane.Bussiness.Abstract;
using Kutuphane.DAL.Context;
using Kutuphane.DAL.Dto.Book;
using Kutuphane.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane.Bussiness.Concrete
{
	public class BookService : IBookService
	{
		private readonly KutuphaneDbContext _kutuphaneDbContext;	
		public BookService(KutuphaneDbContext kutuphaneDbContext)
		{
			_kutuphaneDbContext = kutuphaneDbContext;
		}
		public async Task<int> AddBook(AddBookDto addBookDto)
		{
			var newBook = new Book
			{
				Name = addBookDto.BookName,
				Desc = addBookDto.Desc,
				SubCategoryId = addBookDto.SubCategoryId
			};
			await _kutuphaneDbContext.Books.AddAsync(newBook);
			return await _kutuphaneDbContext.SaveChangesAsync();
		}

		public async Task<int> DeleteBook(int bookId)
		{
			var bookObject = await _kutuphaneDbContext.Books.Where(p => !p.IsDeleted && p.Id == bookId).FirstOrDefaultAsync();
			if (bookObject == null)
			{
				return -1;
			}
			bookObject.IsDeleted = true;
			_kutuphaneDbContext.Books.Update(bookObject);
			return await _kutuphaneDbContext.SaveChangesAsync();
		}

		public async Task<List<GetListBookDto>> GetAllBooks()
		{
			return await _kutuphaneDbContext.Books.Include(p=>p.SubCategoryFK).Where(p => !p.IsDeleted).Select(p => new GetListBookDto
			{
				Id = p.Id,
				Name = p.Name,
				Desc = p.Desc,
				SubCategoryName = p.SubCategoryFK.Name,
				SubCategoryId = p.SubCategoryId
			}).ToListAsync();
		}

		public async Task<GetBookDto> GetBookById(int bookId)
		{
			return await _kutuphaneDbContext.Books.Include(p => p.SubCategoryFK).ThenInclude(p=>p.CategoryFK).Where(p => !p.IsDeleted && p.Id == bookId)
				.Select(p => new GetBookDto
				{
					Id = p.Id,
					Name = p.Name,
					Desc = p.Desc,
					SubCategoryName = p.SubCategoryFK.Name,
					CategoryName=p.SubCategoryFK.CategoryFK.Name
				}).FirstOrDefaultAsync();
		}

		public async Task<int> UpdateBook(int bookId, UpdateBookDto updateBookDto)
		{
			var bookObject = await _kutuphaneDbContext.Books.Where(p => !p.IsDeleted && p.Id == bookId).FirstOrDefaultAsync();
			if (bookObject==null)
			{
				return -1;
			}
			bookObject.Name = updateBookDto.BookName;
			bookObject.Desc = updateBookDto.Desc;
			bookObject.SubCategoryId = updateBookDto.SubCategoryId;

			_kutuphaneDbContext.Books.Update(bookObject);
			return await _kutuphaneDbContext.SaveChangesAsync();
		}
	}
}
