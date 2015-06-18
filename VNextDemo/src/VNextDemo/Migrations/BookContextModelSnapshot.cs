using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Builders;
using Microsoft.Data.Entity.Relational.Migrations.Infrastructure;
using VNextDemo;

namespace VNextDemo.Migrations
{
    [ContextType(typeof(BookContext))]
    partial class BookContextModelSnapshot : ModelSnapshot
    {
        public override IModel Model
        {
            get
            {
                var builder = new BasicModelBuilder()
                    .Annotation("SqlServer:ValueGeneration", "Sequence");
                
                builder.Entity("VNextDemo.Model.Book", b =>
                    {
                        b.Property<Guid>("Id")
                            .GenerateValueOnAdd()
                            .Annotation("OriginalValueIndex", 0);
                        b.Property<decimal>("Price")
                            .Annotation("OriginalValueIndex", 1);
                        b.Property<string>("Title")
                            .Annotation("OriginalValueIndex", 2);
                        b.Key("Id");
                    });
                
                return builder.Model;
            }
        }
    }
}
