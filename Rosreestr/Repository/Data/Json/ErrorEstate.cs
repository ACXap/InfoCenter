namespace Rosreestr.Repository.Data.Json
{
    class ErrorEstate : IRealEstate
    {
        public ErrorEstate(string error)
        {
            Error = error;
        }
        
        public string KadastrNumber => Error;

        public string Address => null;

        public string Area => null;

        public string Cost => null;

        public string CostDateEntering => null;

        public string CostDateValuation => null;

        public string CostDateApproval => null;

        public string ObjectDesc => null;

        public string Name => null;

        public string ConditionalNumber => null;

        public string PreviouslyAssignedNumber => null;

        public string YearUsed => null;

        public string YearBuilt => null;

        public string Error { get; }
    }
}