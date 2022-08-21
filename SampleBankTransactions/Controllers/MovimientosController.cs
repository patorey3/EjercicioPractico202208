using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleBankTransactions.DAL;
using SampleBankTransactions.Model.Responses;
using SampleBankTransactions.Model;

namespace SampleBankTransactions.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientosController : ControllerBase
    {
        ITransactionRepository transactionRepository;

        public MovimientosController(BankTransactions context)
        {
            transactionRepository = new TransactionRepository(context);
        }

        [HttpGet]
        [Route("{identityDocument}")]
        public async Task<ActionResult<CommonResponse>> Get(string identityDocument)
        {
            CommonResponse toReturn = new CommonResponse();

            try
            {
                var found = transactionRepository.Get(identityDocument);
                if (found == null)
                {
                    toReturn.Records = null;
                    toReturn.Errors = 1;
                    toReturn.ErrorMessage = "Record not found";
                }
                else
                {
                    toReturn.Records = new List<object>();
                    toReturn.Records.AddRange(found);
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

        [HttpGet]
        [Route("{identityDocument}&{since}&{until}")]
        public async Task<ActionResult<CommonResponse>> Get(string identityDocument, DateTimeOffset since, DateTimeOffset until)
        {
            CommonResponse toReturn = new CommonResponse();

            try
            {
                if (string.IsNullOrEmpty(identityDocument))
                {
                    toReturn.Errors = 1;
                    toReturn.ErrorMessage = "Parameter identityDocument is required";
                    return Ok(toReturn);

                }
                else if(since == null || until == null)
                {
                    toReturn.Errors = 1;
                    toReturn.ErrorMessage = "Parameter rangeOfDate is required";
                }
                if(toReturn.Errors!=0)
                    return Ok(toReturn);


                var found = transactionRepository.Get(identityDocument, since, until);
                if (found == null)
                {
                    toReturn.Records = null;
                    toReturn.Errors = 1;
                    toReturn.ErrorMessage = "Record not found";
                }
                else
                {
                    toReturn.Records = new List<object>();
                    toReturn.Records.AddRange(found);
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
        public async Task<ActionResult<CommonResponse>> Insert(TransactionForDisplay transaction)
        {
            CommonResponse toReturn = new CommonResponse();

            try
            {
                transaction = transactionRepository.Insert(transaction);
                transactionRepository.Save();
                toReturn.Records = new List<object>()
                { (object)transaction };
            }
            catch (Exception ex)
            {
                toReturn.Records = null;
                toReturn.Errors = 1;
                toReturn.ErrorMessage = ex.Message;
            }

            return Ok(toReturn);
        }

    }
}
