using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;
using WebApi.Repository;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly InterfaceProduct _InterfaceProduct;

        public ProductsController(InterfaceProduct InterfaceProduct)
        {
            _InterfaceProduct = InterfaceProduct;
        }

        [HttpGet("/api/List")]
        [Produces("application/json")]
        public async Task<object> List()
        {
            return await _InterfaceProduct.List();
        }

        [HttpPost("/api/Add")]
        [Produces("application/json")]
        public async Task<object> Add(ProductModel product)
        {
            try
            {
                await _InterfaceProduct.Add(product);
            }
            catch (Exception ERRO)
            {

            }

            return Task.FromResult("OK");
        }

        [HttpPut("/api/Update")]
        [Produces("application/json")]
        public async Task<object> Update(ProductModel product)
        {
            try
            {
                await _InterfaceProduct.Update(product);
            }
            catch (Exception ERRO)
            {

            }

            return Task.FromResult("OK");
        }


        [HttpGet("/api/GetEntityById")]
        [Produces("application/json")]
        public async Task<object> GetEntityById(int id)
        {
            return await _InterfaceProduct.GetEntityById(id);
        }

        [HttpDelete("/api/Delete")]
        [Produces("application/json")]
        public async Task<object> Delete(int id)
        {
            try
            {
                var product = await _InterfaceProduct.GetEntityById(id);

                await _InterfaceProduct.Delete(product);

            }
            catch (Exception ERRO)
            {
                return false;
            }

            return true;

        }

    }
}
