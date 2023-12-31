﻿using FluentValidation;
using SCHAPI.Application.Dtos.Student.Request;
using SCHAPI.Utilities.Static;

namespace SCHAPI.Application.Validators.Student
{
    public class StudentRequestValidator : AbstractValidator<StudentRequestDto>
    {
        public StudentRequestValidator()
        {
            RuleFor(s => s.Name)
                .NotNull().WithMessage("El campo nombre no puede ser nulo.")
                .NotEmpty().WithMessage("El campo nombre no puede ser vacío.");

            RuleFor(s => s.Phone)
                .NotNull().WithMessage("El campo Teléfono no puede ser nulo.")
                .NotEmpty().WithMessage("El campo Teléfono no puede ser vacío.");

            RuleFor(s => s.Email)
                .NotNull().WithMessage("El campo Correo Electrónico no puede ser nulo.")
                .NotEmpty().WithMessage("El campo Correo Electrónico no puede ser vacío.")
                .EmailAddress().WithMessage("Debe ingresar un Correo Electrónico válido.");

            RuleFor(t => t.State)
                .InclusiveBetween((int)StateTypes.Inactive, (int)StateTypes.Active)
                    .WithMessage("Debe seleccionar un estado correcto.");
        }
    }
}
