using Kutuphane.Bussiness.Abstract;
using Kutuphane.Bussiness.Validation.SubCategory;
using Kutuphane.DAL.Dto.SubCategory;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kutuphane.Web.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SubCategoryController : Controller
	{
		private readonly ISubCategoryService _subCategoryService;
		public SubCategoryController(ISubCategoryService subCategoryService)
		{
			_subCategoryService = subCategoryService;
		}
		[HttpGet("GetSubCategoryList")]
		public async Task<ActionResult<List<GetSubCategoryDto>>> GetSubCategoryList()
		{
			try
			{
				return Ok(await _subCategoryService.GetAllSubCategories());
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpGet("GetSubCategoryById/{id}")]
		public async Task<ActionResult<GetSubCategoryDto>> GetSubCategoryById(int id) {

			var list = new List<string>();
			if (id<0)
			{
				list.Add("Geçersiz Id");
				return Ok(new { code=StatusCode(1001), message=list, type="error"});
			}
			try
			{
				var currentSubCategory = await _subCategoryService.GetSubCategory(id);
				if (currentSubCategory==null)
				{
					list.Add("Aranan SubCategory bulunamadı");
					return Ok(new { code=StatusCode(1001), message=list, type="error"});
				}
				else
				{
					return currentSubCategory;
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		
		}

		[HttpPost("AddSubCategory")]
		public async Task<ActionResult<string>> AddSubCategory(AddSubCategoryDto addSubCategoryDto)
		{
			var list = new List<string>();
			/*
			 */
			var validator = new SubCategoryAddValidator();
			var validationResult = validator.Validate(addSubCategoryDto);
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
				var result = await _subCategoryService.AddSubCategory(addSubCategoryDto);
				if (result > 0)
				{
					list.Add("SubCategory ekleme işlemi başarılı");
					return Ok(new { code = StatusCode(1000), message = list, type = "success" });
				}
				else
				{
					list.Add("SubCategory ekleme işlemi başarısız");
					return Ok(new { code = StatusCode(1001), message = list, type = "error" }); 
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}


		}

		[HttpPut("UpdateSubCategory/{id}")]
		public async Task<ActionResult<string>> UpdateSubCategory(int id, UpdateSubCategoryDto updateSubCategoryDto) {

			var list = new List<string>();
			/*
			*/
			var validator = new SubCategoryUpdateValidator();
			var validatorResult = validator.Validate(updateSubCategoryDto);
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
				var result = await _subCategoryService.UpdateSubCategory(id,updateSubCategoryDto);
				if (result>0)
				{
					//başarılı
					list.Add("SubCategory ekleme işlemi başarılı");
					return Ok(new { code=StatusCode(1000), message=list, type="success"});
				}
				else if (result==-1)
				{
					//bulunamadı
					list.Add("Güncellenecek SubCategory bulunamadı");
					return Ok(new { code = StatusCode(1001), message=list, type="error"});
				}
				else
				{
					//başarısız
					list.Add("Güncelleme başarısız");
					return Ok(new { code=StatusCode(1001), message=list, type="error"});
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		
		}
		[HttpDelete("DeleteSubCategory/{id}")]
		public async Task<ActionResult<string>> DeleteSubCategory(int id)
		{
			var list = new List<string>();

			try
			{
				var result = await _subCategoryService.DeleteSubCategory(id);
				if (result>0)//başarılı
				{
					list.Add("SubCategory silme işlemi başarılı");
					return Ok(new { code=StatusCode(1000), message=list, type="success"});
				}
				else if (result==-1)//bulunamadı
				{
					list.Add("Silinecek SubCategory bulunamadı");
					return Ok(new { code=StatusCode(1001), message=list, type="error"});
				}
				else//silinemedi
				{
					list.Add("SubCategory silme işlemi başarısız");
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
