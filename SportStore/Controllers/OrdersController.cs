using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportStore.Models;

namespace SportStore.Controllers
{
    public class OrdersController : Controller
    {
        private IRepository productRepository;
        private IOrderRepository orderRepository;
        public OrdersController(IRepository product, IOrderRepository order)
        {
            productRepository = product;
            orderRepository = order;
        }
        public IActionResult Index()
        {
            return View(orderRepository.Orders);
        }
        public IActionResult EditOrder(long id)
        {
            var product = productRepository.Products;
            Order order = id == 0 ? new Order() : orderRepository.GetOrder(id);
            IDictionary<long, OrderLine> lineMap = order.Lines?.ToDictionary(l => l.ProductId) ?? new Dictionary<long, OrderLine>();
            ViewBag.Lines = product.Select(p => lineMap.ContainsKey(p.Id) ? lineMap[p.Id]
                : new OrderLine { Product = p, ProductId = p.Id, Quantity = 0 });
            return View(order);
        }
        [HttpPost]
        public IActionResult AddOrUpdateOrder(Order order)
        {
            order.Lines = order.Lines.Where(l => l.Id > 0 || (l.Id == 0 && l.Quantity > 0)).ToArray();
            if (order.Id == 0)
            {
                orderRepository.AddOrder(order);
            } else
            {
                orderRepository.UpdateOrder(order);
            }
            return RedirectToAction(nameof(Index)); 
        }

        [HttpPost]
        public IActionResult DeleteOrder (Order order)
        {
            orderRepository.DeleteOrder(order);
            return RedirectToAction(nameof(Index));
        }
    }
}