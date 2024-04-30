using FluentValidation;
using SQLtrybackend.DTOs;

namespace SQLtrybackend.Validators
{
    public class BeerInsertValidation : AbstractValidator<BeerInsertDto>
    {
        public BeerInsertValidation() 
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(x => "El nombre no puede ser null o estar vacío");
            RuleFor(x => x.Name).Length(2, 20).WithMessage(x => "El nombre debe medir entre {MinLength} a {MaxLength} carácteres");
            RuleFor(x => x.BrandID).GreaterThan(0).WithMessage(x => "Error con el valor {PropertyValue} enviado de marca");
            RuleFor(x => x.Alcohol).GreaterThan(0).WithMessage(x => "El valor de {PropertyName} debe ser mayor a 0");
        }
    }
}
