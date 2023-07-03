using AutoMapper;
using Facturacion.Application.Repository.Interfaces;
using Facturacion.Application.Utilities;
using Facturacion.Domain.DTOs;
using Facturacion.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Facturacion.RestApi.Controllers
{
    [Authorize]
    [Route("api/[Controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
      
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        protected ApiResponse response;

        public ClientController( IMapper mapper, IUnitOfWork unitOfWork)
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

                var Client = await _unitOfWork.Client.GetAll();
               
                response.Result = _mapper.Map<List<ClientDTO>>(Client);
                response.StatusCode = HttpStatusCode.OK;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMesseges = new List<string>() { ex.Message.ToString()};

            }

            return response;
            
        }

        [HttpGet("{id:int}", Name ="GetClient")]
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
                var client = await _unitOfWork.Client.GetByid(id);

                if (client is null)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(response);
                };


                response.StatusCode = HttpStatusCode.OK;
                response.Result = _mapper.Map<ClientDTO>(client);
                return Ok(response);

            }
            catch (Exception ex)
            {

                response.IsSuccess = false;
                response.ErrorMesseges = new List<string>(){ ex.Message.ToString()};
            }
            return response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> Post([FromBody]ClientCreateDTO ClientDTO)
        {
            try
            {
                if (ClientDTO is null)
                {
                    return BadRequest();
                }

                //Verificar si existe un cliente con el DNI enviado
                var ExistsClient = await _unitOfWork.Client.Exists(c => c.Dni.Equals(ClientDTO.Dni));
                if (ExistsClient)
                {
                    return BadRequest("El cliente ya existe");
                }

                var Client = _mapper.Map<Client>(ClientDTO);

                await _unitOfWork.Client.Add(Client);
                await _unitOfWork.Save();

                response.Result = _mapper.Map<ClientDTO>(Client);
                return CreatedAtRoute("GetClient", new { id = Client.Id }, response.Result);
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
        public async Task<ActionResult<ApiResponse>> Put( int id, [FromBody]ClientUpdateDTO clientDTO)
        {

            try
            {
                if (id != clientDTO.Id || clientDTO is null)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.IsSuccess = false;

                    return BadRequest(response);
                }

                //verificar si existe un cliente con el DNI enviado
                var existsClient = await _unitOfWork.Client.ExistsUpdate(c => c.Dni == clientDTO.Dni && c.Id != clientDTO.Id);

                if (existsClient >= 1)
                {
                    return BadRequest("El cliente ya existe");
                }

                var existsRegister = await _unitOfWork.Client.Exists(c => c.Id.Equals(clientDTO.Id));

                if (!existsRegister)
                {
                    return NotFound("Este registro no existe en la DB");
                }

                var cliente = _mapper.Map<Client>(clientDTO);
                      _unitOfWork.Client.Update(cliente);
                await _unitOfWork.Save();

                response.IsSuccess = true;
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

                var ExistsClient = await _unitOfWork.Client.GetByid(id);

                if (ExistsClient is null)
                {
                    return NotFound();
                }

                //borrado Logico, no podemos eliminar Registro de tabla relacionadas para no crear inconsistencia de los datos
                ExistsClient.Status = false;
                _unitOfWork.Client.Update(ExistsClient);
                await _unitOfWork.Save();

                response.StatusCode = HttpStatusCode.NoContent;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMesseges = new List<string> { ex.Message.ToString()};
            }


            return response;

        }
    }
}
