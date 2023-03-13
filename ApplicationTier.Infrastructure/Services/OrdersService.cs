﻿using ApplicationTier.ApplicationCore.Interfaces;
using ApplicationTier.ApplicationCore.Models;
using Microsoft.EntityFrameworkCore;
using RocketEshop.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationTier.Infrastructure.Services
{
    public class OrdersService : IOrdersService
    {

        private readonly AppDbContext _context;
        public OrdersService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId)
        {
            // Edo allgi kai pos8iki user
            var orders = await _context.Orders.Include(n => n.OrderItems).ThenInclude(n => n.Game).ToListAsync();

            return orders;
        }

        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress)
        {
            var order = new Order()
            {
                UserId = userId,
                Email = userEmailAddress
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            foreach (var item in items)
            {
                var orderItem = new OrderItem()
                {
                    Amount = item.Amount,
                    GameId = item.Game.Id,
                    OrderId = order.Id,
                    Price = item.Game.Price
                };
                await _context.OrderItems.AddAsync(orderItem);
            }
            await _context.SaveChangesAsync();
        }

    }
}