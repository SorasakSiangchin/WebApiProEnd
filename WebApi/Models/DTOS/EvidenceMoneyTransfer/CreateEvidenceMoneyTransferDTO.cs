using System.Reflection;
using WebApi.Modes.DTOS.Product;

namespace WebApi.Models.DTOS.EvidenceMoneyTransfer
{
    public class CreateEvidenceMoneyTransferDTO
    {
        public IFormFileCollection? FormFiles { get; set; }
        public string OrderID { get; set; }
        public static ValueTask<CreateEvidenceMoneyTransferDTO?> BindAsync(HttpContext context,
                                                  ParameterInfo parameter)
        {
            var file = context.Request.Form.Files;
            return ValueTask.FromResult<CreateEvidenceMoneyTransferDTO?>(new CreateEvidenceMoneyTransferDTO
            {
                OrderID = context.Request.Form["OrderID"],
                FormFiles = file,
            });
        }
    }
}
