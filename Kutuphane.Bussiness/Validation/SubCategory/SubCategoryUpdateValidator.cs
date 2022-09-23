using FluentValidation;
using Kutuphane.DAL.Dto.SubCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane.Bussiness.Validation.SubCategory
{
	public class SubCategoryUpdateValidator : AbstractValidator<UpdateSubCategoryDto>
	{
		public SubCategoryUpdateValidator()
		{
			RuleFor(p => p.Name).NotEmpty().WithMessage("Bu alan boş geçilemez")
				.MaximumLength(200).WithMessage("200 Karakterden fazla veri girişi yapılamaz");
			RuleFor(p => p.CategoryId).NotEmpty().WithMessage("Id boş geçilemez");
		}
	}
}
