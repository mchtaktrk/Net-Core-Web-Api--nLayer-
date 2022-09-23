using Kutuphane.Bussiness.Abstract;
using Kutuphane.DAL.Context;
using Kutuphane.DAL.Dto.SubCategory;
using Kutuphane.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane.Bussiness.Concrete
{
	public class SubCategoryService : ISubCategoryService
	{
		private readonly KutuphaneDbContext _kutuphaneDbContext; 
		public SubCategoryService(KutuphaneDbContext kutuphaneDbContext)
		{
			_kutuphaneDbContext = kutuphaneDbContext;
		}
		public async Task<int> AddSubCategory(AddSubCategoryDto addSubCategoryDto)
		{
			var newSubCategory = new SubCategory { 
				Name = addSubCategoryDto.Name,
				CategoryId=addSubCategoryDto.CategoryId
			};
			await _kutuphaneDbContext.SubCategories.AddAsync(newSubCategory);
			return await _kutuphaneDbContext.SaveChangesAsync();
		}

		public async Task<int> DeleteSubCategory(int subCategoryId)
		{
			var subCategoryObject = await _kutuphaneDbContext.SubCategories.Where(p => !p.IsDeleted && p.Id == subCategoryId).FirstOrDefaultAsync();
			if (subCategoryObject == null)
			{
				return -1;
			}
			subCategoryObject.IsDeleted = true;

			_kutuphaneDbContext.SubCategories.Update(subCategoryObject);
			return await _kutuphaneDbContext.SaveChangesAsync();
		}

		public async Task<List<GetListSubCategoryDto>> GetAllSubCategories()
		{
			return await _kutuphaneDbContext.SubCategories.Include(p => p.CategoryFK).Where(p => !p.IsDeleted)
				.Select(p => new GetListSubCategoryDto {
					Id = p.Id,
					Name = p.Name,
					CategoryName = p.CategoryFK.Name,
					CategoryId = p.CategoryId
				}).ToListAsync();
		}

		public async Task<GetSubCategoryDto> GetSubCategory(int subCategoryId)
		{
			return await _kutuphaneDbContext.SubCategories.Include(p => p.CategoryFK).Where(p => !p.IsDeleted)
				.Select(p => new GetSubCategoryDto
				{
					Id = p.Id,
					Name = p.Name,
					CategoryName = p.CategoryFK.Name
				}).FirstOrDefaultAsync();
		}

		public async Task<int> UpdateSubCategory(int subCategoryId, UpdateSubCategoryDto updateSubCategoryDto)
		{
			var subCategoryObject = await _kutuphaneDbContext.SubCategories.Where(p => !p.IsDeleted && p.Id == subCategoryId).FirstOrDefaultAsync();
			if (subCategoryObject==null)
			{
				return -1;
			}
			subCategoryObject.Name = updateSubCategoryDto.Name;
			subCategoryObject.CategoryId = updateSubCategoryDto.CategoryId;

			_kutuphaneDbContext.SubCategories.Update(subCategoryObject);
			return await _kutuphaneDbContext.SaveChangesAsync();
		}
	}
}
