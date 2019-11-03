using System;
using System.Collections.Generic;
using System.Text;
using JdCat.Basketball.Model.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JdCat.Basketball.Model.Mappings
{
    //public class MatchMapping : IEntityTypeConfiguration<Match>
    //{
    //    public void Configure(EntityTypeBuilder<Match> builder)
    //    {
    //        builder.HasOne(a => a.TeamHost)
    //            .WithOne(a => a.Match)
    //            .HasForeignKey<Match>(a => a.TeamHostId)
    //            .OnDelete(DeleteBehavior.Cascade);

    //        builder.HasOne(a => a.TeamVisitor)
    //            .WithOne(a => a.Match)
    //            .HasForeignKey<Match>(a => a.TeamVisitorId)
    //            .OnDelete(DeleteBehavior.Cascade);
    //    }
    //}
}
