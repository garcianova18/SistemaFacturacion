using AutoMapper;
using Facturacion.Application.Repository;
using Facturacion.Domain.DTOs;
using Facturacion.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Facturacion.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
      
            private readonly IMapper _mapper;
            private readonly IUnitOfWork _unitOfWork;
            protected ApiResponse response;

            public CategoryController(IMapper mapper, IUnitOfWork unitOfWork)
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

                    var Category = await _unitOfWork.Category.GetAll();

                    response.Result = _mapper.Map<List<CategoryDTO>>(Category);
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

            [HttpGet("{id:int}", Name = "GetCategory")]
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
                    var Category = await _unitOfWork.Category.GetByid(id);

                    if (Category is null)
                    {
                        response.IsSuccess = false;
                        response.StatusCode = HttpStatusCode.NotFound;
                        return NotFound(response);
                    };


                    response.StatusCode = HttpStatusCode.OK;
                    response.Result = _mapper.Map<CategoryDTO>(Category);
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
            public async Task<ActionResult<ApiResponse>> Post([FromBody] CategoryCreateDTO CategoryDTO)
            {
                try
                {
                    if (CategoryDTO is null)
                    {
                        return BadRequest();
                    }

                    //Verificar si existe un cliente con el DNI enviado
                    var existsCategory = await _unitOfWork.Category.Exists(c=>c.Name == CategoryDTO.Name);
                    if (existsCategory)
                    {
                        return BadRequest("Esta Categoria ya existe");
                    }

                    var Category = _mapper.Map<Category>(CategoryDTO);
                        Category.DateCreate = DateTime.Now;

                    await _unitOfWork.Category.Add(Category);
                    await _unitOfWork.Save();
                
                    response.Result = CategoryDTO;
                    response.StatusCode = HttpStatusCode.Created;

                    return CreatedAtRoute("GetCategory", new { id = Category.Id }, response.Result);
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
            public async Task<ActionResult<ApiResponse>> Put(int id, [FromBody] CategoryUpdateDTO CategoryDTO)
            {

                try
                {
                    if (id != CategoryDTO.Id || CategoryDTO is null)
                    {
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response.IsSuccess = false;

                        return BadRequest(response);
                    }

                    //verificar si existe una ategoria con el mismo nombre
                    var existsCategory = await _unitOfWork.Category.ExistsUpdate(c => c.Name == CategoryDTO.Name && c.Id != CategoryDTO.Id);

                    if (existsCategory >= 1)
                    {
                        return BadRequest("Esta Category ya existe");
                    }

                     //verificar si existe una categoria con el id enviado
                     var existsRegister = await _unitOfWork.Client.Exists(c => c.Id.Equals(CategoryDTO.Id));

                    if (!existsRegister)
                    {
                        return NotFound("Este Categoria no existe en la DB");
                    }   

                    var Category = _mapper.Map<Category>(CategoryDTO);
                        Category.DateCreate = DateTime.Now;

                    _unitOfWork.Category.Update(Category);
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

                    var ExistsCategory = await _unitOfWork.Category.GetByid(id);

                    if (ExistsCategory is null)
                    {
                        return NotFound();
                    }



                //borrado Logico
                 ExistsCategory.Status = false;
                _unitOfWork.Category.Update(ExistsCategory);
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
