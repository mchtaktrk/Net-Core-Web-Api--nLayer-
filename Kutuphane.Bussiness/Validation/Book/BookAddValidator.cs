using FluentValidation;
using Kutuphane.DAL.Dto.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane.Bussiness.Validation.Book
{
	public class BookAddValidator :AbstractValidator<AddBookDto>
	{
		public BookAddValidator()
		{
			RuleFor(p => p.BookName).NotEmpty().WithMessage("Name Boş geçilemez")
				.MaximumLength(200).WithMessage("En fazla 200 karakter");
			RuleFor(p => p.Desc).NotEmpty().WithMessage("Desc Boş Geçilemez")
				.MaximumLength(200).WithMessage("En fazla 200 karakter");
			RuleFor(p => p.SubCategoryId).NotEmpty().WithMessage("SubCategory Boş geçilemez");
		}
	}
}
