namespace HR.Services
{
    public interface IValidationService
    {
        bool ValidateId(int id, out string errorMessage); 
        bool ValidateCreate(object entity, out string errorMessage);
        bool ValidateUpdate(int id, object entity, out string errorMessage);
    }


}
