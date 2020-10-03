using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Basket.API.Entities;
using Basket.API.Repositories.Interfaces;
using EventBusRabbitMQ.Common;
using EventBusRabbitMQ.Events;
using EventBusRabbitMQ.Producer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Basket.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly ILogger<BasketController> _logger;
        private readonly EventBusRabbitMQProducer _eventBus;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository, ILogger<BasketController> logger, 
            EventBusRabbitMQProducer eventBus, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _logger = logger;
            _eventBus = eventBus;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<BasketCart>> GetBasket(string userName)
        {
            var basket = await _basketRepository.GetBasket(userName);
            return Ok(basket ?? new BasketCart(userName));
        }

        [HttpPost]
        public async Task<ActionResult<BasketCart>> UpdateBasket([FromBody] BasketCart basket)
        {
            return Ok(await _basketRepository.UpdateBasket(basket));
        }


        [HttpDelete("{userName}")]
        public async Task<ActionResult<BasketCart>> DeleteBasket(string userName)
        {
            return Ok(await _basketRepository.DeleteBasket(userName));
        }

        [HttpPost("[action]")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        {
            try
            {
                //get total price of basket
                var basket = await _basketRepository.GetBasket(basketCheckout.UserName);
                if (basket == null)
                    return BadRequest();

                //remove basket
                var basketRemoved = await _basketRepository.DeleteBasket(basket.UserName);
                if (!basketRemoved)
                    return BadRequest();

                //send check out event to rabbitmq
                var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
                eventMessage.RequestId = Guid.NewGuid();
                eventMessage.TotalPrice = basket.TotalPrice;

                _eventBus.PublishBasketCheckout(EventBusConstants.BasketCheckoutQueue, eventMessage);
                return Accepted();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error check out: {ex.Message}");
                return BadRequest(ex.Message);
            }        
        }
    }
}
