using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleBankTransactions.DAL;
using SampleBankTransactions.Model.Responses;
using SampleBankTransactions.Model;

namespace SampleBankTransactions.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        ICustomerRepository customerRepository;
        public ClientesController(BankTransactions context)
        {
            customerRepository = new CustomerRepository(context);
        }

        [HttpGet]
        [Route("{identityDocument}")]
        public async Task<ActionResult<CommonResponse>> Get(string identityDocument)
        {
            CommonResponse toReturn = new CommonResponse();

            try
            {
                var found = customerRepository.Get(identityDocument);
                if (found == null)
                {
                    toReturn.Records = null;
                    toReturn.Errors = 1;
                    toReturn.ErrorMessage = "Record not found";
                }
                else
                {
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

        [HttpGet]
        public async Task<ActionResult<CommonResponse>> GetAll()
        {
            CommonResponse toReturn = new CommonResponse();

            try
            {
                toReturn.Records = customerRepository.GetAll()
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

        [HttpPost]
        public async Task<ActionResult<CommonResponse>> Insert(CustomerForDisplay customer)
        {
            CommonResponse toReturn = new CommonResponse();

            try
            {
                customerRepository.Insert(customer);
                customerRepository.Save();
                var saved = customerRepository.Get(customer.IdentityDocument);
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
        public async Task<ActionResult<CommonResponse>> Update(CustomerForDisplay customer)
        {
            CommonResponse toReturn = new CommonResponse();

            try
            {
                customerRepository.Update(customer);
                customerRepository.Save();
                var saved = customerRepository.Get(customer.IdentityDocument);
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
        public async Task<ActionResult<CommonResponse>> Delete(string identityDocument)
        {
            CommonResponse toReturn = new CommonResponse();
            toReturn.Records = null;

            try
            {
                customerRepository.Delete(identityDocument);
                customerRepository.Save();
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
