namespace BikeRental.DTO.DTO
{
    public class BaseDTO
    {
        public BaseDTO()
        {
            Active = true;
        }
        public Guid Id { get; set; }

        public bool Active { get; set; }
    }
}
