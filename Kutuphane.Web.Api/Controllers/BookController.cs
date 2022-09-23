using Kutuphane.Bussiness.Abstract;
using Kutuphane.Bussiness.Validation.Book;
using Kutuphane.DAL.Dto.Book;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kutuphane.Web.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BookController : ControllerBase
	{
		private readonly IBookService _bookService;
		public BookController(IBookService bookService)
		{
			_bookService = bookService;
		}
		[HttpGet("GetAllBooks")]
		public async Task<ActionResult<List<GetListBookDto>>> GetListBook()
		{
			try
			{
				return Ok(await _bookService.GetAllBooks());
			}
			catch (Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}
		[HttpGet("GetBookById/{id}")]
		public async Task<ActionResult<GetBookDto>> GetBookById(int id)
		{
			var list = new List<String>();
			if (id<=0)
			{
				list.Add("Kitap Id Geçersiz.");
				return Ok(new { code = StatusCode(1001), message = list, type = "error" });
			}
			try
			{
				var currentBook = await _bookService.GetBookById(id);
				if (currentBook==null)
				{
					list.Add("Kitap bulunamadı.");
					return Ok(new { code = StatusCode(1001), message = list, type = "error" });
				}
				else
				{
					return currentBook;
				}
			}
			catch (Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}
		[HttpPost("AddBook")]
		public async Task<ActionResult<string>> AddBook(AddBookDto addBookDto) {
			var list = new List<string>();

			//******************
			var validator = new BookAddValidator();
			var validationResult = validator.Validate(addBookDto);
			if (!validationResult.IsValid)
			{
				foreach (var err in validationResult.Errors)
				{
					list.Add(err.ErrorMessage);
				}
				return Ok(new { code=StatusCode(1002), message=list, type="error"});
			}
			/*
			
			 */

			try
			{
				var result = await _bookService.AddBook(addBookDto);
				if (result > 0)
				{
					list.Add("Ekleme işlemi başarılı.");
					return Ok(new { code = StatusCode(1000), message = list, type = "success" });
				}
				else
				{
					list.Add("Ekleme işlemi başarısız.");
					return Ok(new { code=StatusCode(1001), message=list, type="error"});
				}
			}
			catch (Exception ex)
			{

				return BadRequest(ex.Message);
			}
		
		}
		[HttpPut("UpdateBook/{id}")]
		public async Task<ActionResult<string>> UpdateBook(int id, UpdateBookDto updateBookDto) {

			var list = new List<string>();
			/*
			 */
			var validator = new BookUpdateValidator();
			var validatonResult = validator.Validate(updateBookDto);
			if (!validatonResult.IsValid)
			{
				foreach (var err in validatonResult.Errors)
				{
					list.Add(err.ErrorMessage);
				}
				return Ok(new { code=StatusCode(1002), message=list, type="error"});
			}
			 /*
			 */

			try
			{
				var result = await _bookService.UpdateBook(id,updateBookDto);
				if (result>0)
				{
					list.Add("Günecelleme başarılı.");
					return Ok(new { code=StatusCode(1000), message=list, type="success"});
				}
				else if (result==-1)
				{
					list.Add("Güncellenecek kitap bulunamadı.");
					return Ok(new { code=StatusCode(1001), message=list, type="error"});
				}
				else
				{
					list.Add("Güncelleme başarısız.");
					return Ok(new { code=StatusCode(1001), message=list, type="error"});
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}	
		}
		[HttpDelete("DeleteBook/{id}")]
		public async Task<ActionResult<string>> DeleteBook(int id) {

			var list = new List<string>();
			try
			{
				var result = await _bookService.DeleteBook(id);
				if (result > 0)
				{
					list.Add("Silme işlemi başarılı");
					return Ok(new { code = StatusCode(1000), message = list, type = "success" });
				}
				else if (result == -1)
				{
					list.Add("Silinecek kitap bulunamadı");
					return Ok(new { code = StatusCode(1001), message = list, type = "error" });
				}
				else
				{
					list.Add("Silme işlemi başarısız");
					return Ok(new { code=StatusCode(1001), message=list, type="error"});
				}
			}
			catch (Exception ex)
			{

				return BadRequest(ex.Message);
			}

		}
	}
}
