using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoucherApi.Data;
using VoucherApi.Models;
using VoucherApi.Models.DTOs;

namespace VoucherApi.Controllers
{
    // Define the route and indicate that this class is an API controller
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<VoucherController> _logger;
        private readonly IMapper _mapper;

        // Constructor to inject dependencies
        public VoucherController(AppDbContext context, ILogger<VoucherController> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        // Get all vouchers endpoint
        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetAllVouchers()
        {
            // Create a new ResponseDto instance to handle response data
            var response = new ResponseDto();
            try
            {
                // Retrieve all vouchers from the database asynchronously
                IEnumerable<Voucher> vouchers = await _context.Vouchers.ToListAsync();

                // Map vouchers to VoucherDto using AutoMapper
                IEnumerable<VoucherDto> voucherDtos = _mapper.Map<IEnumerable<VoucherDto>>(vouchers);

                // Set the result property of the response to the mapped voucherDtos
                response.Result = voucherDtos;

                // Indicate success in the response
                response.IsSuccess = true;

                // Return 200 OK response with the response object
                return Ok(response);
            }
            catch (Exception ex)
            {
                // If an exception occurs, log the error and set response properties accordingly
                LogErrorAndSetResponse(ex, response, "An error occurred while retrieving all vouchers.");

                // Return 500 Internal Server Error response with the response object
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        // Get voucher by ID endpoint
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto>> GetVoucherById(int id)
        {
            // Create a new ResponseDto instance to handle response data
            var response = new ResponseDto();
            try
            {
                // Retrieve the voucher with the specified ID from the database asynchronously
                var voucher = await _context.Vouchers.FirstOrDefaultAsync(x => x.VoucherId == id);

                // If the voucher is not found, set response properties accordingly
                if (voucher == null)
                {
                    response.IsSuccess = false;
                    response.Message = $"Voucher with ID {id} not found.";
                    // Return 404 Not Found response with the response object
                    return NotFound(response);
                }

                // Map voucher to VoucherDto using AutoMapper
                VoucherDto voucherDto = _mapper.Map<VoucherDto>(voucher);

                // Set the result property of the response to the mapped voucherDto
                response.Result = voucherDto;

                // Indicate success in the response
                response.IsSuccess = true;

                // Return 200 OK response with the response object
                return Ok(response);
            }
            catch (Exception ex)
            {
                // If an exception occurs, log the error and set response properties accordingly
                LogErrorAndSetResponse(ex, response, $"An error occurred while retrieving voucher with ID: {id}");

                // Return 500 Internal Server Error response with the response object
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet]
        [Route("GetByCode/{code}")]
        public async Task<ActionResult<ResponseDto>> GetVoucherByCode(string code)
        {
            // Create a new ResponseDto instance to handle response data
            var response = new ResponseDto();
            try
            {
                // Retrieve the voucher with the specified ID from the database asynchronously
                var voucher = await _context.Vouchers.FirstOrDefaultAsync(x => x.VoucherCode.ToLower() == code.ToLower());

                // If the voucher is not found, set response properties accordingly
                if (voucher == null)
                {
                    response.IsSuccess = false;
                    response.Message = $"Voucher with ID {code} not found.";
                    // Return 404 Not Found response with the response object
                    return NotFound(response);
                }

                // Map voucher to VoucherDto using AutoMapper
                VoucherDto voucherDto = _mapper.Map<VoucherDto>(voucher);

                // Set the result property of the response to the mapped voucherDto
                response.Result = voucherDto;

                // Indicate success in the response
                response.IsSuccess = true;

                // Return 200 OK response with the response object
                return Ok(response);
            }
            catch (Exception ex)
            {
                // If an exception occurs, log the error and set response properties accordingly
                LogErrorAndSetResponse(ex, response, $"An error occurred while retrieving voucher with ID: {code}");

                // Return 500 Internal Server Error response with the response object
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto>> CreateVoucher([FromBody] VoucherDto voucherDto)
        {
            // Create a new ResponseDto instance to handle response data
            var response = new ResponseDto();
            try
            {
                // Map the incoming VoucherDto to the Voucher entity using AutoMapper
                var voucherEntity = _mapper.Map<Voucher>(voucherDto);

                // Add the voucher entity to the context
                _context.Vouchers.Add(voucherEntity);

                // Save changes asynchronously
                await _context.SaveChangesAsync();

                // Return 201 Created response with the created voucher
                return CreatedAtAction(nameof(GetVoucherById), new { id = voucherEntity.VoucherId }, voucherDto);
            }
            catch (Exception ex)
            {
                // If an exception occurs, log the error and set response properties accordingly
                LogErrorAndSetResponse(ex, response, $"An error occurred while creating voucher.");

                // Return 500 Internal Server Error response with the response object
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPut]
        public async Task<ActionResult<ResponseDto>> UpdateVoucher([FromBody] VoucherDto voucherDto)
        {
            var response = new ResponseDto();

            try
            {
                var voucherEntity = await _context.Vouchers.FindAsync(voucherDto.VoucherId);

                if (voucherEntity == null)
                {
                    // If the voucher with the specified ID doesn't exist, return a 404 Not Found response
                    return NotFound();
                }

                // Map the properties from the DTO to the entity
                _mapper.Map(voucherDto, voucherEntity);

                // Save changes asynchronously
                await _context.SaveChangesAsync();

                // Set the response message
                response.Message = "Voucher updated successfully.";

                // Return 200 OK response
                return Ok(response);
            }
            catch (Exception ex)
            {
                // If an exception occurs, log the error and set response properties accordingly
                LogErrorAndSetResponse(ex, response, $"An error occurred while updating voucher.");

                // Return 500 Internal Server Error response with the response object
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto>> DeleteVoucher(int id)
        {
            var response = new ResponseDto();
            try
            {
                var voucherEntity = await _context.Vouchers.FindAsync(id);

                if (voucherEntity == null)
                {
                    // If the voucher with the specified ID doesn't exist, return a 404 Not Found response
                    return NotFound();
                }

                // Remove the entity from the context
                _context.Vouchers.Remove(voucherEntity);

                // Save changes asynchronously
                await _context.SaveChangesAsync();

                // Set the response message
                response.Message = "Voucher deleted successfully.";

                // Return 204 No Content response
                return NoContent();
            }
            catch (Exception ex)
            {
                // If an exception occurs, log the error and set response properties accordingly
                LogErrorAndSetResponse(ex, response, $"An error occurred while deleting voucher.");

                // Return 500 Internal Server Error response with the response object
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        // Helper method to log errors and set response properties
        private void LogErrorAndSetResponse(Exception ex, ResponseDto response, string errorMessage)
        {
            // Set response properties based on the exception
            response.IsSuccess = false;
            response.Message = ex.Message;
            // Log the error
            _logger.LogError(ex, errorMessage);
        }
    }

}

