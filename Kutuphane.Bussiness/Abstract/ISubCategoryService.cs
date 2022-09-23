using Kutuphane.DAL.Dto.SubCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane.Bussiness.Abstract
{
	public interface ISubCategoryService
	{
		Task<List<GetListSubCategoryDto>> GetAllSubCategories();

		Task<GetSubCategoryDto> GetSubCategory(int subCategoryId);

		Task<int> AddSubCategory(AddSubCategoryDto addSubCategoryDto);

		Task<int> UpdateSubCategory(int subCategoryId,UpdateSubCategoryDto updateSubCategoryDto);

		Task<int> DeleteSubCategory(int subCategoryId);

	}
}
