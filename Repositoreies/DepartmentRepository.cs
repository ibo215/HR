﻿using Domain;
using HR.Contexts;
using Microsoft.EntityFrameworkCore;

namespace HR.Repositoreies
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly HRContext _context;

        public DepartmentRepository(HRContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync(int pageNumber, int pageSize)
        {
            return await _context.Departments
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            return await _context.Departments.Where(d => d.InActive).FirstOrDefaultAsync(d => d.DepartmentId == id);
        }

        public async Task<IEnumerable<Department>> GetDeletedDepartmentsAsync()
        {
            return await _context.Departments
                .Where(d => !d.InActive)
                .ToListAsync();
        }
        public async Task AddDepartmentAsync(Department department)
        {
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDepartmentAsync(Department department)
        {
            _context.Departments.Update(department);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department != null)
            {
                department.InActive = false;
                  await _context.SaveChangesAsync();
            }
        }
    }

}
