namespace Tkv.HealthInsurance.Domain.Domains
{
    public class DomainBase
    {

        #region [Properties]


        public long Id { get; set; }
        public DateTime InsertedDateTime { get; set; }
        public DateTime LastModifiedDateTime { get; set; }
        public DateTime? DeletedDateTime { get; set; } 


        #endregion

    }
}
