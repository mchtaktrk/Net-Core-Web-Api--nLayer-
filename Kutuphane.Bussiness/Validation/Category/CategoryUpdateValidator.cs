using FluentValidation;
using Kutuphane.DAL.Dto.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane.Bussiness.Validation.Category
{
	public class CategoryUpdateValidator : AbstractValidator<UpdateCategoryDto>
	{
		public CategoryUpdateValidator()
		{
			RuleFor(p => p.CategoryName).NotEmpty().WithMessage("Bu alan boş geçemez")
				.MaximumLength(200).WithMessage("200 karakterden fazla karater girişi yapılamaz");
		}
	}
}
