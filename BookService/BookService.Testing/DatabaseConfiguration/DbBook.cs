using System;

namespace BookService.Testing.DatabaseConfiguration
{
    public class DbBook
    {
        public static void InsertDataInAccountTable(string uid, string sysInsertTime, string sysInsertAccountUid, string mandatorUid, string financingUid, string currencyUid, string accountNumber, string regress, string statusUid, string repaymentTypeUid, string denomination, string individualCodeUid, string inactivationDate, string ekey, string openingDate, string description, string comment, string ownershipPromotion)
        {
            //PooledCoverFundModel model = new PooledCoverFundModel();

            //Account account = new Account
            //{
            //    UID = new Guid(uid),
            //    SysInsertTime = Convert.ToDateTime(sysInsertTime),
            //    SysInsertAccountUID = new Guid(sysInsertAccountUid),
            //    MandatorUID = new Guid(mandatorUid),
            //    FinancingUID = new Guid(financingUid),
            //    CurrencyUID = new Guid(currencyUid),
            //    AccountNumber = accountNumber,
            //    Regress = Convert.ToBoolean(regress),
            //    StatusUID = new Guid(statusUid),
            //    RepaymentTypeUID = string.IsNullOrEmpty(repaymentTypeUid) ? (Guid?)null : new Guid(repaymentTypeUid),
            //    Denomination = string.IsNullOrEmpty(denomination) ? (decimal?)null : Convert.ToDecimal(denomination),
            //    IndividualCodeUID = string.IsNullOrEmpty(individualCodeUid) ? (Guid?)null : new Guid(individualCodeUid),
            //    OwnershipPromotion = Convert.ToBoolean(ownershipPromotion)
            //};

            //model.Account.Add(account);
            //model.SaveChanges();
        }
    }
}
