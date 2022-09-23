﻿using Kutuphane.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane.DAL.Configuration
{
	public class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategory>
	{
		public void Configure(EntityTypeBuilder<SubCategory> builder)
		{
			builder.HasKey(p => p.Id);
			builder.Property(p => p.Id).ValueGeneratedOnAdd();
			builder.Property(p => p.Name).HasMaxLength(200).IsRequired();

			builder.HasOne(p => p.CategoryFK).WithMany(p => p.SubCategories).HasForeignKey(p => p.CategoryId);

			//builder.HasMany(p => p.Books).WithOne(p => p.SubCategoryFK).HasForeignKey(p => p.SubCategoryId);
		}
	}
}
