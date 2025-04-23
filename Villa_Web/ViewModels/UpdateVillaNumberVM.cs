
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Villa_Web.Dtos.VillaNumberDtos;

namespace Villa_Web.ViewModels
{
    public class UpdateVillaNumberVM
    {

        public UpdateVillaNumberVM()
        {
            VillaNumber = new UpdateVillaNumberDto();
        }
        public UpdateVillaNumberDto  VillaNumber { get; set; }
        [ValidateNever ]
        public IEnumerable<SelectListItem> VillaList { get; set; }
    }
}
