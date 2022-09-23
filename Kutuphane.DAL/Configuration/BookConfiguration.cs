using Kutuphane.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane.DAL.Configuration
{
	public class BookConfiguration : IEntityTypeConfiguration<Book>
	{
		public void Configure(EntityTypeBuilder<Book> builder)
		{
			builder.HasKey(p => p.Id);
			builder.Property(p => p.Id).ValueGeneratedOnAdd();
			builder.Property(p => p.Name).HasMaxLength(200).IsRequired();
			builder.Property(p => p.Desc).HasMaxLength(200).IsRequired();

			builder.HasOne(p => p.SubCategoryFK).WithMany(p => p.Books).HasForeignKey(p => p.SubCategoryId);
		}
	}
}
