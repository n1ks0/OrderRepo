using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Order.BL.Exceptions;
using Order.BL.Interfaces;
using Order.Web.Models.Order;

namespace Order.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IProviderService _providerService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper, IProviderService providerService)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            _mapper = mapper;
            _providerService = providerService;
        }

        // GET: OrderController
        public async Task<IActionResult> Index()
        {
            var start = DateTime.Today.AddMonths(-1);
            var end = DateTime.Now;

            var orders = await _orderService.GetOrdersInPeriod(start, end);

            var orderIndexDtos = _mapper.Map<List<OrderIndexDto>>(orders);

            var model = new OrderIndexViewModel
            {
                End = end,
                Start = start,
                Orders = orderIndexDtos
            };

            var providers = _providerService.GetAll();
            var numbers = orders.Select(o => o.Number).Distinct();

            ViewBag.Providers = providers.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.Id.ToString()
            });
            ViewBag.Numbers = numbers.Select(n => new SelectListItem
            {
                Text = n,
                Value = n
            });

            return View(model);
        }

        // GET: OrderController/Create
        [HttpGet]
        public IActionResult Create()
        {
            var providers = _providerService.GetAll();
            ViewBag.Providers = providers.Select(p =>
                    new SelectListItem { Text = p.Name, Value = p.Id.ToString() });
            return View();
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool isOccupied = 
                    _orderService.IsOrderNumberOccupied(model.Number, model.ProviderId);

                if (isOccupied)
                {
                    return BadRequest($"Уже существует заказ с номером {model.Number} у выбранного поставщика!");
                }

                var order = _mapper.Map<Order.BL.Entities.Order>(model);
                await _orderService.Create(order);
                return RedirectToAction(nameof(Index));
            }
            return BadRequest("Модель не валидна! Заполните все поля заново!");
        }

        // GET: OrderController/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var order = await _orderService.GetOrderById(id);

            if (order == null)
            {
                throw new EntityNotFoundException(id);
            }

            var model = _mapper.Map<OrderEditViewModel>(order);

            var providers = _providerService.GetAll();

            ViewBag.Providers = providers
                .Select(p =>
                new SelectListItem
                {
                    Text = p.Name,
                    Value = p.Id.ToString(),
                    Selected = p.Id == order.ProviderId
                });

            return View(model);
        }

        // POST: OrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(OrderEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var order = _mapper.Map<Order.BL.Entities.Order>(model);
                await _orderService.Edit(order);
                return RedirectToAction(nameof(Index));
            }
            return BadRequest("Модель не валидна! Заполните все поля заново!");
        }


        [HttpGet]
        public async Task<IActionResult> Info(int id)
        {
            var order = await _orderService.GetOrderById(id);
            var model = _mapper.Map<OrderInfoViewModel>(order);
            return View(model);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _orderService.Delete(id);
            return Ok("Заказ успешно удален!");
        }

        [HttpPost]
        public async Task<IActionResult> Filter(DateTime start, DateTime end,
            IEnumerable<int> providerIds, IEnumerable<string> orderNumbers)
        {
            var fltrdOrders = 
                await _orderService.GetFilteredOrders(start, end, providerIds, orderNumbers);

            var orderIndexDtos = _mapper.Map<IEnumerable<OrderIndexDto>>(fltrdOrders);

            return PartialView("_OrderTablePartial", orderIndexDtos);
        }
    }
}
