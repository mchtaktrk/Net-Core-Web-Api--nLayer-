using Kutuphane.DAL.Dto.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane.Bussiness.Abstract
{
	public interface ICategoryService
	{
		Task<List<GetListCategoryDto>> GetAllCategories();

		Task<GetCategoryDto> GetCategory(int categoryId);

		Task<int> AddCategory(AddCategoryDto addCategoryDto);

		Task<int> UpdateCategory(int categoryId,UpdateCategoryDto updateCategoryDto);

		Task<int> DeleteCategory(int categoryId);
		
	}
}
