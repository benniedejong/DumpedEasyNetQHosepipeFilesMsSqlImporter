namespace DumpedEasyNetQHosepipeFilesMsSqlImporter.Console.Model.Mapping
{
    using Entities;
    using System.Data.Entity.ModelConfiguration;

    public class EasyNetQHosepipeDumpedErrorMessageMap : EntityTypeConfiguration<EasyNetQHosepipeDumpedErrorMessage>
    {
        public EasyNetQHosepipeDumpedErrorMessageMap()
        {
            // Primary Key
            this.HasKey(x => x.Id);

            this.Property(x => x.HeadersDb).HasColumnName("Headers");
        }
    }
}
