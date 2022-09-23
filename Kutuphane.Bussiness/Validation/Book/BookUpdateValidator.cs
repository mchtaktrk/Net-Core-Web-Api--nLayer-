using FluentValidation;
using Kutuphane.DAL.Dto.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane.Bussiness.Validation.Book
{
	public class BookUpdateValidator : AbstractValidator<UpdateBookDto>
	{
		public BookUpdateValidator()
		{
			RuleFor(p => p.BookName).NotEmpty().WithMessage("Boş geçilemez")
				.MaximumLength(200).WithMessage("200 Karakterden fazla olamaz");
			RuleFor(p => p.Desc).NotEmpty().WithMessage("Boş geçilemez")
				.MaximumLength(200).WithMessage("200 karakterden fazla olamaz");
			RuleFor(p => p.SubCategoryId).NotEmpty().WithMessage("Id Boş geçilemez");
		}
	}
}
