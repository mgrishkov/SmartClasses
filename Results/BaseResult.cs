
namespace SmartClasses.Results
{
    public class BaseResult
    {
        public bool   IsOK { get; set; }
        public string Message { get; set; } 

        public BaseResult()
        {
            IsOK = false;
        }
    }
}
