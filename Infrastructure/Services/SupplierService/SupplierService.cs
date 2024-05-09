using System.Data.Common;
using System.Net;
using AutoMapper;
using Domain.DTOs.SupplierDTO;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.SupplierService
{
    public class SupplierService : ISupplierService
    {
        #region ctor

        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public SupplierService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        #endregion

        #region GetSuppliersAsync

        public async Task<PagedResponse<List<GetSupplierDto>>> GetSuppliersAsync(SupplierFilter filter)
        {
            try
            {
                var suppliers = _context.Suppliers.AsQueryable();
                if (!string.IsNullOrEmpty(filter.Name))
                    suppliers = suppliers.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
                var result = await suppliers.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize)
                    .ToListAsync();
                var total = await suppliers.CountAsync();

                var response = _mapper.Map<List<GetSupplierDto>>(result);
                return new PagedResponse<List<GetSupplierDto>>(response, total, filter.PageNumber, filter.PageSize);
            }
            catch (Exception e)
            {
                return new PagedResponse<List<GetSupplierDto>>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        #endregion

        #region GetSupplierByIdAsync

        public async Task<Response<GetSupplierDto>> GetSupplierByIdAsync(int supplierId)
        {
            try
            {
                var existing = await _context.Suppliers.FirstOrDefaultAsync(x => x.SupplierId == supplierId);
                if (existing == null) return new Response<GetSupplierDto>(HttpStatusCode.BadRequest, "Supplier not found");
                var supplier = _mapper.Map<GetSupplierDto>(existing);
                return new Response<GetSupplierDto>(supplier);
            }
            catch (Exception e)
            {
                return new Response<GetSupplierDto>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        #endregion

        #region CreateSupplierAsync

        public async Task<Response<string>> CreateSupplierAsync(AddSupplierDto supplier)
        {
            try
            {
                var newSupplier = _mapper.Map<Supplier>(supplier);
                await _context.Suppliers.AddAsync(newSupplier);
                await _context.SaveChangesAsync();
                return new Response<string>("Successfully created ");
            }
            catch (DbException e)
            {
                return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
            }
            catch (Exception e)
            {
                return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        #endregion

        #region UpdateSupplierAsync

        public async Task<Response<string>> UpdateSupplierAsync(UpdateSupplierDto supplier)
        {
            try
            {
                var existing = await _context.Suppliers.AnyAsync(x => x.SupplierId == supplier.SupplierId);
                if (!existing) return new Response<string>(HttpStatusCode.BadRequest, "Supplier not found");
                var newSupplier = _mapper.Map<Supplier>(supplier);
                _context.Suppliers.Update(newSupplier);
                await _context.SaveChangesAsync();
                return new Response<string>("Supplier successfully updated");
            }
            catch (DbException e)
            {
                return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
            }
            catch (Exception e)
            {
                return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        #endregion

        #region RemoveSupplierAsync

        public async Task<Response<bool>> RemoveSupplierAsync(int supplierId)
        {
            try
            {
                var existing = await _context.Suppliers.Where(x => x.SupplierId == supplierId).ExecuteDeleteAsync();
                return existing == 0
                    ? new Response<bool>(HttpStatusCode.BadRequest, "Supplier not found")
                    : new Response<bool>(true);
            }
            catch (DbException e)
            {
                return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
            }
            catch (Exception e)
            {
                return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        #endregion
    }
}
