using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleBankTransactions.DAL;
using SampleBankTransactions.Model;
using SampleBankTransactions.Model.Responses;

namespace SampleBankTransactions.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentasController : ControllerBase
    {
        private IAccountRepository accountRepository;
        public CuentasController(BankTransactions context)
        {
            accountRepository = new AccountRepository(context);
        }

        [HttpGet]
        public async Task<ActionResult<CommonResponse>> Get()
        {
            CommonResponse toReturn = new CommonResponse();

            try
            {
                toReturn.Records = accountRepository.GetAll()
                    .Select(x => (object)x).ToList();
            }
            catch (Exception ex)
            {
                toReturn.Records = null;
                toReturn.Errors = 1;
                toReturn.ErrorMessage = ex.Message;
            }

            return Ok(toReturn);
        }


        [HttpGet]
        [Route("{accountNumber}")]
        public async Task<ActionResult<CommonResponse>> Get(string accountNumber)
        {
            CommonResponse toReturn = new CommonResponse();

            try
            {
                var found = accountRepository.Get(accountNumber);
                if (found == null)
                {
                    toReturn.Records = null;
                    toReturn.Errors = 1;
                    toReturn.ErrorMessage = "Record not found";
                }
                else {
                    toReturn.Records = new List<object>();
                    toReturn.Records.Add(found);
                        };
            }
            catch (Exception ex)
            {
                toReturn.Records = null;
                toReturn.Errors = 1;
                toReturn.ErrorMessage = ex.Message;
            }

            return Ok(toReturn);
        }

        [HttpPost]
        public async Task<ActionResult<CommonResponse>> Insert(AccountForDisplay account)
        {
            CommonResponse toReturn = new CommonResponse();

            try
            {
                accountRepository.Insert(account);
                accountRepository.Save();
                var saved = accountRepository.Get(account.AccountNumber);
                toReturn.Records = new List<object>() 
                { (object)saved };
            }
            catch (Exception ex)
            {
                toReturn.Records = null;
                toReturn.Errors = 1;
                toReturn.ErrorMessage = ex.Message;
            }

            return Ok(toReturn);
        }

        [HttpPut]
        public async Task<ActionResult<CommonResponse>> Update(AccountForDisplay account)
        {
            CommonResponse toReturn = new CommonResponse();

            try
            {
                accountRepository.Update(account);
                accountRepository.Save();
                var saved = accountRepository.Get(account.AccountNumber);
                toReturn.Records = new List<object>()
                { (object)saved };
            }
            catch (Exception ex)
            {
                toReturn.Records = null;
                toReturn.Errors = 1;
                toReturn.ErrorMessage = ex.Message;
            }

            return Ok(toReturn);
        }

        [HttpDelete]
        public async Task<ActionResult<CommonResponse>> Delete(string accountNumber)
        {
            CommonResponse toReturn = new CommonResponse();
                toReturn.Records = null;

            try
            {
                accountRepository.Delete(accountNumber);
                accountRepository.Save();
            }
            catch (Exception ex)
            {
                toReturn.Errors = 1;
                toReturn.ErrorMessage = ex.Message;
            }

            return Ok(toReturn);
        }

    }
}
