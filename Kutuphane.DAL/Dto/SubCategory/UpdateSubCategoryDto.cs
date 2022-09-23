﻿using AppCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane.DAL.Dto.SubCategory
{
	public class UpdateSubCategoryDto : IDto
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public int CategoryId { get; set; }
	}
}
