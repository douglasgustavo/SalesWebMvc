﻿using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            return _context.Sellers.ToList();
        }

        public void Insert(Seller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Seller FindById(int id)
        {
            return _context.Sellers.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
        }

        public void Delete(int id)
        {
            Seller seller = FindById(id);
            _context.Sellers.Remove(seller);
            _context.SaveChanges();
        }

        public void Update(Seller seller)
        {
            if (!_context.Sellers.Any(x => x.Id == seller.Id)) throw new NotFoundException("Id not found");

            try
            {
                _context.Update(seller);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
