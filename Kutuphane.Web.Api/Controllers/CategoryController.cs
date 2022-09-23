using Kutuphane.Bussiness.Abstract;
using Kutuphane.Bussiness.Validation.Category;
using Kutuphane.DAL.Dto.Category;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kutuphane.Web.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : Controller
	{
		private readonly ICategoryService _categoryService;
		public CategoryController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}
		[HttpGet("GetListCategory")]
		public async Task<ActionResult<List<GetListCategoryDto>>> GetCategoryList()
		{
			try
			{
				return Ok(await _categoryService.GetAllCategories());
			}
			catch (Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}
		[HttpGet("GetCategoryById/{id}")]
		public async Task<ActionResult<GetCategoryDto>> GetCategoryById(int id)
		{
			var list = new List<string>();
			if (id < 0)
			{
				list.Add("Geçersiz Id");
				return Ok(new { code = StatusCode(100), message = list, type = "error" });
			}
			try
			{
				var currentCategory = await _categoryService.GetCategory(id);
				if (currentCategory == null)
				{
					list.Add("Aranan kitap bulunamadı");
					return Ok(new { code = StatusCode(1001), message = list, type = "error" });
				}
				else
				{
					return currentCategory;
				}
			}
			catch (Exception ex)
			{

				return BadRequest(ex.Message);
			}

		}
		[HttpPost("AddCategory")]
		public async Task<ActionResult<string>> AddCategory(AddCategoryDto addCategoryDto)
		{
			var list = new List<string>();
			/*
			*/
			var validator = new CategoryAddValidator();
			var validatorResult = validator.Validate(addCategoryDto);
			if (!validatorResult.IsValid)
			{
				foreach (var err in validatorResult.Errors)
				{
					list.Add(err.ErrorMessage);
				}
				return Ok(new { code=StatusCode(1002), message=list, type="error"});
			}
			 /*
			 */
			try
			{
				var result = await _categoryService.AddCategory(addCategoryDto);
				if (result > 0)
				{
					list.Add("Kategori başarıyla eklendi");
					return Ok(new { code=StatusCode(1000), message=list, type="success"});
				}
				else
				{
					list.Add("Ekleme işlemi başarısız");
					return Ok(new { code=StatusCode(1001), message=list, type="error"});
				}
			}
			catch (Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}
		[HttpPut("UpdateCategory/{categoryId}")]
		public async Task<ActionResult<string>> UpdateCategory(int categoryId, UpdateCategoryDto updateCategoryDto) {

			var list = new List<string>();
			/*
			 */
			var validator = new CategoryUpdateValidator();
			var validatorResult = validator.Validate(updateCategoryDto);
			if (!validatorResult.IsValid)
			{
				foreach (var err in validatorResult.Errors)
				{
					list.Add(err.ErrorMessage);
				}
				return Ok(new { code=StatusCode(1002), message=list, type="error"});
			}
			/*
			 */
			try
			{
				var result = await _categoryService.UpdateCategory(categoryId, updateCategoryDto);
				if (result > 0)
				{
					list.Add("Kategori başarıyla güncellendi");
					return Ok(new { code = StatusCode(1000), message = list, type = "success" });
				}
				else if (result == -1)
				{
					list.Add("Güncellenecek kategori bulunamadı.");
					return Ok(new { code=StatusCode(1001), message=list, type="error"});
				}
				else
				{
					list.Add("Güncelleme başarısız oldu.");
					return Ok(new { code=StatusCode(1001), message=list, type="error"});
				}
			}
			catch (Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}
		[HttpDelete("DeleteCategory/{id}")]
		public async Task<ActionResult<string>> DeleteCategory(int id) {

			var list = new List<string>();
			try
			{
				var result = await _categoryService.DeleteCategory(id);

				if(result>0)
				{
					list.Add("Silme işlemi başarılı");
					return Ok(new { code=StatusCode(1000), message=list, type="success"});
				}
				else if(result==-1)
				{
					list.Add("Silinecek kategori bulunamadı");
					return Ok(new { code=StatusCode(1001), message=list, type="error"});
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
