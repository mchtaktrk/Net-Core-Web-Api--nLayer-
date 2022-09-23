using FluentValidation;
using Kutuphane.DAL.Dto.SubCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane.Bussiness.Validation.SubCategory
{
	public class SubCategoryAddValidator : AbstractValidator<AddSubCategoryDto>
	{
		public SubCategoryAddValidator()
		{
			RuleFor(p => p.Name).NotEmpty().WithMessage("Bu alan boş geçilemez")
				.MaximumLength(200).WithMessage("Bu alana 200 karakterden fazla veri girişi yapılamaz");
			RuleFor(p => p.CategoryId).NotEmpty().WithMessage("Id boş geçilemez");
		}
	}
}
