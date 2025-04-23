
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Villa_Web.Dtos.VillaNumberDtos;

namespace Villa_Web.ViewModels
{
    public class CreateVillaNumberVM
    {

        public CreateVillaNumberVM()
        {
            VillaNumber = new AddVillaNumberDto();
        }
        public AddVillaNumberDto  VillaNumber { get; set; }
        [ValidateNever ]
        public IEnumerable<SelectListItem> VillaList { get; set; }
    }
}
