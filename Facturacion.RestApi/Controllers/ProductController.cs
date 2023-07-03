using AutoMapper;
using Facturacion.Application.Repository.Interfaces;
using Facturacion.Application.Utilities;
using Facturacion.Domain.DTOs;
using Facturacion.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Facturacion.RestApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
      

            private readonly IMapper _mapper;
            private readonly IUnitOfWork _unitOfWork;
            protected ApiResponse response;

            public ProductController(IMapper mapper, IUnitOfWork unitOfWork)
            {


                _mapper = mapper;
                _unitOfWork = unitOfWork;
                response = new ApiResponse();
            }

            [HttpGet]
            [ProducesResponseType(StatusCodes.Status200OK)]
            public async Task<ActionResult<ApiResponse>> Get()
            {
                try
                {

                    var Product = await _unitOfWork.Product.GetAll();

                    response.Result = _mapper.Map<List<ProductDTO>>(Product);
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

            [HttpGet("{id:int}", Name = "GetProduct")]
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
                    var Product = await _unitOfWork.Product.GetByid(id);

                    if (Product is null)
                    {
                        response.IsSuccess = false;
                        response.StatusCode = HttpStatusCode.NotFound;
                        return NotFound(response);
                    };


                    response.StatusCode = HttpStatusCode.OK;
                    response.Result = _mapper.Map<ProductDTO>(Product);
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
            [ProducesResponseType(StatusCodes.Status201Created)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public async Task<ActionResult<ApiResponse>> Post([FromBody] ProductCreateDTO ProductDTO)
            {
                try
                {
                    if (ProductDTO is null)
                    {
                        return BadRequest();
                    }

                    //Verificar si existe una categoria con el id enviado
                    var existsCategory = await _unitOfWork.Category.Exists(c => c.Id == ProductDTO.IdCategory);
                    if (!existsCategory)
                    {
                        return BadRequest("la categoria no existe");
                    }

                    var Product = _mapper.Map<Product>(ProductDTO);
                        Product.DateCreate = DateTime.Now;

                    await _unitOfWork.Product.Add(Product);
                    await _unitOfWork.Save();

                    response.Result = ProductDTO;
                    response.StatusCode = HttpStatusCode.Created;

                    return CreatedAtRoute("GetProduct", new { id = Product.Id }, response.Result);
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
            public async Task<ActionResult<ApiResponse>> Put(int id, [FromBody] ProductUpdateDTO ProductDTO)
            {

                try
                {
                    if (id != ProductDTO.Id || ProductDTO is null)
                    {
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response.IsSuccess = false;

                        return BadRequest(response);
                    }

                //verificar si la categoria con el id enviado existe
                var ExistsCateory = await _unitOfWork.Category.Exists(c => c.Id == ProductDTO.IdCategory);

                if (!ExistsCateory)
                {
                    return NotFound("La categoria no existe");
                }

                //verificar si el registro con el id enviado existe
                var existsRegister = await _unitOfWork.Product.Exists(c => c.Id.Equals(ProductDTO.Id));

                    if (!existsRegister)
                    {
                        return NotFound("Este registro no existe en la DB");
                    }

                    var Product = _mapper.Map<Product>(ProductDTO);
                        Product.DateCreate = DateTime.Now;

                    _unitOfWork.Product.Update(Product);
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

                    var ExistsProduct = await _unitOfWork.Product.GetByid(id);

                    if (ExistsProduct is null)
                    {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(response);
                    }

                //borrado Logico, no podemos eliminar Registro de tabla relacionadas para no crear inconsistencia de los datos
                ExistsProduct.Status = false;
                _unitOfWork.Product.Update(ExistsProduct);
                await _unitOfWork.Save();

                response.StatusCode = HttpStatusCode.NoContent;
                      return Ok(response) ;
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
