namespace DumpedEasyNetQHosepipeFilesMsSqlImporter.Console.Model.Mapping
{
    using Entities;
    using System.Data.Entity.ModelConfiguration;

    public class EasyNetQHosepipeDumpedPropertiesMap : EntityTypeConfiguration<EasyNetQHosepipeDumpedProperties>
    {
        public EasyNetQHosepipeDumpedPropertiesMap()
        {
            // Primary Key
            this.HasKey(x => x.Id);

            this.Property(x => x.HeadersDb).HasColumnName("Headers");
        }
    }
}
