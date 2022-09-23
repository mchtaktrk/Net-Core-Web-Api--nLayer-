using FluentValidation;
using Kutuphane.DAL.Dto.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane.Bussiness.Validation.Category
{
	public class CategoryAddValidator : AbstractValidator<AddCategoryDto>
	{
		public CategoryAddValidator()
		{
			RuleFor(p => p.Name).NotEmpty().WithMessage("Bu alan boş geçilemez")
				.MaximumLength(200).WithMessage("Bu alan 200 Karakterden fazla olmamalı");
		}
	}
}
