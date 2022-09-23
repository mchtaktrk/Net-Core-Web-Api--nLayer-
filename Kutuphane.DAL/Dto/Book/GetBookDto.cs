﻿using AppCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane.DAL.Dto.Book
{
	public class GetBookDto : IDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Desc { get; set; }
		public string CategoryName { get; set; }
		public string SubCategoryName { get; set; }
	}
}
