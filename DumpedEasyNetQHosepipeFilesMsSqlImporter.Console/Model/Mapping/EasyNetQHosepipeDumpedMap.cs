namespace DumpedEasyNetQHosepipeFilesMsSqlImporter.Console.Model.Mapping
{
    using Entities;
    using System.Data.Entity.ModelConfiguration;

    public class EasyNetQHosepipeDumpedMap : EntityTypeConfiguration<EasyNetQHosepipeDumped>
    {
        public EasyNetQHosepipeDumpedMap()
        {
            // Primary Key
            this.HasKey(x => x.Id);

            this.Property(x => x.MessageFilePath).HasMaxLength(2048);
            this.Property(x => x.RawMessage);

            this.Property(x => x.InfoFilePath).HasMaxLength(2048);
            this.Property(x => x.RawInfo);

            this.Property(x => x.PropertiesFilePath).HasMaxLength(2048);
            this.Property(x => x.RawProperties);
        }
    }
}
