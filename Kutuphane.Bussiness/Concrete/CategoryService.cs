using Kutuphane.Bussiness.Abstract;
using Kutuphane.DAL.Context;
using Kutuphane.DAL.Dto.Category;
using Kutuphane.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane.Bussiness.Concrete
{
	public class CategoryService : ICategoryService
	{
		private readonly KutuphaneDbContext _kutuphaneDbContext;
		public CategoryService(KutuphaneDbContext kutuphaneDbContext) //Constructor
		{
			_kutuphaneDbContext = kutuphaneDbContext;
		}
		public async Task<int> AddCategory(AddCategoryDto addCategoryDto)
		{
			var newCategory = new Category //Boş Category nesnesi oluşturma
			{
				//Parametre olarak gelen bilgilerin boş nesneye aktarılması
				Name = addCategoryDto.Name
			};
			await _kutuphaneDbContext.Categories.AddAsync(newCategory);
			return await _kutuphaneDbContext.SaveChangesAsync();
		}

		public async Task<int> DeleteCategory(int categoryId) //Silme
		{
			var categoryObject = await _kutuphaneDbContext.Categories.Where(p => p.Id == categoryId && !p.IsDeleted).FirstOrDefaultAsync();
			if (categoryObject == null)
			{
				return -1;
			}
			categoryObject.IsDeleted = true;

			_kutuphaneDbContext.Categories.Update(categoryObject);
			return await _kutuphaneDbContext.SaveChangesAsync();
		}

		public async Task<List<GetListCategoryDto>> GetAllCategories() //Tüm Kategorileri Listeleme
		{
			return await _kutuphaneDbContext.Categories.Where(p => !p.IsDeleted)
				.Select(p => new GetListCategoryDto
				{
					Id = p.Id,
					Name = p.Name
				}).ToListAsync();
		}

		public async Task<GetCategoryDto> GetCategory(int categoryId) //Id si verilen Kategoriyi Listeleme
		{
			return await _kutuphaneDbContext.Categories.Where(p => !p.IsDeleted && p.Id == categoryId)
				.Select(p => new GetCategoryDto
				{
					Id = p.Id,
					Name=p.Name
				}).FirstOrDefaultAsync();
		}

		public  async Task<int> UpdateCategory(int categoryId, UpdateCategoryDto updateCategoryDto) //Güncelleme
		{
			var categoryObject = await _kutuphaneDbContext.Categories.Where(p=>p.Id==categoryId && !p.IsDeleted).FirstOrDefaultAsync();
			if (categoryObject==null)
			{
				return -1;
			}
			categoryObject.Name = updateCategoryDto.CategoryName;

			_kutuphaneDbContext.Categories.Update(categoryObject);
			return await _kutuphaneDbContext.SaveChangesAsync();
		}
	}
}
