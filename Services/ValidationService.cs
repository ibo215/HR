namespace HR.Services
{
    public class ValidationService : IValidationService
    {
        public bool ValidateId(int id, out string errorMessage)
        {
            if (id > 0)
            {
                errorMessage = string.Empty;
                return true;
            }

            //the ID start from 0
            errorMessage = "Invalid ID. ID must be greater than zero.";
            return false;
        }

        public bool ValidateCreate(object entity, out string errorMessage)
        {
            if (entity != null)
            {
                errorMessage = string.Empty;
                return true;
            }

            errorMessage = "Entity cannot be null.";
            return false;
        }

        public bool ValidateUpdate(int id, object entity, out string errorMessage)
        {
            if (ValidateId(id, out errorMessage) && entity != null)
            {
                errorMessage = string.Empty;
                return true;
            }

            if (entity == null)
            {
                errorMessage = "Entity cannot be null.";
            }

            return false;
        }
    }

}
