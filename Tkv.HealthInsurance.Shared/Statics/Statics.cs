namespace Tkv.HealthInsurance.Shared.Statics
{
    public class Statics
    {
        //Please do not scream. For now && just for simplicity we put connection string in the source; In the design time
        //and between projects we shall need to create migrations without setting a runnable startup project.
        public const string DbConnectionString = "Data Source=.;Initial Catalog=Tkv.HealthInsurance.Domain;User ID=sa;password=123;TrustServerCertificate=True;MultipleActiveResultSets=true;";
    }
}
