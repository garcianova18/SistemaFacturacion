using AutoMapper;
using Facturacion.Application.Repository;
using Facturacion.Application.Services;
using Facturacion.Domain.DTOs;
using Facturacion.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Net;

namespace Facturacion.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceServices _invoiceServices;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        protected ApiResponse response;

        public InvoiceController(IInvoiceServices invoiceServices, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _invoiceServices = invoiceServices;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            response = new ApiResponse();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> Get()
        {
            try
            {

                var Invoice = await _unitOfWork.Invoice.GetAll();



                response.Result = _mapper.Map<List<InvoiceDTO>>(Invoice);
                response.StatusCode = HttpStatusCode.OK;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMesseges = new List<string>() { ex.Message.ToString() };

            }

            return response;

        }

        [HttpGet("{id:int}", Name = "Getinvoice")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> Get(int id)
        {
            try
            {

                if (id == 0)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(response);
                }
                var invoice = await _unitOfWork.Invoice.GetByid(id);

                if (invoice is null)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(response);
                };


                response.StatusCode = HttpStatusCode.OK;
                response.Result = _mapper.Map<InvoiceDTO>(invoice);
                return Ok(response);

            }
            catch (Exception ex)
            {

                response.IsSuccess = false;
                response.ErrorMesseges = new List<string>() { ex.Message.ToString() };
            }
            return response;
        }

        [HttpGet("InvoiceNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> Get(string nombre)
        {
            try
            {

                if (string.IsNullOrEmpty(nombre))
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(response);
                }
                var invoice = await _invoiceServices.GetInvoiceByNumber(nombre);

                if (invoice is null)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(response);
                };


                response.StatusCode = HttpStatusCode.OK;
                response.Result = _mapper.Map<InvoiceDTO>(invoice);
                return Ok(response);

            }
            catch (Exception ex)
            {

                response.IsSuccess = false;
                response.ErrorMesseges = new List<string>() { ex.Message.ToString() };
            }
            return response;
        }




        [HttpGet("GetDetails/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>>GetDetails(int id)
        {
            try
            {

                if (id == 0)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(response);
                }
                var invoice = await _invoiceServices.GetInvoiceDetails(id);

                if (invoice is null)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(response);
                };


                response.StatusCode = HttpStatusCode.OK;
                response.Result = _mapper.Map<InvoiceWhitDetailsDTO>(invoice);
                return Ok(response);

            }
            catch (Exception ex)
            {

                response.IsSuccess = false;
                response.ErrorMesseges = new List<string>() { ex.Message.ToString() };
            }
            return response;
        }



        [HttpPost]
        public async Task<ActionResult<ApiResponse>> Post([FromBody] InvoiceCreateDTO invoiceCreateDTO)
        {
            try
            {
                if (invoiceCreateDTO is null)
                {
                    return BadRequest();
                }

                //Verificar si el cliente enviado existe
                var existsClient = await _unitOfWork.Client.Exists(c=> c.Id == invoiceCreateDTO.IdClient);
                if (!existsClient)
                {
                    return BadRequest("El cliente no existe");
                }

                bool existsProducto = false;
                int idnotvalid = 0;
                //Verificar si el Product enviado existe
                foreach (var item in invoiceCreateDTO.InvoiceDetails)
                {
                   
                   
                   existsProducto  =  await _unitOfWork.Product.Exists(d => d.Id == item.IdProduct);

                    idnotvalid = item.IdProduct;
                    if (existsProducto is false)
                    {
                        break;
                    }
                }

                
                if (!existsProducto)
                {
                    return BadRequest($"El Product con id {idnotvalid} no existe");
                }
               

                var invoiceMapper = _mapper.Map<Invoice>(invoiceCreateDTO);
                var invoice = await _invoiceServices.GetTotalSubtotalTax(invoiceMapper);

                await _unitOfWork.Invoice.Add(invoice);
                await _unitOfWork.Save();

                var invoiceDTO = _mapper.Map<InvoiceDTO>(invoice);

                return CreatedAtRoute("Getinvoice", new { id = invoiceMapper.Id }, invoiceDTO);
            }
            catch (Exception ex)
            {

                response.IsSuccess = false;
                response.ErrorMesseges = new List<string> { ex.Message.ToString() };
            }


            return response;
            
        }
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> Put(int id, [FromBody] InvoiceUpdateDTO invoiceUpdateDTO)
        {

            try
            {
                if (id != invoiceUpdateDTO.Id || invoiceUpdateDTO is null)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.IsSuccess = false;

                    return BadRequest(response);
                }

              
                //Verificar si existe una factura con el id enviado
                var existsRegister = await _unitOfWork.Invoice.Exists(c => c.Id.Equals(invoiceUpdateDTO.Id));

                if (!existsRegister)
                {
                    return NotFound("Este registro no existe en la DB");
                }

                var InvoiceMapper = _mapper.Map<Invoice>(invoiceUpdateDTO);
                var invoice = await _invoiceServices.GetTotalSubtotalTax(InvoiceMapper);

                //foreach (var Details in invoice.InvoiceDetails)
                //{
                //    _unitOfWork.InvoiceDetails.Update(Details);
                //}

                
                _unitOfWork.Invoice.Update(invoice);
                await _unitOfWork.Save();

                response.StatusCode = HttpStatusCode.NoContent;
                return Ok(response);

            }
            catch (Exception ex)
            {

                response.ErrorMesseges = new List<string> { ex.Message.ToString() };
                response.IsSuccess = false;
            }


            return response;

        
        
        
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> Delete(int id)
        {
            try
            {

                if (id == 0)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(response);
                }

                //Verificar si el producto con este id existe 
                var ExistsInvoice = await _unitOfWork.Invoice.GetByid(id);

                if (ExistsInvoice is null)
                {
                    return NotFound();
                }

                _unitOfWork.Invoice.Delete(ExistsInvoice);
                await _unitOfWork.Save();

                response.StatusCode = HttpStatusCode.NoContent;
                return Ok(response);

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMesseges = new List<string> { ex.Message.ToString() };
            }


            return response;

        }
    }
}
