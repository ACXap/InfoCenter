namespace Common.Data
{
    public class ErrorResult
    {
        public ErrorResult(string messageError, EnumTypeError typeError)
        {
            MessageError = messageError;
            TypeError = typeError;

            switch (typeError)
            {
                case EnumTypeError.ErrorBd:
                    Error = "Ведутся технические работы на серере базы данных.\nПопробуйте позже.";
                    break;
                case EnumTypeError.ErrorSite:
                    Error = "Ведутся технические работы на сайте.\nПопробуйте позже.";
                    break;
                case EnumTypeError.ResultNotFound:
                    Error = "Ничего не найдено.\nПопробуйте изменить запрос.";
                    break;
                default:
                    break;
            }
        }

        public string Error { get; private set; }
        public string MessageError { get; private set; }
        public EnumTypeError TypeError { get; private set; }
    }
}