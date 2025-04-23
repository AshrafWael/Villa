
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Villa_Web.Dtos.VillaNumberDtos;

namespace Villa_Web.ViewModels
{
    public class ReadVillaNumberVM
    {

        public ReadVillaNumberVM()
        {
            VillaNumber = new ReadVillaNumberDto();
        }
        public ReadVillaNumberDto  VillaNumber { get; set; }
        [ValidateNever ]
        public IEnumerable<SelectListItem> VillaList { get; set; }
    }
}
