﻿using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLtrybackend.DTOs;
using SQLtrybackend.Models;
using SQLtrybackend.Services;
using SQLtrybackend.Validators;

namespace SQLtrybackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private IValidator<BeerInsertDto> _beerInsertValidator;
        private IValidator<BeerUpdateDto> _beerUpdateValidator;
        private ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto> _beerService;

        public BeerController(IValidator<BeerInsertDto> beerInsertValidator, IValidator<BeerUpdateDto> beerUpdateValidator, [FromKeyedServices("beerService")]ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto> beerService)
        {
            _beerInsertValidator = beerInsertValidator;
            _beerUpdateValidator = beerUpdateValidator;
            _beerService = beerService;
        }

        [HttpGet]
        public async Task<IEnumerable<BeerDto>> Get() =>
            await _beerService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<BeerDto>> GetById(int id)
        {
            var beerDto = await _beerService.GetById(id);

            return beerDto == null ? NotFound() : Ok(beerDto);
        }

        [HttpPost]
        public async Task<ActionResult<BeerDto>> Add(BeerInsertDto beerInsertDto)
        {
            var validationResult = await _beerInsertValidator.ValidateAsync(beerInsertDto);

            if (!validationResult.IsValid) 
            {
                return BadRequest(validationResult.Errors);
            }

            var beerDto = await _beerService.Add(beerInsertDto);

            return CreatedAtAction(nameof(GetById), new { id = beerDto.Id }, beerDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BeerDto>> Update(int id, BeerUpdateDto beerUpdateDto)
        {
            var validationResult = await _beerUpdateValidator.ValidateAsync(beerUpdateDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var beerDto = await _beerService.Update(id, beerUpdateDto);

            return beerDto == null ? NotFound() : Ok(beerDto) ;

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BeerDto>> Delete(int id)
        {
            var beerDto = await _beerService.DeleteById(id);

            return beerDto == null ? NotFound() : Ok(beerDto);
 
        }

    }
}
